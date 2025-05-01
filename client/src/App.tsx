import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import FilmsPage from './pages/FilmsPage/FilmsPage';
import Sidebar from './components/Sidebar/Sidebar';
import Navbar from './components/Navbar/Navbar';
import FilmGrid from './components/FilmGrid/FilmGrid';
import { useEffect, useState } from 'react';
import api from './api/axiosInstance';
import { Film } from './types/Film';
import './App.scss';

function App() {

  const [popularFilms, setPopularFilms] = useState<Film[]>([]);

  useEffect(() => {
    api.get<Film[]>('/Tmdb/popular')
      .then(r => setPopularFilms(r.data))
      .catch(console.error);
  }, []);

  return (
    <Router>
      <div>
          {/* <nav style={{ marginBottom: '20px' }}>
            <Link to="/films/popular">
              <button>Films Layout</button>
            </Link>
            <Link to="/sidebar-test" style={{ marginLeft: '10px' }}>
              <button>Sidebar test</button>
            </Link>
            <Link to="/navbar-test" style={{ marginLeft: '10px' }}>
              <button>Navbar test</button>
            </Link>
            <Link to="/filmgrid-test" style={{ marginLeft: '10px' }}>
              <button>FilmGrid test</button>
            </Link>
          </nav> */}

        <Routes>
          <Route path="/" element={
            <div>

              <div>hello world!!</div>
            </div>
          } />
          <Route path="/films/:category" element={<FilmsPage />} />
          <Route path="/sidebar-test" element={<Sidebar />} />
          <Route path="/navbar-test" element={<Navbar />} />
          <Route path="/filmgrid-test" element={<FilmGrid films={popularFilms}/>} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
