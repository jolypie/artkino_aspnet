import React, { useState } from 'react';
import Navbar from '../Navbar/Navbar';
import Sidebar from '../Sidebar/Sidebar';
import FilmGrid from '../FilmGrid/FilmGrid';
import { Film } from '../../types/Film';
import './mainLayout.scss';

interface MainLayoutProps { films: Film[] }

const MainLayout: React.FC<MainLayoutProps> = ({ films }) => {
  const [sidebarOpen, setSidebarOpen] = useState(false);

  return (
    <div className="main-layout">
      <Navbar onBurgerClick={() => setSidebarOpen(!sidebarOpen)} />
      <div className="main-layout__body">
        <Sidebar open={sidebarOpen} onClose={() => setSidebarOpen(false)} />
        <div className="main-layout__content">
          <FilmGrid films={films} />
        </div>
      </div>
    </div>
  );
};

export default MainLayout;
