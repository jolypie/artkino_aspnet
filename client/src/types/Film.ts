export interface Film {
    tmdbId: number;
    title: string;
    releaseDate: string;
    posterPath: string;
    voteAverage: number;
    countries: string;
    genres: string;
    director: string;
    cast: string;
    description: string;
    genre_ids?: number[];
  }
  