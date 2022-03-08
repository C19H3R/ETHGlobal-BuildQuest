// SPDX-License-Identifier: MIT

pragma solidity 0.8.9;

import "@openzeppelin/contracts/token/ERC721/ERC721.sol";
import "@chainlink/contracts/src/v0.8/VRFConsumerBase.sol";
import "@chainlink/contracts/src/v0.8/ConfirmedOwner.sol";

// Polygon (Matic) Mumbai Testnet Deployment
contract SuprArms is ERC721, VRFConsumerBase, ConfirmedOwner(msg.sender) {

    string private _baseTokenURI;

    bytes32 internal keyHash;
    uint256 internal fee;
    mapping(bytes32 => address) public buyers;
    mapping(address => uint256) public holders;
    uint256 private constant REQUEST_IN_PROGRESS = 99999;

    event WeaponRequested(bytes32 indexed requestId, bytes32 indexed keyHash);
    event WeaponFulfilled(bytes32 indexed requestId, uint256 indexed randomness, uint256 indexed tokenIdTracker);

    /**
     * Constructor inherits VRFConsumerBase
     *
     * Network: Polygon (Matic) Mumbai Testnet
     * Chainlink VRF Coordinator address: 0x8C7382F9D8f56b33781fE506E897a4F1e2d17255
     * LINK token address:                0x326C977E6efc84E512bB9C30f76E30c160eD06FB
     * Key Hash: 0x6e75b569a01ef56d18cab6a8e71e6600d6ce853834d4a5748b720d06f878b3a4
     * Fee: 0.001 * 10 ** 18; // 0.001 LINK
     */
    constructor(string memory name, string memory symbol, string memory baseTokenURI, address _vrfCoordinator, address _link, bytes32 _keyHash, uint256 _fee) 
        VRFConsumerBase(_vrfCoordinator, _link)
        ERC721(name, symbol)
    {
        _baseTokenURI = baseTokenURI;
        keyHash = _keyHash;
        fee = _fee;
    }

    /**
     * Request Weapon
     */
    function requestWeapon(address buyer) public returns (bytes32 requestId) {
        require(LINK.balanceOf(address(this)) > fee, "SuprArms: Not enough LINK to initialte function call");
        require(holders[buyer] == 0, "SuprArms: Weapon already requested");
        bytes32 _requestId = requestRandomness(keyHash, fee);
        buyers[requestId] = buyer;
        holders[buyer] = REQUEST_IN_PROGRESS;
        emit WeaponRequested(_requestId, keyHash);
        return _requestId;
    }

    /**
     * Reveals the weapon metadata by minting the NFT
     */
    function revealWeapon(address buyer) public {
        require(holders[buyer] != 0, "SuprArms: Weapon not requested");
        require(holders[buyer] != REQUEST_IN_PROGRESS, "SuprArms: Request under processing");
        uint256 tokenId = holders[buyer];
        _safeMint(buyer, tokenId);
        holders[buyer] = 0;
    }

    /**
     * Callback function used by VRF Coordinator
     */
    function fulfillRandomness(bytes32 requestId, uint256 randomness) internal override {
        uint256 tokenIdTracker = (randomness % 8400) - 1;
        holders[buyers[requestId]] = tokenIdTracker;
        emit WeaponFulfilled(requestId, randomness, tokenIdTracker);
    }

    /** 
     * Requests the address of the Chainlink Token on this network 
     */
    function getChainlinkTokenAddress() public view returns (address) {
        return address(LINK);
    }

    /**
     * @notice Withdraw LINK from this contract.
     * @dev this is an example only, and in a real contract withdrawals should
     * happen according to the established withdrawal pattern: 
     * https://docs.soliditylang.org/en/v0.4.24/common-patterns.html#withdrawal-from-contracts
     * @param to the address to withdraw LINK to
     * @param value the amount of LINK to withdraw
     */
    function withdrawLINK(address to, uint256 value) public onlyOwner {
        require(LINK.transfer(to, value), "Not enough LINK");
    }

    /**
     * @notice Set the key hash for the oracle
     *
     * @param _keyHash bytes32
     */
    function setKeyHash(bytes32 _keyHash) public onlyOwner {
        keyHash = _keyHash;
    }

    /**
     * @notice Get the current key hash
     *
     * @return bytes32
     */
    function getKeyHash() public view returns (bytes32) {
        return keyHash;
    }

    /**
     * @notice Set the oracle fee for requesting randomness
     *
     * @param _fee uint256
     */
    function setFee(uint256 _fee) public onlyOwner {
        fee = _fee;
    }

    /**
     * @notice Get the current fee
     *
     * @return uint256
     */
    function getFee() public view returns (uint256) {
        return fee;
    }
}