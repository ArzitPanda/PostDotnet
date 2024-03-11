'use client'
import { configureStore } from '@reduxjs/toolkit'

import {FeedReducer} from '@/store/Feed/FeedSlice'
import ProfileReducer from './Profile/ProfileSlice'
import { authReducer } from './Auth/AuthSlice'
import { PostReducer } from './Post/PostSlice'
import { SerachReducer } from './Search/SearchSlicer'
import { RequestReducer } from './Request/RequestSlice'

export const store = configureStore({
    reducer: {
        feed: FeedReducer,
        profile:ProfileReducer,
        auth:authReducer,
        post:PostReducer,
        serach:SerachReducer,
        request:RequestReducer
      },
})

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch