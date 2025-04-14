import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import axios from 'axios';
import { setUsers } from './features/users/usersSlice';
import { RootState } from './app/store';

function App() {
  const users = useSelector((state: RootState) => state.users.list);
  const dispatch = useDispatch();

  useEffect(() => {
    axios.get('/api/User')
      .then(res => dispatch(setUsers(res.data)))
      .catch(err => console.error(err));
  }, [dispatch]);

  return (
    <div>
      <h1>Users:</h1>
      <ul>
        {users.map(user => <li key={user.id}>{user.username}</li>)}
      </ul>
        
        <div>hello world!!</div>
    </div>
  );
}

export default App;
