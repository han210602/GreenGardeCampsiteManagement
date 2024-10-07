import React from 'react';
import Navbar from "../components/Navbar";
import HomeDetail from '../components/HomeDetail';
import Camping from '../components/Camping';  // Importing Camping component
import Event from '../components/Event';  // Importing Camping component
import FeedBackCustomer from '../components/FeedBackCustomer';
import Footer from '../components/Footer';  // Importing Camping component

class Home extends React.Component {
    render() {
        return (
            <>
                <Navbar />
                <HomeDetail
                    cName="HomeDetail"
                    img="https://scontent.fhan2-4.fna.fbcdn.net/v/t39.30808-6/410569930_706142101610927_2810974494115182308_n.jpg?_nc_cat=105&ccb=1-7&_nc_sid=833d8c&_nc_ohc=0TSgK6PinjcQ7kNvgFl3N8_&_nc_ht=scontent.fhan2-4.fna&_nc_gid=Ak5vNPHdEMXjqAabuabCphC&oh=00_AYA0ArrZHJX6NPHZ1CWtLi9uhmyWr36RF7_zbB49bIH5nQ&oe=66F94F41"
                    title="Green Garden Camping"
                    text="Xin chào!"
                    buttonText="Đặt ngay"
                    url="/"
                    btnClass="show"
                /> 
                <Camping /> 
                <Event/>
                <FeedBackCustomer/>
                <Footer/>
            </>
        );
    }
}

export default Home;
