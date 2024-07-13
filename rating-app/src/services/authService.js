const API_URL = 'https://localhost:7014/api';

export const login = async (username, password) => {
    try {
        const response = await fetch(`${API_URL}/Auth/Login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ username, password })
        });

        if (!response.ok) {
            const errorText = await response.text();
            console.error('Login failed:', errorText);
            throw new Error('Login failed');
        }

        const data = await response.json();
        const token = data.token;
        console.log('Received token:', token);
        localStorage.setItem('token', token);
        console.log('Token saved in localStorage:', token);
        return token;
    } catch (error) {
        console.error('Login error:', error);
        throw new Error('Login failed');
    }
};

export const getToken = () => {
    const token = localStorage.getItem('token');
    console.log('Retrieved token:', token);
    return token;
};

export const logout = () => {
    localStorage.removeItem('token');
    console.log('Token removed from localStorage');
};
