import React from 'react';
import Navbar from "../components/Navbar";
import HomeDetail from '../components/HomeDetail';
import CardBlog from '../components/CardBlog';

class About extends React.Component {
    render() {
        return (
            <>
                        <Navbar/>
                        <p style={{ fontSize: '2rem', marginTop:'10px' }}>
  Tin tức mới
</p>

                        <CardBlog data={data} />

            </>
        );
    }
}

const data = [
  {
    image: 'https://scontent.fhan2-5.fna.fbcdn.net/v/t39.30808-6/457852705_990912556379775_4301449055103666944_n.jpg?stp=cp6_dst-jpg&_nc_cat=104&ccb=1-7&_nc_sid=833d8c&_nc_ohc=S09xGC7SWNwQ7kNvgHMwKmd&_nc_ht=scontent.fhan2-5.fna&_nc_gid=AYBYUeAYzTOXAf-NLrMWVle&oh=00_AYAh55PtwPB8bT6ATbmuH1veOla2weqsmUvwlroADk1utw&oe=66F81C9B',
    title: 'Hướng dẫn về một số trường hợp...',
    description: 'Khi bạn mua kìm đa năng...',
    link: '#'
  },
  {
    image: 'https://scontent.fhan2-5.fna.fbcdn.net/v/t39.30808-6/458185119_990912543046443_4708194444760094723_n.jpg?stp=cp6_dst-jpg&_nc_cat=107&ccb=1-7&_nc_sid=833d8c&_nc_ohc=yiCauITKjE4Q7kNvgE3PXm8&_nc_ht=scontent.fhan2-5.fna&_nc_gid=Af1BHxnhjUH_HhlGgKJUKdj&oh=00_AYCw3oPfyEzfmm99z13VQIS0eBBQP1_PGLqJicncoJEe3A&oe=66F83E59',
    title: 'Cắm trại đi ngao du cùng...',
    description: 'Cắm trại sẽ mang đến những trải nghiệm...',
    link: '#'
  },
  {
    image: 'https://scontent.fhan2-5.fna.fbcdn.net/v/t39.30808-6/453259359_965558882248476_6221705252383496036_n.jpg?_nc_cat=107&ccb=1-7&_nc_sid=833d8c&_nc_ohc=g0HSYzjIH60Q7kNvgF8lvmb&_nc_ht=scontent.fhan2-5.fna&_nc_gid=AD7ficw5QqdNdAQb9IQGxfY&oh=00_AYDwqgKJAiojyCDpm9UKdqQNWX4tgZgwTqw7AsVJUrpsqg&oe=66F82461',
    title: 'Một chuyến phiêu lưu đáng nhớ...',
    description: 'Khám phá vùng đất mới cùng bạn bè...',
    link: '#'
  },

  {
    image: 'https://scontent.fhan2-5.fna.fbcdn.net/v/t39.30808-6/453259359_965558882248476_6221705252383496036_n.jpg?_nc_cat=107&ccb=1-7&_nc_sid=833d8c&_nc_ohc=g0HSYzjIH60Q7kNvgF8lvmb&_nc_ht=scontent.fhan2-5.fna&_nc_gid=AD7ficw5QqdNdAQb9IQGxfY&oh=00_AYDwqgKJAiojyCDpm9UKdqQNWX4tgZgwTqw7AsVJUrpsqg&oe=66F82461',
    title: 'Cắm trại đi ngao du cùng...',
    description: 'Cắm trại sẽ mang đến những trải nghiệm...',
    link: '#'
  },
  // Add more items as needed
];
export default About;
