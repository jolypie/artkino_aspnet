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

            <article className="pl-card pl-custom">
              <div className="pl-bg" />
              <div className="pl-content">
                <input
                  placeholder="New playlist"
                  value={newName}
                  onChange={e => setNewName(e.target.value)}
                  className="pl-input"
                />
                <button onClick={add} className="pl-button">＋</button>
              </div>
            </article>
          </div>
        </section>
      </main>
    </div>
    
  );
};

export default ProfilePage;
