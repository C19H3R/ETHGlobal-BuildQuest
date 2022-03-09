import React from "react";
import { Card } from "web3uikit";

const ipfs_string = "";

const NFTCard = (props) => {
  return (
    <div className="flex flex-wrap justify-between">
      {Object.keys(props.NFTs).map((key, value) => {
        return (
          <div
            style={{
              width: "500px",
              marginLeft: "20px",
              marginRight: "20px",
              marginTop: "20px",
              marginBottom: "20px",
            }}
            key={key}
          >
            <Card
              title={props.NFTs[key].name + " #" + props.NFTs[key].token_id}
            >
              <img
                src={`https://ipfs.io/ipfs/QmU9RjtQYzRRVGJavbzXamp1PVQDLdHZgiz1rSUApLa3fo/suprArms-${props.NFTs[key].token_id}.png`}
                className="w-100"
              ></img>
            </Card>
          </div>
        );
      })}
    </div>
  );
};

export default NFTCard;
