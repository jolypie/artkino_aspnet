import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import type { Film } from '../types/Film';

export interface Playlist {
  id: number;
  name: string;
  movieCount: number;
  isSystem: boolean;
}

export interface PlaylistItemDto {
  id: number;
  playlistId: number;
  tmdbFilmId: number;
  addedAt: string;
}

const baseQuery = fetchBaseQuery({
  baseUrl: '/api',
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
  tagTypes: ['Playlists', 'PlaylistFilms', 'PlaylistItems'],
  endpoints: builder => ({

    listPlaylists: builder.query<Playlist[], void>({
      query: () => '/playlists/user',
      providesTags: result =>
        result
          ? [
              ...result.map(p => ({ type: 'Playlists' as const, id: p.id })),
              { type: 'Playlists', id: 'LIST' }
            ]
          : [{ type: 'Playlists', id: 'LIST' }]
    }),

    getPlaylistFilms: builder.query<Film[], number>({
      query: id => `/playlistitems/${id}`,
      providesTags: (_, __, id) => [{ type: 'PlaylistFilms', id }]
    }),

    createPlaylist: builder.mutation<Playlist, { name: string }>({
      query: ({ name }) => ({
        url: '/playlists',
        method: 'POST',
        body: { name }
      }),
      invalidatesTags: [{ type: 'Playlists', id: 'LIST' }]
    }),

    deletePlaylist: builder.mutation<void, { id: number }>({
      query: ({ id }) => ({
        url: `/playlists/${id}`,
        method: 'DELETE'
      }),
      invalidatesTags: (_, __, { id }) => [
        { type: 'Playlists', id },
        { type: 'Playlists', id: 'LIST' }
      ]
    }),

    getPlaylistItems: builder.query<PlaylistItemDto[], number>({
      query: playlistId => `/playlistitems/${playlistId}`,
      providesTags: ['PlaylistItems']
    }),

    addFilmToPlaylist: builder.mutation<PlaylistItemDto, { playlistId: number; tmdbFilmId: number }>({
      query: ({ playlistId, tmdbFilmId }) => ({
        url: '/playlistitems',
        method: 'POST',
        body: { playlistId, tmdbFilmId }
      }),
      invalidatesTags: (_, __, { playlistId }) => [
        { type: 'PlaylistItems' },
        { type: 'PlaylistFilms', id: playlistId }
      ]
    }),

    removeFilmFromPlaylist: builder.mutation<void, { id: number; playlistId: number }>({
      query: ({ id, playlistId }) => ({
        url: `/playlistitems/${id}?playlistId=${playlistId}`,
        method: 'DELETE'
      }),
      invalidatesTags: (_, __, { playlistId }) => [
        { type: 'PlaylistItems' },
        { type: 'PlaylistFilms', id: playlistId }
      ]
    })

  })
});

export const {
  useListPlaylistsQuery,
  useGetPlaylistFilmsQuery,
  useCreatePlaylistMutation,
  useDeletePlaylistMutation,
  useGetPlaylistItemsQuery,
  useAddFilmToPlaylistMutation,
  useRemoveFilmFromPlaylistMutation
} = playlistApi;