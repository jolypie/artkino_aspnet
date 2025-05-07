import { configureStore } from '@reduxjs/toolkit';
import usersReducer from '../features/users/usersSlice';
import moviesReducer from '../features/movies/moviesSlice';
import { tmdbApi } from '../api/tmdbApi';

export const store = configureStore({   
  reducer: {
    users: usersReducer,
    movies: moviesReducer,
    [tmdbApi.reducerPath]: tmdbApi.reducer,
  },

  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(tmdbApi.middleware),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
