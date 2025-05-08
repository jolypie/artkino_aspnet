import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import type { Film } from '../types/Film';

export interface Playlist {
  id: number;
  name: string;
  movieCount: number;
  isSystem: boolean;
}

const baseQuery = fetchBaseQuery({
  baseUrl: '/api/playlists',
  credentials: 'include',
  prepareHeaders: headers => {
    const token = localStorage.getItem('token');
    if (token) headers.set('Authorization', `Bearer ${token}`);
    return headers;
  }
});

export const playlistApi = createApi({
  reducerPath: 'playlistApi',
  baseQuery,
  tagTypes: ['Playlists', 'PlaylistFilms'],
  endpoints: builder => ({

    listPlaylists: builder.query<Playlist[], void>({
        query: () => 'user',
        providesTags: result =>
          result
            ? [
                ...result.map(p => ({ type: 'Playlists' as const, id: p.id })),
                { type: 'Playlists', id: 'LIST' }
              ]
            : [{ type: 'Playlists', id: 'LIST' }]
      }),
      
    createPlaylist: builder.mutation<Playlist, { name: string }>({
      query: ({ name }) => ({
        url: '',
        method: 'POST',
        body: { name }
      }),
      invalidatesTags: [{ type: 'Playlists', id: 'LIST' }]
    }),

    deletePlaylist: builder.mutation<void, { id: number }>({
      query: ({ id }) => ({
        url: `/${id}`,
        method: 'DELETE'
      }),
      invalidatesTags: (_ , __ , { id }) => [
        { type: 'Playlists', id },
        { type: 'Playlists', id: 'LIST' }
      ]
    }),

    getPlaylistFilms: builder.query<Film[], number>({
        query: id => `/${id}`,
        providesTags: (_, __, id) => [{ type: 'PlaylistFilms', id }]
    })
  })
});

export const {
  useListPlaylistsQuery,
  useCreatePlaylistMutation,
  useDeletePlaylistMutation,
  useGetPlaylistFilmsQuery
} = playlistApi;
