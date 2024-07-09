import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getCategoryById, deleteCategory } from '../services/categoryService';

const CategoryDetail = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [category, setCategory] = useState(null);

    useEffect(() => {
        const fetchCategory = async () => {
            const data = await getCategoryById(id);
            setCategory(data);
        };

        fetchCategory();
    }, [id]);

    const handleDelete = async () => {
        await deleteCategory(id);
        navigate('/categories');
    };

    return (
        <div>
            <h2>Nazwa: {category.name}</h2>
            <p>Id: {category.categoryId}</p>
            <p>Status: {category.status}</p>
            <p>Utworzona przez: {category.createdBy}</p>
            <p>Data utworzenia: {category.createdAt}</p>
            <p>Zaktualizowana przez: {category.modifiedBy || 'N/A'}</p>
            <p>Data aktualizacji: {category.modifiedAt || 'N/A'}</p>
            <button onClick={handleDelete}>Usuń</button>
            <Link to={`/categories/edit/${category.categoryId}`}>Edytuj</Link>
        </div>
    );
};

export default CategoryDetail;
