import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { Film } from '../types/Film';

export const tmdbApi = createApi({
  reducerPath : 'tmdbApi',
  baseQuery   : fetchBaseQuery({ baseUrl:'/api/Tmdb/' }),
  tagTypes    : ['List'],
  endpoints   : builder => ({

    getFilmDetails : builder.query<Film, number>({
      query : id => `movie_details/${id}`,
      keepUnusedDataFor: 300
    }),

    getFilmTrailer : builder.query<{key:string|null}, number>({
      query : id => `trailer/${id}`,
      keepUnusedDataFor: 300
    }),

    listCategory : builder.query<Film[], {type:'popular'|'trending'|'top250', page:number}>({
      query : ({type, page}) => `${type}?page=${page}`,
      providesTags: (_, __, {type, page}) => [{ type:'List', id:`${type}-${page}` }],
      keepUnusedDataFor: 300
    }),

    listGenre : builder.query<Film[], {genreId:number, page:number}>({
      query : ({genreId, page}) => `genre/${genreId}?page=${page}`,
      providesTags: (_, __, {genreId, page}) => [{ type:'List', id:`g${genreId}-${page}` }],
      keepUnusedDataFor: 300
    }),

    getGenres : builder.query<{genres:{id:number;name:string}[]}, void>({
      query : () => `genres`,
      keepUnusedDataFor: 86400
    })
  })
});

export const {
  useGetFilmDetailsQuery,
  useGetFilmTrailerQuery,
  useListCategoryQuery,
  useListGenreQuery,
  useGetGenresQuery
} = tmdbApi;
