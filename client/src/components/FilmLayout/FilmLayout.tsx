import React from 'react';
import Navbar from '../Navbar/Navbar';
import './filmLayout.scss';

const FilmLayout: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  return (
    <div className="film-layout">
      <Navbar />
      <main className="film-layout__content">
        {children}
      </main>
    </div>
  );
};

export default FilmLayout;
