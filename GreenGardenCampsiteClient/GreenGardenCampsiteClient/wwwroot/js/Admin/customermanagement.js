const customerTableBody = document.getElementById('customerTableBody');
const searchInput = document.getElementById('searchInput');
const createButton = document.getElementById('createButton');

// **Optional function to generate unique IDs**
function generateUniqueID() {
    // Implement your logic to generate unique IDs (e.g., using a counter)
}

// Function to create a new customer row
function createCustomerRow(id, name, account, phone, address) {
    const newRow = document.createElement('tr');
    newRow.innerHTML = `
    <td><span class="math-inline">\{id\}</td\>
<td\></span>{name}</td>
    <td><span class="math-inline">\{account\}</td\>
<td\></span>{phone}</td>
    <td>${address}</td>
    <td>
      <button class="btn btn-primary">Edit</button>
      <button class="btn btn-danger">Block</button>
    </td>
  `;
    customerTableBody.appendChild(newRow);
}

// **Sample customer data (replace with your actual data source)**
const customerData = [
    { id: 1, name: 'John Doe', account: 'john.doe@company.com', phone: '1234567890', address: '123 Main St' },
    { id: 2, name: 'Jane Smith', account: 'jane.smith@company.com', phone: '9876543210', address: '456 Elm St' },
    { id: 3, name: 'Michael Brown', account: 'michael.brown@company.com', phone: '0987654321', address: '789 Oak Ave' },
];

// Function to populate the table with initial data
function populateTable(data) {
    customerTableBody.innerHTML = ''; // Clear existing rows
    for (const customer of data) {
        createCustomerRow(customer.id, customer.name, customer.account, customer.phone, customer.address);
    }
}

// Function to filter customers based on search input
function filterCustomers(searchTerm) {
    const filteredData = customerData.filter(customer => {
        const fullName = customer.name.toLowerCase();
        return fullName.includes(searchTerm.toLowerCase());
    });
    populateTable(filteredData);
}

// Event listener for search input changes
search