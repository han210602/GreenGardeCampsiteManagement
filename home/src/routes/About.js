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
const blogPosts = [
    {
      image: 'image1-url',
      title: 'Địa điểm vui chơi dã ngoại lý tưởng...',
      description: 'Địa điểm vui chơi dã ngoại lý tưởng dành cho các gia đình...',
    },
    {
      image: 'image2-url',
      title: 'Chương trình VUI DÃ NGOẠI...',
      description: 'Chương trình VUI DÃ NGOẠI - TRẢI NGHIỆM TẾT XƯA...',
    },
    {
      image: 'image3-url',
      title: 'Địa điểm vui chơi lý tưởng cho dịp...',
      description: 'Địa điểm vui chơi lý tưởng cho dịp nghỉ lễ...',
    },
    // Add more blog posts here
  ];
const BlogPage = () => {
    return (
      <div style={styles.gridContainer}>
        {blogPosts.map((post, index) => (
          <BlogCard
            key={index}
            image={post.image}
            title={post.title}
            description={post.description}
          />
        ))}
      </div>
    );
  };
export default About;
