import React, {useState} from 'react';
import {Link, useHistory, useParams} from 'react-router-dom';
import { FiArrowLeft } from 'react-icons/fi';

import './styles.css';

import logoImage from '../../assets/logo.svg';

import api from "../../services/api";
import { useEffect } from 'react/cjs/react.development';

export default function NewBook(){

    const [id, setId] = useState(null);
    const [author, setAuthor] = useState('');
    const [title, setTitle] = useState('');
    const [lanchDate, setLanchDate] = useState('');
    const [price, setPrice] = useState('');
    
    const history = useHistory();

    const {bookId} = useParams();

    const accessToken = localStorage.getItem('accessToken');

    const authorization = {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    };

    useEffect(() => {
        if(bookId === '0') return;
        else loadBook();
    }, bookId);

    async function loadBook(){
        try {
            const response = await api.get(`api/book/v1/${bookId}`, authorization);

            let adjustedDate = response.data.lanchDate.split("T", 10)[0];


            setId(response.data.id);
            setTitle(response.data.title);
            setAuthor(response.data.author);
            setPrice(response.data.price);
            setLanchDate(adjustedDate);
            
        } catch (err) {
            alert('Erro ao buscar os dados!');
            history.push('/books');
        }
    }

   
    async function saveOrUpdate(e){
        e.preventDefault();

        const data = {
            title, 
            author,
            lanchDate,
            price,
        }

        try {
            if(bookId === '0'){
                await api.post('api/book/v1',data, authorization);  
            } else {
                data.id = id;
                await api.put('api/book/v1',data, authorization);
            }
            
        } catch (err) {
            alert('Erro enquando cadastra o livro. Tente novamente!');
        }
        history.push('/books');


    }

    return (
        <div className="new-book-container">
            <div className="content">
                <section className="form">
                    <img src={logoImage} alt="Erudio"/>
                    <h1>{bookId === '0'? 'Adicionar' : 'Salvar'}</h1>
                    <p>Entre com as informações do livro e clique em {bookId === '0'? `'Adicionar'` : `'Salvar'`}!  </p>
                    <Link className="back-link" to="/books">
                        <FiArrowLeft size={16} color="#251FC5"/>
                          Voltar para lista de livros
                    </Link>
                </section>

                <form onSubmit={saveOrUpdate}>
                    <input 
                        placeholder="Título"
                        value={title}
                        onChange={e => setTitle(e.target.value)}
                    />
                    <input 
                        placeholder="Autor"
                        value={author}
                        onChange={e => setAuthor(e.target.value)}
                    />
                    <input 
                        type="date"
                        value={lanchDate}
                        onChange={e => setLanchDate(e.target.value)}
                    />
                    <input 
                        placeholder="Preço"
                        value={price}
                        onChange={e => setPrice(e.target.value)}
                    />      

                    <button className="button" type="submit" >{bookId === '0'? 'Adicionar' : 'Salvar'}</button>
                </form>
            </div>
        </div>
    );
}
