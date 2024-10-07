import React, { useRef } from 'react';
import "../components/ServiceStyle.css";
import Navbar from '../components/Navbar';
import HomeDetail from '../components/HomeDetail'; // Make sure HomeDetail is imported

const ServiceCard = ({ image, title, link }) => {
  return (
    <div className="service-card">
      <img src={image} alt={title} className="service-image" />
      <h3>{title}</h3>
      <a href={link} className="service-link">
        Xem thêm
      </a>
    </div>
  );
};

const Service = () => {
  const servicesContainerRef = useRef(null);

  const scrollLeft = () => {
    servicesContainerRef.current.scrollBy({
      left: -350, // Adjust the scroll amount as needed
      behavior: 'smooth',
    });
  };

  const scrollRight = () => {
    servicesContainerRef.current.scrollBy({
      left: 350, // Adjust the scroll amount as needed
      behavior: 'smooth',
    });
  };

  const services = [
    {
      image: 'https://scontent.fhan2-4.fna.fbcdn.net/v/t39.30808-6/422549821_843051381165894_4720250931053626582_n.jpg?stp=cp6_dst-jpg&_nc_cat=110&ccb=1-7&_nc_sid=6ee11a&_nc_ohc=Mmg85G3xrIMQ7kNvgEwmbZH&_nc_ht=scontent.fhan2-4.fna&_nc_gid=Al6qn11_s6dUkSi_JCW6jns&oh=00_AYClYHgYHnjy6VjJsOTRJItrL3mrTD5IVEeR_z-CilB8Jw&oe=66FF4AB4',
      title: 'Đặt vé đơn',
      link: '/bookingtent',
    },
    {
      image: 'https://scontent.fhan2-4.fna.fbcdn.net/v/t39.30808-6/422549821_843051381165894_4720250931053626582_n.jpg?stp=cp6_dst-jpg&_nc_cat=110&ccb=1-7&_nc_sid=6ee11a&_nc_ohc=Mmg85G3xrIMQ7kNvgEwmbZH&_nc_ht=scontent.fhan2-4.fna&_nc_gid=Al6qn11_s6dUkSi_JCW6jns&oh=00_AYClYHgYHnjy6VjJsOTRJItrL3mrTD5IVEeR_z-CilB8Jw&oe=66FF4AB4',
      title: 'Đặt vé theo nhóm',
      link: '/services/tickets',
    },
    {
      image: 'https://scontent.fhan2-4.fna.fbcdn.net/v/t39.30808-6/422549821_843051381165894_4720250931053626582_n.jpg?stp=cp6_dst-jpg&_nc_cat=110&ccb=1-7&_nc_sid=6ee11a&_nc_ohc=Mmg85G3xrIMQ7kNvgEwmbZH&_nc_ht=scontent.fhan2-4.fna&_nc_gid=Al6qn11_s6dUkSi_JCW6jns&oh=00_AYClYHgYHnjy6VjJsOTRJItrL3mrTD5IVEeR_z-CilB8Jw&oe=66FF4AB4',
      title: 'Thuê dụng cụ',
      link: '/services/digital-transformation',
    },
    {
      image: 'https://scontent.fhan2-4.fna.fbcdn.net/v/t39.30808-6/422549821_843051381165894_4720250931053626582_n.jpg?stp=cp6_dst-jpg&_nc_cat=110&ccb=1-7&_nc_sid=6ee11a&_nc_ohc=Mmg85G3xrIMQ7kNvgEwmbZH&_nc_ht=scontent.fhan2-4.fna&_nc_gid=Al6qn11_s6dUkSi_JCW6jns&oh=00_AYClYHgYHnjy6VjJsOTRJItrL3mrTD5IVEeR_z-CilB8Jw&oe=66FF4AB4',
      title: 'Đặt đồ ăn riêng',
      link: '/services/flight-tickets',
    },
    {
      image: 'https://scontent.fhan2-4.fna.fbcdn.net/v/t39.30808-6/422549821_843051381165894_4720250931053626582_n.jpg?stp=cp6_dst-jpg&_nc_cat=110&ccb=1-7&_nc_sid=6ee11a&_nc_ohc=Mmg85G3xrIMQ7kNvgEwmbZH&_nc_ht=scontent.fhan2-4.fna&_nc_gid=Al6qn11_s6dUkSi_JCW6jns&oh=00_AYClYHgYHnjy6VjJsOTRJItrL3mrTD5IVEeR_z-CilB8Jw&oe=66FF4AB4',
      title: 'Đặt đồ ăn combo',
      link: '/services/tour-booking',
    },
  ];

  return (
    <>
      <Navbar />
      <HomeDetail
        cName="HomeDetail"
        img="https://scontent.fhan2-3.fna.fbcdn.net/v/t39.30808-6/381166711_760632636074436_7724956641768752735_n.jpg?stp=cp6_dst-jpg&_nc_cat=108&ccb=1-7&_nc_sid=cc71e4&_nc_ohc=iDeV0MtFpoUQ7kNvgGhEGdg&_nc_ht=scontent.fhan2-3.fna&_nc_gid=Aa55Mn_Y4UmW_sznMObFDv4&oh=00_AYAvcCNs1uLkS7h22SWb_zaJt0-xZ6AbRIVAu1bKLt1F4A&oe=66FF4798"
      />
      
      <div className="services-section">
        <h2>Các dịch vụ chúng tôi cung cấp cho bạn</h2>

        {/* Scroll Left Button */}
        <button className="arrow arrow-left" onClick={scrollLeft}>
          &#8249;
        </button>

        <div className="services-container" ref={servicesContainerRef}>
          {services.map((service, index) => (
            <ServiceCard
              key={index}
              image={service.image}
              title={service.title}
              link={service.link}
            />
          ))}
        </div>

        {/* Scroll Right Button */}
        <button className="arrow arrow-right" onClick={scrollRight}>
          &#8250;
        </button>
      </div>

      <div className="service-section-container">
      <div className="left-column">
        <h2>Cung cấp dịch vụ</h2>
        <p>
          VNLink hoạt động chủ yếu trên lĩnh vực truyền thông, du lịch. Cụ thể như:
        </p>
        <ul>
          <li>
            Tư vấn thiết kế xây dựng các hệ thống web cao cấp: Tư vấn, xây dựng web giới thiệu năng lực công ty, web thương mại điện tử, các dịch vụ hỗ trợ và vận hành web cho doanh nghiệp, tư vấn xây dựng các web theo yêu cầu khách hàng...
          </li>
          <li>
            Tư vấn và tạo ra những sản phẩm phần mềm hiện đại, thông minh được người tiêu dùng đánh giá cao như Ezihotel, TravelLink, Channel Manager, Hub Hotel, Booking Golf...
          </li>
          <li>
            Dịch vụ nền tảng du lịch trực tuyến – TravelLink: cho phép người dùng đặt các booking du lịch thông qua thiết bị di động một cách dễ dàng; bên cạnh đó, việc thanh toán qua ví điện tử sẽ đem lại nhiều giá trị cho khách hàng bởi sự an toàn, tiện dụng, nhanh chóng và bảo mật tuyệt đối.
          </li>
        </ul>
      </div>

      <div className="right-column">
        <h2>Phương châm hoạt động</h2>
        <ul>
          <li>Lấy khách hàng làm trung tâm, cốt lõi.</li>
          <li>Coi khách hàng là bạn đồng hành trong mỗi chặng đường.</li>
          <li>Kim chỉ nam tốt nhất chính là sự phục vụ tận tâm và nhiệt huyết với công việc.</li>
          <li>Ưu tiên trải nghiệm của du khách trên hàng đầu.</li>
          <li>
            Khách hàng là cốt lõi của VNLink, không bạn không có chúng tôi. Luôn đồng hành cùng nhau trên mọi chặng đường.
          </li>
        </ul>
      </div>
    </div>
    </>
  );
};

export default Service;
