import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { getAllRatings } from '../services/ratingService';

const RatingList = () => {
    const [ratings, setRatings] = useState([]);

    useEffect(() => {
        const fetchRatings = async () => {
            const data = await getAllRatings();
            setRatings(data);
        };

        fetchRatings();
    }, []);

    return (
        <div>
            <h2>Lista Ocen</h2>
            <ul>
                {ratings.map(rating => (
                    <li key={rating.ratingId}>
                        <Link to={`/ratings/${rating.ratingId}`}>{rating.callId}</Link>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default RatingList;
