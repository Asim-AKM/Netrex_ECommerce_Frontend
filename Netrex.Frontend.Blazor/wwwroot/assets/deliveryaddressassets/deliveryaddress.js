// Wait for page to load completely
document.addEventListener('DOMContentLoaded', function () {
    // Get the delivery address page container
    const deliveryPage = document.getElementById('delivery-address-page');

    if (!deliveryPage) return; // If page not found, exit

    // Dark / Light Mode Toggle - Scoped to delivery page
    const lightBtn = deliveryPage.querySelector('#light-mode');
    const darkBtn = deliveryPage.querySelector('#dark-mode');

    if (lightBtn) {
        lightBtn.addEventListener('click', () => {
            deliveryPage.style.backgroundColor = '#f3f4f6';
            deliveryPage.style.color = '#333';
            // Also update card background
            const card = deliveryPage.querySelector('.card');
            if (card) card.style.backgroundColor = '#fff';
        });
    }

    if (darkBtn) {
        darkBtn.addEventListener('click', () => {
            deliveryPage.style.backgroundColor = '#0f172a';
            deliveryPage.style.color = '#fff';
            // Also update card background
            const card = deliveryPage.querySelector('.card');
            if (card) card.style.backgroundColor = '#1e293b';
        });
    }

    // Cancel Button - Scoped to delivery page
    const cancelBtn = deliveryPage.querySelector('#cancel-btn');
    if (cancelBtn) {
        cancelBtn.addEventListener('click', () => {
            const addressForm = deliveryPage.querySelector('#address-form');
            if (addressForm) addressForm.reset();
        });
    }

    // Close Button - Scoped to delivery page
    const closeBtn = deliveryPage.querySelector('#close-btn');
    if (closeBtn) {
        closeBtn.addEventListener('click', () => {
            const card = deliveryPage.querySelector('.card');
            if (card) card.style.display = 'none';
        });
    }

    // Form submission handling
    const addressForm = deliveryPage.querySelector('#address-form');
    if (addressForm) {
        addressForm.addEventListener('submit', function (e) {
            e.preventDefault();

            // Get all form values
            const formData = {
                fullName: deliveryPage.querySelector('input[type="text"]:first-of-type')?.value,
                province: deliveryPage.querySelector('select:first-of-type')?.value,
                phone: deliveryPage.querySelector('input[type="tel"]')?.value,
                city: deliveryPage.querySelectorAll('select')[1]?.value,
                building: deliveryPage.querySelectorAll('input[type="text"]')[1]?.value,
                area: deliveryPage.querySelectorAll('select')[2]?.value,
                colony: deliveryPage.querySelectorAll('input[type="text"]')[2]?.value,
                address: deliveryPage.querySelectorAll('input[type="text"]')[3]?.value,
                addressType: deliveryPage.querySelector('input[name="address_type"]:checked')?.value
            };

            console.log('Form submitted:', formData);
            alert('Address saved successfully!');

            // I can add AJAX call here to save data to server
            // fetch('/api/address', {
            //     method: 'POST',
            //     headers: { 'Content-Type': 'application/json' },
            //     body: JSON.stringify(formData)
            // });
        });
    }
});

// Alternative: If you want to use Blazor JS Interop
window.deliveryAddressFunctions = {
    initialize: function () {
        const deliveryPage = document.getElementById('delivery-address-page');
        if (!deliveryPage) return;

        // All event listeners from above can go here
    },

    showToast: function (message) {
        alert(message);
    },

    closeCard: function () {
        const card = document.querySelector('#delivery-address-page .card');
        if (card) card.style.display = 'none';
    }
};