import { createContext, useContext, useEffect, useState } from 'react';
import api from '../api/axiosInstance';

export interface User { id: number; username: string; role: string }

interface AuthCtx {
  user: User | null;
  login: (u: string, p: string) => Promise<void>;
  register: (u: string, p: string) => Promise<void>;
  logout: () => void;
}

const Ctx = createContext<AuthCtx>(null!);
export const useAuth = () => useContext(Ctx);

function parseJwt(token: string) {
  const payload = token.split('.')[1];
  return JSON.parse(atob(payload));
}

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [user, setUser] = useState<User | null>(null);

  const logout = () => {
    ['token', 'username', 'role', 'userId'].forEach(k => localStorage.removeItem(k));
    setUser(null);
  };

  const refresh = async () => {
    try {
      const { data } = await api.post('/auth/refresh', {}, { withCredentials: true });
      const token = data.accessToken;
      localStorage.setItem('token', token);

      const payload = parseJwt(token);
      const id = +payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
      const username = payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
      const role = payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

      setUser({ id, username, role });
    } catch {
      logout();
    }
  };

  useEffect(() => {
    if (localStorage.getItem('token')) {
      refresh();
    }
  }, []);

  const login = async (username: string, password: string) => {
    const { data } = await api.post('/auth/login', { username, password }, { withCredentials: true });
    const token = data.accessToken ?? data.token;
    localStorage.setItem('token', token);

    const payload = parseJwt(token);
    const id = +payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
    const role = payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

    setUser({ id, username: data.user.username, role });
  };

  const register = async (u: string, p: string) => {
    await api.post('/auth/register', { username: u, password: p });
    await login(u, p);
  };

  return (
    <Ctx.Provider value={{ user, login, register, logout }}>
      {children}
    </Ctx.Provider>
  );
};
