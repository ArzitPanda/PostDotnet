'use client'
import { Button, Card, CardBody, CardFooter, Image } from '@nextui-org/react'
import React from 'react'
import { BiHeart, BiHeartSquare, BiShare } from 'react-icons/bi'

const SmallRecomendUserCard = () => {
  return (
    <Card shadow="sm"  isPressable onPress={() => console.log("item pressed")} className='col-span-4 '>
    <div className="overflow-visible p-0 w-full flex flex-row items-center justify-start gap-x-2">
      <Image
        shadow="sm"
        radius="lg"
        width={'100%'}
        alt={"some user"}
        className="object-cover h-24 p-2"
        src="https://nextui.org/images/hero-card.jpeg"
      />
   <div className='h-full w-10/12 grid grid-cols-2'>
   <div className='h-1/2 flex items-start justify-start flex-col mt-6 col-span-2'>
        <h1 className='text-yellow-500 text-left font-medium text-sm'>username</h1>
            <h2 className='text-left font-light text-xs text-gray-200'>_some_@</h2>
        </div>
        <div className='h-auto flex flex-row items-center justify-around col-span-2 mb-2'>

            <BiHeart color='white' size={20}/>
            <BiShare color='white' size={20}/>
            <Button color='warning'>Share</Button>
        </div>
   </div>
 

    </div>
 
  </Card>
  )
}

export default SmallRecomendUserCard