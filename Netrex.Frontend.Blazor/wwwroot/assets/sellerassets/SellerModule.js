// ============ SELLER PROFILE DATA ============
let sellerProfile = {
    name: 'saqibkhan',
    email: 'saqibkhanskm1@gmail.com',
    phone: '0333501115',
    bio: 'Premium electronics store with quality products',
    image: '/assets/sellerassets/Pictures/MyImage.png'
};

// ============ TAB NAVIGATION ============
function showTab(tabName, event) {
    event.preventDefault();
    document.querySelectorAll('.tab-pane').forEach(tab => { tab.classList.remove('active'); });
    const selectedTab = document.getElementById(tabName);
    if (selectedTab) selectedTab.classList.add('active');

    document.querySelectorAll('.nav-link').forEach(link => link.classList.remove('active'));
    event.target.closest('.nav-link').classList.add('active');

    // Charts initialize only
    if (tabName === 'reports') setTimeout(initializeCharts, 300);
}

// ============ PROFILE IMAGE PREVIEW ============
function previewProfileImage(event) {
    const file = event.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            document.getElementById('previewProfileImg').src = e.target.result;
        };
        reader.readAsDataURL(file);
    }
}

// ============ SAVE PROFILE CHANGES ============
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

    document.getElementById('profileImg').src = sellerProfile.image;
    document.getElementById('sellerName').textContent = sellerProfile.name;
    document.getElementById('sellerEmail').textContent = sellerProfile.email;
    document.getElementById('sellerPhone').textContent = sellerProfile.phone;

    bootstrap.Modal.getInstance(document.getElementById('editProfileModal')).hide();
    alert('✅ Profile updated successfully!');
}

// ============ TOGGLE SIDEBAR ============
function toggleSidebar() {
    document.querySelector('.sidebar').classList.toggle('active');
}

// ============ CHARTS CONFIGURATION ============
let salesChart, revenueChart, productChart, categoryChart;

function initializeCharts() {
    // Sales Chart
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

    // Revenue Chart
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

    // Product Performance Chart
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

    // Category Chart
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

// ============ DOWNLOAD REPORT ============
function downloadReport() {
    alert('📥 Report downloaded successfully!');
}

// ============ INITIALIZE ON PAGE LOAD ============
document.addEventListener('DOMContentLoaded', function () {
    // Edit Profile Modal setup
    const editProfileModal = document.getElementById('editProfileModal');
    if (editProfileModal) {
        editProfileModal.addEventListener('show.bs.modal', function () {
            document.getElementById('editSellerName').value = sellerProfile.name;
            document.getElementById('editSellerEmail').value = sellerProfile.email;
            document.getElementById('editSellerPhone').value = sellerProfile.phone;
            document.getElementById('editSellerBio').value = sellerProfile.bio;
            document.getElementById('previewProfileImg').src = sellerProfile.image;
        });
    }

    // Initialize charts if reports tab is active
    if (document.getElementById('reports')?.classList.contains('active')) {
        setTimeout(initializeCharts, 300);
    }
});


window.scrollToElement = (elementId) => {
    const element = document.getElementById(elementId);
    if (element) {
        element.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
};

let searchTimeout;
window.setupSearchDebounce = (dotNetHelper) => {
    const searchInput = document.querySelector('.topbar-search');
    if (searchInput) {
        searchInput.addEventListener('input', function (e) {
            clearTimeout(searchTimeout);
            searchTimeout = setTimeout(() => {
                dotNetHelper.invokeMethodAsync('OnSearchChanged', e.target.value);
            }, 300); 
        });
    }
};