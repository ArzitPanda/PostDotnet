"use client"
import { getSession, getUserById } from '@/store/Auth/AuthSlice'
import axios from 'axios'
import { useSearchParams,useRouter } from 'next/navigation'
import React, { useEffect } from 'react'
import { useDispatch } from 'react-redux'

const page = () => {
  const searchParams = useSearchParams()

  const router = useRouter();
  const search = searchParams.get('token')
  const clientId =searchParams.get('id');
    
  const dispatch =useDispatch();



  const storeToken = () => {
    axios.post('http://localhost:5283/store-token', { token:search})
      .then(response => {
        console.log('Token stored successfully');
      })
      .catch(error => {
        console.error('Error storing token:', error);
      });
  };
  useEffect(()=>{

      localStorage.setItem('token', search);
      localStorage.setItem('id', clientId);
        localStorage.setItem('request', 1);

    storeToken();





      if(localStorage.getItem('token') =="") {
          router.push("/signup");


      }
      else {

        dispatch(getSession());
      dispatch(getUserById(localStorage.getItem('id')));
      localStorage.setItem('request', 2);


          router.push("/");
      }






  },[])




  return (
    <>
   
    <div className='text-white'>{search}</div>

    
    </>  )
}

export default page