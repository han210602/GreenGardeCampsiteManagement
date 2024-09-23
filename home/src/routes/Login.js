import React,{useState } from 'react';
import Navbar from "../components/Navbar";
import { PiCampfire } from "react-icons/pi";

import "../components/LoginStyle.css"
const Login = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
  
    const handleLogin = (e) => {
      e.preventDefault();
      console.log("Logging in with:", email, password);
      // You would handle authentication here
    };

        return (
            <>
                        <Navbar/>

                        <div className="login-container">
      <div className="logo">
      <PiCampfire
  style={{
    fontSize: "150px", // Increase the font size to make the icon bigger
    width: "150px", // Optionally control width
    height: "150px", // Optionally control height
    transition: "0.3s ease-in-out", // Smooth transition effect
  }}


/>


        <h2>Sông Quê Green Garden</h2>
        <p>Nơi giúp bạn hoà mình vào thiên nhiên,thư giãn,khám phá sự bình yên</p>
      </div>
      <div className="login-box">
        <form onSubmit={handleLogin}>
          <div className="form-group">
            <label htmlFor="email">Email/Số điện thoại/Tên đăng nhập</label>
            <input
              type="text"
              id="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
              placeholder="Email/SĐT/Tên đăng nhập"
            />
          </div>
          <div className="form-group">
            <label htmlFor="password">Mật khẩu</label>
            <input
              type="password"
              id="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
              placeholder="Mật khẩu"
            />
          </div>
          <button type="submit" className="login-btn">
            ĐĂNG NHẬP
          </button>
          <div className="login-options">
            <a href="/forgot-password">Quên mật khẩu</a>
            <span>HOẶC</span>
            <div className="social-login">
              <button className="facebook-login">Facebook</button>
              <button className="google-login">Google</button>
            </div>
          </div>
          <div className="signup-link">
            Bạn mới biết đến Sông quê? <a href="/signup">Đăng ký</a>
          </div>
        </form>
      </div>
    </div>


            </>
        );
    }

export default Login;
