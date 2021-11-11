import React, {useState, useEffect} from 'react';
import {Link, useHistory} from 'react-router-dom';
import {FiPower, FiEdit, FiTrash2} from 'react-icons/fi';

import api from "../../services/api";

import './styles.css';

import logoImage from '../../assets/logo.svg'




export default function Books(){

    const [books, setBooks]     = useState([]);

    const [page, setPage]     = useState(1);

    const userName = localStorage.getItem('userName');

    const accessToken = localStorage.getItem('accessToken');

    const authorization = {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    };

    const history = useHistory();
    
    useEffect(() => {
        fetchMoreBooks(); 
    }, [accessToken]);

    async function fetchMoreBooks(){
        const response = await api.get(`api/book/v1/asc/4/${page}`,authorization);
        // juntando os arrays
        setBooks([ ...books, ...response.data.list]);
        setPage(page + 1);
    };

    async function editBook(id){
        try {
            history.push(`book/new/${id}`)

        } catch (err) {
            alert('Falha ao editar o livro. Tente novamente!')
        }
    }

    async function deleteBook(id){
        try {
            await api.delete(`api/book/v1/${id}`, authorization);

            setBooks(books.filter(book => book.id !== id));

        } catch (err) {
            alert('Falha ao deletar o livro. Tente novamente!')
        }
    }

    async function logout(){
        try {
            await api.get('api/auth/v1/revoke', authorization);

            localStorage.clear();
            history.push('/');
          

        } catch (err) {
            alert('Falha ao Sair!')
        }
    }


    return <div className="book-container">
        <header>
            <img src={logoImage} alt="Erudio"/>
            <span>Bem Vindo, <strong>{userName.toUpperCase()}</strong>!</span>
            <Link className="button" to="book/new/0">Adicionar novo livro</Link>
            <button onClick={logout} type="button">
                <FiPower size={18} color="#251FC5"></FiPower>
            </button>

        </header>

        <h1> Registro de livros</h1>
        <ul>
            {books.map(book =>(
                <li key= {book.id} >
                    <strong>Título:</strong>
                    <p>{book.title}</p>
                    <strong>Autor:</strong>
                    <p>{book.author}</p>
                    <strong>Preço:</strong>
                    <p>{Intl.NumberFormat('pt-BR', {style: 'currency', currency: 'BRL'}).format(book.price)}</p>
                    <strong>Data de lançamento:</strong>
                    <p>{Intl.DateTimeFormat('pt-BR').format(new Date(book.lanchDate)) }</p>   

                    <button onClick={() => editBook(book.id)} type="button">
                        <FiEdit size={20} color="#251FC5"/>
                    </button>             

                    <button onClick={() => deleteBook(book.id)} type="button">
                        <FiTrash2 size={20} color="#251FC5"/>
                    </button>   

                </li>
            ))}
        </ul>
        <button className="button" onClick={fetchMoreBooks} type="button">Load more</button>
    </div>
}
