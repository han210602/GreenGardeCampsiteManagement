import React from 'react'; // Don't forget to import React
import "./EventStyle.css";
import EventData from './EventData';
class Event extends React.Component {
  render() {
    return (
      <div className="event">
        <h1 style={{color:'#013927'}}>Event-teambuilding</h1>
        <p style={{color:'#000000'}}>Khoảnh khắc tại Sông Quê Green Garden</p>

        <div className="card">      
              <EventData
            image="https://scontent.fhan14-5.fna.fbcdn.net/v/t39.30808-6/444456217_922323616572003_3863819850566247166_n.jpg?stp=cp6_dst-jpg&_nc_cat=109&ccb=1-7&_nc_sid=833d8c&_nc_eui2=AeHffu2WW2wcL2EdQMkTkzIR9YkaUog2TtL1iRpSiDZO0qdbgvjGytKzxHhBWpjbXJOQKSbb_o4nBCGxvEOPgidE&_nc_ohc=fnLrzYphxKIQ7kNvgETn9v2&_nc_ht=scontent.fhan14-5.fna&_nc_gid=AgUiaYdDXZgZsAPkuoEvYD9&oh=00_AYCFj_DKUmBCSRlwMjOeV1Sk2xb99E_HIqMkgeURMPo4sg&oe=66F25924"
           heading="Tham gia hoạt động nhóm"
           text="Hoạt động nhóm tại sông quê Green Garden mang đến trải nghiệm gần gũi thiên nhiên và gắn kết cộng đồng. Tham gia các trò chơi dân gian giúp rèn luyện tinh thần đồng đội, tận hưởng không khí trong lành và tạo kỷ niệm đáng nhớ cùng bạn bè."
            />
              <EventData
            image="https://scontent.fhan14-1.fna.fbcdn.net/v/t39.30808-6/429550514_862583475879351_8104774468648500688_n.jpg?stp=cp6_dst-jpg&_nc_cat=107&ccb=1-7&_nc_sid=833d8c&_nc_eui2=AeG_GOpMSzhmunGA8Uzsd4nFeSrZRuADYF55KtlG4ANgXsm49KIvKXg2tqnJXszRVE_SKGI0HYQ6TCpuF-ZtGp9I&_nc_ohc=YCu5tJK4aywQ7kNvgGsmQYw&_nc_ht=scontent.fhan14-1.fna&_nc_gid=AhpIZ-VMoqSVAnoApu17oSy&oh=00_AYCF67UNmbZi39tuAeA1kGE0cjy1kmhbKaT21lbsFhJUbA&oe=66F256E7"
            heading="Tổ chức tiệc ngoài trời"
            text="Tổ chức tiệc ngoài trời tại sông quê Green Garden là cơ hội tuyệt vời để tận hưởng không gian thiên nhiên thoáng đãng và khung cảnh sông nước yên bình. Bạn có thể tổ chức tiệc nướng BBQ, cắm trại, tham gia các hoạt động giải trí ngoài trời cùng gia đình và bạn bè, tạo nên những khoảnh khắc thư giãn và gắn kết giữa thiên nhiên xanh mát."
            />
              <EventData
            image="https://scontent.fhan14-1.fna.fbcdn.net/v/t39.30808-6/457809615_989896659814698_8246563421649129744_n.jpg?stp=cp6_dst-jpg&_nc_cat=107&ccb=1-7&_nc_sid=833d8c&_nc_eui2=AeEuHnF5KC5Q1P3-qO9zwOLl1jzioiRxhZPWPOKiJHGFkyzw0Z2_fNRe2d4VXjxSIk4R4GHMu7DzQIBljdjfrFmC&_nc_ohc=UatfbU0vpPoQ7kNvgHMOXJ7&_nc_ht=scontent.fhan14-1.fna&oh=00_AYCsc07Yx5YuZqjKV520vME4L0t7WUk5dADSX5WafNQSeQ&oe=66F24696"
           heading="Hoà mình vào thiên nhiên"
           text="Hòa mình vào thiên nhiên tại sông quê Green Garden mang đến cảm giác yên bình và thư thái, với cảnh quan xanh tươi, sông nước êm đềm và không khí trong lành. Đây là nơi lý tưởng để thư giãn và kết nối với thiên nhiên, tạm rời xa nhịp sống hối hả."
            />
          
        </div>
      </div>
    );
  }
}

export default Event;
