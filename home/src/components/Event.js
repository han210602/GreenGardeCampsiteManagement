import React from 'react'; 
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
              image="https://scontent.fhan20-1.fna.fbcdn.net/v/t39.30808-6/444456217_922323616572003_3863819850566247166_n.jpg?stp=cp6_dst-jpg&_nc_cat=109&ccb=1-7&_nc_sid=833d8c&_nc_ohc=BDQcgTU_KWAQ7kNvgHq-POt&_nc_ht=scontent.fhan20-1.fna&_nc_gid=AoUUebrRBbLK4PsjlZw5NAA&oh=00_AYDz5G8BJLtu_3wBxBSqcuN4mT5Jzl2a-oO-PgrXqYptPg&oe=66FEA724"
           heading="Tham gia hoạt động nhóm"
           text="Hoạt động nhóm tại sông quê Green Garden mang đến trải nghiệm gần gũi thiên nhiên và gắn kết cộng đồng. Tham gia các trò chơi dân gian giúp rèn luyện tinh thần đồng đội, tận hưởng không khí trong lành và tạo kỷ niệm đáng nhớ cùng bạn bè."
            />
              <EventData
              image="https://scontent.fhan2-5.fna.fbcdn.net/v/t39.30808-6/447257580_930119842459047_1466711361009336670_n.jpg?stp=cp6_dst-jpg&_nc_cat=107&ccb=1-7&_nc_sid=833d8c&_nc_ohc=Zbzr9yEu_P4Q7kNvgG2qLH9&_nc_ht=scontent.fhan2-5.fna&_nc_gid=A94EOC74VrWdC0bgSuvrR9j&oh=00_AYAaDGL8YI6qn4OiH2j-dtsmb6hj4NvTtTfG-Mfb6dmyNQ&oe=66FE9164"
            heading="Tổ chức tiệc ngoài trời"
            text="Tổ chức tiệc ngoài trời tại sông quê Green Garden là cơ hội tuyệt vời để tận hưởng không gian thiên nhiên thoáng đãng và khung cảnh sông nước yên bình. Bạn có thể tổ chức tiệc nướng BBQ, cắm trại, tham gia các hoạt động giải trí ngoài trời cùng gia đình và bạn bè, tạo nên những khoảnh khắc thư giãn và gắn kết giữa thiên nhiên xanh mát."
            />
              <EventData
              image="https://scontent.fhan2-3.fna.fbcdn.net/v/t39.30808-6/457843760_989896719814692_5722428934490056108_n.jpg?stp=cp6_dst-jpg&_nc_cat=111&ccb=1-7&_nc_sid=833d8c&_nc_ohc=I-Z8ssq2iWMQ7kNvgFW4uzT&_nc_ht=scontent.fhan2-3.fna&oh=00_AYAQwpyqnvyeaTvedbFKM81OFaRgR0C7Ee6jL4xQorjcgQ&oe=66FEADC3"
           heading="Hoà mình vào thiên nhiên"
           text="Hòa mình vào thiên nhiên tại sông quê Green Garden mang đến cảm giác yên bình và thư thái, với cảnh quan xanh tươi, sông nước êm đềm và không khí trong lành. Đây là nơi lý tưởng để thư giãn và kết nối với thiên nhiên, tạm rời xa nhịp sống hối hả."
            />
          
        </div>
      </div>
    );
  }
}

export default Event;
