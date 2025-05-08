import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { AxiosError } from 'axios';
import { useAuth } from '../../context/AuthContext';
import '../LoginPage/loginPage.scss';

const RegisterPage: React.FC = () => {
  const { register } = useAuth();
  const nav = useNavigate();

  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError]       = useState('');

  const submit = async () => {
    if (password.length < 4) {
      setError('Password too short');
      return;
    }
    try {
      await register(username, password);
      nav('/');
    } catch (e: unknown) {
      const err = e as AxiosError<string>;
      setError(err.response?.data ?? 'Registration error');
    }
  };

  return (
    <section className="auth">
      <h1>Register</h1>
      <input
        placeholder="Username"
        value={username}
        onChange={e => setUsername(e.target.value)}
      />
      <input
        type="password"
        placeholder="Password"
        value={password}
        onChange={e => setPassword(e.target.value)}
      />
      {error && <p className="auth__error">{error}</p>}
      <button onClick={submit}>Create account</button>
      <p className="auth__switch">
        Have an account? <Link to="/login">Login</Link>
      </p>
    </section>
  );
};

export default RegisterPage;
