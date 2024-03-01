'use client'

import { BASE_URL } from "@/Constant";
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit"
import axios from "axios";




interface userProfile
{
    post: any[],
    status: 'idle' | 'loading' | 'succeeded' | 'failed';
    error: string | null;
}



const initialstate:userProfile ={
    error: null,
    status: 'idle',
    post: [],
} 


export const fetchPostOfPerson = createAsyncThunk("post/fetchPost",async (params: { id: string })=>{







const {id} = params;
// http://localhost:5283/api/Post/person/5

    const response  = await axios.get(BASE_URL+"/api/Post/person/"+id);

    return response.data;





})






const PostSlice = createSlice({
name:'post',
initialState:initialstate,
reducers:{},
extraReducers:builder =>builder
.addCase(fetchPostOfPerson.fulfilled,(state,action)=>{

    state.post = action.payload,
    state.status ="succeeded",
    state.error= null



})
.addCase(fetchPostOfPerson.rejected,(state,action)=>{
    state.status = 'failed';
    state.error = action.error.message || 'Failed to fetch feed';

})




});


export const PostReducer = PostSlice.reducer;
export const PostAction = PostSlice.actions;