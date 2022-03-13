import Navbar from "../components/Navbar";

export default function Home() {
  return (
    <div className="bg-black h-screen">
      <video
        autoPlay
        loop
        className="flex z-10 w-auto"
      >
        <source src="crawl.mp4" type="video/mp4" />
        Your browser does not support the video tag.
      </video>
    </div>
  );
}
