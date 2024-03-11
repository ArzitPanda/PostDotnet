"use client";
import {
  Avatar,
  AvatarGroup,
  Button,
  ButtonGroup,
  Image,
  Modal,
  Popover,
  PopoverContent,
  PopoverTrigger,
  Tooltip,
  useDisclosure,
} from "@nextui-org/react";

import NavbarCustom from "./Component/Ui/Navbar";
import CardCustom from "./Component/Ui/Card";
import { BiChat, BiHeartCircle, BiNotification } from "react-icons/bi";
import SmallRecomendUserCard from "./Component/Ui/SmallRecomendUserCard";
import { FaCamera, FaHome, FaSearch, FaUserFriends } from "react-icons/fa";
import { useEffect, useState } from "react";
import axios from "axios";
import { FEED_TYPE } from "@/Constant";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "@/store";
import { fetchFeed } from "@/store/Feed/FeedSlice";
import UploadModal from "./Component/Ui/UploadModal";
import { useRouter } from "next/navigation";
import { CgNotifications } from "react-icons/cg";
import { GiThreeFriends } from "react-icons/gi";

export default function Home() {
  const data = [
    {
      link: "https://img.freepik.com/free-vector/multigenerational-family-concept-illustration_114360-21169.jpg",
      details: "Family",
      type: FEED_TYPE.FAMILY,
    },
    {
      link: "https://img.freepik.com/free-vector/group-young-people-posing-photo_52683-18823.jpg",
      details: "Friend",
      type: FEED_TYPE.FRIEND,
    },
    {
      link: "https://img.freepik.com/free-vector/flat-design-working-day-scene_52683-62253.jpg",
      details: "Colleague",
      type: FEED_TYPE.OFFICE,
    },
    {
      link: "https://img.freepik.com/free-vector/bloggers-influencers-writing-articles-posting-content-cartoon-illustration_74855-14289.jpg",
      details: "Public",
      type: FEED_TYPE.PUBLIC,
    },
  ];
  const dispatch = useDispatch();

  const state = useSelector((state: RootState) => state.feed);
  const auth = useSelector((state: RootState) => state.auth);
  console.log(state);
  const { isOpen, onOpen, onClose,onOpenChange } = useDisclosure();
 
  const [userPhoto, setUserPhoto] = useState();
  const [userData, setUserData] = useState();
  const [friendRequestModal,setFriendRequestModal] = useState(true);

  const [type, SetType] = useState(FEED_TYPE.PUBLIC);
  const router = useRouter();

  const [notificationOpen, SetNotificationOpen] = useState(false);
  const handleOpen = () => {
    onOpen();
  };
  const handleUserPhoto = () => {
    let id = localStorage.getItem("id");

    axios
      .get("http://localhost:5283/api/User/" + id)
      .then((response) => {
        // Handle the response data
        console.log("Response:", response.data);

        setUserData(response.data);
        setUserPhoto(response.data?.photoUrl);
      })
      .catch((error) => {
        // Handle errors
        console.error("Error:", error);
      });
  };

  useEffect(() => {
    handleUserPhoto();
  }, []);

  useEffect(() => {
    dispatch<any>(
      fetchFeed({ id: parseInt(auth.userId as string), type: type })
    );
  }, [type]);

  return (
    <main className="w-screen h-screen bg-zinc-900">
      {/* interactive */}

      <div className="w-full  grid grid-cols-1  lg:grid-cols-5">
        <div className="block  col-span-1 h-20  bg-zinc-900 lg:hidden">
          <NavbarCustom data={userData} />
        </div>

        <div className="w-full col-span-1 lg:col-span-4  bg-zinc-900 flex mx-auto  flex-col">
          <AvatarGroup className="lg:hidden">
            {data.map((item, index) => (
              <Popover key={index} placement="bottom" color="primary">
                <PopoverTrigger>
                  <Avatar src={item.link}>{item.details}</Avatar>
                </PopoverTrigger>
                <PopoverContent className="bg-yellow-400">
                  <div className="px-1 text-black font-semibold">
                    <div className="text-tiny">{item.details}</div>
                  </div>
                </PopoverContent>
              </Popover>
            ))}
          </AvatarGroup>
          <div className="w-10/12  lg:grid grid-cols-10 h-28 mx-auto hidden   rounded-2xl">
            <div className=" col-span-9 flex items-center justify-center">
              <Image
                src="brand.png"
                alt="brand.png"
                width={"40%"}
                className="invert object-contain h-full ml-12"
              />
            </div>

            <div className="col-span-1 flex items-center justify-end relative">
           
              <CgNotifications color="white" size={30} className="cursor-pointer" onClick={()=>{SetNotificationOpen(!notificationOpen)}}/>
        
              {
                notificationOpen && (<div className="h-96 w-[350px] overflow-scroll overflow-x-hidden  absolute top-20 left-20 border-1 border-slate-900 rounded-md bg-transparent z-30 shadow-lg backdrop-filter backdrop-blur-sm ">
                <div className="w-full">
                  <p className="text-lg font-semibold text-yellow-400 bg-gray-900 bg-opacity-40 h-10 mb-6 text-center">
                    Notifications for You
                  </p>
                </div>
                <div className="w-full max-h-[400px]  flex flex-col items-center justify-start gap-y-1">
                  {new Array(5).fill(0).map((i) => {
                    return (
                      <div className="w-full  bg-slate-400 bg-opacity-30 h-[200px]  border-b-zinc-950 ">
                        <div className="opacity-100">hi hi</div>
                      </div>
                    );
                  })}
                </div>
              </div>)
              }

              <FaUserFriends color="white" size={30} className="cursor-pointer mx-4" />

              
            </div>
          </div>

          <div className="w-11/12 lg:w-7/12 flex flex-col items-center mx-auto justify-center mt-6 gap-4 mb-28">
            {state.posts.map((ele, idx) => {
              return <CardCustom data={ele} key={idx} pageIdx={idx} />;
            })}
          </div>
        </div>
        <div className="hidden lg:flex col-span-1   bg-zinc-800  flex-col items-center justify-start gap-y-4">
          <Avatar
            src={userPhoto}
            isBordered
            color="warning"
            className="w-48 h-48 text-large mt-24"
            onClick={() => {
              router.push("/Profile");
            }}
          />
          <ButtonGroup>
            <Button isIconOnly>
              <BiHeartCircle />
            </Button>
            <Button isIconOnly>
              <BiChat />
            </Button>
            <Button isIconOnly>
              <BiNotification />
            </Button>
          </ButtonGroup>
          <div className="w-full flex flex-col items-center justify-start">
            <h1 className="text-slate-300 text-sm font-semibold">
              User You May Like
            </h1>
            <div className="w-10/12 grid grid-cols-4 gap-x-6 my-4 gap-y-6 mb-6">
              {new Array(2).fill(0).map((ele,idx) => (
                <SmallRecomendUserCard key={idx} />
              ))}
            </div>
          </div>
        </div>
      </div>
      <div className="fixed lg:bottom-10 bottom-[0.5px] left-0 w-full flex justify-center items-end z-40">
        <div className="h-16 bg-yellow-400 w-full  lg:w-4/12 xl:w-2/12 lg:rounded-2xl shadow-lg">
          <ButtonGroup className="flex justify-between items-center h-full px-4">
            <Button className="bg-yellow-400">
              {" "}
              <FaHome className="text-black" size={20} />
            </Button>
            <Button className="bg-yellow-400" onClick={handleOpen}>
              <FaCamera className="text-black" size={20} />
            </Button>
            <Button
              className="bg-yellow-400"
              onClick={() => {
                router.push("/search");
              }}
            >
              <FaSearch className="text-black" size={20} />
            </Button>
          </ButtonGroup>
        </div>
      </div>
      <div className="w-20 lg:flex flex-col fixed bottom-10 left-10 gap-y-6 z-30 hidden ">
        {data.map((ele, idx) => {
          return (
            <Tooltip
              key={idx}
              showArrow={true}
              content={ele.details}
              className="bg-yellow-600 text-white"
              placement="right"
            >
              <Avatar
                isBordered={type === ele.type}
                color="warning"
                src={ele.link}
                className="w-10 h-10 "
                onClick={() => {
                  SetType(ele.type);
                }}
              />
            </Tooltip>
          );
        })}
      </div>
      <UploadModal isOpen={isOpen} onOpen={onOpen} onClose={onClose} />
      
    </main>
  );
}
