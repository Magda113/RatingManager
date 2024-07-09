import { getToken } from './authService';

const API_URL = 'https://localhost:7014/api/Rating';

const getHeaders = () => {
    const token = getToken();
    if (!token) {
        throw new Error('No token found, please log in first.');
    }
    return {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
    };
};

export const getRatingById = async (id) => {
    const headers = getHeaders();
    try {
        const response = await fetch(`${API_URL}/${id}`, {
            method: 'GET',
            headers: headers,
            mode: 'cors'
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Failed to fetch rating: ${errorText}`);
        }

        const data = await response.json();
        console.log('Fetched rating:', data);
        return data;
    } catch (error) {
        console.error(`Fetch error for rating ${id}:`, error);
        throw new Error('Failed to fetch rating');
    }
};

export const addRating = async (rating) => {
    const headers = getHeaders();
    try {
        const response = await fetch(API_URL, {
            method: 'POST',
            headers: headers,
            body: JSON.stringify(rating)
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Failed to add rating: ${errorText}`);
        }

        const data = await response.json();
        console.log('Added rating:', data);
        return data;
    } catch (error) {
        console.error('Add rating error:', error);
        throw new Error('Failed to add rating');
    }
};

export const updateRating = async (id, rating) => {
    const headers = getHeaders();
    try {
        const response = await fetch(`${API_URL}/${id}`, {
            method: 'PUT',
            headers: headers,
            body: JSON.stringify(rating)
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Failed to update rating: ${errorText}`);
        }

        return true; // Zwrócenie flagi sukcesu
    } catch (error) {
        console.error(`Update rating error for ${id}:`, error);
        throw new Error('Failed to update rating');
    }
};

export const deleteRating = async (id) => {
    const headers = getHeaders();
    try {
        const response = await fetch(`${API_URL}/${id}`, {
            method: 'DELETE',
            headers: headers
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Nie udało się usunąć oceny: ${response.status} - ${errorText}`);
        }

        return {}; // Return empty object or success message if needed
    } catch (error) {
        console.error(`Błąd usuwania oceny ${id}:`, error);
        throw new Error('Nie udało się usunąć oceny');
    }
};

export const getAllRatings = async () => {
    const headers = getHeaders();
    try {
        const response = await fetch(API_URL, {
            method: 'GET',
            headers: headers,
            mode: 'cors'
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Failed to fetch ratings: ${errorText}`);
        }

        const data = await response.json();
        console.log('Fetched all ratings:', data);
        return data;
    } catch (error) {
        console.error('Fetch error for all ratings:', error);
        throw new Error('Failed to fetch all ratings');
    }
};

export const searchRatingsByCategory = async (categoryName) => {
    const headers = getHeaders();
    try {
        const response = await fetch(`${API_URL}/ByCategoryName/${categoryName}`, {
            method: 'GET',
            headers: headers,
            mode: 'cors'
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Failed to search ratings by category: ${errorText}`);
        }

        const data = await response.json();
        console.log(`Fetched ratings for category ${categoryName}:`, data);
        return data;
    } catch (error) {
        console.error(`Search ratings by category ${categoryName} error:`, error);
        throw new Error('Failed to search ratings by category');
    }
};

export const searchRatingsByUser = async (userName) => {
    const headers = getHeaders();
    try {
        const response = await fetch(`${API_URL}/ByUserName/${userName}`, {
            method: 'GET',
            headers: headers,
            mode: 'cors'
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Failed to search ratings by user: ${errorText}`);
        }

        const data = await response.json();
        console.log(`Fetched ratings for user ${userName}:`, data);
        return data;
    } catch (error) {
        console.error(`Search ratings by user ${userName} error:`, error);
        throw new Error('Failed to search ratings by user');
    }
};