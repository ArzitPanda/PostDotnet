import { BASE_URL } from "@/Constant";
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from "axios";


interface Auth
{
    token: string;
    user: any;
    userId:number | string|null ;
}

const initialState: Auth = {
    token: "",
    user: null,
    userId: ""
  };




export const getSession = createAsyncThunk("auth/getToken", async () => {

    const response = await axios.get(`${BASE_URL}/get-token`, {
        withCredentials: true
    });
    console.log(response.data,"getsession");
    return response.data;
  })
  export const getUserById = createAsyncThunk("auth/getUserById", async (userId: string | number|null) => {
    const response = await axios.get(`${BASE_URL}/api/User/${userId}`);
    console.log(response.data,"getUerById");
    return response.data;
  });
  const authSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {},
    extraReducers: (builder) => {
      builder
        .addCase(getSession.fulfilled, (state, action) => {
          // Update state with the session token
          state.token = action.payload.token;
        
        
        })
        .addCase(getSession.rejected, (state, action) => {
          // Handle error if session token retrieval fails
          console.error("Failed to retrieve session token:", action.error);
        })
        .addCase(getUserById.fulfilled, (state, action) => {
            // Update state with the user data fetched by ID
            state.user = action.payload;
            state.userId = action.meta.arg;
          })
          .addCase(getUserById.rejected, (state, action) => {
            // Handle error if user data retrieval fails
            console.error("Failed to retrieve user data by ID:", action.error);
          });
    }
  });


  export const authActions = authSlice.actions;
export const authReducer = authSlice.reducer;