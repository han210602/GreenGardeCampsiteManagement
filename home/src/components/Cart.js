import React from 'react';

const Cart = ({ cartItems, removeFromCart }) => {
  return (
    <div className="cart">
      <h3>Kiểm tra thông tin đặt hàng</h3>
      {cartItems.length > 0 ? (
        cartItems.map((item, index) => (
          <div key={index} className="cart-item">
            <p>Loại vé: {item.ticketType === 'children' ? 'Trẻ em' : item.ticketType === 'adults' ? 'Người lớn' : 'Trẻ em dưới 3 tuổi (Miễn phí)'}</p>
            <p>Số lượng: {item.totalTickets}</p>
            <p>Giá vé: {item.ticketType === 'free' ? '0₫' : `${item.totalPrice}₫`}</p>
            <p>Thông tin thời gian: {item.date}</p>
            <p>Tạm tính: {item.totalPrice}₫</p>
            {/* Add a button to remove this item from the cart */}
            <button onClick={() => removeFromCart(index)}>Xóa</button>
          </div>
        ))
      ) : (
        <p>Không có vé được chọn</p>
      )}
    </div>
  );
};

export default Cart;
