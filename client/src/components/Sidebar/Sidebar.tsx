import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import TheatersOutlinedIcon from '@mui/icons-material/TheatersOutlined';
import SlideshowIcon from '@mui/icons-material/Slideshow';
import LocalActivityOutlinedIcon from '@mui/icons-material/LocalActivityOutlined';
import api from '../../api/axiosInstance';
import './sidebar.scss';

interface Genre { id: number; name: string; }

interface SidebarProps {
  open: boolean;
  onClose: () => void;
}

const Sidebar: React.FC<SidebarProps> = ({ open, onClose }) => {
  const [genres, setGenres] = useState<Genre[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    api.get<{ genres: Genre[] }>('/Tmdb/genres')
       .then(r => setGenres(r.data.genres))
       .finally(() => setLoading(false));
  }, []);

  const categories = [
    { text:'Films',        icon:<TheatersOutlinedIcon/>, link:'/' },
    { text:'Popular now',  icon:<SlideshowIcon/>,        link:'/films/popular' },
    { text:'Top 250',      icon:<LocalActivityOutlinedIcon/>, link:'/top250' }
  ];

  return (
    <aside className={`sidebar ${open ? 'sidebar--open' : ''}`}>
      <div className="sidebar__section" onClick={onClose}>
        <h2 className="sidebar__title">Categories</h2>
        <ul className="sidebar__list">
          {categories.map(c=>(
            <li key={c.link} className="sidebar__item">
              <Link to={c.link} className="sidebar__link">
                <span className="sidebar__icon">{c.icon}</span>{c.text}
              </Link>
            </li>
          ))}
        </ul>
      </div>

      <div className="sidebar__section" onClick={onClose}>
        <h2 className="sidebar__title">Genres</h2>
        <ul className="sidebar__list">
          {loading
            ? <li className="sidebar__item">Loading…</li>
            : genres.map(g=>(
                <li key={g.id} className="sidebar__item">
                  <Link to={`/genre/${g.id}`} className="sidebar__link">{g.name}</Link>
                </li>
              ))}
        </ul>
      </div>
    </aside>
  );
};

export default Sidebar;
