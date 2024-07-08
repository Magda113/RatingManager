import React, { useState, useEffect } from 'react';
import { useParams, Link, useHistory } from 'react-router-dom';
import { getRatingById, deleteRating } from '../services/ratingService';

const RatingDetail = () => {
    const { id } = useParams();
    const history = useHistory();
    const [rating, setRating] = useState(null);

    useEffect(() => {
        const fetchRating = async () => {
            const data = await getRatingById(id);
            setRating(data);
        };

        fetchRating();
    }, [id]);

    const handleDelete = async () => {
        await deleteRating(id);
        history.push('/ratings');
    };

    if (!rating) return <div>Loading...</div>;

    return (
        <div>
            <h2>Ocena: {rating.callId}</h2>
            <p>Użytkownik: {rating.userId}</p>
            <p>Wynik: {rating.result}</p>
            {/* Dodaj pozostałe pola oceny */}
            <button onClick={handleDelete}>Usuń</button>
            <Link to={`/ratings/edit/${rating.ratingId}`}>Edytuj</Link>
        </div>
    );
};

export default RatingDetail;
