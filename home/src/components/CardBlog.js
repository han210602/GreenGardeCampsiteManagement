import React from 'react';
import CardBlogData from './CardBlogData'; 
import './CardBlogStyle.css'; 

const CardBlog = ({ data }) => {  
  return (
    <div className="card-container">
      {data.map((blog, index) => (
        <CardBlogData 
          key={index}
          image={blog.image}
          title={blog.title}
          description={blog.description}
          link={blog.link}
        />
      ))}
    </div>

    
  );
};


  
export default CardBlog;
