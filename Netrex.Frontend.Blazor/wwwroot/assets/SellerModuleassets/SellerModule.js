// Seller Profile Data
let sellerProfile = {
    name: 'saqibkhan',
    email: 'saqibkhanskm1@gmail.com',
    phone: '+1-555-0123',
    bio: 'Premium electronics store with quality products',
    image: 'Pictures/WhatsApp Image 2025-12-27 at 09.30.06.jpeg"'
};

// Product Data
let products = [
    {
        id: 1,
        name: 'Wireless Headphones',
        sku: 'WH-001',
        price: 129.99,
        stock: 45,
        image:'Pictures/image.png'
    },
    {
        id: 2,
        name: 'Wireless Keyboard',
        sku: 'USB-002',
        price: 19.99,
        stock: 200,
        image: 'Pictures/image copy.png'
    },
    {
        id: 3,
        name: 'IPhone 16 pro max case',
        sku: 'PC-003',
        price: 24.99,
        stock: 150,
        image: 'Pictures/image copy 2.png'
    },
    {
        id:4,
        name:'Portable HardDrive',
        sku:'PC-004',
        price:12.99,
        stock:25,
         image: 'Pictures/image copy 3.png'
    }
];

// Tab Navigation
function showTab(tabName, event) {
    event.preventDefault();

    // Hide all tabs
    const tabs = document.querySelectorAll('.tab-pane');
    tabs.forEach(tab => tab.classList.remove('active'));

    // Show selected tab
    const selectedTab = document.getElementById(tabName);
    if (selectedTab) {
        selectedTab.classList.add('active');
    }

    // Update nav links
    const navLinks = document.querySelectorAll('.nav-link');
    navLinks.forEach(link => link.classList.remove('active'));
    event.target.closest('.nav-link').classList.add('active');

    // Initialize charts if reports tab
    if (tabName === 'reports') {
        setTimeout(initializeCharts, 500);
    }
}

// Render Products
function renderProducts() {
    const productGrid = document.getElementById('productGrid');
    if (!productGrid) return;

    productGrid.innerHTML = products.map(product => `
        <div class="product-card">
            <div class="product-image">
                <img src="${product.image}" alt="${product.name}">
            </div>
            <div class="product-info">
                <div class="product-name">${product.name}</div>
                <div class="product-sku">SKU: ${product.sku}</div>
                <div class="product-price-row">
                    <div class="product-price">$${product.price}</div>
                    <div class="product-stock">${product.stock} in stock</div>
                </div>
                <div class="product-actions">
                    <button class="btn-sm btn-view">View</button>
                    <button class="btn-sm btn-edit">Edit</button>
                    <button class="btn-sm btn-delete">Delete</button>
                </div>
            </div>
        </div>
    `).join('');

    // Update active products count
    document.getElementById('activeProductsCount').textContent = products.length;
}

// Add Product
function addProduct() {
    const nameInput = document.getElementById('productName');
    const skuInput = document.getElementById('productSKU');
    const priceInput = document.getElementById('productPrice');
    const stockInput = document.getElementById('productStock');
    const imageInput = document.getElementById('productImage');
    const imagePreview = document.getElementById('imagePreview');

    if (!nameInput.value || !skuInput.value || !priceInput.value || !stockInput.value) {
        alert('Please fill all fields');
        return;
    }

    if (!imageInput.files[0]) {
        alert('❌ Product image is mandatory! Please upload an image.');
        return;
    }

    const newProduct = {
        id: products.length + 1,
        name: nameInput.value,
        sku: skuInput.value,
        price: parseFloat(priceInput.value),
        stock: parseInt(stockInput.value),
        image: imagePreview.src
    };

    products.push(newProduct);
    renderProducts();

    // Reset form
    nameInput.value = '';
    skuInput.value = '';
    priceInput.value = '';
    stockInput.value = '';
    imageInput.value = '';
    imagePreview.src = '';
    imagePreview.style.display = 'none';

    bootstrap.Modal.getInstance(document.getElementById('addProductModal')).hide();
    alert('✅ Product added successfully!');
}

// Preview Product Image
function previewProductImage(event) {
    const file = event.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function(e) {
            const preview = document.getElementById('imagePreview');
            preview.src = e.target.result;
            preview.style.display = 'block';
        };
        reader.readAsDataURL(file);
    }
}

// Profile Image Preview
function previewProfileImage(event) {
    const file = event.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function(e) {
            document.getElementById('previewProfileImg').src = e.target.result;
        };
        reader.readAsDataURL(file);
    }
}

