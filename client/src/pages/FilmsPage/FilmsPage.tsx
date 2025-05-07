import { skipToken } from '@reduxjs/toolkit/query';
import { useParams, useSearchParams } from 'react-router-dom';
import MainLayout from '../../components/MainLayout/MainLayout';
import {
  useListCategoryQuery,
  useListGenreQuery
} from '../../api/tmdbApi';
import { Film } from '../../types/Film';

type PageType = 'popular' | 'trending' | 'top250' | 'genre';

interface FilmsPageProps {
  type: PageType;
}

const FilmsPage: React.FC<FilmsPageProps> = ({ type }) => {
  const { genreId } = useParams<{ genreId: string }>();
  const [search]    = useSearchParams();
  const page        = Number(search.get('page') ?? 1);

  const {
    data: categoryFilms = [],
    isLoading: categoryLoading
  } = useListCategoryQuery(
        type === 'genre'
          ? skipToken
          : { type, page }
      );

  const {
    data: genreFilms = [],
    isLoading: genreLoading
  } = useListGenreQuery(
        type !== 'genre' || !genreId
          ? skipToken
          : { genreId: Number(genreId), page }
      );

  const films      : Film[] = type === 'genre' ? genreFilms    : categoryFilms;
  const isLoading  : boolean = type === 'genre' ? genreLoading : categoryLoading;

  if (isLoading)          return <p style={{ padding:'5rem' }}>Loading…</p>;
  if (films.length === 0) return <p style={{ padding:'5rem' }}>No films found.</p>;

  return <MainLayout films={films} />;
};

export default FilmsPage;
