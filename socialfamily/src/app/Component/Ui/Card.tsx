"use client";
import React from "react";
import {
  Card,
  CardHeader,
  CardBody,
  CardFooter,
  Avatar,
  Button,
} from "@nextui-org/react";
import Link from "next/link";
import { BsHeartFill } from "react-icons/bs";
import { TiHeartFullOutline } from "react-icons/ti";
import { IoHeartOutline, IoHeartSharp } from "react-icons/io5";
import { useDispatch } from "react-redux";
import { AddLikeToPost } from "@/store/Feed/FeedSlice";
import { BiComment, BiCommentX, BiShare } from "react-icons/bi";
import { useRouter } from "next/navigation";

const CardCustom: React.FC<any> = ({ data, pageIdx }) => {
  const [isFollowed, setIsFollowed] = React.useState(true);

  console.log(data);
  const dispatch = useDispatch();

  const handlelike = (id: any, pageIdx: any) => {
    const localId = localStorage.getItem("id");

    dispatch<any>(
      AddLikeToPost({ postId: id, postIndex: pageIdx, userId: localId })
    );
  };



const router  = useRouter();
const handleRedirectToImage =(id:number)=>{

      router.push("/posts/"+id)



}





  return (
    <Card className="w-full">
      <CardHeader className="justify-between">
        <div className="w-full">
          <Link href={`user/${data?.authorId}`} className="flex gap-5">
            <Avatar
              isBordered
              radius="full"
              size="md"
              src={data?.userPhotoUrl}
            />
            <div className="flex flex-col gap-1 items-start justify-center">
              <h4 className="text-small font-semibold leading-none text-default-600">
                {data?.userName}
              </h4>
            </div>
          </Link>
        </div>

        <Button
          className={
            isFollowed
              ? "bg-transparent text-foreground border-default-200"
              : ""
          }
          color="primary"
          radius="full"
          size="sm"
          variant={isFollowed ? "bordered" : "solid"}
          onPress={() => setIsFollowed(!isFollowed)}
        >
          {isFollowed ? "Unfollow" : "Follow"}
        </Button>
      </CardHeader>
      <CardBody className=" text-small text-default-400 flex flex-col items-center">
        <img src={data?.imgUrl} className="w-full lg:w-6/12 object-cover" />
        <p className="text-xs">{data.description}</p>
        <span className="pt-2 text-xs">
          {data.title}
          <span className="py-2" aria-label="computer" role="img">
            💻
          </span>
        </span>
      </CardBody>
      <CardFooter className="gap-3 flex flex-col items-start ">
        <div className="flex items-center justify-center gap-x-4">
          {data?.isLiked === true ? (
            <IoHeartSharp size={20} color="yellow" />
          ) : (
            <IoHeartOutline
              size={20}
              onClick={() => {
                handlelike(data.id, pageIdx);
              }}
            />
          )}

          <BiComment  onClick={()=>{handleRedirectToImage(data?.id)}}/>

          <BiShare />
        </div>
        <div className="flex flex-row items-start">
          <h4 className="text-xs">
            {data?.recentLikedUser[0]}{" "}
            <span className="font-extralight text-opacity-35 text-gray-300">
              {" "}
              and {data?.likecount} liked the Post
            </span>{" "}
          </h4>
        </div>
        {data?.commentCount !== 0 && (
          <div>
            <div className="flex flex-row items-start text-xs text-gray-400 text-opacity-75">
              view all {data?.commentCount} comments
            </div>
            {data?.topComments.map((ele: any, idx: number) => {
              return (
                <div
                  key={idx}
                  className="text-xs text-opacity-35 text-gray-300"
                >
                  {ele.content}
                </div>
              );
            })}
          </div>
        )}
      </CardFooter>
    </Card>
  );
};
export default CardCustom;
