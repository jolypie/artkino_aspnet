import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext';
import FilmsPage     from './pages/FilmsPage/FilmsPage';
import CurrentFilm   from './pages/CurrentFilmPage/CurrentFilmPage';
import SearchPage    from './pages/SearchPage/SearchPage';
import LoginPage     from './pages/LoginPage/LoginPage';
import RegisterPage  from './pages/RegisterPage/RegisterPage';

import './App.scss';
import ProfilePage from './pages/ProfilePage/ProfilePage';
import PlaylistPage from './pages/PlaylistPage/PlaylistPage';

const App = () => (
  <AuthProvider>
    <Router>
      <Routes>
        <Route path="/"                   element={<FilmsPage type="trending" />} />
        <Route path="/films/popular"      element={<FilmsPage type="popular"  />} />
        <Route path="/films/trending"     element={<FilmsPage type="trending" />} />
        <Route path="/top250"             element={<FilmsPage type="top250"   />} />
        <Route path="/genre/:genreId"     element={<FilmsPage type="genre"    />} />
        <Route path="/film/:id"           element={<CurrentFilm />} />
        <Route path="/search"             element={<SearchPage />} />
        <Route path="/login"              element={<LoginPage />} />
        <Route path="/register"           element={<RegisterPage />} />
        <Route path='/profile' element={<ProfilePage/>}/>
        <Route path='/playlist/:id' element={<PlaylistPage />} />
      </Routes>
    </Router>
  </AuthProvider>
);

export default App;
