import { BASE_URL } from "@/Constant";
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from "axios";





interface Search 
{
    profile:any[],
    status:"loading"|"complete"|"idle"|"failed",
    error:string|undefined|null,
}



const initialState:Search ={
    error:null,
    profile:[],
    status:"idle"
}


export const searchByKeyWord = createAsyncThunk("search/people",async (params:{search:string})=>{

    const {search} = params;

        const data =  await axios.get(BASE_URL+`/api/User?$filter=contains(userName,'${search}') or contains(userEmail,'${search}')`);


        return data.data;

        // http://localhost:5283/api/User?$filter=contains(userName, 'hello') or contains(userEmail, 'odia')

})





const SearchSlice = createSlice({

name:'search',
initialState:initialState,
reducers:{},
extraReducers:builders=>{
    builders.addCase(searchByKeyWord.pending,(state,action)=>{
                state.status="loading"


    })
    .addCase(searchByKeyWord.rejected,(state,action)=>{
            state.status="failed",
            state.error = action.error.message


    })
    .addCase(searchByKeyWord.fulfilled,(state,action)=>{
            state.status="complete",
            state.profile= action.payload,
            state.error=null



    });
}




})



export const SerachReducer = SearchSlice.reducer;
export const SerachActions = SearchSlice.actions;