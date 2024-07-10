// src/services/categoryService.js

import { getToken } from './authService';

const API_URL = 'https://localhost:7014/api/Category';

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


export const getAllCategories = async () => {
    const headers = getHeaders();
    try {
        const response = await fetch(API_URL, {
            method: 'GET',
            headers: headers,
            mode: 'cors'
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Failed to fetch categories: ${errorText}`);
        }

        const data = await response.json();
        console.log('Fetched categories:', data);
        return data;
    } catch (error) {
        console.error('Fetch error:', error);
        throw new Error('Failed to fetch categories');
    }
};

export const getCategoryById = async (id) => {
    const headers = getHeaders();
    try {
        const response = await fetch(`${API_URL}/${id}`, {
            method: 'GET',
            headers: headers,
            mode: 'cors'
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Failed to fetch category: ${errorText}`);
        }

        const data = await response.json();
        console.log('Fetched category:', data);
        return data;
    } catch (error) {
        console.error(`Fetch error for category ${id}:`, error);
        throw new Error('Failed to fetch category');
    }
};

export const addCategory = async (category) => {
    const headers = getHeaders();
    try {
        const response = await fetch(API_URL, {
            method: 'POST',
            headers: headers,
            body: JSON.stringify(category)
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Failed to add category: ${errorText}`);
        }

        const data = await response.json();
        console.log('Added category:', data);
        return data;
    } catch (error) {
        console.error('Add category error:', error);
        throw new Error('Failed to add category');
    }
};

export const updateCategory = async (id, category) => {
    const headers = getHeaders();
    try {
        const response = await fetch(`${API_URL}/${id}`, {
            method: 'PUT',
            headers: headers,
            body: JSON.stringify(category)
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Failed to update category: ${errorText}`);
        }

        return true; // Zwrócenie flagi sukcesu
    } catch (error) {
        console.error(`Update category error for ${id}:`, error);
        throw new Error('Failed to update category');
    }
};

export const deleteCategory = async (id) => {
    const headers = getHeaders();
    try {
        const response = await fetch(`${API_URL}/${id}`, {
            method: 'DELETE',
            headers: headers
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Failed to delete category: ${errorText}`);
        }

        console.log('Category deleted successfully');
    } catch (error) {
        console.error('Delete category error:', error);
        throw new Error('Failed to delete category');
    }
};
