import './App.css';
import Home from './routes/Home';
import {Route, Routes} from "react-router-dom";
import About from './routes/About';
import Service from './routes/Service';
import Contact from './routes/Contact';
import Login from './routes/Login';
function App() {
  return (
    <div className="App">
    <Routes>
 
    <Route path="/" element={<Home />} />
          <Route path="/about" element={<About/>} />
          <Route path="/service" element={<Service/>} />
          <Route path="/contact" element={<Contact/>} />
          <Route path="/login" element={<Login/>} />

    </Routes>

 
    </div>
  );
}

export default App;
