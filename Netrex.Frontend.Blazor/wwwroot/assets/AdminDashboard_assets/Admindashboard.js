// Hamburger menu toggle
const hamburgerBtn = document.querySelector('.ad-hamburger-btn');
const sidebar = document.querySelector('.ad-sidebar');

hamburgerBtn.addEventListener('click', () => {
    sidebar.classList.toggle('open');
});

// Search bar toggle on mobile
const searchBtn = document.querySelector('.ad-search-btn');
const searchContainer = document.querySelector('.ad-search-container');

searchBtn.addEventListener('click', () => {
    searchContainer.style.display = searchContainer.style.display === 'flex' ? 'none' : 'flex';
});

// Close sidebar when clicking a menu item on mobile
document.querySelectorAll('.ad-sidebar ul li a').forEach(link => {
    link.addEventListener('click', () => {
        sidebar.classList.remove('open');
    });
});
