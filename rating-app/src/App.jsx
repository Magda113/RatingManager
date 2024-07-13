import { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route, Link, Navigate, useNavigate } from 'react-router-dom';
import RatingList from './components/RatingList';
import RatingForm from './components/RatingForm';
import CategoryList from './components/CategoryList';
import CategoryForm from './components/CategoryForm';
import UserList from './components/UserList';
import UserForm from './components/UserForm';
import Login from './components/Login';
import './App.css';

import { getToken, logout } from './services/authService';

const PrivateComponent = () => {
    return <div className="content">Private Component</div>;
};

// eslint-disable-next-line react/prop-types
const Logout = ({ onLogout }) => {
    const navigate = useNavigate();

    const handleLogout = () => {
        logout();
        onLogout();
        navigate('/login');
    };

    return (
        <div className>
            <h2>Logging out...</h2>
            {handleLogout()}
        </div>
    );
};

const Home = () => {
    return <div >Strona główna</div>;
};

const App = () => {
    const [isLoggedIn, setIsLoggedIn] = useState(!!getToken());

    useEffect(() => {
        setIsLoggedIn(!!getToken());
    }, []);

    const handleLogin = () => {
        setIsLoggedIn(true);
        navigate('/');
    };

    const handleLogout = () => {
        logout();
        setIsLoggedIn(false);
        navigate('/login');
    };

    return (
        <Router>
            <div>
                <nav>
                    <ul>
                        <li>
                            <Link to="/">Strona główna</Link>
                        </li>
                        <li>
                            <Link to="/ratings">Oceny</Link>
                        </li>
                        <li>
                            <Link to="/categories">Kategorie</Link>
                        </li>
                        <li>
                            <Link to="/users">Użytkownicy</Link>
                        </li>
                        <li>
                            {isLoggedIn ? (
                                <Link to="/logout" onClick={handleLogout}>
                                    Wyloguj
                                </Link>
                            ) : (
                                <Link to="/login">Zaloguj</Link>
                            )}
                        </li>
                    </ul>
                </nav>
                    <Routes>
                        <Route path="/" element={<Home/>}/>
                        <Route path="/ratings" element={<RatingList/>}/>
                        <Route path="/ratings/new" element={<RatingForm/>}/>
                        <Route path="/ratings/edit/:id" element={<RatingForm/>}/>
                        <Route path="/categories" element={<CategoryList/>}/>
                        <Route path="/categories/new" element={<CategoryForm/>}/>
                        <Route path="/categories/edit/:id" element={<CategoryForm/>}/>
                        <Route path="/users" element={<UserList/>}/>
                        <Route path="/users/new" element={<UserForm/>}/>
                        <Route path="/users/edit/:id" element={<UserForm/>}/>
                        <Route path="/login" element={<Login onLogin={handleLogin}/>}/>
                        {isLoggedIn ? (
                            <Route path="/private" element={<PrivateComponent/>}/>
                        ) : (
                            <Route path="/login" element={<Login onLogin={handleLogin}/>}/>
                        )}
                        <Route path="/logout" element={<Logout onLogout={handleLogout}/>}/>
                        <Route path="/login" element={<Navigate to="/login" replace/>}/>
                    </Routes>
                </div>
        </Router>
);
};

export default App;
