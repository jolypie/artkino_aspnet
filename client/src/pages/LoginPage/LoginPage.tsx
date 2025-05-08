import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import './loginPage.scss';

const LoginPage: React.FC = () => {
  const { login } = useAuth();
  const nav = useNavigate();
  const [u, setU] = useState('');
  const [p, setP] = useState('');
  const [err, setErr] = useState('');

  const submit = async () => {
    try {
      await login(u, p);
      nav('/');
    } catch {
      setErr('Invalid credentials');
    }
  };

  return (
    <section className="auth">
      <h1>Sign in</h1>
      <input placeholder="Username" value={u} onChange={e => setU(e.target.value)} />
      <input type="password" placeholder="Password" value={p} onChange={e => setP(e.target.value)} />
      {err && <p className="auth__error">{err}</p>}
      <button onClick={submit}>Login</button>
      <p className="auth__switch">No account? <Link to="/register">Register</Link></p>
    </section>
  );
};

export default LoginPage;
