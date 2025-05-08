import React from 'react';
import StarBorderIcon from '@mui/icons-material/StarBorder';
import './filmHero.scss';
import { Film } from '../../types/Film';
import PlaylistButtons from '../PlaylistButtons/PlaylistButtons';

interface Props {
  film: Film;
}

const FilmHero: React.FC<Props> = ({ film }) => {
  return (
    <section
      className="hero"
      style={{ backgroundImage: `url(https://image.tmdb.org/t/p/original${film.posterPath})` }}
    >
      <div className="hero__overlay" />
      <div className="hero__inner">
        <h1 className="hero__title">{film.title}</h1>

        <div className="hero__rating">
          <StarBorderIcon />
          <span>{film.voteAverage.toFixed(1)}</span>
          <span className="hero__year">{new Date(film.releaseDate).getFullYear()}</span>
        </div>

        <PlaylistButtons filmId={film.tmdbId} />
      </div>
    </section>
  );
};

export default FilmHero;
