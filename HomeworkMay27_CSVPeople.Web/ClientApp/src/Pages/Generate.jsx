import { useState } from 'react';
import axios from 'axios';

const Generate = () => {

    const [amount, setAmount] = useState('');

    const onGenerateClick = async () => {
        window.location.href = `/api/home/generate?amount=${amount}`;
    }

    return (
        <div className='container' style={{ marginTop: 60 }}>
            <div className='d-flex vh-100' style={{ marginTop: -70 }}>
                <div className='d-flex w-100 justify-content-center align-self-center'>
                    <div className='row'>
                        <input value={amount} onChange={e => setAmount(e.target.value)} type='text' className='form-control-lg' placeholder='Amount' />
                    </div>
                    <div className='row'>
                        <div className='col-md-4 offset-md-2'>
                            <button onClick={onGenerateClick} className='btn btn-outline-success btn-lg'>Generate</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}
export default Generate;