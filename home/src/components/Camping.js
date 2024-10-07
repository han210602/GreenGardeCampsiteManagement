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
              img1={"https://scontent.fhan2-4.fna.fbcdn.net/v/t39.30808-6/422549821_843051381165894_4720250931053626582_n.jpg?stp=cp6_dst-jpg&_nc_cat=110&ccb=1-7&_nc_sid=6ee11a&_nc_ohc=Mmg85G3xrIMQ7kNvgH3GpN2&_nc_ht=scontent.fhan2-4.fna&_nc_gid=AP1JLVu9GhjzYrkf3uoYxyj&oh=00_AYBaIbMqynaUUdSvd855ztkl88X5Ve3J2fJX0BoDl0fpuA&oe=66FEA1F4"}
             img2={"https://scontent.fhan2-5.fna.fbcdn.net/v/t39.30808-6/446925907_930120909125607_7508003193744153017_n.jpg?_nc_cat=104&ccb=1-7&_nc_sid=833d8c&_nc_ohc=3Ee7ShS7wzEQ7kNvgHt0XbK&_nc_ht=scontent.fhan2-5.fna&_nc_gid=AINx_gbxoik1mNDCtodn5gx&oh=00_AYA9Srz4yuZkspDgCFGuQV01zltM97QEAhNmgUKd5PaRxA&oe=66FE9136"}
         />

<CampingData
         className="second-camping"

              heading="Vùng đất của sự bình đẳng"
 text="Chắc hẳn nhiều người không còn xa lạ với trường Hogwarts trong loạt phim Harry Potter, nơi mà các học sinh từ nhiều nguồn gốc khác nhau cùng học tập trong môi trường công bằng và bình đẳng.
Lấy cảm hứng từ tinh thần CÔNG BẰNG và HÒA BÌNH, khi đến với trại, khách sẽ tạm quên đi những gánh nặng hay địa vị bên ngoài, bước vào thế giới Sông Quê Green Garden. Tại đây, mọi người đều có thể thư giãn và tìm lại sự thanh thản cho bản thân!"
img1={"https://scontent.fhan2-3.fna.fbcdn.net/v/t39.30808-6/447964128_930144095789955_697459597222447268_n.jpg?stp=cp6_dst-jpg&_nc_cat=101&ccb=1-7&_nc_sid=833d8c&_nc_ohc=PVYoS1-Q7dsQ7kNvgGfOjNB&_nc_ht=scontent.fhan2-3.fna&oh=00_AYDFOOZBsKqPYeDMn_nmbZ4iIs86qoT_XZ3XV1NwY9PYlw&oe=66FEB2CF"}
img2={"https://scontent.fhan20-1.fna.fbcdn.net/v/t39.30808-6/461315272_1008434324627598_7428430072911438078_n.jpg?_nc_cat=109&ccb=1-7&_nc_sid=833d8c&_nc_ohc=dkPmvaF4DTAQ7kNvgH_KCXf&_nc_ht=scontent.fhan20-1.fna&_nc_gid=AM7E0xpGGA1BO4r_-lSiVe3&oh=00_AYADQUiG5YfTgHenEvJOf5loiKzjYPu43QnedW_U5rbmPA&oe=66FE9334"}
         />
        </div>
        
    );
};
export default Camping;
