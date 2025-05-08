import { useParams } from 'react-router-dom';
import Navbar from '../../components/Navbar/Navbar';
import {
  useGetPlaylistItemsQuery,
  useListPlaylistsQuery
} from '../../api/playlistApi';
import FilmLoader from '../../components/FilmLoader/FilmLoader';
import './playlistPage.scss';

const palette: Record<string, string> = {
  favorites : 'pl-fav',
  watched   : 'pl-watched',
  'watch later': 'pl-later'
};

const PlaylistPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const pid    = Number(id);

  const { data: playlists = [] } = useListPlaylistsQuery();
  const playlist                  = playlists.find(p => p.id === pid);

  const { data: items = [], isLoading } = useGetPlaylistItemsQuery(pid, {
    skip: isNaN(pid)
  });

  const filmIds = items.map(i => i.tmdbFilmId);

  const heroClass =
    'pl-hero ' +
    (playlist ? palette[playlist.name.toLowerCase()] ?? 'pl-default' : '');

  return (
    <div className="pl-page">
      <Navbar />

      {playlist && (
        <section className={heroClass}>
          <div className="overlay">
            <h1>{playlist.name}</h1>
            <p>
              {filmIds.length} {filmIds.length === 1 ? 'film' : 'films'}
            </p>
          </div>
        </section>
      )}

      <main className="pl-main">
        {isLoading ? (
          <p className="loading">Loading…</p>
        ) : (
          <div className="film-grid">
            {filmIds.map(id => (
              <FilmLoader key={id} tmdbId={id} />
            ))}
          </div>
        )}
      </main>
    </div>
  );
};

export default PlaylistPage;
