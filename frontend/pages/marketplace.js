import React, { useEffect, useState } from "react";
import { useMoralisWeb3Api } from "react-moralis";
import { Card, Illustration } from "web3uikit";
import { MoralisProvider } from "react-moralis";
import NFTCard from "../components/NFTCard";

const API_ID = process.env.NEXT_PUBLIC_MORALIS_APP_ID;
const SERVER_URL = process.env.NEXT_PUBLIC_MORALIS_SERVER_URL;

export default function Marketplace() {
  const [NFTs, setNFT] = useState({});
  const Web3Api = useMoralisWeb3Api();

  const NFTMetadata = {};

  const fetchAllTokenIds = async () => {
    const options = {
      address: "0x78ae7cfc6b0903f71277cad2b66528c8044cdaa5",
      chain: "mumbai",
    };
    NFTMetadata = await Web3Api.token.getAllTokenIds(options);
    console.log(NFTMetadata);
    Object.keys(NFTMetadata).forEach((key) => {
      if (key === "result") {
        NFTMetadata = NFTMetadata[key];
        setNFT(NFTMetadata);
      }
    });
    console.log(NFTMetadata);
  };

  useEffect(() => {
    fetchAllTokenIds();
  }, []);

  return (
    <MoralisProvider appId={API_ID} serverUrl={SERVER_URL} isAuthenticated>
      <div className="bg-gradient-to-tl from-blue-900 to-green-700">
        <div className="flex flex-col items-center justify-evenly">
          {<NFTCard NFTs={NFTs}></NFTCard>}
        </div>
      </div>
    </MoralisProvider>
  );
}
