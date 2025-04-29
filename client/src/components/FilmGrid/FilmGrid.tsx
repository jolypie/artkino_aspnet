import React from 'react';
import { Film as FilmType } from '../../types/Film';
import FilmCard from '../FilmCard/FilmCard';
import './filmGrid.scss';

interface FilmGridProps {
  films: FilmType[];
}

const FilmGrid: React.FC<FilmGridProps> = ({ films }) => {
  return (
    <section className="film-grid">
      {films.length > 0 ? (
        films.map((film) => (
          <FilmCard key={film.tmdbId} movie={film} />
        ))
      ) : (
        <p className="film-grid__empty">No films found.</p>
      )}
    </section>
  );
};

export default FilmGrid;
