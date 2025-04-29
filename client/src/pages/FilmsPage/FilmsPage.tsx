import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import MainLayout from '../../components/MainLayout/MainLayout';
import api from '../../api/axiosInstance';
import { Film } from '../../types/Film';

const FilmsPage: React.FC = () => {
  const { category } = useParams<{ category: string }>();
  const [films, setFilms] = useState<Film[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchFilms = async () => {
      try {
        let endpoint = '';

        if (category === 'popular') {
          endpoint = '/Tmdb/popular';
        } else {
          console.error('Unknown category:', category);
          return;
        }

        const response = await api.get<Film[]>(endpoint);
        setFilms(response.data);
      } catch (error) {
        console.error('Error fetching films:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchFilms();
  }, [category]);

  if (loading) return <div>Loading...</div>;

  return <MainLayout films={films} />;
};

export default FilmsPage;
