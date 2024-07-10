import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getUserById, addUser, updateUser } from '../services/userService';

const UserForm = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [user, setUser] = useState({
        userName: '',
        email: '',
        role: '',
        department: '',
        passwordHash: ''
    });

    useEffect(() => {
        if (id) {
            const fetchUser = async () => {
                try {
                    const data = await getUserById(id);
                    setUser(data); // Ustaw dane pobrane z backendu do 'user'
                } catch (error) {
                    console.error('Błąd pobierania użytkownika:', error);
                }
            };
            fetchUser();
        }
    }, [id]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setUser({ ...user, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            if (id) {
                const { passwordHash, ...userWithoutPassword } = user; // Usuń passwordHash podczas edycji
                await updateUser(id, userWithoutPassword);
            } else {
                await addUser(user);
            }
            navigate('/users');
        } catch (error) {
            console.error('Błąd zapisywania użytkownika:', error);
        }
    };

    return (
        <div>
            <h2>{id ? 'Edytuj Użytkownika' : 'Dodaj Użytkownika'}</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Nazwa Użytkownika</label>
                    <input name="userName" value={user.userName} onChange={handleChange} required />
                </div>
                <div>
                    <label>Email</label>
                    <input name="email" value={user.email} onChange={handleChange} required />
                </div>
                <div>
                    <label>Rola</label>
                    <input name="role" value={user.role} onChange={handleChange} required />
                </div>
                <div>
                    <label>Dział</label>
                    <input name="department" value={user.department} onChange={handleChange} required />
                </div>
                {!id && (
                    <div>
                        <label>Hasło</label>
                        <input name="passwordHash" type="password" value={user.passwordHash} onChange={handleChange} required />
                    </div>
                )}
                <button type="submit">{id ? 'Zapisz zmiany' : 'Dodaj Użytkownika'}</button>
            </form>
        </div>
    );
};

export default UserForm;


