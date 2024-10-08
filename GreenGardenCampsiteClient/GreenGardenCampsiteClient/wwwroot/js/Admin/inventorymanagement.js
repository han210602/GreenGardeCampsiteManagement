
const inventoryData = [
    { id: 103, name: "Thit bo", category: "Food & Drinks", stock: 20, price: 200 },
    { id: 215, name: "Leu nho", category: "Camping Gears", stock: 20, price: 200 },
    { id: 330, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 331, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 332, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 333, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 334, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 335, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 3355, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 336, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 337, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 3377, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 338, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 339, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 3302, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 3301, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 3303, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 3304, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 3305, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 3306, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 3307, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 3308, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 3309, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 312, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 311, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },
    { id: 315, name: "Ve tre em", category: "Tickets", stock: 20, price: 200 },

    
];

const itemsPerPage = 3;
let currentPage = 1;
let totalPages = Math.ceil(inventoryData.length / itemsPerPage);

// Function to load and display paginated data
function loadTableData(page) {
    const start = (page - 1) * itemsPerPage;
    const end = start + itemsPerPage;
    const paginatedData = inventoryData.slice(start, end);
    const inventoryTable = document.getElementById('inventoryTable').getElementsByTagName('tbody')[0];

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

    // Update item count display
    document.getElementById('itemCount').innerText = `Showing ${start + 1} to ${Math.min(end, inventoryData.length)} of ${inventoryData.length} items`;
}

// Function to update pagination controls
function updatePaginationControls() {
    const pageNumbers = document.getElementById('pageNumbers');
    pageNumbers.innerHTML = '';

    for (let i = 1; i <= totalPages; i++) {
        const activeClass = (i === currentPage) ? 'active' : '';
        const pageButton = `<li class="page-item ${activeClass}">
                                <a class="page-link" href="#">${i}</a>
                            </li>`;
        pageNumbers.innerHTML += pageButton;
    }

    // Enable/disable previous and next buttons
    document.getElementById('prevPage').disabled = currentPage === 1;
    document.getElementById('nextPage').disabled = currentPage === totalPages;
}

// Event listener for pagination controls
document.getElementById('pageNumbers').addEventListener('click', function (e) {
    if (e.target.tagName === 'A') {
        currentPage = Number(e.target.textContent);
        loadTableData(currentPage);
        updatePaginationControls();
    }
});

// Previous and Next buttons
document.getElementById('prevPage').addEventListener('click', function () {
    if (currentPage > 1) {
        currentPage--;
        loadTableData(currentPage);
        updatePaginationControls();
    }
});

document.getElementById('nextPage').addEventListener('click', function () {
    if (currentPage < totalPages) {
        currentPage++;
        loadTableData(currentPage);
        updatePaginationControls();
    }
});

// Initial page load
loadTableData(currentPage);
updatePaginationControls();

