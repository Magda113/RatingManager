// components/UserList.js

import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { getAllUsers, deleteUser } from '../services/userService';

const UserList = () => {
    const [users, setUsers] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchUsers();
    }, []);

    const fetchUsers = async () => {
        try {
            const data = await getAllUsers();
            setUsers(data);
        } catch (error) {
            console.error(error);
            setError('Error fetching users');
        }
    };

    const handleDelete = async (id) => {
        try {
            await deleteUser(id);
            setUsers(users.filter(user => user.userId !== id));
        } catch (error) {
            console.error(error);
            setError('Error deleting user');
        }
    };

    return (
        <div>
            <h2>Users</h2>
            <Link to="/users/new">
                <button>Dodaj nowego użytkownika</button>
            </Link>

            <ul>
                {users.map(user => (
                    <li key={user.userId}>
                        <div><strong>Id:</strong> {user.userId}</div>
                        <div><strong>Imię i nazwisko:</strong> {user.userName}</div>
                        <div><strong>Email:</strong> {user.email}</div>
                        <div><strong>Rola:</strong> {user.role}</div>
                        <div><strong>Departament:</strong> {user.department}</div>
                        <button onClick={() => handleDelete(user.userId)}>Usuń</button>
                        <Link to={`/users/edit/${user.userId}`}>Zmień</Link>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default UserList;
