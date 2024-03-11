"use client";
import useUserData from "@/Hooks/useUserData";
import React, { useEffect } from "react";
import NavbarCustom from "../Component/Ui/Navbar";
import { Image } from "@nextui-org/react";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "@/store";
import { getRequestData } from "@/store/Request/RequestSlice";

const page = () => {
  const { userData, userPhoto } = useUserData();



  const RequestedData  = useSelector((state:RootState)=>state.request);

    const dispatch = useDispatch();

useEffect(() => {


  console.log(userData);
    if(userData){
      dispatch<any>(getRequestData({id:userData?.userId}
        ))

    }


  },[])











  console.log(RequestedData);
  return (
    <div>
      <div className="block  col-span-1 h-20  bg-zinc-900 lg:hidden">
        <NavbarCustom data={userData} />
      </div>
      <div className="grid w-full grid-cols-8">
      <div className=" col-span-8 flex items-center justify-center">
              <Image
                src="brand.png"
                alt="brand.png"
                width={"40%"}
                className="invert object-contain h-full ml-12"
              />
            </div>
            <div className="lg:col-start-3  col-span-8 lg:col-span-3">
              hello

            </div>
      </div>
    </div>
  );
};

export default page;
