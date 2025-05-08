import { useState } from 'react';
import { Link } from 'react-router-dom';
import Navbar from '../../components/Navbar/Navbar';
import PlaylistCard from '../../components/PlaylistCard/PlaylistCard';
import { useAuth } from '../../context/AuthContext';
import {
  useListPlaylistsQuery,
  useCreatePlaylistMutation
} from '../../api/playlistApi';
import './profilePage.scss';

const ProfilePage: React.FC = () => {
  const { user } = useAuth();

  const { data: lists = [] } = useListPlaylistsQuery();
  const [create] = useCreatePlaylistMutation();
  const [newName, setNewName] = useState('');

  const add = async () => {
    if (!newName.trim()) return;
    await create({ name: newName.trim() });
    setNewName('');
  };

  if (!user) return null;

  const system = lists.filter(p => p.isSystem);
  const custom = lists.filter(p => !p.isSystem);

  return (
    <div className="profile-page">
      <Navbar />
      <main className="profile-main">
        <div className="profile-head">
          <div className="profile-avatar">{user.username[0].toUpperCase()}</div>
          <h1 className="profile-name">{user.username}</h1>
        </div>

        <section className="profile-section">
          <h2 className="profile-title">My playlists</h2>
          <div className="profile-grid">
            {system.map(p => (
              <Link key={p.id} to={`/playlist/${p.id}`} className="profile-link">
                <PlaylistCard title={p.name} count={p.movieCount} />
              </Link>
            ))}
          </div>
        </section>

        <section className="profile-section">
          <h2 className="profile-title">Custom</h2>
          <div className="profile-grid">
            {custom.map(p => (
              <Link key={p.id} to={`/playlist/${p.id}`} className="profile-link">
                <PlaylistCard title={p.name} count={p.movieCount} />
              </Link>
            ))}
            <div className="profile-add">
              <input
                placeholder="New playlist"
                value={newName}
                onChange={e => setNewName(e.target.value)}
              />
              <button onClick={add}>＋</button>
            </div>
          </div>
        </section>
      </main>
    </div>
  );
};

export default ProfilePage;
