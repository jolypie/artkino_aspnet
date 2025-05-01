import React, { useEffect, useState } from 'react';
import { Link, useMatch, useResolvedPath, useNavigate } from 'react-router-dom';
import { Avatar } from '@mui/material';
import SearchOutlinedIcon from '@mui/icons-material/SearchOutlined';
import { deepPurple } from '@mui/material/colors';
import './navbar.scss';

interface NavbarProps {
  className?: string;
}

const Navbar: React.FC<NavbarProps> = ({ className }) => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const [searchQuery, setSearchQuery] = useState('');
  const [username, setUsername] = useState('');
  const navigate = useNavigate();

  const isUserLoggedIn = !!localStorage.getItem('token');

  useEffect(() => { 
    const storedUsername = localStorage.getItem('username') || 'Guest';
    setUsername(storedUsername);
  }, []);

  const handleSearch = () => {
    if (searchQuery.trim()) {
      navigate(`/search?query=${searchQuery.trim()}`);
      setSearchQuery('');
    }
  };

  const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === 'Enter') {
      handleSearch();
    }
  };

  const handleLogout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('username');
    navigate('/login');
    window.location.reload();
  };

  const firstLetter = username.charAt(0).toUpperCase();

  return (
    <header className="navbar">
      <Link to="/" className="navbar__logo">ARTKINO</Link>

      <div className="navbar__search">
        <input
          type="text"
          value={searchQuery}
          onChange={(e) => setSearchQuery(e.target.value)}
          onKeyDown={handleKeyDown}
          placeholder="Search..."
          className="navbar__search-input"
        />
        <SearchOutlinedIcon onClick={handleSearch} className="navbar__search-icon" />
      </div>

      {isUserLoggedIn ? (
        <div 
          className="navbar__user" 
          onMouseEnter={() => setIsMenuOpen(true)} 
          onMouseLeave={() => setIsMenuOpen(false)}
        >
          <span className="navbar__username">{username}</span>
          <Avatar sx={{ bgcolor: deepPurple[500] }} className="navbar__avatar">
            {firstLetter}
          </Avatar>
          {isMenuOpen && (
            <div className="navbar__menu">
              <Link to="/profile" className="navbar__menu-item">My Profile</Link>
              <Link to="/settings" className="navbar__menu-item">Settings</Link>
              <button onClick={handleLogout} className="navbar__menu-item">Log Out</button>
            </div>
          )}
        </div>
      ) : (
        <CustomLink to="/login" className="navbar__signin">SIGN IN</CustomLink>
      )}
    </header>
  );
};

interface CustomLinkProps {
  to: string;
  children: React.ReactNode;
  className?: string;
}

const CustomLink: React.FC<CustomLinkProps> = ({ to, children, className }) => {
  const resolvedPath = useResolvedPath(to);
  const isActive = useMatch({ path: resolvedPath.pathname, end: true });

  return (
    <Link to={to} className={`${className} ${isActive ? 'active' : ''}`}>
      {children}
    </Link>
  );
};

export default Navbar;