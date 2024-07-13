import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { login } from '../services/authService';

// eslint-disable-next-line react/prop-types
const Login = ({ onLogin }) => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await login(username, password);
            onLogin();
            navigate('/');
        } catch (error) {
            console.error('Error: Login failed:', error);
        }
    };

    return (
        <div>
            <h2>Zaloguj</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Użytkownik</label>
                    <input type="text" value={username} onChange={(e) => setUsername(e.target.value)} required />
                </div>
                <div>
                    <label>Hasło</label>
                    <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} required />
                </div>
                <button type="submit">Zaloguj</button>
            </form>
        </div>
    );
};

export default Login;
