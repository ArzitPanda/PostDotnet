import { BASE_URL } from "@/Constant";
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from "axios";

interface RequestData {
  status: "loading" | "pending" | "complete" | "idle";
  error: null | string | undefined;
  data: any[];
}

const initialdata: RequestData = {
  data: [],
  error: null,
  status: "idle",
};

export const getRequestData = createAsyncThunk('request/data',async (params: { id: any })=>{

    const {id} = params;
    console.log(params,"params here")
    const response = await axios.get(BASE_URL+`/api/RelationRequest?id=${13}`);


      return response.data;

})


const RequestSlice = createSlice({
  name: "request",
  initialState: initialdata,
  reducers: {},
  extraReducers: builder =>{
      builder.addCase(getRequestData.pending,(state,action)=>{

            state.data=[]
            state.status ='pending'

      })
      .addCase(getRequestData.rejected,(state,action)=>{

              state.status ="idle"
              state.error =action.error.message;

      })
      .addCase(getRequestData.fulfilled,(state,action)=>{

        state.status ="complete"
        state.data = action.payload;

})


  },
});

export const RequestReducer = RequestSlice.reducer;
 