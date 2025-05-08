import axios from 'axios';

const api = axios.create({
  baseURL: '/api',
  withCredentials: true
});

api.interceptors.request.use(cfg => {
  const token = localStorage.getItem('token');
  if (token) cfg.headers.Authorization = `Bearer ${token}`;
  return cfg;
});

let isRefreshing = false;
let queue: ((t: string | null) => void)[] = [];

api.interceptors.response.use(
  r => r,
  async err => {
    const original = err.config;
    if (err.response?.status !== 401 || original._retry) throw err;

    if (isRefreshing) {
      const t = await new Promise<string | null>(res => queue.push(res));
      if (t) {
        original.headers.Authorization = `Bearer ${t}`;
        return api(original);
      }
      throw err;
    }

    original._retry = true;
    isRefreshing = true;
    try {
      const { data } = await api.post<{ accessToken: string }>('/Auth/refresh');
      localStorage.setItem('token', data.accessToken);
      queue.forEach(cb => cb(data.accessToken));
      queue = [];
      original.headers.Authorization = `Bearer ${data.accessToken}`;
      return api(original);
    } catch {
      queue.forEach(cb => cb(null));
      queue = [];
      localStorage.removeItem('token');
      throw err;
    } finally {
      isRefreshing = false;
    }
  }
);

export default api;
