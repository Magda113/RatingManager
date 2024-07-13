import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getRatingById, addRating, updateRating } from '../services/ratingService';

const RatingForm = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [rating, setRating] = useState({
        callId: '',
        userName: '',
        safety: '',
        knowledge: '',
        communication: '',
        creativity: '',
        technicalAspects: '',
        result: 0,
        categoryName: '',
        status: ''
    });

    useEffect(() => {
        if (id) {
            const fetchRating = async () => {
                try {
                    const data = await getRatingById(id);
                    setRating(data);
                } catch (error) {
                    console.error(error);
                }
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
        try {
            if (id) {
                await updateRating(id, rating);
            } else {
                await addRating(rating);
            }
            navigate('/ratings');
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <div>
            <h2>{id ? 'Edytuj' : 'Dodaj'}</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Numer rozmowy: </label>
                    <input name="callId" value={rating.callId} onChange={handleChange} required />
                </div>
                <div>
                    <label>Użytkownik: </label>
                    <input name="userName" value={rating.userName} onChange={handleChange} required />
                </div>
                <div>
                    <label>Bezpieczeństwo: </label>
                    <input name="safety" value={rating.safety} onChange={handleChange} />
                </div>
                <div>
                    <label>Wiedza: </label>
                    <input name="knowledge" value={rating.knowledge} onChange={handleChange} />
                </div>
                <div>
                    <label>Komunikacja: </label>
                    <input name="communication" value={rating.communication} onChange={handleChange} />
                </div>
                <div>
                    <label>Kreatywność: </label>
                    <input name="creativity" value={rating.creativity} onChange={handleChange} />
                </div>
                <div>
                    <label>Aspekty techniczne: </label>
                    <input name="technicalAspects" value={rating.technicalAspects} onChange={handleChange} />
                </div>
                <div>
                    <label>Wynik: </label>
                    <input type="number" name="result" value={rating.result} onChange={handleChange} required />
                </div>
                <div>
                    <label>Kategoria: </label>
                    <input name="categoryName" value={rating.categoryName} onChange={handleChange} required />
                </div>
                {id && (
                    <div>
                        <label>Status oceny (Robocza/ Opublikowana): </label>
                        <input name="status" value={rating.status} onChange={handleChange} required />
                    </div>
                )}
                <button type="submit">{id ? 'Zapisz' : 'Dodaj'}</button>
            </form>
        </div>
    );
};

export default RatingForm;

