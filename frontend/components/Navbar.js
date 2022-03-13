import { useState } from "react";
import Link from "next/link";
import { ConnectButton } from "web3uikit";
import { useRouter } from "next/router";

const navlinks = [
  { title: "Home", path: "/" },
  { title: "Mint", path: "/mint" },
  { title: "Marketplace", path: "/marketplace" },
  { title: "My Assets", path: "/myassets" },
];

const Navbar = () => {
  const [active, setActive] = useState(false);

  const router = useRouter();

  const handleClick = () => {
    setActive(!active);
  };

  return (
    <div className="z-50">
      <div>
        <nav className="flex flex-row justify-between items-center flex-wrap pt-2 pl-6 pr-6 bg-black">
          <img
            className="w-20 rounded-full lg:w-24 "
            src="/images/logo.png"
          ></img>
          <p className="mr-10 ml-6 text-3xl font-bold text-white">Supr Arms</p>

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
              {navlinks.map((item, index) => {
                return (
                  <Link key={index} href={item.path}>
                    <a
                      className={`mr-6 mt-2 lg:mt-0 text-xl ${
                        router.pathname === item.path
                          ? "text-white"
                          : "hover:text-white text-gray-400"
                      }  `}
                    >
                      {item.title}
                    </a>
                  </Link>
                );
              })}
              <div className="lg:mt-0 mt-4 z-50 rounded-lg bg-wheat">
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
