"use client"
import { useSearchParams,useRouter } from 'next/navigation'
import React, { useEffect } from 'react'

const page = () => {
  const searchParams = useSearchParams()

  const router = useRouter();
  const search = searchParams.get('token')
  const clientId =searchParams.get('id');
    
  
  useEffect(()=>{

      localStorage.setItem('token', search);
      localStorage.setItem('id', clientId);

      if(localStorage.getItem('token') =="") {
          router.push("/signup");


      }
      else {

          router.push("/");
      }






  },[])




  return (
    <>
   
    <div className='text-white'>{search}</div>

    
    </>  )
}

export default page