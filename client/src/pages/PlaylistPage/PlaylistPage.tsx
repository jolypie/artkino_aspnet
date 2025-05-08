import { useParams } from 'react-router-dom';
import Navbar from '../../components/Navbar/Navbar';
import FilmGrid from '../../components/FilmGrid/FilmGrid';
import { useGetPlaylistFilmsQuery } from '../../api/playlistApi';
import './playlistPage.scss';

const PlaylistPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const pid = Number(id);

  const { data: films = [], isLoading } = useGetPlaylistFilmsQuery(pid, {
    skip: isNaN(pid)
  });

  return (
    <div className="pl-page">
      <Navbar />
      <main className="pl-main">
        {isLoading ? <p>Loading…</p> : <FilmGrid films={films} />}
      </main>
    </div>
  );
};

export default PlaylistPage;
