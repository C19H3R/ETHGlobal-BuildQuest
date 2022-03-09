import React, { useState } from "react";
import { useMoralis, useWeb3ExecuteFunction } from "react-moralis";

import LoadSpinner from "../components/LoadSpinner";
import { contractAddress } from "../contract";

const OPENSEA_LINK = "https://testnets.opensea.io/assets/mumbai";
var Web3 = require("web3");

export default function Mint() {
  const { isAuthenticated } = useMoralis();
  const [opensea, setOpenSea] = useState(null);
  const [minting, setMinting] = useState(null);
  const delay = (ms) => new Promise((res) => setTimeout(res, ms));

  var web3 = new Web3(Web3.givenProvider);
  const contractProcessor = useWeb3ExecuteFunction();

  async function mint() {
    setOpenSea(null);
    let requestWeaponOptions = {
      abi: [
        {
          inputs: [],
          name: "requestWeapon",
          outputs: [
            { internalType: "bytes32", name: "requestId", type: "bytes32" },
          ],
          stateMutability: "nonpayable",
          type: "function",
        },
      ],
      contractAddress: contractAddress,
      functionName: "requestWeapon",
      params: {},
    };

    let id = await contractProcessor.fetch({
      params: requestWeaponOptions,
    });
    setMinting(true);
    console.log(id);

    await delay(120000);
    console.log("waited 40 seconds");

    let revealWeaponOptions = {
      abi: [
        {
          inputs: [],
          name: "revealWeapon",
          outputs: [],
          stateMutability: "nonpayable",
          type: "function",
        },
      ],
      contractAddress: CONTRACT_ADDRESS,
      functionName: "revealWeapon",
      params: {},
    };

    let tx = await contractProcessor.fetch({
      params: revealWeaponOptions,
    });
    console.log("minting...please wait");
    await delay(10000);
    console.log(tx);
    setMinting(false);

    web3.eth.getTransactionReceipt(tx.hash).then(function (data) {
      let transaction = data;
      let logs = data.logs;
      console.log(logs);
      console.log(web3.utils.hexToNumber(logs[0].topics[3]));
      setOpenSea(
        `${OPENSEA_LINK}/${contractAddress}/${web3.utils.hexToNumber(
          logs[0].topics[3]
        )}`
      );
    });
  }

  const handleClickOpenSea = () => {
    window.open(opensea, "_blank");
  };

  return (
    <div className="bg-gradient-to-tl from-blue-900 to-green-700 h-screen">
      <div className="flex flex-col items-center justify-evenly">
        {isAuthenticated ? (
          !minting ? (
            <button
              onClick={mint}
              className="text-white bg-purple-700 hover:bg-purple-800 focus:ring-4 focus:ring-purple-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center mb-2 mt-20 dark:bg-purple-600 dark:hover:bg-purple-700 dark:focus:ring-purple-900 w-40"
            >
              Mint NFT
            </button>
          ) : (
            <LoadSpinner />
          )
        ) : (
          <p className="mb-2 mt-20">Please connect your wallet</p>
        )}
        {opensea != null && (
          <button
            className="text-white bg-purple-700 hover:bg-purple-800 focus:ring-4 focus:ring-purple-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center  dark:bg-purple-600 dark:hover:bg-purple-700 dark:focus:ring-purple-900"
            onClick={handleClickOpenSea}
          >
            View on OpenSea
          </button>
        )}
      </div>
    </div>
  );
}
