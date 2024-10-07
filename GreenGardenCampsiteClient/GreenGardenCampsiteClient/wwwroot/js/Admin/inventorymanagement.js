// Sample data (in a real app, you'd fetch this from the server)
const inventoryData = [
    { id: 103, name: "Thit bo", category: "Food & Drinks", stock: 20, price: 200 },
    { id: 215, name: "Leu nho", category: "Camping Gears", stock: 20, price: 200 },
    { id: 330, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 330, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 330, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 330, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 330, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 330, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 330, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 330, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 330, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 330, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 330, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 330, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 330, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 330, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    // ... Add more items here to test pagination
];

// Variables to control pagination
const itemsPerPage = 6;
let currentPage = 1;
let totalPages = Math.ceil(inventoryData.length / itemsPerPage);

// Function to load and display paginated data
function loadTableData(page) {
    const start = (page - 1) * itemsPerPage;
    const end = start + itemsPerPage;
    const paginatedData = inventoryData.slice(start, end);
    const inventoryTable = document.getElementById('inventoryTable');

    // Clear previous data
    inventoryTable.innerHTML = '';

    // Render new data
    paginatedData.forEach(item => {
        const row = `<tr>
                        <td>${item.id}</td>
                        <td>${item.name}</td>
                        <td>${item.category}</td>
                        <td>${item.stock}</td>
                        <td>${item.price}</td>
                        <td>
                            <button class="btn btn-primary">Edit</button>
                            <button class="btn btn-danger">Delete</button>
                        </td>
                    </tr>`;
        inventoryTable.innerHTML += row;
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
