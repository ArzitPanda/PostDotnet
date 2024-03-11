"use client"
import { useState, useEffect } from 'react';
import axios from 'axios';

const useUserData = () :{ userData: any| null; userPhoto: any | null } => {
  const [userData, setUserData] = useState({});
  const [userPhoto, setUserPhoto] = useState();

  const fetchUserData = () => {
    let id = localStorage.getItem("id");

    axios
      .get("http://localhost:5283/api/User/" + id)
      .then((response) => {
        // Handle the response data
        console.log("Response:", response.data);

        setUserData(response.data);
        setUserPhoto(response.data?.photoUrl);
      })
      .catch((error) => {
        // Handle errors
        console.error("Error:", error);
      });
  };

  useEffect(() => {
    fetchUserData();
  }, []);

  return { userData, userPhoto };
};

export default useUserData;
