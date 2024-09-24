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
                    img="https://scontent.fhan20-1.fna.fbcdn.net/v/t39.30808-6/427457417_852235940247438_5893304871313187228_n.jpg?_nc_cat=102&ccb=1-7&_nc_sid=833d8c&_nc_ohc=mJHimb69aRgQ7kNvgHLjLIQ&_nc_ht=scontent.fhan20-1.fna&_nc_gid=As5vM5m5AGSkR9oJcWmeIJG&oh=00_AYCcZTl1BNLhtNtnVirRJOrRULzHJtBuIGn7hKZf2ezBuw&oe=66F8A1D7"
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
