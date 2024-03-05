'use client'
import { Image } from "@nextui-org/react";
import { useRouter } from "next/navigation";
import React from "react";

const PhotoFeedSection: React.FC<any> = ({ data }) => {

const router  =useRouter();

  return (
    <div className="w-full col-span-3 grid grid-cols-3 gap-1">
      {data.map((ele: any, idx: any) => {
        return (
          <>
            {ele === undefined || ele.imgUrl === "" ? (
              <div className="col-span-1 aspect-square bg-yellow-300 text-9xl flex items-center justify-center text-center font-bold text-black">
                {String.fromCharCode(65 + idx)}
              </div>
            ) : (
              <div className="col-span-1 aspect-square text-9xl flex items-center justify-center text-center font-bold text-black">
                <Image
                  src={ele.imgUrl}
                  alt={"image"}
                  className="w-full h-full object-cover aspect-square rounded-none"
                />
              </div>
            )}
          </>
        );
      })}

      {new Array(26 - data?.length).fill(0).map((ele, idx) => {
        return (
          <div className="col-span-1 aspect-square bg-yellow-300 text-9xl flex items-center justify-center text-center font-bold text-black">
            {String.fromCharCode(65 + idx + data?.length)}
          </div>
        );
      })}

    </div>
  );
};

export default PhotoFeedSection;
