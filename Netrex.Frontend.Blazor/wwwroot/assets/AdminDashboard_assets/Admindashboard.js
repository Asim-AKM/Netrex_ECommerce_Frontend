window.ntxNavigation = {
    init: function () {
        const ntxHamburgerBtn = document.querySelector('.ad-hamburger-btn');
        const ntxCloseBtn = document.querySelector('.close-icon');
        const ntxSearchBtn = document.querySelector('.ad-search-btn');
        const ntxSidebar = document.querySelector('.ad-sidebar');
        const ntxSearchContainer = document.querySelector('.ad-search-container');
        const ntxOverlay = document.querySelector('.ad-sidebar-overlay');

        console.log("Navigation Initialized");

        // Open Sidebar
        if (ntxHamburgerBtn) {
            ntxHamburgerBtn.onclick = function () {
                if (ntxSidebar) ntxSidebar.classList.add('open');
                if (ntxOverlay) ntxOverlay.classList.add('active');
            };
        }

        // Close Sidebar
        if (ntxCloseBtn) {
            ntxCloseBtn.onclick = function () {
                if (ntxSidebar) ntxSidebar.classList.remove('open');
                if (ntxOverlay) ntxOverlay.classList.remove('active');
                console.log("close");
            };
        }

        // Overlay click to close
        if (ntxOverlay) {
            ntxOverlay.onclick = function () {
                if (ntxSidebar) ntxSidebar.classList.remove('open');
                if (ntxOverlay) ntxOverlay.classList.remove('active');
            };
        }

        // Search Toggle
        if (ntxSearchBtn && ntxSearchContainer) {
            ntxSearchBtn.addEventListener('click', function () {
                const isVisible =
                    window.getComputedStyle(ntxSearchContainer).display === 'flex';
                ntxSearchContainer.style.display = isVisible ? 'none' : 'flex';
                console.log("Search toggled");
            });
        }

        // ? Sidebar Navigation with smooth scroll
        const sectionMap = {
            'dashboard': 'ad-dashboard',
            'UserManagment': 'ad-user-management',
            'SellerModule': 'ad-seller-module',
            'ProductManagment': 'ad-product-management',
            'customers': 'ad-cart-order',
            'analytics': 'ad-analytics',
            'reports': 'ad-reports',
            'settings': 'ad-settings'
        };

        const navLinks = document.querySelectorAll('.ntx-nav-link');
        navLinks.forEach(function (link) {
            link.addEventListener('click', function (e) {
                e.preventDefault();

                // Update active class
                navLinks.forEach(function (l) { l.classList.remove('active'); });
                link.classList.add('active');

                // Get target from href
                const href = link.getAttribute('href').replace('#', '');
                const sectionId = sectionMap[href];
                const section = document.getElementById(sectionId);

                console.log("Navigating to:", sectionId, section);

                if (section) {
                    section.scrollIntoView({ behavior: 'smooth', block: 'start' });
                }

                // Close sidebar on mobile
                if (window.innerWidth <= 768) {
                    if (ntxSidebar) ntxSidebar.classList.remove('open');
                    if (ntxOverlay) ntxOverlay.classList.remove('active');
                }
            });
        });
    }
};