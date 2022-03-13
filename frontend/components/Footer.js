import React from "react";

const Footer = () => {
  return (
    <div className="flex justify-center items-center flex-wrap pt-6 pb-6 bg-black border-0">
      <p className=" text-md text-bold text-gray-400">Powered by</p>
          <img className="ml-2 w-[6.9%] lg:w-[2%] md:w-[4%]" src="/images/polygon_logo.png"></img>
          <img className="ml-2 w-[6.9%] lg:w-[2%] md:w-[4%]" src="/images/Moralis-Icon-Light.png"></img>
          <img className="ml-2 w-[7.2%] lg:w-[2.25%] md:w-[4.25%]" src="/images/ipfs_logo.png"></img>
          <img className="ml-2 w-[15%] lg:w-[4.5%] md:w-[7.5%]" src="/images/pinata_logo.svg"></img>
    </div>
  );
};

export default Footer;
