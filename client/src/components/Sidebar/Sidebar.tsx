import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import TheatersOutlinedIcon from '@mui/icons-material/TheatersOutlined';
import SlideshowIcon from '@mui/icons-material/Slideshow';
import LocalActivityOutlinedIcon from '@mui/icons-material/LocalActivityOutlined';
import api from '../../api/axiosInstance';
import './sidebar.scss';

interface Genre {
  id: number;
  name: string;
}

const Sidebar: React.FC = () => {
  const [genres, setGenres] = useState<Genre[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    api
      .get<{ genres: Genre[] }>('/Tmdb/genres')
      .then(r => setGenres(r.data.genres))
      .catch(err => console.error('Error fetching genres:', err))
      .finally(() => setLoading(false));
  }, []);

  const categories = [
    { text: 'Films',        icon: <TheatersOutlinedIcon />,       link: '/films'   },
    { text: 'Popular now',  icon: <SlideshowIcon />,              link: '/films/popular' },
    { text: 'Top 250',      icon: <LocalActivityOutlinedIcon />,  link: '/top250'  },
  ];

  return (
    <aside className="sidebar">
      {/* Categories */}
      <nav className="sidebar__section">
        <h2 className="sidebar__title">Categories</h2>
        <ul className="sidebar__list">
          {categories.map(({ text, icon, link }) => (
            <li key={link} className="sidebar__item">
              <Link to={link} className="sidebar__link">
                <span className="sidebar__icon">{icon}</span>
                {text}
              </Link>
            </li>
          ))}
        </ul>
      </nav>

      {/* Genres */}
      <nav className="sidebar__section">
        <h2 className="sidebar__title">Genres</h2>
        <ul className="sidebar__list">
          {loading
            ? <li className="sidebar__item"></li>
            : genres.map(g => (
                <li key={g.id} className="sidebar__item">
                  <Link to={`/genre/${g.id}`} className="sidebar__link">
                    {g.name}
                  </Link>
                </li>
              ))}
        </ul>
      </nav>
    </aside>
  );
};

export default Sidebar;
