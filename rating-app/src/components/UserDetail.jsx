// components/UserDetail.js

import React, { useEffect, useState } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import { getUserById, deleteUser } from '../services/userService';

const UserDetail = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [user, setUser] = useState(null);

    useEffect(() => {
        const fetchUser = async () => {
            const data = await getUserById(id);
            setUser(data);
        };

        fetchUser();
    }, [id]);

    const handleDelete = async () => {
        await deleteUser(id);
        navigate('/users'); // Redirect to user list after deletion
    };

    if (!user) {
        return <div>Loading...</div>;
    }

    return (
        <div>
            <h2>Użytkownik: {user.userName}</h2>
            <p>Email: {user.email}</p>
            <p>Rola: {user.role}</p>
            <p>Departament: {user.department}</p>
            <button onClick={handleDelete}>Usuń</button>
            <Link to={`/users/edit/${user.userId}`}>Zmień</Link>
        </div>
    );
};

export default UserDetail;
