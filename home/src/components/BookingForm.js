import React, { useState, useEffect } from 'react';
import './BookingFormStyle.css'; // Assume CSS styling from above

const BookingForm = ({ addToCart }) => {
  const [date, setDate] = useState('');
  const [childTickets, setChildTickets] = useState(0);
  const [adultTickets, setAdultTickets] = useState(0);
  const [freeChildTickets, setFreeChildTickets] = useState(0);
  const [totalPrice, setTotalPrice] = useState(0);

  const [groupAdultTickets, setGroupAdultTickets] = useState(0);
  const [groupTotalPrice, setGroupTotalPrice] = useState(0);

  const CHILD_TICKET_PRICE = 280000;
  const ADULT_TICKET_PRICE = 350000;

  const today = new Date().toISOString().split('T')[0];

  // Calculate total price for individual bookings
  useEffect(() => {
    const price = (childTickets * CHILD_TICKET_PRICE) + (adultTickets * ADULT_TICKET_PRICE);
    setTotalPrice(price);
  }, [childTickets, adultTickets]);

  // Calculate total price for group bookings
  useEffect(() => {
    const price = (groupAdultTickets * ADULT_TICKET_PRICE);
    setGroupTotalPrice(price);
  }, [groupAdultTickets]);

  // Handle individual form submission
  const handleSubmit = (e) => {
    e.preventDefault();
    const totalTickets = childTickets + adultTickets + freeChildTickets;
    addToCart({ date, totalTickets, totalPrice });
  };

  // Handle group booking submission (if needed)
  const handleGroupSubmit = (e) => {
    e.preventDefault();
    addToCart({ date, groupAdultTickets, groupTotalPrice });
  };

  return (
    <div className="booking-form-container">
      {/* Individual Booking Form */}
      <div className="individual-booking-form">
        <h2>Thông tin và giá vé vui chơi</h2>
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Ngày chơi:</label>
            <input
              type="date"
              value={date}
              onChange={(e) => setDate(e.target.value)}
              required
              min={today}
            />
          </div>

          <div className="form-group">
            <label>Số lượng vé - Trẻ em (280,000 VND/vé):</label>
            <input
              type="number"
              min="0"
              value={childTickets}
              onChange={(e) => setChildTickets(parseInt(e.target.value))}
            />
          </div>

          <div className="form-group">
            <label>Số lượng vé - Người lớn (350,000 VND/vé):</label>
            <input
              type="number"
              min="0"
              value={adultTickets}
              onChange={(e) => setAdultTickets(parseInt(e.target.value))}
            />
          </div>

          <div className="form-group">
            <label>Số lượng vé - Trẻ em dưới 3 tuổi (Miễn phí):</label>
            <input
              type="number"
              min="0"
              value={freeChildTickets}
              onChange={(e) => setFreeChildTickets(parseInt(e.target.value))}
            />
          </div>

          <div className="form-group">
            <label>Tổng giá vé:</label>
            <input
              type="text"
              value={totalPrice.toLocaleString('vi-VN')}
              readOnly
            />
          </div>

          <button type="submit" className="buy-button">Mua vé</button>
        </form>
      </div>

      {/* Group Booking Section */}
      <div className="group-booking-form">
        <h2>Giá vé đoàn</h2>
        <form onSubmit={handleGroupSubmit}>
          <div className="form-group">
            <label>Ngày chơi:</label>
            <input
              type="date"
              value={date}
              onChange={(e) => setDate(e.target.value)}
              required
              min={today}
            />
          </div>
          <div className="form-group">
            <label>Số lượng vé (từ 20 khách):</label>
            <input
              type="number"
              min="0"
              value={groupAdultTickets}
              onChange={(e) => setGroupAdultTickets(parseInt(e.target.value))}
            />
          </div>

          <div className="form-group">
            <label>Tổng giá vé đoàn:</label>
            <input
              type="text"
              value={groupTotalPrice.toLocaleString('vi-VN')}
              readOnly
            />
          </div>

          <button type="submit" className="buy-button">Mua vé đoàn</button>
        </form>
      </div>
    </div>
  );
};

export default BookingForm;
