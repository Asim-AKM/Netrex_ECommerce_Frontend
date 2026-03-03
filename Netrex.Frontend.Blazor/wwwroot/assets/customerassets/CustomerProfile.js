// SpendWise - Customer Profile JavaScript
// Unique naming with 'sp' prefix

class SpendWiseDashboard {
    constructor() {
        this.currentUser = {
            name: 'Saqib Khan',
            email: 'saqibkhanskm1@gmail.com',
            phone: '+92 333 5011154',
            address: '123 Main Street, City, Country',
            dob: '1995-05-15'
        };

        this.cartItems = [
            { id: 1, name: 'Wireless Mouse', price: 25.99, quantity: 1, image: 'https://via.placeholder.com/40x40/04ADB1/fff?text=M' },
            { id: 2, name: 'USB-C Cable', price: 9.99, quantity: 2, image: 'https://via.placeholder.com/40x40/04ADB1/fff?text=C' },
            { id: 3, name: 'Laptop Stand', price: 45.00, quantity: 1, image: 'https://via.placeholder.com/40x40/04ADB1/fff?text=S' }
        ];

        this.init();
    }

    init() {
        this.cacheElements();
        this.attachEvents();
        this.loadUserData();
        this.updateCartDisplay();
        this.setupSidebarNavigation();
    }

    cacheElements() {
        // Navigation
        this.navLinks = document.querySelectorAll('.sp-nav-link[data-section]');
        this.sections = document.querySelectorAll('.sp-section');
        this.logoutBtns = document.querySelectorAll('#spLogoutBtn, #spLogoutBtnDesktop');

        // Avatar upload
        this.avatarUpload = document.getElementById('spAvatarUpload');
        this.desktopAvatar = document.getElementById('spDesktopAvatar');
        this.mobileAvatar = document.getElementById('spMobileAvatar');

        // Profile form
        this.profileForm = document.getElementById('spProfileForm');
        this.updateProfileBtn = document.getElementById('spUpdateProfile');
        this.fullNameInput = document.getElementById('spFullName');
        this.emailInput = document.getElementById('spEmail');
        this.phoneInput = document.getElementById('spPhone');
        this.dobInput = document.getElementById('spDob');
        this.addressInput = document.getElementById('spAddress');

        // Cart elements
        this.cartItemsContainer = document.getElementById('spCartItems');
        this.cartTotalSpan = document.getElementById('spCartTotal');
        this.cartCountSpan = document.getElementById('spCartItemCount');
        this.cartStatValue = document.getElementById('spCartCount');
        this.checkoutBtn = document.getElementById('spCheckoutBtn');

        // Copy buttons
        this.copyButtons = document.querySelectorAll('.sp-btn-copy, .sp-copy-voucher');

        // View all links
        this.viewAllLinks = document.querySelectorAll('.sp-view-all-link');

        // Quick edit
        this.quickEditBtn = document.getElementById('spQuickEdit');

        // Logout modal
        this.logoutModal = new bootstrap.Modal(document.getElementById('spLogoutModal'));
        this.confirmLogout = document.getElementById('spConfirmLogout');
    }

    attachEvents() {
        // Navigation
        this.navLinks.forEach(link => {
            link.addEventListener('click', (e) => this.handleNavigation(e));
        });

        // Avatar upload
        if (this.avatarUpload) {
            this.avatarUpload.addEventListener('change', (e) => this.handleAvatarUpload(e));
        }

        // Update profile
        if (this.updateProfileBtn) {
            this.updateProfileBtn.addEventListener('click', () => this.updateProfile());
        }

        // Remove cart items
        document.addEventListener('click', (e) => {
            if (e.target.closest('.sp-remove-item')) {
                e.preventDefault();
                const removeLink = e.target.closest('.sp-remove-item');
                const itemId = removeLink.dataset.id;
                this.removeCartItem(itemId);
            }
        });

        // Copy voucher codes
        this.copyButtons.forEach(btn => {
            btn.addEventListener('click', (e) => {
                e.preventDefault();
                const code = btn.dataset.code || btn.closest('[data-code]')?.dataset.code;
                if (code) this.copyVoucherCode(code, btn);
            });
        });

        // View all links
        this.viewAllLinks.forEach(link => {
            link.addEventListener('click', (e) => {
                e.preventDefault();
                const section = link.dataset.section;
                if (section) {
                    this.switchToSection(section);
                }
            });
        });

        // Quick edit
        if (this.quickEditBtn) {
            this.quickEditBtn.addEventListener('click', () => {
                this.switchToSection('profile');
            });
        }

        // Logout
        this.logoutBtns.forEach(btn => {
            btn.addEventListener('click', (e) => {
                e.preventDefault();
                this.logoutModal.show();
            });
        });

        if (this.confirmLogout) {
            this.confirmLogout.addEventListener('click', () => {
                this.handleLogout();
            });
        }

        // Checkout
        if (this.checkoutBtn) {
            this.checkoutBtn.addEventListener('click', (e) => {
                e.preventDefault();
                alert('Proceeding to checkout (demo)');
            });
        }

        // Save settings
        const saveSettings = document.getElementById('spSaveSettings');
        if (saveSettings) {
            saveSettings.addEventListener('click', () => {
                alert('Settings saved (demo)');
            });
        }
    }

