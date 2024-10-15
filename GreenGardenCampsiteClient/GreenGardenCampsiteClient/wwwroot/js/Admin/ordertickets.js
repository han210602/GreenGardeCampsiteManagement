function switchTab(tabId) {
    // Ẩn tất cả nội dung tab
    const contents = document.querySelectorAll('.tab-content');
    contents.forEach(content => content.classList.remove('active'));

    // Bỏ active trên tất cả tab
    const tabs = document.querySelectorAll('.tab');
    tabs.forEach(tab => tab.classList.remove('active'));

    // Hiện tab và nội dung tương ứng
    document.getElementById(tabId).classList.add('active');
    document.querySelector(`.tab[onclick="switchTab('${tabId}')"]`).classList.add('active');
}

// Khởi động với tab Đặt Vé
switchTab('ticket');
