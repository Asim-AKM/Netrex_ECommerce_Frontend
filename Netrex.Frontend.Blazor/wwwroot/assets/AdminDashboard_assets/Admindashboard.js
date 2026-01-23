window.ntxNavigation = {
    init: function () {
        const ntxHamburgerBtn = document.querySelector('.ad-hamburger-btn');
        const ntxCloseBtn = document.querySelector('.close-icon'); // Naya button
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

        // Close Sidebar (Icon par click karne se)
        if (ntxCloseBtn) {
            ntxCloseBtn.onclick = function () {
                if (ntxSidebar) ntxSidebar.classList.remove('open');
                if (ntxOverlay) ntxOverlay.classList.remove('active');
                console.log("close");
            };
        }

        // Overlay par click karne se bhi band ho jaye
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
    }
};