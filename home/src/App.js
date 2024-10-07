import './App.css';
import Home from './routes/Home';
import {Route, Routes} from "react-router-dom";
import About from './routes/About';
import Service from './routes/Service';
import Contact from './routes/Contact';
import Login from './routes/Login';
import BookingFood from './routes/BookingFood';
import BookingTent from './routes/BookingTent';

function App() {
  return (
    <div className="App">
    <Routes>
 
    <Route path="/" element={<Home />} />
          <Route path="/about" element={<About/>} />
          <Route path="/service" element={<Service/>} />
          <Route path="/contact" element={<Contact/>} />
          <Route path="/login" element={<Login/>} />
          <Route path="/bookingfood" element={<BookingFood/>} />
          <Route path="/bookingtent" element={<BookingTent/>} />
          <Route path="/bookingticket" element={<BookingTent/>} />

    </Routes>

 
    </div>
  );
}

export default App;
