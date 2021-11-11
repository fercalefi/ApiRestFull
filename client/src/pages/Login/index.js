import React, { useState } from "react";
import {useHistory} from "react-router-dom";

import "./styles.css";

import api from "../../services/api";

import logoImage from "../../assets/logo.svg";
import padlock from "../../assets/padlock.png";

export default function Login() {
  const [userName, setUserName] = useState();
  const [passWord, setPassWord] = useState();

  const history = useHistory();

  async function login(e) {
    e.preventDefault();

    const data = {
      userName,
      passWord,
    };

    try {
      
      const response = await api.post('api/auth/v1/signin', data);
      localStorage.setItem('userName', userName);
      localStorage.setItem('accessToken', response.data.accessToken);
      localStorage.setItem('refreshToken', response.data.refreshToken);
      history.push('/books');
    
    } catch (error) {
      alert("Falha no login. Tente novamente");
    }

  }

  return (
    <div className="login-container">
      <section className="form">
        <img src={logoImage} alt="Erudio Logo" />

        <form onSubmit={login}>
          <h1>Acesse sua conta</h1>

          <input
            placeholder="Nome do usuário"
            value={userName}
            onChange={(e) => setUserName(e.target.value)}
          />

          <input
            type="password"
            placeholder="Senha"
            value={passWord}
            onChange={(e) => setPassWord(e.target.value)}
          />

          <button className="button" type="submit">
            Login
          </button>
        </form>
      </section>
      <img src={padlock} alt="Login" />
    </div>
  );
}
