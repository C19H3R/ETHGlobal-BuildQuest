import React, { useEffect, useState } from "react";
import { useMoralis, useMoralisWeb3Api } from "react-moralis";
import { MoralisProvider } from "react-moralis";
import NFTCard from "../components/NFTCard";

const API_ID = process.env.NEXT_PUBLIC_MORALIS_APP_ID;
const SERVER_URL = process.env.NEXT_PUBLIC_MORALIS_SERVER_URL;

export default function MyAssets() {
  const { isAuthenticated, account } = useMoralis();
  const [NFTs, setNFT] = useState({});
  const Web3Api = useMoralisWeb3Api();

  const NFTMetadata = {};

  const fetchAllTokenIds = async () => {
    if (isAuthenticated) {
      const options = {
        address: account,
        chain: "mumbai",
      };
      NFTMetadata = await Web3Api.account.getNFTs(options);
      Object.keys(NFTMetadata).forEach((key) => {
        if (key === "result") {
          NFTMetadata = NFTMetadata[key];
          setNFT(NFTMetadata);
        }
      });
    }
  };

  useEffect(() => {
    fetchAllTokenIds();
  }, [isAuthenticated]);

  return (
    <MoralisProvider appId={API_ID} serverUrl={SERVER_URL} isAuthenticated>
      {isAuthenticated ? (
        <div className="bg-black">
          <div className="flex flex-col items-center justify-evenly">
            {<NFTCard NFTs={NFTs}></NFTCard>}
          </div>
        </div>
      ) : (
        <div className="bg-black h-screen">
          <div className="flex flex-col items-center justify-evenly">
            <p className="mb-2 mt-20">Please connect your wallet</p>
          </div>
        </div>
      )}
    </MoralisProvider>
  );
}
