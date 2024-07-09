import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { getAllCategories, deleteCategory } from '../services/categoryService';

const CategoryList = () => {
    const [categories, setCategories] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchCategories();
    }, []);

    const fetchCategories = async () => {
        try {
            const data = await getAllCategories();
            setCategories(data);
        } catch (error) {
            console.error('Error fetching categories:', error);
            setError('Nie udało się pobrać kategorii');
        }
    };

    const handleDelete = async (id) => {
        try {
            await deleteCategory(id);
            setCategories(categories.filter(category => category.categoryId !== id));
        } catch (error) {
            console.error('Error deleting category:', error);
            setError('Nie udało się usunąć kategorii');
        }
    };

    return (
        <div>
            <h2>Kategorie</h2>
            <Link to="/categories/new">
                <button>Dodaj nową kategorię</button>
            </Link>
            <ul>
                {categories.map(category => (
                    <li key={category.categoryId}>
                        <div><strong>ID:</strong> {category.categoryId}</div>
                        <div><strong>Nazwa:</strong> {category.name}</div>
                        <div><strong>Status:</strong> {category.status}</div>
                        <div><strong>Utworzone przez:</strong> {category.createdBy}</div>
                        <div><strong>Data utworzenia:</strong> {category.createdAt}</div>
                        <div><strong>Zaktualizowane przez:</strong> {category.modifiedBy}</div>
                        <div><strong>Data aktualizacji:</strong> {category.modifiedAt}</div>
                        <button onClick={() => handleDelete(category.categoryId)}>Usuń</button>
                        <Link to={`/categories/edit/${category.categoryId}`}>Edytuj</Link>
                    </li>
                ))}
            </ul>
            {error && <p>{error}</p>}
        </div>
    );
};

export default CategoryList;
