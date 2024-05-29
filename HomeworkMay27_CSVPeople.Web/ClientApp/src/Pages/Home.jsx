import { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';

const Home = () => {

    const [people, setPeople] = useState([]);

    const [isLoading, setIsLoading] = useState(true);

    const getPeople = async () => {
        const { data } = await axios.get('/api/home/getpeople');
        setPeople(data);
    };

    useEffect(() => {
        getPeople();
        setIsLoading(false);
    }, []);

    if (isLoading) {
        return <div className='container' style={{ marginTop: 180 }}>
            <div className='d-flex w-100 justify-content-center align-self-center'>
                <img src='/src/pages/loadingimage/Vanilla@1x-1.0s-280px-250px.gif' />
            </div>
        </div>
    }

    const onDeleteClick = async() => {
        await axios.post('/api/home/delete');
        getPeople();
    }

    return (
        <>
            <div className='container' style={{ marginTop: 60 }}>
                <div className='row'>
                    <div className='col-md-6 offset-md-3 mt-5'>
                        <button onClick={onDeleteClick} className='btn btn-outline-danger btn-lg w-100'>Delete All</button>
                    </div>
                </div>
                <table className='table table-hover table-striped table-bordered mt-5'>
                    <thead>
                        <tr>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Age</th>
                            <th>Address</th>
                            <th>Email Address</th>
                        </tr>
                    </thead>
                    <tbody>
                        {people.map(p => 
                            <tr key={p.id}>
                                <td>{p.firstName}</td>
                                <td>{p.lastName}</td>
                                <td>{p.age}</td>
                                <td>{p.address}</td>
                                <td>{p.email}</td>
                            </tr>)}
                    </tbody>
                </table>
            </div>
        </>
    );
};

export default Home;