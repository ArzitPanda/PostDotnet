"use client"
import React from "react";
import {Card, CardHeader, CardBody, CardFooter, Avatar, Button} from "@nextui-org/react";
import Link from "next/link";

const CardCustom:React.FC<any>=({data})=> {
  const [isFollowed, setIsFollowed] = React.useState(true);

  return (
    <Card className="w-full">
      <CardHeader className="justify-between">
        <div className="w-full">
          <Link href={`user/${data?.authorId}`} className="flex gap-5">

          <Avatar isBordered radius="full" size="md" src={data?.author.photoUrl} />
          <div className="flex flex-col gap-1 items-start justify-center">
            <h4 className="text-small font-semibold leading-none text-default-600">{data?.author.userName}</h4>
            <h5 className="text-small tracking-tight text-default-400">{data?.author.userEmail}</h5>
          </div>
          </Link>
        </div>
        
        <Button
          className={isFollowed ? "bg-transparent text-foreground border-default-200" : ""}
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
        <img src={data?.imgUrl} className="w-full lg:w-6/12 object-cover"/>
        <p className="text-xs">
         {data.description}
        </p>
        <span className="pt-2 text-xs">
          {data.title}
          <span className="py-2" aria-label="computer" role="img">
            💻
          </span>
        </span>
      </CardBody>
      <CardFooter className="gap-3">
        <div className="flex gap-1">
          <p className="font-semibold text-default-400 text-small">4</p>
          <p className=" text-default-400 text-small">Following</p>
        </div>
        <div className="flex gap-1">
          <p className="font-semibold text-default-400 text-small">97.1K</p>
          <p className="text-default-400 text-small">Followers</p>
        </div>
      </CardFooter>
    </Card>
  );
}
export default CardCustom