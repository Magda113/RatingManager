import React, { useState, useEffect } from 'react';
import { useParams, useNavigate, Link } from 'react-router-dom';
import { getCategoryById, deleteCategory } from '../services/categoryService';

const CategoryDetail = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [category, setCategory] = useState(null);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchCategory = async () => {
            try {
                const data = await getCategoryById(id);
                setCategory(data);
            } catch (error) {
                console.error('Error fetching category:', error);
                setError('Błąd podczas pobierania danych kategorii');
            }
        };

        fetchCategory();
    }, [id]);

    const handleDelete = async () => {
        try {
            await deleteCategory(id);
            navigate('/categories');
        } catch (error) {
            console.error('Error deleting category:', error);
            setError('Nie udało się usunąć kategorii');
        }
    };

    if (!category) {
        return <p>Ładowanie danych...</p>;
    }

    return (
        <div>
            <h2>Nazwa: {category.name}</h2>
            <p>Id: {category.categoryId}</p>
            <p>Status: {category.status}</p>
            <p>Utworzona przez: {category.createdByUserName}</p>
            <p>Data utworzenia: {category.createdAt}</p>
            <p>Zaktualizowana przez: {category.modifiedByUserName || 'N/A'}</p>
            <p>Data aktualizacji: {category.modifiedAt || 'N/A'}</p>
            <button onClick={handleDelete}>Usuń</button>
            <Link to={`/categories/edit/${category.categoryId}`}>Edytuj</Link>
            {error && <p>{error}</p>}
        </div>
    );
};

export default CategoryDetail;
