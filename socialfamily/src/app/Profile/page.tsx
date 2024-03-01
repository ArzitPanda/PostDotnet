'use client'
import { Avatar, Card, CardBody, Chip, Image, Tab, Tabs } from "@nextui-org/react";
import React, { useEffect } from "react";
import { BiLeftArrowCircle, BiPhotoAlbum, BiSolidLeftArrowCircle, BiText } from "react-icons/bi";
import PhotoFeedSection from "../Component/Ui/PhotoFeedSection";
import Link from "next/link";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "@/store";
import { fetchPostOfPerson } from "@/store/Post/PostSlice";

const page = () => {














    const state = useSelector((state:RootState)=>state.auth.user);


    const ProfilePictures = useSelector((state:RootState)=>state.post);
    let tabs = [
        {
          id: "photos",
          label: "Photos",
          title:<div className="flex items-center space-x-2">
                <BiPhotoAlbum/>
                <span>Photos</span>


          </div>,
          content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
        },
        {
          id: "Text",
          label: "Text",
          title:  <div className="flex items-center space-x-2">
                <BiText/>
                <span>Texts</span>


          </div>,
          content: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur."
        }
       
      ];


const dispatch  = useDispatch();



      useEffect(() =>{
            const id :string |null = localStorage.getItem('id');

            console.log(id);
            dispatch<any>(fetchPostOfPerson({id:id as string}));










      },[]);













      function calculateAge(dateOfBirth:any) {
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
                <BiSolidLeftArrowCircle color="black" className="text-2xl"/>
                </Link>
        </div>


      <div className="absolute -top-48 lg:-top-64  w-screen h-screen -z-30">
        <Image
          src="ProfileWave.svg"
          className="w-full h-screen object-cover "
        />
      </div>
      <div className="container grid grid-cols-3 w-full  lg:w-8/12 mx-auto mt-40 lg:mt-48  sm:mt-42  lg:px-6 xl:mt-64 gap-0">
        <div className="flex col-span-3  justify-start gap-x-4 px-6">
          <Avatar
          src={state?.photoUrl}
            className="h-24 w-24 sm:h-28 sm:w-28  lg:h-32 lg:w-32  col-span-1"
            isBordered
          />
        </div>
        <div className="lg:max-w-xs col-span-3  lg:col-span-1 mx-auto  mr-28  px-6 lg:px-4 rounded-lg shadow-lg gap-0">
          <Chip className="text-2xl font-semibold text-left mt-4">
            {state?.userName}
          </Chip>
          <span className="text-2xl italic">{calculateAge(state?.dateOfBirth)}Y</span>
          <p className="text-sm text-left mt-2">{bioDescription}</p>
          <p className="text-xs text-left mt-2">{achievement}</p>
        </div>

            <div className="col-span-3  min-h-screen mt-6">
            <Tabs aria-label="Dynamic tabs" items={tabs} size="lg" fullWidth={true} color="warning" variant="underlined" >
        {(item) => (
          <Tab key={item.id} title={item.title} >
            <PhotoFeedSection  data={ProfilePictures.post}/>
          </Tab>
        )}
      </Tabs>
            </div>


      </div>
    </div>
  );
};

export default page;
