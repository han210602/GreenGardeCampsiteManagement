const orderData = [
    { id: 1, customer: 'Hau Nguyen', employee: 'Hai Ha', orderDate: '12/01/2024', preDeposit: 600, totalAmount: 1000, remaining: 400, status: 'Chua chot', activity: 'Not yet' },
    { id: 2, customer: 'John Doe', employee: 'Jane Smith', orderDate: '15/01/2024', preDeposit: 700, totalAmount: 1200, remaining: 500, status: 'Da chot', activity: 'Checkin' },
    { id: 3, customer: 'Alice Johnson', employee: 'Tom Hanks', orderDate: '18/01/2024', preDeposit: 500, totalAmount: 900, remaining: 400, status: 'Chua chot', activity: 'Not yet' },
    { id: 4, customer: 'Bruce Wayne', employee: 'Clark Kent', orderDate: '20/01/2024', preDeposit: 800, totalAmount: 1300, remaining: 500, status: 'Huy', activity: 'Checkout' },
    { id: 4, customer: 'Bruce Wayne', employee: 'Clark Kent', orderDate: '20/01/2024', preDeposit: 800, totalAmount: 1300, remaining: 500, status: 'Huy', activity: 'Checkout' },
    { id: 4, customer: 'Bruce Wayne', employee: 'Clark Kent', orderDate: '20/01/2024', preDeposit: 800, totalAmount: 1300, remaining: 500, status: 'Huy', activity: 'Checkout' },
    { id: 4, customer: 'Bruce Wayne', employee: 'Clark Kent', orderDate: '20/01/2024', preDeposit: 800, totalAmount: 1300, remaining: 500, status: 'Huy', activity: 'Checkout' },
    { id: 4, customer: 'Bruce Wayne', employee: 'Clark Kent', orderDate: '20/01/2024', preDeposit: 800, totalAmount: 1300, remaining: 500, status: 'Huy', activity: 'Checkout' },
    { id: 4, customer: 'Bruce Wayne', employee: 'Clark Kent', orderDate: '20/01/2024', preDeposit: 800, totalAmount: 1300, remaining: 500, status: 'Huy', activity: 'Checkout' },
    { id: 4, customer: 'Bruce Wayne', employee: 'Clark Kent', orderDate: '20/01/2024', preDeposit: 800, totalAmount: 1300, remaining: 500, status: 'Huy', activity: 'Checkout' },
    { id: 4, customer: 'Bruce Wayne', employee: 'Clark Kent', orderDate: '20/01/2024', preDeposit: 800, totalAmount: 1300, remaining: 500, status: 'Huy', activity: 'Checkout' },
    // Add more sample data as needed
];

// Variables to control pagination
const itemsPerPage = 6;
let currentPage = 1;
let totalPages = Math.ceil(orderData.length / itemsPerPage);

// Function to load and display paginated data
function loadTableData(page) {
    const start = (page - 1) * itemsPerPage;
    const end = start + itemsPerPage;
    const paginatedData = orderData.slice(start, end);
    const orderTableBody = document.getElementById('orderTableBody');

    // Clear previous data
    orderTableBody.innerHTML = '';

    // Render new data
    paginatedData.forEach(item => {
        const row = `<tr>
                        <td>${item.id}</td>
                        <td>${item.customer}</td>
                        <td>${item.employee}</td>
                        <td>${item.orderDate}</td>
                        <td>${item.preDeposit}</td>
                        <td>${item.totalAmount}</td>
                        <td>${item.remaining}</td>
                        <td>${item.status}</td>
                        <td>${item.activity}</td>
                        <td>
                            <a class="btn btn-primary">Edit</a>
                            <button class="btn btn-danger">Delete</button>
                        </td>
                    </tr>`;
        orderTableBody.innerHTML += row;
    });
}

// Function to update pagination controls
function updatePaginationControls() {
    const paginationControls = document.getElementById('paginationControls');
    paginationControls.innerHTML = '';

    for (let i = 1; i <= totalPages; i++) {
        const activeClass = (i === currentPage) ? 'active' : '';
        const pageButton = `<li class="page-item ${activeClass}">
                                <a class="page-link" href="#">${i}</a>
                            </li>`;
        paginationControls.innerHTML += pageButton;
    }
}

// Event listener for pagination controls
document.getElementById('paginationControls').addEventListener('click', function (e) {
    if (e.target.tagName === 'A') {
        currentPage = Number(e.target.textContent);
        loadTableData(currentPage);
        updatePaginationControls();
    }
});

// Initial page load
loadTableData(currentPage);
updatePaginationControls();