import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getCategoryById, addCategory, updateCategory } from '../services/categoryService';

const CategoryForm = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [category, setCategory] = useState({
        name: '',
        status: ''
    });

    useEffect(() => {
        if (id) {
            const fetchCategory = async () => {
                try {
                    const data = await getCategoryById(id);
                    setCategory(data);
                } catch (error) {
                    console.error('Error fetching category:', error);
                }
            };

            fetchCategory();
        }
    }, [id]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setCategory({ ...category, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            if (id) {
                await updateCategory(id, category);
                console.log('Udało się zmienić kategorię');
            } else {
                await addCategory(category);
                console.log('Udało się dodać kategorię');
            }
            navigate('/categories');
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <div>
            <h2>{id ? 'Edytuj Kategorię' : 'Dodaj Kategorię'}</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Nazwa:</label>
                    <input type="text" name="name" value={category.name} onChange={handleChange} required />
                </div>
                {id && (
                    <div>
                        <label>Status: </label>
                        <input name="status" value={category.status} onChange={handleChange} required />
                    </div>
                )}
                <button type="submit">{id ? 'Zapisz zmiany' : 'Dodaj'}</button>
            </form>
        </div>
    );
};

export default CategoryForm;
