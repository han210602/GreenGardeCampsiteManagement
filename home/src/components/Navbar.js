import React, { Component } from "react";
import "./NavbarStyles.css";
import '@fortawesome/fontawesome-free/css/all.min.css';
import { Link } from 'react-router-dom';
class Navbar extends Component {
  state = {
    clicked: false,
  };

  handleClick = () => {
    this.setState({ clicked: !this.state.clicked });
  };

  render() {
    return (
      <nav className="navbaritems">
        <h1 className="navbar-logo">Green Garden</h1>
        
        <div className="menu-icon" onClick={this.handleClick}>
          <i className={this.state.clicked ? "fas fa-times" : "fas fa-bars"}></i>
        </div>
        
        <ul className={this.state.clicked ? "nav-menu active" : "nav-menu"}>
        <li>
             <Link to="/" style={{ color: 'white' ,fontSize:'15px'}}><i className="fa-solid fa-house" ></i> Home</Link>
          </li>
          <li>
            <Link to="/about" style={{ color: 'white',fontSize:'15px' }}><i className="fa-solid fa-circle-info"></i> About</Link>
          </li>
          <li>
            <Link to="/service" style={{ color: 'white',fontSize:'15px' }}><i className="fa-solid fa-briefcase"></i> Service</Link>
          </li>
          <li>
            <Link to="/contact" style={{ color: 'white',fontSize:'15px' }}><i className="fa-solid fa-address-book"></i> Contact</Link>
          </li>
          <li>
            <Link to="/login" style={{ color: 'white',fontSize:'15px' }}><i className="fa-solid fa-user"></i> Login</Link>
          </li>
        </ul>
      </nav>
    );
  }
}

export default Navbar;
