import { useSearchParams } from 'react-router-dom';
import MainLayout from '../../components/MainLayout/MainLayout';
import { useSearchMoviesQuery } from '../../api/tmdbApi';
import { Film } from '../../types/Film';

const SearchPage: React.FC = () => {
  const [params] = useSearchParams();
  const term     = params.get('query') ?? '';
  const page     = Number(params.get('page') ?? 1);

  const {
    data : films = [] as Film[],
    isLoading
  } = useSearchMoviesQuery(
        { query: term, page },
        { skip: !term.trim() }
      );

  if (!term.trim())       return <p style={{padding:'5rem'}}>Enter a query</p>;
  if (isLoading)          return <p style={{padding:'5rem'}}>Loading…</p>;
  if (films.length === 0) return <p style={{padding:'5rem'}}>Nothing found</p>;

  return <MainLayout films={films} />;
};

export default SearchPage;
