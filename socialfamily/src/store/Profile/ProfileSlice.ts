'use client'
import { BASE_URL, FEED_TYPE } from '@/Constant';
import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import axios from 'axios';


interface User {
    dateOfBirth: string;
    photoUrl: string;
    userEmail: string;
    userId: number;
    userName: string;
    userPhone: string;
  }
  

interface ProfileState {
    posts:any[];
    user:User | null;
    RelationType: string;
    status: 'idle' | 'loading' | 'succeeded' | 'failed';
    error: string | null;
}

const initialState: ProfileState = {
error: null,
posts: [],
RelationType: FEED_TYPE.PUBLIC,
user: null,
status: 'idle'



}




export const fetchProfile = createAsyncThunk(
    'profile/fetchProfile',
    async (params: { id: any; id2: any }) => {
      const { id, id2 } = params;
  
      try {
        const relationResponse = await axios.get(`${BASE_URL}/api/Relation/users?id1=${id}&id2=${id2}`);

        console.log("relationresponse",relationResponse);
        const relation = relationResponse?.data.relationType;
        
        const userResponse = await axios.get(`${BASE_URL}/api/User/${id}`);
        console.log("userresponse",userResponse);
        const user = userResponse.data;
  
        const userPhotoResponse = await axios.get(`${BASE_URL}/api/Post/person/${id}/visibility/${relation}`);
        console.log("userPhotoresponse",userPhotoResponse);
        const userPhoto = userPhotoResponse.data;
  
        return { user, userPhoto, relation };
      } catch (error) {
        throw new Error('Failed to fetch profile');
      }
    }
  );
  
  const profileSlice = createSlice({
    name: 'profile',
    initialState,
    reducers: {},
    extraReducers: builder => {
      builder
        .addCase(fetchProfile.pending, state => {
          state.status = 'loading';
        })
        .addCase(fetchProfile.fulfilled, (state, action) => {
          state.status = 'succeeded';
          state.user = action.payload.user;
          state.RelationType = action.payload.relation;
          state.posts = [...action.payload.userPhoto];
          // Assuming posts and other data from userPhoto are stored in the state as needed
        })
        .addCase(fetchProfile.rejected, (state, action) => {
          state.status = 'failed';
          state.error = action.error.message || 'Failed to fetch profile';
        });
    }
  });
  
  export default profileSlice.reducer;




