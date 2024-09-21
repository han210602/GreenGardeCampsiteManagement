import React from 'react';
import Navbar from "../components/Navbar";
import HomeDetail from '../components/HomeDetail';
import Camping from '../components/Camping';

class About extends React.Component {
    render() {
        return (
            <>
                        <Navbar/>
                        <HomeDetail
                    cName="HomeDetailmid" 
                    img="https://images.unsplash.com/photo-1520918998343-a33f59b7c079?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                    title="Welcome to "  
                    text="Garden Camping!"  
                    buttonText="Book now"
                    url="/"
                    btnClass="show"
                />
                <p>This is About</p>
                <Camping/>
            </>
        );
    }
}

export default About;
