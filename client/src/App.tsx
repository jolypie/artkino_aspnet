import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import FilmsPage      from './pages/FilmsPage/FilmsPage';
import CurrentFilm    from './pages/CurrentFilmPage/CurrentFilmPage';
import './App.scss';   

const App = () => (
  <Router>
    <Routes>
      <Route path='/'                     element={<FilmsPage type='trending' />} />
      <Route path='/films/popular'        element={<FilmsPage type='popular'  />} />
      <Route path='/films/trending'       element={<FilmsPage type='trending' />} />
      <Route path='/top250'               element={<FilmsPage type='top250'   />} />
      <Route path='/genre/:genreId'       element={<FilmsPage type='genre'    />} />
      <Route path='/film/:id'             element={<CurrentFilm />} />
    </Routes>
  </Router>
);

export default App;
