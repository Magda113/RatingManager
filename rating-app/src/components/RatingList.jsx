import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { getAllRatings, deleteRating, searchRatingsByCategory, searchRatingsByUser } from '../services/ratingService';

const RatingList = () => {
    const [ratings, setRatings] = useState([]);
    const [error, setError] = useState(null);
    const [categoryName, setCategoryName] = useState('');
    const [userId, setUserId] = useState('');

    useEffect(() => {
        fetchRatings();
    }, []);

    const fetchRatings = async () => {
        try {
            const data = await getAllRatings();
            setRatings(data);
        } catch (error) {
            console.error(error);
            setError('Coś poszło nie tak');
        }
    };

    const handleDelete = async (id) => {
        try {
            await deleteRating(id);
            setRatings(ratings.filter(rating => rating.ratingId !== id));
        } catch (error) {
            console.error(error);
            setError('Coś poszło nie tak');
        }
    };
    const handleCategorySearch = async () => {
        try {
            const data = await searchRatingsByCategory(categoryName);
            setRatings(data);
        } catch (error) {
            console.error(error);
            setError('Błąd podczas wyszukiwania ocen');
        }
    };

    const handleUserSearch = async () => {
        try {
            const data = await searchRatingsByUser(userId);
            setRatings(data);
        } catch (error) {
            console.error(error);
            setError('Błąd podczas wyszukiwania ocen');
        }
    };
    return (
        <div>
            <h2>Oceny</h2>
            <Link to="/ratings/new">
                <button>Dodaj nową ocenę</button>
            </Link>
            <div>
                <input type="text" value={categoryName} onChange={(e) => setCategoryName(e.target.value)}
                       placeholder="Wyszukaj po kategorii"/>
                <button onClick={handleCategorySearch}>Szukaj po kategorii</button>
            </div>
            <div>
                <input type="text" value={userId} onChange={(e) => setUserId(e.target.value)}
                       placeholder="Wyszukaj po użytkowniku"/>
                <button onClick={handleUserSearch}>Szukaj po użytkowniku</button>
            </div>


            <ul>
                {ratings.map(rating => (
                    <li key={rating.ratingId}>
                        <div><strong>ID: </strong> {rating.ratingId}</div>
                        <div><strong>Numer oceny: </strong> {rating.callId}</div>
                        <div><strong>Użytkownik: </strong> {rating.userName}</div>
                        <div><strong>Kategoria: </strong> {rating.categoryName}</div>
                        <div><strong>Bezpieczeństwo: </strong> {rating.safety}</div>
                        <div><strong>Wiedza: </strong> {rating.knowledge}</div>
                        <div><strong>Komunikacja: </strong> {rating.communication}</div>
                        <div><strong>Kreatywność: </strong> {rating.creativity}</div>
                        <div><strong>Aspekty techniczne: </strong> {rating.technicalAspects}</div>
                        <div><strong>Wynik: </strong> {rating.result}</div>
                        <div><strong>Status oceny: </strong> {rating.status}</div>
                        <div><strong>Utworzona przez: </strong> {rating.createdByUserName}</div>
                        <div><strong>Data utworzenia: </strong> {rating.createdAt}</div>
                        <div><strong>Zmodyfikowana przez: </strong> {rating.modifiedByUserName || 'N/A'}</div>
                        <div><strong>Data modyfikacji: </strong> {rating.modifiedAt || 'N/A'}</div>

                        <button onClick={() => handleDelete(rating.ratingId)}>Usuń</button>
                        <Link to={`/ratings/edit/${rating.ratingId}`}>Edytuj</Link>
                    </li>
                ))}
            </ul>
            {error && <p>{error}</p>}
        </div>
    );
};

export default RatingList;
