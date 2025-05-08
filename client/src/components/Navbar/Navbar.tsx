import { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Avatar } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import SearchOutlinedIcon from '@mui/icons-material/SearchOutlined';
import { deepPurple } from '@mui/material/colors';
import { useAuth } from '../../context/AuthContext';
import './navbar.scss';

interface Props { onBurgerClick?: () => void }

const Navbar: React.FC<Props> = ({ onBurgerClick }) => {
  const { user, logout }   = useAuth();
  const nav                = useNavigate();
  const [q, setQ]          = useState('');
  const [menuOpen, setOpen]= useState(false);

  const doSearch = () => { if (q.trim()) nav(`/search?query=${q.trim()}`); };

    useEffect(() => {
    const close = () => setOpen(false);
    document.addEventListener('click', close);
    return () => document.removeEventListener('click', close);
    }, []);

  const stop = (e: React.MouseEvent) => e.stopPropagation();


  const username  = user?.username || localStorage.getItem('username') || '';
  const avatarCh  = username ? username[0].toUpperCase() : '?';

  return (
    <header className="navbar" onClick={stop}>
      <button className="navbar__burger" onClick={onBurgerClick}><MenuIcon/></button>

      <Link to="/" className="navbar__logo">ARTKINO</Link>

      <div className="navbar__search">
        <input
          className="navbar__search-input"
          placeholder="Search…"
          value={q}
          onChange={e => setQ(e.target.value)}
          onKeyDown={e => e.key === 'Enter' && doSearch()}
        />
        <SearchOutlinedIcon className="navbar__search-icon" onClick={doSearch}/>
      </div>

      {user ? (
        <div className="navbar__user">
          <button className="navbar__user-btn" onClick={()=>setOpen(o=>!o)}>
            <span className="navbar__username">{username}</span>
            <Avatar sx={{ bgcolor: deepPurple[500] }} className="navbar__avatar">
              {avatarCh}
            </Avatar>
          </button>

          <nav className={`navbar__menu ${menuOpen ? 'navbar__menu--open' : ''}`} onClick={stop}>
            <Link   to="/profile"  className="navbar__menu-item">My Profile</Link>
            <Link   to="/settings" className="navbar__menu-item">Settings</Link>
            <button onClick={logout} className="navbar__menu-item">Log Out</button>
          </nav>
        </div>
      ) : (
        <Link to="/login" className="navbar__signin">SIGN IN</Link>
      )}
    </header>
  );
};

export default Navbar;
