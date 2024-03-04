"use client"
import { RootState } from '@/store'
import { searchByKeyWord } from '@/store/Search/SearchSlicer'
import { Avatar, Card, CardBody, Image, Input } from '@nextui-org/react'
import React, { useState } from 'react'
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
  }, 500); // Adjust delay as needed (e.g., 500ms)

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
                <Input type='text' placeholder='enter your search' className='col-span-6 mb-6 ' value={search} onChange={handleChange}/>
                    <div className='flex flex-col gap-y-1 col-span-6 lg:col-span-3 '>
                    {
                    photodata.map((ele)=>{return (  <Card className='w-full rounded-none border-1 border-gray-900'>
                        <CardBody className='w-full grid grid-cols-10  items-center justify-center'>
                            
                            <Avatar   src={ele.img} isBordered className='col-span-2  md:col-span-1 '/>
                            <div className='flex flex-col h-full items-start justify-start col-span-8 lg:col-span-9'>
                                <h4 className='text-lg font-semibold text-left'>{ele.name}</h4>
                                <p className='text-sm font-extralight text-gray-500'>{ele.username}</p>
                            </div>
                        </CardBody>
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