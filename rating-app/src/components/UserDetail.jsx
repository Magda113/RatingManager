import React, { useEffect, useState } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import { getUserById, deleteUser } from '../services/userService';

const UserDetail = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [user, setUser] = useState(null);

    useEffect(() => {
        const fetchUser = async () => {
            try {
                const data = await getUserById(id);
                setUser(data);
            } catch (error) {
                console.error('Błąd pobierania użytkownika:', error);
            }
        };

        fetchUser();
    }, [id]);

    const handleDelete = async () => {
        try {
            await deleteUser(id);
            navigate('/users');
        } catch (error) {
            console.error('Błąd usuwania użytkownika:', error);
        }
    };

    if (!user) {
        return <div>Loading...</div>;
    }

    return (
        <div>
            <h2>Szczegóły użytkownika: {user.userId}</h2>
            <p>Nazwa użytkownika: {user.userName}</p>
            <p>Email: {user.email}</p>
            <p>Rola: {user.role}</p>
            <p>Departament: {user.department}</p>
            <p>Utworzony przez: {user.createdByFullName}</p>
            <p>Data utworzenia: {user.createdAt ? new Date(user.createdAt).toLocaleString() : 'Brak danych'}</p>
            <p>Data aktualizacji: {user.modifiedAt ? new Date(user.modifiedAt).toLocaleString() : 'Brak danych'}</p>
            <p>Zmodyfikowany przez: {user.modifiedByFullName ? user.modifiedByFullName : 'Brak danych'}</p>
            <button onClick={handleDelete}>Usuń</button>
            <Link to={`/users/edit/${user.userId}`}>Edytuj</Link>
        </div>
    );
};

export default UserDetail;
