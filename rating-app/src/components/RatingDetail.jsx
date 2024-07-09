import React, { useState, useEffect } from 'react';
import { useParams, useNavigate, Link } from 'react-router-dom';
import { getRatingById, deleteRating } from '../services/ratingService';

const RatingDetail = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [rating, setRating] = useState(null);

    useEffect(() => {
        const fetchRating = async () => {
            try {
                const data = await getRatingById(id);
                setRating(data);
            } catch (error) {
                console.error('Error fetching rating:', error);
            }
        };

        fetchRating();
    }, [id]);

    const handleDelete = async () => {
        await deleteRating(id);
        navigate('/ratings');
    };

    if (!rating) {
        return <div>Loading...</div>;
    }

    return (
        <div>
            <h2>Szczegóły Oceny</h2>
            <p><strong>ID: </strong> {rating.ratingId}</p>
            <p><strong>Numer rozmowy: </strong> {rating.callId}</p>
            <p><strong>Użytkownik: </strong> {rating.userName}</p>
            <p><strong>Kategoria: </strong> {rating.categoryName}</p>
            <p><strong>Bezpieczeństwo: </strong> {rating.safety}</p>
            <p><strong>Wiedza: </strong> {rating.knowledge}</p>
            <p><strong>Komunikacja: </strong> {rating.communication}</p>
            <p><strong>Kreatywność: </strong> {rating.creativity}</p>
            <p><strong>Aspekty techniczne: </strong> {rating.technicalAspects}</p>
            <p><strong>Wynik: </strong> {rating.result}</p>
            <p><strong>Status: </strong> {rating.status}</p>
            <p><strong>Utworzona przez: </strong> {rating.createdByUserName}</p>
            <p><strong>Data utworzenia: </strong> {rating.createdAt}</p>
            <p><strong>Zaktualizowana przez: </strong> {rating.modifiedByUserName || 'N/A'}</p>
            <p><strong>Data aktualizacji: </strong> {rating.modifiedAt || 'N/A'}</p>
            <button onClick={handleDelete}>Usuń</button>
            <Link to={`/ratings/edit/${rating.ratingId}`}>Edytuj</Link>
        </div>
    );
};

export default RatingDetail;

