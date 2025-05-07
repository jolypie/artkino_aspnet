import React from 'react';
import './trailer.scss';

const Trailer:React.FC<{youtubeKey:string}> = ({ youtubeKey }) => (
  <div className="trailer">
    <iframe
      src={`https://www.youtube.com/embed/${youtubeKey}`}
      allowFullScreen
      title="Trailer"
    />
  </div>
);

export default Trailer;
