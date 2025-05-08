// src/components/PlaylistButtons/PlaylistButtons.tsx
import React, { useMemo, useState } from 'react';
import FavoriteIcon      from '@mui/icons-material/FavoriteBorder';
import EyeIcon           from '@mui/icons-material/VisibilityOutlined';
import WatchLaterIcon    from '@mui/icons-material/AccessTime';
import PlaylistAddIcon   from '@mui/icons-material/PlaylistAdd';
import CloseIcon         from '@mui/icons-material/Close';
import {
  useListPlaylistsQuery,
  useGetPlaylistItemsQuery,
  useAddFilmToPlaylistMutation,
  useRemoveFilmFromPlaylistMutation
} from '../../api/playlistApi';
import { skipToken } from '@reduxjs/toolkit/query/react';

import './playlistButtons.scss';

const ICONS = {
  Favorites   : <FavoriteIcon />,
  Watched     : <EyeIcon />,
  'Watch Later': <WatchLaterIcon />
};

interface Props { filmId: number }

const PlaylistButtons: React.FC<Props> = ({ filmId }) => {

  const { data: playlists = [] } = useListPlaylistsQuery();
  const system = useMemo(() => playlists.filter(p => p.isSystem), [playlists]);
  const custom = useMemo(() => playlists.filter(p => !p.isSystem), [playlists]);

  const favId     = system.find(p => /^favorites$/i.test(p.name))?.id;
  const watchedId = system.find(p => /^watched$/i.test(p.name))?.id;
  const laterId   = system.find(p => /^watch later$/i.test(p.name))?.id;

  const { data: favItems     = [] } = useGetPlaylistItemsQuery(favId     ?? skipToken);
  const { data: watchedItems = [] } = useGetPlaylistItemsQuery(watchedId ?? skipToken);
  const { data: laterItems   = [] } = useGetPlaylistItemsQuery(laterId   ?? skipToken);

  const allItems = [...favItems, ...watchedItems, ...laterItems];

  const [addFilm]    = useAddFilmToPlaylistMutation();
  const [removeFilm] = useRemoveFilmFromPlaylistMutation();

  const [open, setOpen] = useState(false);

  const isIn = (plId: number) =>
    allItems.some(it => it.tmdbFilmId === filmId && it.playlistId === plId);

  const toggle = async (plId: number) => {
    const existing = allItems.find(it => it.tmdbFilmId === filmId && it.playlistId === plId);
    return existing
      ? removeFilm({ id: existing.id, playlistId: plId })
      : addFilm   ({ playlistId: plId,   tmdbFilmId: filmId });
  };

  return (
    <>
      <div className="hero__actions">
        {system.map(p => (
          <button
            key={p.id}
            className={`action-btn ${isIn(p.id) ? 'active' : ''}`}
            aria-label={p.name}
            onClick={() => toggle(p.id)}
          >
            {ICONS[p.name as keyof typeof ICONS]}
          </button>
        ))}

        <button className="action-btn" onClick={() => setOpen(true)}>
          <PlaylistAddIcon />
        </button>
      </div>

      {open && (
        <div className="playlist-backdrop" onClick={() => setOpen(false)}>
          <div
            className="playlist-modal"
            onClick={e => e.stopPropagation()}
          >
            <header className="playlist-modal__head">
              <h2>Select playlist</h2>
              <button className="close-btn" onClick={() => setOpen(false)}>
                <CloseIcon />
              </button>
            </header>

            <ul className="playlist-list">
              {custom.length === 0 && <li className="playlist-empty">You have no custom playlists yet</li>}

              {custom.map(p => (
                <li key={p.id}>
                  <button
                    className={`playlist-item ${isIn(p.id) ? 'active' : ''}`}
                    onClick={() => toggle(p.id)}
                  >
                    {p.name}
                  </button>
                </li>
              ))}
            </ul>
          </div>
        </div>
      )}
    </>
  );
};

export default PlaylistButtons;
