
import { Image } from '@nextui-org/react'
import React, { useEffect } from 'react'
import { FaComment, FaHeart, FaShare } from 'react-icons/fa';

const PhotoViewer:React.FC<any> = ({open,handleClose,data}) => {
        console.log(data)
    useEffect(() => {
        if (open) {
          document.body.style.overflow = 'hidden';
        } else {
          document.body.style.overflow = 'visible';
        }
    
        const scrollPosition = window.scrollY;
        console.log(scrollPosition,"scroll position")
        // Clean up by resetting the overflow style when component unmounts
        return () => {
          document.body.style.overflow = 'visible';
        };
      }, [open]);

      const handleClick = (event: React.MouseEvent<HTMLDivElement>) => {
        // If the clicked element has the class 'mainElement', do nothing
        let currentElement = event.target as HTMLElement | null;
    while (currentElement && currentElement !== document.body) {
      if (currentElement.classList.contains('mainElement')) {
        return;
      }
      currentElement = currentElement.parentElement;
      }
      handleClose();
    }

  return (
    <div className={`${open?"absolute top-0 z-50":"hidden"} bg-slate-900  bg-opacity-75 w-screen h-screen flex  items-center justify-center ` } onClick={handleClick}>
    <div className="w-full lg:w-6/12 h-auto grid grid-cols-6 mainElement bg-slate-900 p-4 rounded-xl">
      <div className="col-span-6 lg:col-span-3 mainElement flex items-center justify-center">
        <Image src={data?.imgUrl} className="w-full  object-contain mainElement"/>
      </div>
      <div className="col-span-6 lg:col-span-3 mainElement grid lg:grid-rows-12 h-auto lg:h-full p-2">
          <div className="flex justify-between mt-2 mainElement row-span-1 ">
            <h2 className="text-white font-bold">{data?.title}</h2>
            <div className="flex items-center space-x-2 justify-center">
              <FaHeart className="text-red-500 cursor-pointer" />
              <FaComment className="text-white cursor-pointer" />
              <FaShare className="text-white cursor-pointer" />
            </div>
          </div>
          <p className="text-white row-span-1">{data?.description}</p>
          <div className="flex items-center space-x-2 mt-2 mainElement row-span-1">
            <p className="text-white">{data?.likecount} Likes</p>
            <p className="text-white">{data?.commentCount} Comments</p>
          </div>
          <div className="flex items-center space-x-2 mt-2 mainElement row-span-1">
            {data?.recentLikedUser.map((user:any) => (
              <p key={user} className="text-white">{user}</p>
            ))}
          </div>
          <div className="hidden lg:flex justify-between items-end mainElement row-span-8">

            <input type="text" className="w-full py-1 px-2 mt-2 rounded-md border-none focus:outline-none bg-gray-800 text-white" placeholder="Type a comment..." />
            <button className="px-4 py-1 rounded-md bg-blue-500 text-white focus:outline-none">Post</button>
          </div>
          <div className='block lg:hidden'>
                
          </div>
        </div>
    </div>

</div>
  )
}

export default PhotoViewer