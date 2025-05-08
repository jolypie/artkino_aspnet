import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { Film } from '../types/Film';

const baseQuery = fetchBaseQuery({
  baseUrl: '/api/Tmdb/',
  credentials: 'include',
  prepareHeaders: h => {
    const t = localStorage.getItem('token');
    if (t) h.set('Authorization', `Bearer ${t}`);
    return h;
  }
});

export const tmdbApi = createApi({
  reducerPath: 'tmdbApi',
  baseQuery,
  tagTypes: ['List'],
  endpoints: b => ({

    getFilmDetails: b.query<Film, number>({
      query: id => `movie_details/${id}`,
      keepUnusedDataFor: 300
    }),

    getFilmTrailer: b.query<{ key: string | null }, number>({
      query: id => `trailer/${id}`,
      keepUnusedDataFor: 300
    }),

    listCategory: b.query<Film[], { type: 'popular' | 'trending' | 'top250'; page: number }>({
      query: ({ type, page }) => `${type}?page=${page}`,
      providesTags: (_, __, { type, page }) => [{ type: 'List', id: `${type}-${page}` }],
      keepUnusedDataFor: 300
    }),

    listGenre: b.query<Film[], { genreId: number; page: number }>({
      query: ({ genreId, page }) => `genre/${genreId}?page=${page}`,
      providesTags: (_, __, { genreId, page }) => [{ type: 'List', id: `g${genreId}-${page}` }],
      keepUnusedDataFor: 300
    }),

    searchMovies: b.query<Film[], { query: string; page: number }>({
      query: ({ query, page }) => `search?query=${encodeURIComponent(query)}&page=${page}`,
      keepUnusedDataFor: 300
    }),

    getGenres: b.query<{ genres: { id: number; name: string }[] }, void>({
      query: () => 'genres',
      keepUnusedDataFor: 86400
    })
  })
});

export const {
  useGetFilmDetailsQuery,
  useGetFilmTrailerQuery,
  useListCategoryQuery,
  useListGenreQuery,
  useSearchMoviesQuery,
  useGetGenresQuery
} = tmdbApi;
