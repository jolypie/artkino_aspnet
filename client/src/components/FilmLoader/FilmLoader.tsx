
import { useGetFilmDetailsQuery } from '../../api/tmdbApi';
import FilmCard from '../FilmCard/FilmCard';

interface Props {
  tmdbId: number;
}

const FilmLoader: React.FC<Props> = ({ tmdbId }) => {
  const { data: film, isLoading } = useGetFilmDetailsQuery(tmdbId);

  if (isLoading || !film) return null;
  return <FilmCard movie={film} />;
};

export default FilmLoader;
