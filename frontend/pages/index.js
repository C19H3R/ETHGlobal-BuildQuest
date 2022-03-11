import Navbar from "../components/Navbar";
import gif from "../public/images/suprarms.gif";
import Image from "next/image";

export default function Home() {
  return (
    <div className="bg-gradient-to-tl from-blue-900 to-green-700 h-screen">
      <div>
        <div className="flex flex-col justify-around">
          <p className="flex font-semibold text-xl text-white justify-around mt-20 ml-6 mr-6 overflow-hidden">
            Welcome To SuprArms, a collection of 8400 one on one weapon NFTs
            each with unique stats for use in the game SuprArms
          </p>
        </div>
      </div>
      <div className="flex justify-around">
        <div className="mt-20 drop-shadow-lg hover:drop-shadow-2xl">
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
