

// Sample employee data
const employeeData = [
    { id: 1, name: 'John Doe', account: 'john.doe@company.com', phone: '1234567890' },
    { id: 2, name: 'Jane Smith', account: 'jane.smith@company.com', phone: '9876543210' },
    { id: 3, name: 'Michael Brown', account: 'michael.brown@company.com', phone: '0987654321' },
    { id: 4, name: 'Alice Johnson', account: 'alice.johnson@company.com', phone: '6543210987' },
    { id: 5, name: 'Tom Hanks', account: 'tom.hanks@company.com', phone: '3216549870' },
    { id: 6, name: 'Emma Watson', account: 'emma.watson@company.com', phone: '1122334455' },
    { id: 7, name: 'Chris Evans', account: 'chris.evans@company.com', phone: '6677889900' },
    { id: 8, name: 'Scarlett Johansson', account: 'scarlett.johansson@company.com', phone: '4433221100' }
];

// Pagination variables
let currentPage = 1;
const itemsPerPage = 6; // Change this value to display more/less items per page
const totalPages = Math.ceil(employeeData.length / itemsPerPage);

// Function to load and display paginated data
function loadTableData(page) {
    const start = (page - 1) * itemsPerPage;
    const end = start + itemsPerPage;
    const paginatedData = employeeData.slice(start, end);
    const employeeTableBody = document.getElementById('employeeTableBody');

    // Clear previous data
    employeeTableBody.innerHTML = '';

    // Render new data
    paginatedData.forEach(item => {
        const row = `<tr>
                        <td>${item.id}</td>
                        <td>${item.name}</td>
                        <td>${item.account}</td>
                        <td>${item.phone}</td>
                        <td>
                            <button class="btn btn-primary">Edit</button>
                            <button class="btn btn-danger">Delete</button>
                        </td>
                    </tr>`;
        employeeTableBody.innerHTML += row;
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