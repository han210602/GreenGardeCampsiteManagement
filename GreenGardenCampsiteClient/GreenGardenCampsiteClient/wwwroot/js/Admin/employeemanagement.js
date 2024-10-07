const employeeTableBody = document.getElementById('employeeTableBody');
const searchInput = document.getElementById('searchInput');
const createButton = document.getElementById('createButton');

// **Optional function to generate unique IDs**
function generateUniqueID() {
    // Implement your logic to generate unique IDs (e.g., using a counter)
}

// Function to create a new employee row
function createEmployeeRow(id, name, account, phone) {
    const newRow = document.createElement('tr');
    newRow.innerHTML = `
    <td><span class="math-inline">\{id\}</td\>
<td\></span>{name}</td>
    <td><span class="math-inline">\{account\}</td\>
<td\></span>{phone}</td>
    <td>
      <button class="btn btn-primary">Edit</button>
      <button class="btn btn-danger">Delete</button>
    </td>
  `;
    employeeTableBody.appendChild(newRow);
}

// **Sample employee data (replace with your actual data source)**
const employeeData = [
    { id: 1, name: 'John Doe', account: 'john.doe@company.com', phone: '1234567890' },
    { id: 2, name: 'Jane Smith', account: 'jane.smith@company.com', phone: '9876543210' },
    { id: 3, name: 'Michael Brown', account: 'michael.brown@company.com', phone: '0987654321' },
];

// Function to populate the table with initial data
function populateTable(data) {
    employeeTableBody.innerHTML = ''; // Clear existing rows
    for (const employee of data) {
        createEmployeeRow(employee.id, employee.name, employee.account, employee.phone);
    }
}

// Function to filter employees based on search input
function filterEmployees(searchTerm) {
    const filteredData = employeeData.filter(employee => {
        const fullName = employee.name.toLowerCase();
        return fullName.includes(searchTerm.toLowerCase());
    });
    populateTable(filteredData);
}

// Event listener for search input changes
searchInput.addEventListener('keyup', () => {
    const searchTerm = searchInput.value.trim();
    filterEmployees(searchTerm);
});

// Call populateTable to display