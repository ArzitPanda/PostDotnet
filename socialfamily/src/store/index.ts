'use client'
import { configureStore } from '@reduxjs/toolkit'

import {FeedReducer} from '@/store/Feed/FeedSlice'
import ProfileReducer from './Profile/ProfileSlice'
import { authReducer } from './Auth/AuthSlice'
import { PostReducer } from './Post/PostSlice'

export const store = configureStore({
    reducer: {
        feed: FeedReducer,
        profile:ProfileReducer,
        auth:authReducer,
        post:PostReducer,
      },
})

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch