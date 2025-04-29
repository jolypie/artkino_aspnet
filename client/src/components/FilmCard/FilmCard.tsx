import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import PlayCircleOutlineIcon from '@mui/icons-material/PlayCircleOutline';
import StarBorderOutlinedIcon from '@mui/icons-material/StarBorderOutlined';
import { orange } from '@mui/material/colors';
import { Film as FilmType } from '../../types/Film';
import './filmCard.scss';

interface FilmCardProps {
  movie: FilmType;
}

const FilmCard: React.FC<FilmCardProps> = ({ movie }) => {
  const [genres, setGenres] = useState<string>('');

  useEffect(() => {
    axios
      .get<FilmType>(`/api/Tmdb/movie_details/${movie.tmdbId}`)
      .then(r => setGenres(r.data.genres))
      .catch(console.error);
  }, [movie.tmdbId]);

  return (
    <article className="film-card">
      <Link to={`/film/${movie.tmdbId}`} className="film-card__link">
        <div className="film-card__poster-wrapper">
          <img
            className="film-card__poster"
            src={`https://image.tmdb.org/t/p/w300${movie.posterPath}`}
            alt={movie.title}
            loading="lazy"
          />
          <PlayCircleOutlineIcon
            className="film-card__play"
            sx={{ fontSize: 90, color: 'white' }}
          />
        </div>

        <div className="film-card__info">
          <header className="film-card__header">
            <h3 className="film-card__title">{movie.title}</h3>
            <p className="film-card__genres">{genres || 'No genres available'}</p>
          </header>

          <footer className="film-card__meta">
            <StarBorderOutlinedIcon sx={{ color: orange[500], fontSize: 20 }} />
            <span className="film-card__rating">{movie.voteAverage.toFixed(1)}</span>
            <span className="film-card__year">
              {new Date(movie.releaseDate).getFullYear()}
            </span>
          </footer>
        </div>
      </Link>
    </article>
  );
};

export default FilmCard;
