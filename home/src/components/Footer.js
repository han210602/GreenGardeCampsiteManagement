import "./FooterStyle.css"

const Footer = () => {
  return (
    <div className="footer">
      <div className="top">
        <div>
          <h1>Thông Tin Liên Hệ</h1>
        </div>

        <div>
          <a href="/">
            <i className="fab fa-facebook"></i>
          </a>

          <a href="/">
            <i className="fab fa-instagram"></i>
          </a>
          <a href="/">
            <i className="fab fa-tiktok"></i>
          </a>
        </div>
      </div>

      <div className="bottom">
        <div>
          <p>
            <i className="fas fa-map-marker-alt"></i> Đường Đê vàng lối rẽ Đình
            chùa thái linh Quán tự phường Giang- Biên -Long -Biên -Hà -Nội đi
            thêm 50m rẽ phải là đến Sông Quê Green Garden, Hanoi, Vietnam
          </p>
          <p>
            <i className="fas fa-phone-alt"></i> 098 732 76 69
          </p>
          <p>
            <i className="fas fa-envelope"></i> hocausongque@gmail.com
          </p>
        </div>
      </div>
    </div>
  );
};

export default Footer;
