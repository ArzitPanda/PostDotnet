// src/features/feed/feedSlice.ts
'use client'
import { BASE_URL } from '@/Constant';
import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import axios from 'axios';

interface FeedState {
  posts: any[]; // Define your post type appropriately
  status: 'idle' | 'loading' | 'succeeded' | 'failed';
  error: string | null;
}

const initialState: FeedState = {
  posts: [],
  status: 'idle',
  error: null,
};

export const fetchFeed = createAsyncThunk('feed/fetchFeed', async (params: { id: number; type: string }) => {
  const { id, type } = params;
  const response = await axios.get(`${BASE_URL}/api/Feed?id=${id}&type=${type}`);
  return response.data;
});

const feedSlice = createSlice({
  name: 'feed',
  initialState,
  reducers: {},
  extraReducers: builder => {
    builder
      .addCase(fetchFeed.pending, state => {
        state.status = 'loading';
      })
      .addCase(fetchFeed.fulfilled, (state, action) => {
        state.status = 'succeeded';
        state.posts = action.payload;
      })
      .addCase(fetchFeed.rejected, (state, action) => {
        state.status = 'failed';
        state.error = action.error.message || 'Failed to fetch feed';
      });
  },
});

export const FeedReducer = feedSlice.reducer;
