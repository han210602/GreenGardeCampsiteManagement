import React from 'react';
import Navbar from "../components/Navbar";
import HomeDetail from '../components/HomeDetail';
import Camping from '../components/Camping';  // Importing Camping component
import Event from '../components/Event';  // Importing Camping component

class Home extends React.Component {
    render() {
        return (
            <>
                <Navbar />
                <HomeDetail
                    cName="HomeDetail"
                    img="https://scontent.fhan14-3.fna.fbcdn.net/v/t39.30808-6/278306774_497799305313682_6686106468907275869_n.jpg?_nc_cat=103&ccb=1-7&_nc_sid=cc71e4&_nc_ohc=RmiKJa1EEGMQ7kNvgFJcrpD&_nc_ht=scontent.fhan14-3.fna&_nc_gid=AYQwKaBLSFC9X6ngVXaTO-e&oh=00_AYAM7qCkW3M_MwDOU7rPJ6rlCBbo_hrm34p0oLYdfOCh5A&oe=66F4C085"
                    title="Green Garden Camping"
                    text="Hello!"
                    buttonText="Book now"
                    url="/"
                    btnClass="show"
                /> 
                <Camping /> 
                <Event/>
            </>
        );
    }
}

export default Home;
