import { useState } from "react";
import Link from "next/link";
import { ConnectButton } from "web3uikit";

const Navbar = () => {
  const [active, setActive] = useState(false);

  const handleClick = () => {
    setActive(!active);
  };

  return (
    <div>
      <div>
        <nav className="flex justify-between items-center flex-wrap p-6 bg-black">
          <p className="mr-10 text-3xl font-bold text-white">Supr Arms</p>
          <button
            className="inline-flex p-3 rounded lg:hidden text-white ml-auto hover:text-white outline-none"
            onClick={handleClick}
          >
            <svg
              className="w-6 h-6"
              fill="none"
              stroke="currentColor"
              viewBox="0 0 24 24"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth={2}
                d="M4 6h16M4 12h16M4 18h16"
              />
            </svg>
          </button>
          <div
            className={`${
              active ? "" : "hidden"
            } w-full lg:inline-flex lg:flex-grow lg:w-auto`}
          >
            <div className="lg:inline-flex lg:flex-row lg:ml-auto lg:w-auto w-full lg:items-center items-start  flex flex-col lg:h-auto">
              <Link href="/">
                <a className="mr-6 mt-2 lg:mt-0 text-gray-400 text-xl">Home</a>
              </Link>
              <Link href="/mint">
                <a className="mr-6 mt-3 lg:mt-0 text-gray-400 text-xl">Mint</a>
              </Link>
              <Link href="/marketplace">
                <a className="mr-6 mt-3 lg:mt-0 text-gray-400 text-xl">
                  Marketplace
                </a>
              </Link>
              <Link href="/myassets">
                <a className="mr-6 mt-3 lg:mt-0 text-gray-400 text-xl">
                  My Assets
                </a>
              </Link>
              <div className="lg:mt-0 -ml-5 mt-4 z-50 rounded-lg bg-wheat">
                <ConnectButton />
              </div>
            </div>
          </div>
        </nav>
      </div>
    </div>
  );
};

export default Navbar;