    setupSidebarNavigation() {
        // Highlight active section based on URL hash or default to dashboard
        const hash = window.location.hash.substring(1) || 'dashboard';
        this.switchToSection(hash);
    }

    handleNavigation(e) {
        e.preventDefault();
        const section = e.currentTarget.dataset.section;
        if (section) {
            this.switchToSection(section);
            // Update URL hash
            window.location.hash = section;

            // Close mobile menu if open
            const mobileOffcanvas = document.getElementById('spMobileMenu');
            if (mobileOffcanvas.classList.contains('show')) {
                const offcanvas = bootstrap.Offcanvas.getInstance(mobileOffcanvas);
                if (offcanvas) offcanvas.hide();
            }
        }
    }

    switchToSection(sectionId) {
        // Hide all sections
        this.sections.forEach(section => {
            section.classList.remove('sp-active-section');
            section.style.display = 'none';
        });

        // Show selected section
        const activeSection = document.getElementById(`${sectionId}Section`);
        if (activeSection) {
            activeSection.style.display = 'block';
            setTimeout(() => {
                activeSection.classList.add('sp-active-section');
            }, 10);
        }

        // Update nav links
        this.navLinks.forEach(link => {
            link.classList.remove('sp-active');
            if (link.dataset.section === sectionId) {
                link.classList.add('sp-active');
            }
        });
    }

