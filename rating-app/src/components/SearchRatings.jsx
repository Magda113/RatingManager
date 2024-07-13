import React, { useState } from 'react';
import { getToken } from '../services/authService';
import { searchRatingsByCategory, searchRatingsByUser, getAllRatings } from '../services/ratingService';

const SearchRatings = () => {
    const [categoryName, setCategoryName] = useState('');
    const [userName, setUserName] = useState('');
    const [ratings, setRatings] = useState([]);
    const [error, setError] = useState(null);

    const handleCategorySearch = async () => {
        try {
            const token = getToken();
            const data = await searchRatingsByCategory(categoryName, token);
            setRatings(data);
        } catch (error) {
            console.error(error);
            setError('Błąd podczas wyszukiwania ocen po kategorii');
        }
    };

    const handleUserSearch = async () => {
        try {
            const token = getToken();
            const data = await searchRatingsByUser(userName, token);
            setRatings(data);
        } catch (error) {
            console.error(error);
            setError('Błąd podczas wyszukiwania ocen po użytkowniku');
        }
    };

    const handleGetAllRatings = async () => {
        try {
            const token = getToken();
            const data = await getAllRatings(token);
            setRatings(data);
        } catch (error) {
            console.error(error);
            setError('Błąd podczas pobierania wszystkich ocen');
        }
    };

    return (
        <div>
            <h2>Wyszukaj Oceny</h2>
            <div>
                <label>Nazwa Kategorii:</label>
                <input type="text" value={categoryName} onChange={(e) => setCategoryName(e.target.value)} />
                <button onClick={handleCategorySearch}>Wyszukaj po kategorii</button>
            </div>
            <div>
                <label>Nazwa Użytkownika:</label>
                <input type="text" value={userName} onChange={(e) => setUserName(e.target.value)} />
                <button onClick={handleUserSearch}>Wyszukaj po użytkowniku</button>
            </div>

            {error && <div>{error}</div>}

            <ul>
                {ratings.map((rating) => (
                    <li key={rating.ratingId}>
                    </li>
                ))}
            </ul>

            <div>
                <Link to="/ratings/new">
                    <button>Dodaj nową ocenę</button>
                </Link>
            </div>

            <button onClick={handleGetAllRatings}>Pobierz wszystkie oceny</button>
        </div>
    );
};

export default SearchRatings;