// Save Profile Changes
function saveProfileChanges() {
    const name = document.getElementById('editSellerName').value;
    const email = document.getElementById('editSellerEmail').value;
    const phone = document.getElementById('editSellerPhone').value;
    const bio = document.getElementById('editSellerBio').value;
    const imgSrc = document.getElementById('previewProfileImg').src;

    if (name) sellerProfile.name = name;
    if (email) sellerProfile.email = email;
    if (phone) sellerProfile.phone = phone;
    if (bio) sellerProfile.bio = bio;
    if (imgSrc) sellerProfile.image = imgSrc;

    // Update profile display
    document.getElementById('profileImg').src = sellerProfile.image;
    document.getElementById('sellerName').textContent = sellerProfile.name;
    document.getElementById('sellerEmail').textContent = sellerProfile.email;
    document.getElementById('sellerPhone').textContent = sellerProfile.phone;

    bootstrap.Modal.getInstance(document.getElementById('editProfileModal')).hide();
    alert('✅ Profile updated successfully!');
}

// Populate edit form
document.addEventListener('DOMContentLoaded', function() {
    const editProfileModal = document.getElementById('editProfileModal');
    if (editProfileModal) {
        editProfileModal.addEventListener('show.bs.modal', function() {
            document.getElementById('editSellerName').value = sellerProfile.name;
            document.getElementById('editSellerEmail').value = sellerProfile.email;
            document.getElementById('editSellerPhone').value = sellerProfile.phone;
            document.getElementById('editSellerBio').value = sellerProfile.bio;
            document.getElementById('previewProfileImg').src = sellerProfile.image;
        });
    }
});

// Charts Configuration
let salesChart, revenueChart, productChart, categoryChart;

function initializeCharts() {
    // Sales Chart (Line Chart)
    const salesCtx = document.getElementById('salesChart');
    if (salesCtx) {
        if (salesChart) salesChart.destroy();
        salesChart = new Chart(salesCtx, {
            type: 'line',
            data: {
                labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
                datasets: [{
                    label: 'Orders',
                    data: [45, 52, 48, 65, 72, 85],
                    borderColor: '#04ADB1',
                    backgroundColor: 'rgba(4, 173, 177, 0.1)',
                    borderWidth: 3,
                    fill: true,
                    tension: 0.4,
                    pointBackgroundColor: '#04ADB1',
                    pointBorderColor: '#fff',
                    pointBorderWidth: 2,
                    pointRadius: 5
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        display: true,
                        labels: { usePointStyle: true, padding: 20 }
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        grid: { color: '#e2e8f0' }
                    },
                    x: {
                        grid: { display: false }
                    }
                }
            }
        });
    }

    // Revenue Chart (Area Chart)
    const revenueCtx = document.getElementById('revenueChart');
    if (revenueCtx) {
        if (revenueChart) revenueChart.destroy();
        revenueChart = new Chart(revenueCtx, {
            type: 'line',
            data: {
                labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
                datasets: [{
                    label: 'Revenue ($)',
                    data: [3500, 4200, 3800, 5100, 5800, 6750],
                    borderColor: '#0C284D',
                    backgroundColor: 'rgba(12, 40, 77, 0.1)',
                    borderWidth: 3,
                    fill: true,
                    tension: 0.4,
                    pointBackgroundColor: '#0C284D',
                    pointBorderColor: '#fff',
                    pointBorderWidth: 2,
                    pointRadius: 5
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        display: true,
                        labels: { usePointStyle: true, padding: 20 }
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        grid: { color: '#e2e8f0' }
                    },
                    x: {
                        grid: { display: false }
                    }
                }
            }
        });
    }

    // Product Performance Chart (Bar Chart)
    const productCtx = document.getElementById('productChart');
    if (productCtx) {
        if (productChart) productChart.destroy();
        productChart = new Chart(productCtx, {
            type: 'bar',
            data: {
                labels: ['Wireless Headphones', 'USB-C Cable', 'Phone Case', 'Screen Protector', 'Charger'],
                datasets: [{
                    label: 'Sales',
                    data: [342, 1200, 678, 456, 823],
                    backgroundColor: [
                        '#04ADB1',
                        '#0C284D',
                        '#10b981',
                        '#f59e0b',
                        '#ef4444'
                    ],
                    borderRadius: 8,
                    borderSkipped: false
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                indexAxis: 'y',
                plugins: {
                    legend: { display: false }
                },
                scales: {
                    x: {
                        beginAtZero: true,
                        grid: { color: '#e2e8f0' }
                    },
                    y: {
                        grid: { display: false }
                    }
                }
            }
        });
    }

    // Category Chart (Pie Chart)
    const categoryCtx = document.getElementById('categoryChart');
    if (categoryCtx) {
        if (categoryChart) categoryChart.destroy();
        categoryChart = new Chart(categoryCtx, {
            type: 'doughnut',
            data: {
                labels: ['Electronics', 'Accessories', 'Cables', 'Protective Gear'],
                datasets: [{
                    data: [35, 28, 22, 15],
                    backgroundColor: [
                        '#04ADB1',
                        '#0C284D',
                        '#10b981',
                        '#f59e0b'
                    ],
                    borderColor: '#fff',
                    borderWidth: 2
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        position: 'bottom',
                        labels: { padding: 15 }
                    }
                }
            }
        });
    }
}

// Download Report
function downloadReport() {
    alert('📥 Report downloaded successfully!');
}

// Initialize on page load
document.addEventListener('DOMContentLoaded', function() {
    renderProducts();
});
