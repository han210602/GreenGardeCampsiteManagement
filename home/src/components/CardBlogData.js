import React from 'react';
import './CardBlogStyle.css'; 

const CardBlogData = ({ image, title, description, link }) => {
    return (
      <div className="cardblog">
        <img src={image} alt={title} />
        <h3>{title}</h3>
        <p>{description}</p>
        <a href={link}>Xem thêm</a>
      </div>
    );
};

export default CardBlogData;
