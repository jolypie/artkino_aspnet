import React from 'react';
import { useParams } from 'react-router-dom';
import { useGetFilmDetailsQuery, useGetFilmTrailerQuery } from '../../api/tmdbApi';
import FilmHero from '../../components/FilmHero/ FilmHero';
import Trailer from '../../components/Trailer/Trailer';
import ReviewForm from '../../components/ReviewPlaceholder/ReviewForm';
import FilmInfo from '../../components/FilmInfo/FilmInfo';
import FilmLayout from '../../components/FilmLayout/FilmLayout';


const CurrentFilmPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const filmId = Number(id);

  const { data: film, isLoading } = useGetFilmDetailsQuery(filmId);
  const { data: trailer } = useGetFilmTrailerQuery(filmId);

  if (isLoading || !film) return <p style={{ padding: '5rem' }}></p>;

  return (
    <FilmLayout>
      <FilmHero film={film} />
      <div className="film-page__content">
        <FilmInfo film={film} />
        {trailer?.key && <Trailer youtubeKey={trailer.key} />}
        <ReviewForm />
      </div>
    </FilmLayout>
  );
};

export default CurrentFilmPage;
