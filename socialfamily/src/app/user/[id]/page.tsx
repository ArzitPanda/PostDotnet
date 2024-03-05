"use client";
import {
  Avatar,
  Button,
  Card,
  CardBody,
  Chip,
  Dropdown,
  DropdownItem,
  DropdownMenu,
  DropdownTrigger,
  Image,
  Tab,
  Tabs,
} from "@nextui-org/react";
import React, { useEffect } from "react";
import {
  BiLeftArrowCircle,
  BiPhotoAlbum,
  BiSolidLeftArrowCircle,
  BiText,
} from "react-icons/bi";
import PhotoFeedSection from "@/app/Component/Ui/PhotoFeedSection";
import Link from "next/link";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "@/store";
import { fetchPostOfPerson } from "@/store/Post/PostSlice";
import { fetchProfile, profileActions } from "@/store/Profile/ProfileSlice";
import { RiUserAddFill } from "react-icons/ri";
import axios from "axios";
import { BASE_URL } from "@/Constant";

const page: React.FC<any> = ({ params }) => {
  const profileId = params.id;


  const state = useSelector((state: RootState) => state.profile);
  let tabs = [
    {
      id: "photos",
      label: "Photos",
      title: (
        <div className="flex items-center space-x-2">
          <BiPhotoAlbum />
          <span>Photos</span>
        </div>
      ),
      content:
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
    },
    {
      id: "Text",
      label: "Text",
      title: (
        <div className="flex items-center space-x-2">
          <BiText />
          <span>Texts</span>
        </div>
      ),
      content:
        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
    },
  ];



  const config = {
    headers: {
      "Access-Control-Allow-Origin": "http://localhost:3000",
      "Access-Control-Allow-Methods": "GET,PUT,POST,DELETE,PATCH,OPTIONS"
    }
  };



    const handlesentFriendRequest = async (item:string) =>{
      const userId = localStorage.getItem("id");

      try {
        const data = await  axios.post(BASE_URL+"/api/RelationRequest",{userId:profileId,relationShip:item,requestorId:userId},config);
        console.log(data);
        dispatch<any>(profileActions.addFriendRequest(data.data))
      } catch (error) {
        console.log(error);
      }

      

    }








  const dispatch = useDispatch();

  useEffect(() => {
    const userId = localStorage.getItem("id");

    dispatch<any>(fetchProfile({ id: profileId, id2: userId }));
  }, []);

  function calculateAge(dateOfBirth: any) {
    const dob = new Date(dateOfBirth);
    const now = new Date();

    let age = now.getFullYear() - dob.getFullYear();
    const monthDiff = now.getMonth() - dob.getMonth();

    // If the current month is less than the birth month,
    // or if it's the same month but the current day is before the birth day,
    // decrement the age by 1
    if (monthDiff < 0 || (monthDiff === 0 && now.getDate() < dob.getDate())) {
      age--;
    }

    return age;
  }

  const authorName = "John Doe";
  const bioDescription =
    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed et lectus urna. In hac habitasse platea dictumst. Maecenas vel justo vel orci mollis dapibus.";
  const achievement = "Lorem ipsum";
  return (
    <div className="w-screen">
      <div className="absolute top-10 left-5 lg:top-20 lg:left-20">
        <Link href="/">
          <BiSolidLeftArrowCircle color="black" className="text-2xl" />
        </Link>
      </div>

      <div className="absolute -top-96  lg:-top-64  w-screen h-screen -z-30">
        <Image
          src="http://localhost:3000/profilewave.svg"
          className="w-full h-screen object-cover "
        />
      </div>
      <div className="container grid grid-cols-3 w-full  lg:w-8/12 mx-auto mt-40 lg:mt-48  sm:mt-42  lg:px-6 xl:mt-64 gap-0">
        <div className="flex col-span-3  justify-start gap-x-4 px-6">
          <Avatar
            src={state?.user?.photoUrl}
            className="h-24 w-24 sm:h-28 sm:w-28  lg:h-32 lg:w-32  col-span-1"
            isBordered
          />
        </div>
        <div className="w-full flex flex-col col-span-3 ">
        <div className="w-full  items-center justify-between mx-auto flex mr-28  px-6 lg:px-4 rounded-lg shadow-lg gap-0 ">
        <div>
        <Chip className="text-2xl font-semibold text-left mt-4">
            {state?.user?.userName}
          </Chip>
          <span className="text-2xl italic">
            {calculateAge(state?.user?.dateOfBirth)}Y
          </span>
        </div>
          {state.RelationIsExist ? (
            <Chip className="text-2xl font-semibold text-left mt-4 bg-yellow-300 text-black ml-2">
              {state?.RelationType}
            </Chip>
          ) : (
            <div className="justify-end ">
            {
              state.RequestIsExist?(<Chip>requested</Chip>):(  <Dropdown>
                <DropdownTrigger>
                  <div className="flex items-center justify-center gap-2 border-3 rounded-3xl border-spacing-1 border-gray-600 p-3 cursor-pointer hover:border-yellow-500"> <RiUserAddFill size={15} color="yellow"/>   <div className="hidden lg:block text-sm">Sent Request</div> </div>
                </DropdownTrigger>
                <DropdownMenu aria-label="Static Actions">
                  <DropdownItem key="Family" onClick={async ()=>{await handlesentFriendRequest("Family")}}>Family</DropdownItem>
                  <DropdownItem key="Friend" onClick={async ()=>{ await handlesentFriendRequest("Friend")}}>Friend</DropdownItem>
                  <DropdownItem key="Office" onClick={async ()=>{ await handlesentFriendRequest("Office")}}>Office</DropdownItem>
                  <DropdownItem
                    key="Public"
                    onClick={async ()=>{ await handlesentFriendRequest("Public")}}
                    className="text-warning-300"
                    color="warning"
                  >
                    Public
                  </DropdownItem>
                </DropdownMenu>
              </Dropdown>) 
            }
            </div>
          )}
</div>
<div className="w-10/12 lg:w-4/12 pl-6">
         
          <p className="text-sm text-left mt-2">{bioDescription}</p>
          <p className="text-xs text-left mt-2">{achievement}</p>
        </div>
        </div>
        <div className="col-span-3  min-h-screen mt-6">
          <Tabs
            aria-label="Dynamic tabs"
            items={tabs}
            size="lg"
            fullWidth={true}
            color="warning"
            variant="underlined"
          >
            {(item) => (
              <Tab key={item.id} title={item.title}>
                <PhotoFeedSection data={state.posts} />
              </Tab>
            )}
          </Tabs>
        </div>
      </div>
    </div>
  );
};

export default page;
