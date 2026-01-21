document.addEventListener("DOMContentLoaded", function () {
    const sidebar = document.querySelector('.ad-sidebar');
    const toggleBtn = document.getElementById('sidebarToggle');

    document.addEventListener("click", function (e) {
        // 1. Hamburger toggle logic (using closest to catch icon clicks)
        if (e.target.closest('#sidebarToggle')) {
            sidebar.classList.toggle('open');
            console.log("Sidebar Status: ", sidebar.classList.contains('open') ? "Open" : "Closed");
        }
        // 2. Sidebar ke bahar click karne par band ho jaye
        else if (sidebar.classList.contains('open') && !sidebar.contains(e.target)) {
            sidebar.classList.remove('open');
        }
    });
});