    handleAvatarUpload(e) {
        const file = e.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = (event) => {
                // Update both desktop and mobile avatars
                if (this.desktopAvatar) {
                    this.desktopAvatar.src = event.target.result;
                }
                if (this.mobileAvatar) {
                    this.mobileAvatar.src = event.target.result;
                }

                // Also update mobile header avatar
                const mobileHeaderAvatar = document.querySelector('.sp-mobile-avatar img');
                if (mobileHeaderAvatar) {
                    mobileHeaderAvatar.src = event.target.result;
                }

                // Show success message
                this.showToast('Profile picture updated!');
            };
            reader.readAsDataURL(file);
        }
    }

    loadUserData() {
        // Update profile form with current user data
        if (this.fullNameInput) this.fullNameInput.value = this.currentUser.name;
        if (this.emailInput) this.emailInput.value = this.currentUser.email;
        if (this.phoneInput) this.phoneInput.value = this.currentUser.phone;
        if (this.dobInput) this.dobInput.value = this.currentUser.dob;
        if (this.addressInput) this.addressInput.value = this.currentUser.address;

        // Update user name/email in sidebar
        document.querySelectorAll('.sp-user-name').forEach(el => {
            el.textContent = this.currentUser.name;
        });

        document.querySelectorAll('.sp-user-email').forEach(el => {
            el.textContent = this.currentUser.email;
        });
    }

    updateProfile() {
        // Get values from form
        this.currentUser.name = this.fullNameInput.value;
        this.currentUser.email = this.emailInput.value;
        this.currentUser.phone = this.phoneInput.value;
        this.currentUser.dob = this.dobInput.value;
        this.currentUser.address = this.addressInput.value;

        // Update UI
        this.loadUserData();

        // Show success message
        this.showToast('Profile updated successfully!');

        // Switch back to dashboard
        setTimeout(() => {
            this.switchToSection('dashboard');
        }, 1500);
    }

    updateCartDisplay() {
        // Update cart count in stats
        if (this.cartStatValue) {
            this.cartStatValue.textContent = this.cartItems.length;
        }

        // Update cart count in cart summary
        if (this.cartCountSpan) {
            this.cartCountSpan.textContent = this.cartItems.length;
        }

        // Calculate total
        const total = this.cartItems.reduce((sum, item) => sum + (item.price * item.quantity), 0);
        if (this.cartTotalSpan) {
            this.cartTotalSpan.textContent = total.toFixed(2);
        }

        // Update cart items list in dashboard
        if (this.cartItemsContainer) {
            this.renderCartItems();
        }

        // Update cart section
        this.updateCartSection();
    }

    renderCartItems() {
        if (!this.cartItemsContainer) return;

        if (this.cartItems.length === 0) {
            // Show empty cart message
            const emptyCart = document.querySelector('.sp-empty-cart');
            if (emptyCart) emptyCart.style.display = 'block';
            this.cartItemsContainer.innerHTML = '';
            return;
        }

        // Hide empty cart message
        const emptyCart = document.querySelector('.sp-empty-cart');
        if (emptyCart) emptyCart.style.display = 'none';

        // Render items
        let html = '';
        this.cartItems.forEach(item => {
            html += `
                <tr class="sp-cart-row" data-id="${item.id}">
                    <td>
                        <div class="d-flex align-items-center">
                            <img src="${item.image}" alt="${item.name}" class="rounded me-3" width="40">
                            <span>${item.name}</span>
                        </div>
                    </td>
                    <td>$${item.price.toFixed(2)}</td>
                    <td>${item.quantity}</td>
                    <td>$${(item.price * item.quantity).toFixed(2)}</td>
                    <td>
                        <a href="#" class="sp-remove-item" data-id="${item.id}">
                            <i class="fas fa-trash"></i>
                        </a>
                    </td>
                </tr>
            `;
        });

        this.cartItemsContainer.innerHTML = html;
    }

    updateCartSection() {
        const cartSection = document.getElementById('cartSection');
        if (!cartSection) return;

        const cartItemsList = cartSection.querySelector('.sp-cart-items-list');
        const emptyCart = cartSection.querySelector('.sp-empty-cart');

        if (this.cartItems.length === 0) {
            if (emptyCart) emptyCart.style.display = 'block';
            if (cartItemsList) cartItemsList.innerHTML = '';
            return;
        }

        if (emptyCart) emptyCart.style.display = 'none';

        // Render cart items for cart section
        let html = '<div class="sp-cart-items">';
        this.cartItems.forEach(item => {
            html += `
                <div class="sp-order-card mb-3">
                    <div class="row align-items-center">
                        <div class="col-md-8">
                            <h6>${item.name}</h6>
                            <p class="text-muted mb-1">Quantity: ${item.quantity} × $${item.price.toFixed(2)}</p>
                        </div>
                        <div class="col-md-4 text-md-end">
                            <strong>$${(item.price * item.quantity).toFixed(2)}</strong>
                            <button class="btn sp-btn-outline btn-sm ms-2 sp-remove-item" data-id="${item.id}">
                                <i class="fas fa-trash"></i>
                            </button>
                        </div>
                    </div>
                </div>
            `;
        });
        html += `
            <div class="d-flex justify-content-between align-items-center mt-4">
                <h5>Total: $${this.cartItems.reduce((sum, item) => sum + (item.price * item.quantity), 0).toFixed(2)}</h5>
                <button class="btn sp-btn-primary" id="spCheckoutBtnCart">Checkout</button>
            </div>
        `;

        if (cartItemsList) cartItemsList.innerHTML = html;

        // Attach checkout event to cart section button
        const checkoutBtnCart = document.getElementById('spCheckoutBtnCart');
        if (checkoutBtnCart) {
            checkoutBtnCart.addEventListener('click', () => {
                alert('Proceeding to checkout (demo)');
            });
        }
    }

    removeCartItem(itemId) {
        const index = this.cartItems.findIndex(item => item.id == itemId);
        if (index !== -1) {
            this.cartItems.splice(index, 1);
            this.updateCartDisplay();
            this.showToast('Item removed from cart');
        }
    }

    copyVoucherCode(code, button) {
        // Copy to clipboard
        navigator.clipboard.writeText(code).then(() => {
            const originalText = button.textContent;
            button.textContent = 'Copied!';
            button.disabled = true;

            setTimeout(() => {
                button.textContent = originalText;
                button.disabled = false;
            }, 2000);

            this.showToast(`Voucher code ${code} copied!`);
        }).catch(() => {
            // Fallback for older browsers
            const textarea = document.createElement('textarea');
            textarea.value = code;
            document.body.appendChild(textarea);
            textarea.select();
            document.execCommand('copy');
            document.body.removeChild(textarea);

            this.showToast(`Voucher code ${code} copied!`);
        });
    }

    showToast(message) {
        // Create a simple toast notification
        const toast = document.createElement('div');
        toast.className = 'sp-toast';
        toast.innerHTML = `
            <div class="sp-toast-content">
                <i class="fas fa-check-circle me-2" style="color: var(--sp-success);"></i>
                ${message}
            </div>
        `;

        // Style the toast
        toast.style.position = 'fixed';
        toast.style.bottom = '20px';
        toast.style.right = '20px';
        toast.style.backgroundColor = 'var(--sp-white)';
        toast.style.color = 'var(--sp-text-dark)';
        toast.style.padding = '12px 24px';
        toast.style.borderRadius = '50px';
        toast.style.boxShadow = '0 10px 30px rgba(0,0,0,0.1)';
        toast.style.border = '1px solid var(--sp-border)';
        toast.style.zIndex = '9999';
        toast.style.animation = 'spSlideIn 0.3s ease';

        document.body.appendChild(toast);

        setTimeout(() => {
            toast.style.animation = 'spSlideOut 0.3s ease';
            setTimeout(() => {
                document.body.removeChild(toast);
            }, 300);
        }, 3000);
    }

    handleLogout() {
        this.logoutModal.hide();

        // Show logout message
        this.showToast('Logged out successfully!');

        // In a real app, redirect to login page
        setTimeout(() => {
            alert('Redirecting to login page... (demo)');
            // window.location.href = '/login';
        }, 1500);
    }
}

