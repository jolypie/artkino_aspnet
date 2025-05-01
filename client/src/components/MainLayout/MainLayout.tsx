import React from 'react';
import Navbar from '../Navbar/Navbar';
import Sidebar from '../Sidebar/Sidebar';
import FilmGrid from '../FilmGrid/FilmGrid';
import { Film } from '../../types/Film';
import './mainLayout.scss';

interface MainLayoutProps {
  films: Film[];
}

const MainLayout: React.FC<MainLayoutProps> = ({ films }) => {
  return (
    <div className="main-layout">
      <Navbar className="main-layout__header" />
      <div className="main-layout__body">
        <Sidebar />
        <div className="main-layout__content">
          <FilmGrid films={films} />
        </div>
      </div>
    </div>
  );
};

export default MainLayout;
