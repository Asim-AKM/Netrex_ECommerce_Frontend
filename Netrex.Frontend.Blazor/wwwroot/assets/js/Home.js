// Main initialization function
function initializeHomePage() {
    // Scroll effect
    const nav = document.querySelector("nav");
    if (nav) {
        window.addEventListener("scroll", () => {
            nav.classList.toggle("scrolled", window.scrollY > 10);
        });
    }

    // Category dropdown
    // Category dropdown – STRICT hover on button + list only
    let categBtn = document.querySelector(".Category-btn");
    let categoryList = document.querySelector(".Categories-list");

    if (categBtn && categoryList) {

        // SHOW when hovering button
        categBtn.addEventListener("mouseenter", () => {
            categoryList.classList.add("Categories-list-show");
        });

        // KEEP OPEN when hovering category list
        categoryList.addEventListener("mouseenter", () => {
            categoryList.classList.add("Categories-list-show");
        });

        // HIDE when leaving button
        categBtn.addEventListener("mouseleave", () => {
            setTimeout(() => {
                if (!categoryList.matches(":hover")) {
                    categoryList.classList.remove("Categories-list-show");
                }
            }, 100);
        });

        // HIDE when leaving category list
        categoryList.addEventListener("mouseleave", () => {
            categoryList.classList.remove("Categories-list-show");
        });
    }


    // Slider functionality
    let flag = 0;
    let slideInterval;

    function slideControler(val) {
        flag += val;
        slideHandle(flag);
    }

    function slideHandle(num) {
        let allSlide = document.querySelectorAll(".slide");
        if (allSlide.length === 0) return;

        if (num == allSlide.length) {
            flag = 0;
            num = 0;
        }
        if (num < 0) {
            flag = allSlide.length - 1;
            num = allSlide.length - 1;
        }
        for (const sl of allSlide) {
            sl.style.display = "none";
        }
        allSlide[num].style.display = "block";
    }

    // Initialize slider if slides exist
    if (document.querySelectorAll(".slide").length > 0) {
        slideHandle(flag);

        // Clear any existing interval
        if (window.slideInterval) {
            clearInterval(window.slideInterval);
        }

        // Set new interval
        window.slideInterval = setInterval(() => {
            flag++;
            slideHandle(flag);
        }, 8000);

        // Add arrow click handlers
        let prevArrow = document.querySelector(".arrow.prev");
        let nextArrow = document.querySelector(".arrow.next");

        if (prevArrow) {
            prevArrow.onclick = () => slideControler(-1);
        }
        if (nextArrow) {
            nextArrow.onclick = () => slideControler(1);
        }
    }

    // Reviews functionality
    let reviews = [
        {
            Image: "assets/clients-images/clients.jpeg",
            description: "Amazing shopping experience! The products were exactly as shown, delivery was super fast, and customer support was very helpful. I'll definitely shop again.",
            cust_name: "Affan Wajid"
        },
        {
            Image: "",
            description: "Highly recommended! Product quality exceeded my expectations, and the team was very responsive to my questions.",
            cust_name: "Saqib Khan"
        },
        {
            Image: "",
            description: "Fast delivery, original products, and great support. This website has become my go-to place for online shopping.",
            cust_name: "Fayaz Khan"
        },
    ];

    let rightArrow = document.querySelector(".right-arrow");
    let leftArrow = document.querySelector(".left-arrow");
    let cust_image = document.querySelector(".cust-image");
    let user_review = document.querySelector("#user-review");
    let user_Name = document.querySelector(".user-name");
    let index = 0;

    function showReviews() {
        if (!user_review || !user_Name) return;
        let user = reviews[index];
        if (cust_image) cust_image.src = user.Image;
        user_review.textContent = user.description;
        user_Name.innerHTML = user.cust_name;
    }

    if (rightArrow && leftArrow) {
        // Clone to remove old event listeners
        let newRightArrow = rightArrow.cloneNode(true);
        let newLeftArrow = leftArrow.cloneNode(true);
        rightArrow.parentNode.replaceChild(newRightArrow, rightArrow);
        leftArrow.parentNode.replaceChild(newLeftArrow, leftArrow);

        newRightArrow.addEventListener("click", () => {
            index++;
            if (index > reviews.length - 1) {
                index = 0;
            }
            showReviews();
        });

        newLeftArrow.addEventListener("click", () => {
            index--;
            if (index < 0) {
                index = reviews.length - 1;
            }
            showReviews();
        });

        showReviews();
    }

    // Cart functionality
    let cartBtn = document.querySelectorAll(".add-cart");
    let cartCounter = document.querySelector(".counter");
    let counterLabel = document.querySelector(".cart-count");

    // Initialize cart count from storage if available
    if (!window.cartCount) {
        window.cartCount = 0;
    }

    if (cartCounter && counterLabel) {
        cartCounter.innerHTML = window.cartCount;
        if (window.cartCount > 0) {
            counterLabel.style.display = "flex";
        }
    }

    cartBtn.forEach((btn) => {
        // Clone to remove old listeners
        let newBtn = btn.cloneNode(true);
        btn.parentNode.replaceChild(newBtn, btn);

        newBtn.addEventListener("click", () => {
            if (counterLabel) counterLabel.style.display = "flex";
            window.cartCount++;
            if (cartCounter) cartCounter.innerHTML = window.cartCount;
        });
    });
}

// Run on initial load
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', initializeHomePage);
} else {
    initializeHomePage();
}

// Listen for Blazor enhanced navigation
if (typeof Blazor !== 'undefined') {
    Blazor.addEventListener('enhancedload', () => {
        console.log('Page navigated - reinitializing...');
        initializeHomePage();
    });
}

// Fallback for regular navigation
window.addEventListener('popstate', () => {
    setTimeout(initializeHomePage, 100);
});