import React from 'react';
import Navbar from "../components/Navbar";
import '../components/BookingTentStyle.css';


class BookingTent extends React.Component {
    render() {
        const cards = [
            { title: 'Nhanh chóng', icon: '🌍', description: 'Cung cấp, cập nhật thông tin nhanh.' },
            { title: 'Giá ưu đãi', icon: '📊', description: 'Không qua trung gian, đem đến mức giá tốt nhất.' },
            { title: 'Tiện lợi', icon: '🎯', description: 'Ứng dụng công nghệ 4.0 giúp khách hàng sử dụng dịch vụ mọi nơi.' },
            { title: 'Thông tin', icon: '⏱', description: 'Cập nhật nhanh, chính xác các thông tin liên quan.' },
            { title: 'Mọi nơi', icon: '🌍', description: 'Đưa du khách tới mọi nơi bạn muốn.' },
        ];

        return (
            <>
                <Navbar />
                <div className="card-grid-container">
                    {/* Header Section */}
                    <div className="header">
                        <h1>Đặt vé máy bay</h1>
                        <p>Tìm kiếm thông tin và đặt vé máy bay giá tốt chỉ với vài thao tác đơn giản!</p>
                    </div>

                    {/* Card Grid Section */}
                    <div className="card-grid">
                        {cards.map((card, index) => (
                            <div key={index} className="card">
                                <div className="icon">{card.icon}</div>
                                <h3>{card.title}</h3>
                                <p>{card.description}</p>
                            </div>
                        ))}
                    </div>
                </div>
            </>
        );
    }
}

export default BookingTent;
