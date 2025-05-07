import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Avatar } from '@mui/material';
import SearchOutlinedIcon from '@mui/icons-material/SearchOutlined';
import MenuIcon from '@mui/icons-material/Menu';
import { deepPurple } from '@mui/material/colors';
import './navbar.scss';

interface NavbarProps {
  onBurgerClick?: () => void;
}

const Navbar: React.FC<NavbarProps> = ({ onBurgerClick }) => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const [searchQuery, setSearchQuery] = useState('');
  const [username, setUsername] = useState('');
  const navigate = useNavigate();
  const isUserLoggedIn = !!localStorage.getItem('token');

  useEffect(() => {
    setUsername(localStorage.getItem('username') || 'Guest');
  }, []);

  const handleSearch = () => {
    if (searchQuery.trim()) {
      navigate(`/search?query=${searchQuery.trim()}`);
      setSearchQuery('');
    }
  };
  const handleKey = (e: React.KeyboardEvent<HTMLInputElement>) => e.key === 'Enter' && handleSearch();
  const handleLogout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('username');
    navigate('/login');
    window.location.reload();
  };

  const firstLetter = username.charAt(0).toUpperCase();

  return (
    <header className="navbar">
      <button className="navbar__burger" onClick={onBurgerClick}>
        <MenuIcon />
      </button>

      <Link to="/" className="navbar__logo">ARTKINO</Link>

      <div className="navbar__search">
        <input
          className="navbar__search-input"
          placeholder="Search…"
          value={searchQuery}
          onChange={e => setSearchQuery(e.target.value)}
          onKeyDown={handleKey}
        />
        <SearchOutlinedIcon className="navbar__search-icon" onClick={handleSearch} />
      </div>

      {isUserLoggedIn ? (
        <div
          className="navbar__user"
          onMouseEnter={() => setIsMenuOpen(true)}
          onMouseLeave={() => setIsMenuOpen(false)}
        >
          <span className="navbar__username">{username}</span>
          <Avatar sx={{ bgcolor: deepPurple[500] }} className="navbar__avatar">{firstLetter}</Avatar>

          {isMenuOpen && (
            <div className="navbar__menu">
              <Link to="/profile"   className="navbar__menu-item">My Profile</Link>
              <Link to="/settings"  className="navbar__menu-item">Settings</Link>
              <button onClick={handleLogout} className="navbar__menu-item">Log Out</button>
            </div>
          )}
        </div>
      ) : (
        <Link to="/login" className="navbar__signin">SIGN IN</Link>
      )}
    </header>
  );
};

export default Navbar;
