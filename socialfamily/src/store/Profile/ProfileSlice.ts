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
    RelationIsExist: boolean;
    RequestIsExist: boolean;
}

const initialState: ProfileState = {
error: null,
posts: [],
RelationType: FEED_TYPE.PUBLIC,
user: null,
status: 'idle',
RelationIsExist:false,
RequestIsExist:false,

}




export const fetchProfile = createAsyncThunk(
    'profile/fetchProfile',
    async (params: { id: any; id2: any }) => {
      const { id, id2 } = params;
  
      try {
        const relationResponse = await axios.get(`${BASE_URL}/api/Relation/users?id1=${id}&id2=${id2}`);
        const userResponse = await axios.get(`${BASE_URL}/api/User/${id}`);
        console.log("userresponse",userResponse);
        const user = userResponse.data;
  



        console.log("relationresponse",relationResponse);
       const  relation = relationResponse?.data.relationType;
          const isExist = relation!==undefined?true:false;

          
        if(!isExist) {
            const relationRequest = await axios.get(`${BASE_URL}/api/RelationRequest/requestor/${id2}/receiver/${id}`)

            console.log("relationRequest",relationRequest);

              const isrequestExist = relationRequest.data?true:false;


            const userPhotoResponse = await axios.get(`${BASE_URL}/api/Post/person/${id}/visibility/${relation!==undefined?relation:'Public'}`);
        console.log("userPhotoresponse",userPhotoResponse);
        const userPhoto = userPhotoResponse.data;
          console.log("last data",{ user, userPhoto, relation,isExist,isrequestExist })
        return { user, userPhoto, relation,isExist,isrequestExist };
        }





      console.log("relation",relation);

       
        const userPhotoResponse = await axios.get(`${BASE_URL}/api/Post/person/${id}/visibility/${relation!==undefined?relation:'Public'}`);
        console.log("userPhotoresponse",userPhotoResponse);
        const userPhoto = userPhotoResponse.data;
  
        return { user, userPhoto, relation,isExist };
      } catch (error) {
        throw new Error('Failed to fetch profile');
      }
    }
  );
  
  const profileSlice = createSlice({
    name: 'profile',
    initialState,
    reducers: {

        addFriendRequest:(state,action)=>{

              state.RequestIsExist= true;




        }

    },
    extraReducers: builder => {
      builder
        .addCase(fetchProfile.pending, state => {
          state.status = 'loading';
        })
        .addCase(fetchProfile.fulfilled, (state, action) => {
          state.status = 'succeeded';
          state.user = action.payload.user;
          state.RelationType = action.payload.relation;
          state.RelationIsExist = action.payload.isExist;
          state.posts = [...action.payload.userPhoto];
          state.RequestIsExist =action.payload?.isrequestExist || false;
          // Assuming posts and other data from userPhoto are stored in the state as needed
        })
        .addCase(fetchProfile.rejected, (state, action) => {
          state.status = 'failed';
          state.error = action.error.message || 'Failed to fetch profile';
        });
    }
  });
  export const profileActions = profileSlice.actions;
  export default profileSlice.reducer;




