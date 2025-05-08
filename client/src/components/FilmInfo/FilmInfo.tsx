import React from 'react';
import './filmInfo.scss';
import { Film } from '../../types/Film';

interface Props { film: Film }

const FilmInfo: React.FC<Props> = ({ film }) => (
  <section className="info">
    <p className="info__overview">{film.description}</p>

    <div className="info__grid">
      <div className="info__field">
        <span className="info__label">Title</span>
        <span>{film.title}</span>
      </div>

      <div className="info__field">
        <span className="info__label">Year</span>
        <span>{new Date(film.releaseDate).getFullYear()}</span>
      </div>

      <div className="info__field">
        <span className="info__label">Rating</span>
        <span>{film.voteAverage}</span>
      </div>

      <div className="info__field">
        <span className="info__label">Countries</span>
        <span>{film.countries}</span>
      </div>

      <div className="info__field">
        <span className="info__label">Genres</span>
        <p>{film.genres.map(g => g.name).join(', ')}</p>
      </div>

      <div className="info__field">
        <span className="info__label">Director</span>
        <span>{film.director}</span>
      </div>

      <div className="info__field info__field--full">
        <span className="info__label">Cast</span>
        <span>{film.cast}</span>
      </div>
    </div>
  </section>
);

export default FilmInfo;
