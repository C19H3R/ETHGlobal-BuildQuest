import React, { useState } from "react";
import { useMoralis, useWeb3ExecuteFunction } from "react-moralis";

import gif from "../public/images/suprarms.gif";
import Image from "next/image";

import { Canvas } from "@react-three/fiber";
import { Suspense } from "react";
import SMG from "../components/SMG_1";

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

    await delay(120000);

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
      contractAddress: contractAddress,
      functionName: "revealWeapon",
      params: {},
    };

    let tx = await contractProcessor.fetch({
      params: revealWeaponOptions,
    });
    await delay(10000);
    setMinting(false);

    web3.eth.getTransactionReceipt(tx.hash).then(function (data) {
      let transaction = data;
      let logs = data.logs;
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
    <div className="bg-black">
      <div className="flex flex-col items-center justify-evenly">
        <div className="flex-col h-screen w-[95%] lg:-mt-10">
          <Canvas
            className="sm:h-20 sm:w-20"
            concurrent
            pixelRatio={[1, 2]}
            camera={{ position: [0, 0, 2] }}
          >
            <ambientLight intensity={0.3} />
            <spotLight
              intensity={0.3}
              angle={0.1}
              penumbra={1}
              position={[5, 25, 20]}
            />
            <Suspense fallback={null}>
              <SMG />
            </Suspense>
          </Canvas>
          <div className="flex justify-center"><p className="text-white text-bold text-lg mt-[-750px]">Mint your NFT Weapon (~2 minutes)</p></div>
          
        </div>
        <div className="flex flex-row justify-center">
          <div className="flex flex-col">
            <p className="text-white text-bold text-xl -mt-60">
              3d Model - SuprArms Weapon
            </p>
            <p className="text-white text-bold ml-4 mt-4">
              Rendered with threejs + gltfjsx
            </p>
          </div>
        </div>
        {isAuthenticated ? (
          !minting ? (
            !opensea ? (
              <button
                onClick={mint}
                className="bg-gradient-to-r from-teal-400 to-blue-500 hover:from-pink-600 hover:to-orange-600 text-white font-semibold px-4 py-2 rounded w-40 mb-20"
              >
                Mint NFT
              </button>
            ) : (
              <>
                <button
                  onClick={mint}
                  className="bg-gradient-to-r from-teal-400 to-blue-500 hover:from-pink-600 hover:to-orange-600 text-white font-semibold px-4 py-2 rounded w-40"
                >
                  Mint NFT
                </button>
                <button
                  className="bg-gradient-to-r from-teal-400 to-blue-500 hover:from-pink-500 hover:to-orange-500 text-white font-semibold px-4 py-2 rounded px-4 py-2 mt-4 mb-20"
                  onClick={handleClickOpenSea}
                >
                  View on OpenSea
                </button>
              </>
            )
          ) : (
            <LoadSpinner />
          )
        ) : (
          <p className="mb-2 mt-20">Please connect your wallet</p>
        )}
      </div>
      <div className="flex justify-around">
        <div className="mt-20 drop-shadow-lg hover:drop-shadow-2xl mb-20">
          <Image
            className="rounded-lg"
            width={350}
            height={350}
            alt="suprArms NFT gif"
            src={gif}
          ></Image>
        </div>
      </div>
    </div>
  );
}
