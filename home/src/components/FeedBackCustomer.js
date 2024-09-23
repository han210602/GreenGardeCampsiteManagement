import React from 'react';
import './FeedBackCustomerStyle.css';

const FeedBackCustomer = () => {
  return (
    <div className="happy-customers-section">
      <span style={{color:'#ffffff',fontSize:'2rem'}}>Khách hàng tiêu biểu</span>
      <div className="feedback-grid">
        {FeedbackData.map((feedback) => (
          <FeedbackCard
            key={feedback.id}
            name={feedback.name}
            image={feedback.image}
            text={feedback.text}
          />
        ))}
      </div>
    </div>
  );
};

export default FeedBackCustomer;

const FeedbackData = [
  {
    id: 1,
    name: 'Nguyễn Đăng Hoàng',
    image: 'https://vnn-imgs-f.vgcloud.vn/2020/03/23/11/trend-avatar-1.jpg',
    text: 'Far far away, behind the word mountains...',
  },
  {
    id: 2,
    name: 'Đoàn Văn Hậu',
    image: 'https://vnn-imgs-f.vgcloud.vn/2020/03/23/11/trend-avatar-1.jpg',
    text: 'Far far away, behind the word mountains...',
  },
  {
    id: 3,
    name: 'Nguyễn Hữu Khánh',
    image: 'https://vnn-imgs-f.vgcloud.vn/2020/03/23/11/trend-avatar-1.jpg',
    text: 'Far far away, behind the word mountains...',
  },
];

const FeedbackCard = ({ name, image, text }) => {
  return (
    <div className="feedback-card">
      <img src={image} alt={name} className="feedback-image" />
      <p className="feedback-text">"{text}"</p>
      <p className="feedback-name">— {name}</p>
    </div>
  );
};