// Initialize dashboard when DOM is ready
document.addEventListener('DOMContentLoaded', () => {
    new SpendWiseDashboard();
});

// Add animation styles dynamically
const style = document.createElement('style');
style.textContent = `
    @keyframes spSlideIn {
        from { transform: translateX(100%); opacity: 0; }
        to { transform: translateX(0); opacity: 1; }
    }
    
    @keyframes spSlideOut {
        from { transform: translateX(0); opacity: 1; }
        to { transform: translateX(100%); opacity: 0; }
    }
`;
document.head.appendChild(style);

// ========== Global Wallet Functions (for onclick attributes) ==========

// Toggle balance visibility
window.toggleBalance = function () {
    const balanceSpan = document.getElementById('spWalletBalance');
    const eyeIcon = document.getElementById('spBalanceEyeIcon');
    const hiddenMessage = document.getElementById('spBalanceHiddenMessage');

    if (!balanceSpan || !eyeIcon) return;

    const isHidden = balanceSpan.getAttribute('data-hidden') === 'true';

    if (isHidden) {
        // SHOW
        const original = balanceSpan.getAttribute('data-original');
        balanceSpan.textContent = original;
        balanceSpan.setAttribute('data-hidden', 'false');
        eyeIcon.className = 'fas fa-eye';
        if (hiddenMessage) hiddenMessage.style.display = 'none';
    } else {
        // First time store original
        if (!balanceSpan.getAttribute('data-original')) {
            balanceSpan.setAttribute('data-original', balanceSpan.textContent);
        }

        // HIDE
        balanceSpan.textContent = '****';
        balanceSpan.setAttribute('data-hidden', 'true');
        eyeIcon.className = 'fas fa-eye-slash';
        if (hiddenMessage) hiddenMessage.style.display = 'block';
    }
};

// Withdraw confirmation
window.withdrawConfirm = function () {
    const amountInput = document.getElementById('spWithdrawAmount');
    if (!amountInput) return;

    const amount = amountInput.value.trim();
    const methodSelect = document.getElementById('spWithdrawMethod');
    const method = methodSelect.options[methodSelect.selectedIndex].text;

    if (!amount || parseFloat(amount) < 10) {
        alert('Please enter a valid amount (minimum $10)');
        return;
    }

    // Close modal
    const modal = bootstrap.Modal.getInstance(document.getElementById('spWithdrawModal'));
    if (modal) modal.hide();

    // Show success toast
    showToast(`Withdrawal request of $${amount} via ${method} submitted successfully!`);

    // Reset form
    amountInput.value = '';
    methodSelect.selectedIndex = 0;
};

window.showToast = function (message) {
    const toast = document.createElement('div');
    toast.className = 'sp-toast';
    toast.innerHTML = '<div class="sp-toast-content"><i class="fas fa-check-circle me-2" style="color: var(--customer-success);"></i>' + message + '</div>';

    toast.style.position = 'fixed';
    toast.style.bottom = '20px';
    toast.style.right = '20px';
    toast.style.backgroundColor = 'var(--customer-white)';
    toast.style.color = 'var(--customer-text-dark)';
    toast.style.padding = '12px 24px';
    toast.style.borderRadius = '50px';
    toast.style.boxShadow = '0 10px 30px rgba(0,0,0,0.1)';
    toast.style.border = '1px solid var(--customer-border)';
    toast.style.zIndex = '9999';
    toast.style.animation = 'spSlideIn 0.3s ease';

    document.body.appendChild(toast);

    setTimeout(() => {
        toast.style.animation = 'spSlideOut 0.3s ease';
        setTimeout(() => {
            document.body.removeChild(toast);
        }, 300);
    }, 3000);
};
