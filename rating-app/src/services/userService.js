import { getToken } from './authService';

const API_URL = 'https://localhost:7014/api/User';

const getHeaders = () => {
    const token = getToken();
    if (!token) {
        throw new Error('Brak tokenu, zaloguj się ponownie.');
    }
    return {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
    };
};

export const getAllUsers = async () => {
    const headers = getHeaders();
    try {
        const response = await fetch(API_URL, {
            method: 'GET',
            headers: headers,
            mode: 'cors'
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Nie udało się pobrać użytkowników: ${response.status} - ${errorText}`);
        }

        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Błąd pobierania użytkowników:', error);
        throw new Error('Nie udało się pobrać użytkowników');
    }
};

export const getUserById = async (id) => {
    const headers = getHeaders();
    try {
        const response = await fetch(`${API_URL}/${id}`, {
            method: 'GET',
            headers: headers,
            mode: 'cors'
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Nie udało się pobrać użytkownika: ${response.status} - ${errorText}`);
        }

        const data = await response.json();
        return data;
    } catch (error) {
        console.error(`Błąd pobierania użytkownika ${id}:`, error);
        throw new Error('Nie udało się pobrać użytkownika');
    }
};

export const addUser = async (user) => {
    const headers = getHeaders();
    try {
        const response = await fetch(API_URL, {
            method: 'POST',
            headers: headers,
            body: JSON.stringify(user)
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Nie udało się dodać użytkownika: ${response.status} - ${errorText}`);
        }

        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Błąd dodawania użytkownika:', error);
        throw new Error('Nie udało się dodać użytkownika');
    }
};

export const updateUser = async (id, user) => {
    const headers = getHeaders();
    try {
        const response = await fetch(`${API_URL}/${id}`, {
            method: 'PUT',
            headers: headers,
            body: JSON.stringify(user)
        });

        const responseText = await response.text();
        if (!response.ok) {
            console.error(`Szczegóły błędu: ${responseText}`);
            throw new Error(`Nie udało się zaktualizować użytkownika: ${response.status} - ${responseText}`);
        }

        const data = responseText ? JSON.parse(responseText) : {}; // Parsowanie tylko jeśli jest tekst
        console.log('Update successful, received data:', data);
        return data;
    } catch (error) {
        console.error(`Błąd aktualizacji użytkownika ${id}:`, error);
        throw new Error('Nie udało się zaktualizować użytkownika');
    }
};

export const deleteUser = async (id) => {
    const headers = getHeaders();
    try {
        const response = await fetch(`${API_URL}/${id}`, {
            method: 'DELETE',
            headers: headers
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Nie udało się usunąć użytkownika: ${response.status} - ${errorText}`);
        }

        return {}; // Return empty object or success message if needed
    } catch (error) {
        console.error(`Błąd usuwania użytkownika ${id}:`, error);
        throw new Error('Nie udało się usunąć użytkownika');
    }
};
