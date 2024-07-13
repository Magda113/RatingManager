import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getAllUsers, getUserById, deleteUser } from '../services/userService';

const UserList = () => {
    const [users, setUsers] = useState([]);
    const [searchId, setSearchId] = useState('');

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const data = await getAllUsers();
                setUsers(data);
            } catch (error) {
                console.error(error);
            }
        };
        fetchUsers();
    }, []);

    const handleDelete = async (id) => {
        try {
            await deleteUser(id);
            setUsers(users.filter(user => user.userId !== id));
        } catch (error) {
            console.error(error);
        }
    };
    const handleIdSearch = async () => {
        try {
            const data = await getUserById(searchId);
            setUsers([data]);
            setSearchId('');
        } catch (error) {
            console.error(error);
        }
    };
    return (
        <div>
            <h2>Użytkownicy</h2>
            <Link to="/users/new">
                <button>Dodaj nowego użytkownika</button>
            </Link>
            <div>
                <input type="text" value={searchId}
                       onChange={(e) => setSearchId(e.target.value)} // Updated to use searchId state
                       placeholder="Wyszukaj po id"/>
                <button onClick={handleIdSearch}>Szukaj po id użytkownika</button>
            </div>
            <ul>
                {users.map(user => (
                    <li key={user.userId}>
                        <div><strong>UserId:</strong> {user.userId}</div>
                        <div><strong>Nazwa użytkownika:</strong> {user.userName}</div>
                        <div><strong>Email:</strong> {user.email}</div>
                        <div><strong>Rola:</strong> {user.role}</div>
                        <div><strong>Departament:</strong> {user.department}</div>
                        <div><strong>Utworzony przez:</strong> {user.createdByFullName}</div>
                        <div><strong>Data utworzenia:</strong> {user.createdAt ? new Date(user.createdAt).toLocaleString() : 'Brak danych'}</div>
                        <div><strong>Zmodyfikowany przez:</strong> {user.modifiedByFullName ? user.modifiedByFullName : 'Brak danych'}</div>
                        <div><strong>Data aktualizacji:</strong> {user.modifiedAt ? new Date(user.modifiedAt).toLocaleString() : 'Brak danych'}</div>
                        <button onClick={() => handleDelete(user.userId)}>Usuń</button>
                        <Link to={`/users/edit/${user.userId}`}>Edytuj</Link>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default UserList;

