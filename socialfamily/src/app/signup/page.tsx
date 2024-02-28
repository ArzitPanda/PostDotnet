"use client";
import { Button, Divider, Image, Input, Progress } from "@nextui-org/react";
import { AnimatePresence, motion } from "framer-motion";
import React, { useMemo, useState } from "react";
import { FaArrowCircleLeft, FaArrowCircleRight } from "react-icons/fa";
import SignUpConfirmCard from "../Component/Ui/SignUpConfirmCard";
import { League_Spartan } from "next/font/google";
import Link from "next/link";


import wave from "../../../public/wave.png";
import axios from "axios";
const spatan = League_Spartan({style:'normal', subsets:['latin'],weight:'700'});
const page = () => {
  const [signUpState, setSignupState] = useState(0);
  const [ProgressValue, setProgressValue] = useState(30);
  const [email, setEmail] = useState('');
  const [phone, setPhone] = useState('');
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
const [UserPhoto,setUserPhoto] = useState(undefined);
  const data = useMemo(() => (signUpState + 1) * 33.34, [signUpState, setSignupState]);
const [dob,setDob] = useState("2024-02-22 11:07:06.2323688");

  const handleEmailChange = (e:any) => {
    setEmail(e.target.value);
  };

  const handlePhoneChange = (e:any) => {
    setPhone(e.target.value);
  };

  const handleUsernameChange = (e:any) => {
    setUsername(e.target.value);
  };

  const handlePasswordChange = (e:any) => {
    setPassword(e.target.value);
  };


const handleDobChange = (e:any) => {

setDob(e.target.value);

};



  const handleSubmit =async  (e:any) => {
    e.preventDefault();
  
  try {
    const formData = new FormData();
    formData.append('UserEmail', email);
    formData.append('UserPhone', phone);
    formData.append('UserName', username);
    formData.append('Password', password);
    formData.append('Photo', UserPhoto===undefined ?"":UserPhoto); // Append the selected photo to the FormData
    formData.append('DateOfBirth', dob);
  
    const response = await axios.post('http://localhost:5283/api/Auth/signup', formData, {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    });

  console.log(response.data);
  setEmail("");
  setPassword("");
  setPhone("");
  
  } catch (error) {
    console.error('Error:', error);
  }
    // Logic to handle form submission
  };


const handleUserPhotoChange =(e:any) => {
  console.log(e.target.files);
  setUserPhoto(e.target.files[0] );


};

  return (
    <div className="flex w-full h-screen items-center justify-around">
      <Image
        src="login.svg"
        width={700}
        className="hidden lg:block h-full object-cover"
        alt=""
      />

      <div className="h-2/3 w-full lg:w-1/3  flex flex-col items-center justify-around px-6">
        <AnimatePresence>
          {signUpState === 0 && (
            <motion.div
              className="w-2/3 h-full  items-center justify-center flex  flex-col shadow-gray-900 shadow-sm  rounded-2xl gap-y-5 border-3 border-gray-300"
              initial={{ opacity: 0, y: -50 }}
              animate={{ opacity: 1, y: 0 }}
              exit={{ opacity: 0, x: 30 }}
              transition={{ duration: 0.5 }}
            >
              <Image src="brand.png" height={100} width={100} className="object-contain invert" />
              <div className="w-11/12 h-2/3 p-4 flex flex-col items-center justify-center gap-y-2  ">
                <h1 className={`lg:text-black text-3xl w-full uppercase flex items-start justify-start  ${spatan.className}`}>
                  <span className="text-white">sign</span>
                   up
                </h1>
                <Divider className="my-2" />
                <div className="flex flex-col gap-y-2 items-center justify-start w-full">
                  <Input size="md" type="email" label="Email" value={email} onChange={handleEmailChange} className="w-full" />
                  <Input size="md" type="number" label="Phone" maxLength={12} value={phone} onChange={handlePhoneChange} />
                  <Input size="md" type="date"  value={dob} onChange={handleDobChange} />
                  <Button type="button" onClick={()=>{setSignupState(1)}} className="bg-black rounded-xl text-white border-3 border-gray-200 mt-10">Next <FaArrowCircleRight color="white" /></Button>
                </div>
              </div>
             
              <div className={`my-2 p-5 ${spatan.className} flex gap-x-2`}>
                <h1 >Already have an account</h1>
                <a href="http://localhost:5283/api/Auth/login" className="text-yellow-200 underline underline-offset-1">Login</a>
              </div>



            </motion.div>
          )}

          {signUpState === 1 && (
            <motion.div
              className="w-2/3 h-full rounded-2xl flex flex-col items-center justify-start  shadow-gray-900 shadow-sm  border-3 border-gray-300 py-6"
              initial={{ opacity: 0, x: -30 }}
              animate={{ opacity: 1, y: 0, x: 0 }}
              exit={{ opacity: 0, x: 30 }}
              transition={{ duration: 0.5 }}
            >
               <Image src="brand.png" height={100} width={100} className="object-contain invert" />
              <div className="w-full h-full p-5 flex flex-col items-center justify-start mt-4 gap-y-2 ">
                <h1 className={`text-3xl w-full uppercase flex items-start justify-start ${spatan.className}`}>
                  Here
                  <span className="text-black"> we Go</span>
                </h1>
                <Divider className="my-2" />
                <input type="file"  onChange={handleUserPhotoChange} name="file"  required/>
                <Input size="md" type="text" label="Username" value={username} onChange={handleUsernameChange} />
                <Input size="md" type="password" label="Password" value={password} onChange={handlePasswordChange} />
                <Button type="submit"  className="bg-black rounded-xl text-white border-3 border-gray-200 mt-10 hover:border-yellow-500 font-mono" onClick={handleSubmit}>Signup</Button>
              </div>
            </motion.div>
          )}
        </AnimatePresence>
        {
        signUpState==2 &&  (
            <motion.div
            className="w-2/3 h-2/3 my-16 flex items-center justify-center "
          

            animate={{ opacity: 1}}
            exit={{ opacity: 0, x: 100,y:-40 }}
            transition={{ duration: 1,ease:'linear',type:'tween' }}
          >
            <SignUpConfirmCard useremail={email}/>
            </motion.div>


        )
    }



        {/* SignUpConfirmCard component */}
        <div className="w-full p-2 flex flex-row gap-x-2 items-center justify-end">
          {signUpState !== 0 && (
            <Button
              isIconOnly
              color="warning"
              onClick={() => {
                signUpState === 0
                  ? setSignupState(0)
                  : setSignupState(signUpState - 1);
              }}
            >
              <FaArrowCircleLeft color="white" />
            </Button>
          )}

         
        </div>

        <Progress
          size="sm"
          className="w-1/12"
          aria-label="Loading..."
          value={data}
          color="warning"
        />
      </div>
      <div className="-z-20 w-screen absolute -top-10   lg:-top-10  rotate-180">
        <Image src="wave.png" className="w-screen object-cover"/>
      </div>
    </div>
  );
};

export default page;
