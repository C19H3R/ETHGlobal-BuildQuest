import React, { useEffect, useState } from "react";
import { useMoralis, useMoralisWeb3Api } from "react-moralis";
import { MoralisProvider } from "react-moralis";
import NFTCard from "../components/NFTCard";

const API_ID = process.env.NEXT_PUBLIC_MORALIS_APP_ID;
const SERVER_URL = process.env.NEXT_PUBLIC_MORALIS_SERVER_URL;

export default function MyAssets() {
  const { isAuthenticated, account} = useMoralis();
  const [NFTs, setNFT] = useState({});
  const Web3Api = useMoralisWeb3Api();
  console.log(account)

  const NFTMetadata = {};

  const fetchAllTokenIds = async () => {
    if(isAuthenticated) {const options = {
      address: account,
      chain: "mumbai",
    };
    NFTMetadata = await Web3Api.account.getNFTs(options)
    console.log(NFTMetadata);
    Object.keys(NFTMetadata).forEach((key) => {
      if (key === "result") {
        NFTMetadata = NFTMetadata[key];
        setNFT(NFTMetadata);
      }
    });
    console.log(NFTMetadata);}
    
  };

  useEffect(() => {
    fetchAllTokenIds();
  }, []);

  return (
    <div className="bg-gradient-to-tl from-blue-900 to-green-700">
      {isAuthenticated ? (
        <div className="flex flex-col items-center justify-evenly">
          <NFTCard NFTs={NFTs}></NFTCard>
        </div>
      ): <p>Please connect your wallet</p>}
      </div>
  );
}
