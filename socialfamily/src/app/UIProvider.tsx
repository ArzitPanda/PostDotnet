
'use client'

import { getSession, getUserById } from '@/store/Auth/AuthSlice';
import {NextUIProvider} from '@nextui-org/react'
import { useEffect } from 'react';
import { useDispatch } from 'react-redux';

export function UIProviders({children}: { children: React.ReactNode }) {

  const dispatch =useDispatch();



  useEffect(()=>{


    let data = localStorage.getItem('request');
   
   if(data === null || data === undefined)
   {
     
   }
   else
   {
   
     if(parseInt(data) ===2)
     {
     
         
      dispatch<any>(getSession());
      if(localStorage.getItem('id') !== undefined || localStorage.getItem('id') !== null)
        {

            const data  =localStorage.getItem('id');


          dispatch<any>(getUserById(data));
        }
     
   
   
     }
     
   }
   
   
   
   
   
   
   
   },[]);





  return (
    <>
    <NextUIProvider>
      {children}
    </NextUIProvider>
    </>
  )
}