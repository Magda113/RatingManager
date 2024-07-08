import React, { useState, useEffect } from 'react';
import { useParams, useHistory } from 'react-router-dom';
import { getRatingById, addRating, updateRating } from '../services/ratingService';

const RatingForm = () => {
    const { id } = useParams();
    const history = useHistory();
    const [rating, setRating] = useState({
        callId: '',
        userId: '',
        safety: '',
        knowledge: '',
        communication: '',
        creativity: '',
        technicalAspects: '',
        result: '',
        categoryId: ''
    });

    useEffect(() => {
        if (id) {
            const fetchRating = async () => {
                const data = await getRatingById(id);
                setRating(data);
            };

            fetchRating();
        }
    }, [id]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setRating({ ...rating, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (id) {
            await updateRating(id, rating);
        } else {
            await addRating(rating);
        }
        history.push('/ratings');
    };

    return (
        <div>
            <h2>{id ? 'Edytuj Ocenę' : 'Dodaj Ocenę'}</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Numer rozmowy</label>
                    <input name="callId" value={rating.callId} onChange={handleChange} required />
                </div>
                <div>
                    <label>Użytkownik</label>
                    <input name="userId" value={rating.userId} onChange={handleChange} required />
                </div>
                <div>
                    <label>Bezpieczeństwo</label>
                    <textarea name="safety" value={rating.safety} onChange={handleChange}></textarea>
                </div>
                <div>
                    <label>Wiedza</label>
                    <textarea name="knowledge" value={rating.knowledge} onChange={handleChange}></textarea>
                </div>
                <div>
                    <label>Komunikacja</label>
                    <textarea name="communication" value={rating.communication} onChange={handleChange}></textarea>
                </div>
                <div>
                    <label>Kreatywność</label>
                    <textarea name="creativity" value={rating.creativity} onChange={handleChange}></textarea>
                </div>
                <div>
                    <label>Aspekty techniczne</label>
                    <textarea name="technicalAspects" value={rating.technicalAspects} onChange={handleChange}></textarea>
                </div>
                <div>
                    <label>Wynik</label>
                    <input name="result" value={rating.result} onChange={handleChange} required />
                </div>
                <div>
                    <label>Kategoria</label>
                    <input name="categoryId" value={rating.categoryId} onChange={handleChange} required />
                </div>
                <button type="submit">{id ? 'Zapisz' : 'Dodaj'}</button>
            </form>
        </div>
    );
};

export default RatingForm;
