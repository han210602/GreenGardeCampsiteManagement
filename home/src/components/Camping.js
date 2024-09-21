import React from 'react';
import "./CampingStyle.css";
import CampingData from './CampingData';
const Camping = () => {
    return (
        <div className="campingclass" >
<span style={{ color: '#ffffff', fontSize: '2em' }}>Những địa điểm vui chơi mới</span>
{/* <p>Trải nghiệm sẽ mang lại cho bạn khám phá nhiều điều thú vị, tươi đẹp trong cuộc sống</p> */}

         <CampingData
         className="first-camping"
              heading="Câu chuyện về vùng đất mới "
              text="Thiên nhiên là nguồn sống, mang lại sự tĩnh lặng giữa cuộc sống hiện đại. Sông Quê Green Garden tự hào là điểm đến tiên phong tại Việt Nam, nơi du khách có thể tận hưởng thiên nhiên mà vẫn đầy đủ tiện nghi. Chúng tôi cam kết mang đến không gian trong lành và dịch vụ chân thành, giúp khách hàng tìm thấy sự bình yên và kết nối với giá trị chân thật trong cuộc sống."
         img1={"https://scontent.fhan14-3.fna.fbcdn.net/v/t39.30808-6/422549821_843051381165894_4720250931053626582_n.jpg?stp=cp6_dst-jpg&_nc_cat=110&ccb=1-7&_nc_sid=6ee11a&_nc_eui2=AeG4lfchv_oD4TUzlSkD6PVEUqdcjOA9Y45Sp1yM4D1jjqHevYKWsUnWCMIQn7x6iWLWIxwxqBCd93PnSE20VMaJ&_nc_ohc=eI1mV4rsCh0Q7kNvgHDH88e&_nc_ht=scontent.fhan14-3.fna&_nc_gid=A4otN4TuLkY_MZ7EmrH7raW&oh=00_AYAW-P2o5MT1gnXslgJInD1O-BNd-4G3QZH1M0bLpivGiQ&oe=66F1AB34"}
         img2={"https://scontent.fhan14-3.fna.fbcdn.net/v/t39.30808-6/409778566_814603534010679_2261547119429019830_n.jpg?_nc_cat=103&ccb=1-7&_nc_sid=127cfc&_nc_eui2=AeHp1F8dvPxBbs8ziTNjeSnGVNAE6WdDAmdU0ATpZ0MCZw7_btdKJCuR_kZVab9QFJV7_G8iBpROJDrfvewLfkX6&_nc_ohc=rdcHCuv5_DYQ7kNvgEddcc-&_nc_ht=scontent.fhan14-3.fna&_nc_gid=AVfdRkI5oWiVdmnhZ8dQOgY&oh=00_AYAxx4mREiKuJ8EPxNesHUmqz-Q0f5U_acEUXpLXiY3Emw&oe=66F1C9D2"}
         />

<CampingData
         className="second-camping"

              heading="Vùng đất của sự bình đẳng"
 text="Chắc hẳn nhiều người không còn xa lạ với trường Hogwarts trong loạt phim Harry Potter, nơi mà các học sinh từ nhiều nguồn gốc khác nhau cùng học tập trong môi trường công bằng và bình đẳng.
Lấy cảm hứng từ tinh thần CÔNG BẰNG và HÒA BÌNH, khi đến với trại, khách sẽ tạm quên đi những gánh nặng hay địa vị bên ngoài, bước vào thế giới Sông Quê Green Garden. Tại đây, mọi người đều có thể thư giãn và tìm lại sự thanh thản cho bản thân!"
         img1={"https://scontent.fhan14-1.fna.fbcdn.net/v/t39.30808-6/447964128_930144095789955_697459597222447268_n.jpg?stp=cp6_dst-jpg&_nc_cat=101&ccb=1-7&_nc_sid=833d8c&_nc_eui2=AeEdd0XACCqfMdexeOJ20l7Zgw0YV6JefKGDDRhXol58od0C5HEZMILn8YazLot74QEg0yPlj5Id9eYNTzsd3ho_&_nc_ohc=d3YnnyEMJXIQ7kNvgG03riJ&_nc_ht=scontent.fhan14-1.fna&_nc_gid=APH_CAZP7MAyTWOwm5L83fI&oh=00_AYDdoXCy6sA7a9bO7b177fd4bJD3TbKxh1q_ywOd7-6GhA&oe=66F22C8F"}
         img2={"https://scontent.fhan14-5.fna.fbcdn.net/v/t39.30808-6/447856128_930143872456644_7725153911401479940_n.jpg?_nc_cat=109&ccb=1-7&_nc_sid=833d8c&_nc_eui2=AeGdWj3JZ2weMoalH1CySPJzSYyTnJgH_1JJjJOcmAf_Uoew3AO_SK5D6wG5ZfZ9HclXKJPjt85EgZg79P5D-hTA&_nc_ohc=PrNSxwFdqOsQ7kNvgF1lsKe&_nc_ht=scontent.fhan14-5.fna&oh=00_AYAIPxu2mkYt5ersTAnFpISBM_3yEUb6bHc1HUGikoKZnA&oe=66F24FC4"}
         />
        </div>
        
    );
};
export default Camping;
