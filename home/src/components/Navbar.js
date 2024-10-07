import React, { Component } from "react";
import './NavbarStyles.css';
import '@fortawesome/fontawesome-free/css/all.min.css';
import { Link } from 'react-router-dom';
import BookingFood from './BookingFoodForm.js';

class Navbar extends Component {
  state = {
    isMenuOpen: false,
    isServiceDropdownVisible: false,
  };

  toggleMenu = () => {
    this.setState(prevState => ({
      isMenuOpen: !prevState.isMenuOpen
    }));
  };

  toggleServiceDropdown = () => {
    this.setState(prevState => ({
      isServiceDropdownVisible: !prevState.isServiceDropdownVisible,
    }));
  };

  render() {
    return (
      <nav className="navbaritems">
        <h1 className="navbar-logo">Sông Quê Green Garden</h1>
        
        {/* Menu Icon for Mobile */}
        <div className="menu-icon" onClick={this.toggleMenu}>
          <i className={this.state.isMenuOpen ? "fas fa-times" : "fas fa-bars"}></i>
        </div>

        {/* Navigation Menu */}
        <ul className={this.state.isMenuOpen ? "nav-menu active" : "nav-menu"}>
          <li>
            <Link to="/" style={{ color: 'white', fontSize: '15px' }}>
              <i className="fa-solid fa-house"></i> Trang chủ
            </Link>
          </li>
          <li>
            <Link to="/about" style={{ color: 'white', fontSize: '15px' }}>
              <i className="fa-solid fa-circle-info"></i> Chi Tiết
            </Link>
          </li>

       {/* Dropdown Section for Dịch vụ */}
<li className="dropdown-container">
  <div className="dropdown-trigger" onClick={this.toggleServiceDropdown}>
    <span style={{ color: 'white', fontSize: '15px' }}>
    <Link to="/service" style={{ color: 'white', fontSize: '15px' }}>
      <i className="fa-solid fa-briefcase"></i> Dịch vụ
      </Link>

    </span>
    {!this.state.isServiceDropdownVisible && (
      <i className="fas fa-chevron-down" style={{ marginLeft: '5px', color: 'white' }}></i>
    )}
  </div>
  {this.state.isServiceDropdownVisible && (

    
    <ul className="dropdown-menu" >
      <li>
        <Link to="/bookingtent" style={{ color: 'white', fontSize: '15px' }}>Thuê đồ</Link>
      </li>
      <li>
        <Link  to="/bookingfood" style={{ color: 'white', fontSize: '15px' }}>Đặt đồ ăn</Link>
      </li>
      <li>
        <Link to="/bookingticket" style={{ color: 'white', fontSize: '15px' }}>Mua vé</Link>
      </li>
    </ul>
  )}
</li>


          <li>
            <Link to="/contact" style={{ color: 'white', fontSize: '15px' }}>
              <i className="fa-solid fa-address-book"></i> Liên Hệ
            </Link>
          </li>
          <li>
            <Link to="/login" style={{ color: 'white', fontSize: '15px' }}>
              <i className="fa-solid fa-user"></i> Đăng nhập
            </Link>
          </li>
        </ul>
      </nav>
    );
  }
}

export default Navbar;
