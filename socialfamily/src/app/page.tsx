'use client'
import { Avatar, AvatarGroup, Button, ButtonGroup, Image, Popover, PopoverContent, PopoverTrigger, Tooltip } from "@nextui-org/react";

import NavbarCustom from "./Component/Ui/Navbar";
import CardCustom from "./Component/Ui/Card";
import { BiChat, BiHeartCircle, BiNotification } from "react-icons/bi";
import SmallRecomendUserCard from "./Component/Ui/SmallRecomendUserCard";
import { FaCamera, FaHome, FaSearch } from "react-icons/fa";
import { useEffect, useState } from "react";
import axios from "axios";







const data = [
  { link: "https://img.freepik.com/free-vector/multigenerational-family-concept-illustration_114360-21169.jpg", details: "Family" },
  { link: "https://img.freepik.com/free-vector/group-young-people-posing-photo_52683-18823.jpg", details: "Friend" },
  { link: "https://img.freepik.com/free-vector/flat-design-working-day-scene_52683-62253.jpg", details: "Colleague" },
  { link: "https://img.freepik.com/free-vector/bloggers-influencers-writing-articles-posting-content-cartoon-illustration_74855-14289.jpg", details: "Public" }
];




export default function Home() {
  const [userPhoto,setUserPhoto] = useState();
  const [userData,setUserData] = useState();
const handleUserPhoto=()=>{

 let id=  localStorage.getItem("id");


  axios.get('http://localhost:5283/api/User/'+id)
  .then(response => {
    // Handle the response data
    console.log('Response:', response.data);

    setUserData(response.data);
    setUserPhoto(response.data?.photoUrl);
  })
  .catch(error => {
    // Handle errors
    console.error('Error:', error);
  });


}





  useEffect(() => {

        
handleUserPhoto();


  
  },[])


  return (
    <main className="w-screen bg-zinc-900">
    
      {/* interactive */}
      <div className="w-full  grid grid-cols-1  lg:grid-cols-5">
        <div className="block  col-span-1 h-20  bg-zinc-900 lg:hidden">
            <NavbarCustom data={userData}/>



        </div>

        <div className="w-full col-span-1 lg:col-span-4  bg-zinc-900 flex mx-auto  flex-col">
        <AvatarGroup className="lg:hidden">
      {data.map((item, index) => (
        <Popover key={index} placement="bottom" color="primary">
          <PopoverTrigger>
            <Avatar src={item.link} >
              {item.details}
            </Avatar>
          </PopoverTrigger>
          <PopoverContent className="bg-yellow-400"> 
            <div className="px-1 text-black font-semibold">
              
              <div className="text-tiny">{item.details}</div>
            </div>
          </PopoverContent>
        </Popover>
      ))}
    </AvatarGroup>
        <div className="w-10/12  h-28 mx-auto hidden lg:flex items-center justify-center  rounded-2xl">
        <Image src="brand.png" alt="brand.png" width={'40%'} className="invert object-contain h-full ml-12"/>
      </div>
          <div className="w-11/12 lg:w-7/12 flex flex-col items-center mx-auto justify-center mt-6 gap-4">
        {
          new Array(5).fill(0).map((ele)=>{return (<CardCustom/>)})
        }
</div>

        </div>
        <div className="hidden lg:flex col-span-1   bg-zinc-800  flex-col items-center justify-start gap-y-4">
        <Avatar src={userPhoto} isBordered color="warning" className="w-48 h-48 text-large mt-24" />
        <ButtonGroup>
      <Button isIconOnly><BiHeartCircle/></Button>
      <Button isIconOnly><BiChat/></Button>
      <Button isIconOnly  ><BiNotification/></Button>
    </ButtonGroup>
    <div className="w-full flex flex-col items-center justify-start">        
      <h1 className="text-slate-300 text-sm font-semibold">User You May Like</h1>
          <div className="w-10/12 grid grid-cols-4 gap-x-6 my-4 gap-y-6">

            {new Array(5).fill(0).map(ele=><SmallRecomendUserCard/>)}

          </div>
          </div>

        </div>


      </div>
      <div className="fixed lg:bottom-10 bottom-[0.5px] left-0 w-full flex justify-center items-end z-40">
      <div className="h-16 bg-yellow-400 w-full lg:w-2/12 lg:rounded-2xl shadow-lg">
        <ButtonGroup className="flex justify-between items-center h-full px-4">
          <Button className="bg-yellow-400"> <FaHome className="text-black" size={20} /></Button>
          <Button className="bg-yellow-400"><FaCamera className="text-black" size={20} /></Button>
          <Button className="bg-yellow-400"><FaSearch className="text-black"  size={20}/></Button>
        </ButtonGroup>
      </div>
    </div>
    <div className="w-20 lg:flex flex-col fixed bottom-10 left-10 gap-y-6 z-30 hidden ">
          <Tooltip showArrow={true} content="Family" className="bg-yellow-600 text-white" placement="right">
        <Avatar src="https://img.freepik.com/free-vector/multigenerational-family-concept-illustration_114360-21169.jpg"  className="w-10 h-10 " />
        </Tooltip>
        <Tooltip showArrow={true} content="Friend" className="bg-yellow-600 text-white" placement="right">
        <Avatar src="https://img.freepik.com/free-vector/group-young-people-posing-photo_52683-18823.jpg"  className="w-10 h-10 " />
        </Tooltip>
        <Tooltip showArrow={true} content="Collegue" className="bg-yellow-600 text-white" placement="right">
        <Avatar src="https://img.freepik.com/free-vector/flat-design-working-day-scene_52683-62253.jpg"  className="w-10 h-10 " />
        </Tooltip>
        <Tooltip showArrow={true} content="Public" className="bg-yellow-600 text-white" placement="right">
        <Avatar src="https://img.freepik.com/free-vector/bloggers-influencers-writing-articles-posting-content-cartoon-illustration_74855-14289.jpg"  className="w-10 h-10 " />
        </Tooltip>
      
        
        </div>
      

    </main>
  );
}
