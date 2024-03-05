"use client"
import { RootState } from '@/store'
import { searchByKeyWord } from '@/store/Search/SearchSlicer'
import { Avatar, Card, CardBody, Image, Input } from '@nextui-org/react'
import Link from 'next/link'
import React, { useState } from 'react'
import { CgNotifications } from 'react-icons/cg'
import { useDispatch, useSelector } from 'react-redux'

const page = () => {

const photodata =[
    {
        img:`http://localhost:5283/profile/aa7fc2cf-7abf-4a9e-82d7-9b58cac51b79.png`,
        name:"Arzit Panda",
        username:'@arzit'
    },
    {
        img:`http://localhost:5283/profile/aa7fc2cf-7abf-4a9e-82d7-9b58cac51b79.png`,
        name:"Arzit Panda",
        username:'@arzit'
    },
    {
        img:`http://localhost:5283/profile/aa7fc2cf-7abf-4a9e-82d7-9b58cac51b79.png`,
        name:"Arzit Panda",
        username:'@arzit'
    },
    {
        img:`http://localhost:5283/profile/aa7fc2cf-7abf-4a9e-82d7-9b58cac51b79.png`,
        name:"Arzit Panda",
        username:'@arzit'
    },
    {
        img:`http://localhost:5283/profile/aa7fc2cf-7abf-4a9e-82d7-9b58cac51b79.png`,
        name:"Arzit Panda",
        username:'@arzit'
    },
    {
        img:`http://localhost:5283/profile/aa7fc2cf-7abf-4a9e-82d7-9b58cac51b79.png`,
        name:"Arzit Panda",
        username:'@arzit'
    },
    {
        img:`http://localhost:5283/profile/aa7fc2cf-7abf-4a9e-82d7-9b58cac51b79.png`,
        name:"Arzit Panda",
        username:'@arzit'
    },
    {
        img:`http://localhost:5283/profile/aa7fc2cf-7abf-4a9e-82d7-9b58cac51b79.png`,
        name:"Arzit Panda",
        username:'@arzit'
    },
    {
        img:`http://localhost:5283/profile/aa7fc2cf-7abf-4a9e-82d7-9b58cac51b79.png`,
        name:"Arzit Panda",
        username:'@arzit'
    },
    {
        img:`http://localhost:5283/profile/aa7fc2cf-7abf-4a9e-82d7-9b58cac51b79.png`,
        name:"Arzit Panda",
        username:'@arzit'
    },
    {
        img:`http://localhost:5283/profile/aa7fc2cf-7abf-4a9e-82d7-9b58cac51b79.png`,
        name:"Arzit Panda",
        username:'@arzit'
    }
]

const postData = [

]


const [search,setSearch] = useState("");


const dispatch = useDispatch();


const searchResult  = useSelector((state:RootState)=>state.serach);
const debounce = <F extends (...args: any[]) => void>(func: F, delay: number) => {
    let timerId: ReturnType<typeof setTimeout>;
    return (...args: Parameters<F>) => {
      if (timerId) clearTimeout(timerId);
      timerId = setTimeout(() => {
        func(...args);
      }, delay);
    };
  };
  

  // Function to handle search
  const handleSearch = debounce(() => {
    dispatch<any>(searchByKeyWord({ search }));
  }, 1000); // Adjust delay as needed (e.g., 500ms)

  // Function to update search state
  const handleChange = (e:any) => {
    const { value } = e.target;
    setSearch(value);
    handleSearch();
    console.log(searchResult);
    
    // Trigger search after debounce
  };





  return (
    <div className='w-full'>
            <div className='lg:w-8/12 w-full mx-auto grid grid-cols-5'>
           
                <div className='col-span-5 flex items-center justify-center my-6'>
                <Image
              src="brand.png"
              alt="brand.png"
              width={"40%"}
              className="invert object-contain h-full ml-12"
            />
               
                </div>
                <Input type='text' placeholder='enter your search' className='col-span-6 mb-6 ' value={search} onChange={handleChange} variant='underlined'/>
                    <div className='flex flex-col gap-y-1 col-span-6 lg:col-span-3 '>
                    {
                    searchResult.profile?.map((ele)=>{return (  <Card className='w-full rounded-none border-1 border-gray-900'>
                        <Link href={`/user/${ele.userId}`}>                        <CardBody className='w-full grid grid-cols-10  items-center justify-center'>
                            
                            <Avatar   src={ele.photoUrl} isBordered className='col-span-2  md:col-span-1 '/>
                            <div className='flex flex-col h-full items-start justify-start col-span-8 lg:col-span-9'>
                                <h4 className='text-lg font-semibold text-left'>{ele.userName}</h4>
                                <p className='text-sm font-extralight text-gray-500'>{ele.userEmail}</p>
                            </div>
                        </CardBody>
                        </Link>

                      </Card>)})
                    }

                    </div>
                    <div className='bg-gray-900 col-span-2 trending-posts'>

                    </div>



            </div>


       
    </div>
  )
}

export default page