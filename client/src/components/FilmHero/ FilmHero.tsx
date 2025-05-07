import React from 'react';
import StarBorder from '@mui/icons-material/StarBorder';
import './filmHero.scss';
import { Film } from '../../types/Film';

interface Props { film: Film }

const FilmHero: React.FC<Props> = ({ film }) => (
  <section className="hero" style={{backgroundImage:`url(https://image.tmdb.org/t/p/original${film.posterPath})`}}>
    <div className="hero__overlay" />
    <div className="hero__inner">
      <h1 className="hero__title">{film.title}</h1>

      <div className="hero__rating">
        <StarBorder/> <span>{film.voteAverage.toFixed(1)}</span>
        <span className="hero__year">{new Date(film.releaseDate).getFullYear()}</span>
      </div>
    </div>
  </section>
);

export default FilmHero;
    