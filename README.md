# Netrex E-Commerce Frontend

Netrex Frontend is a modern, responsive, and highly interactive user interface designed for the Netrex E-Commerce ecosystem. Built using modern frontend best practices, it provides a seamless shopping experience for customers and a robust dashboard for store administrators.

The application connects to the secure **Netrex .NET Clean Architecture Web API** to manage authentication, products, shopping carts, and order processing.

---

## 🚀 Key Features

* **Responsive Shopping UI:** Fully optimized for mobile, tablet, and desktop screens.
* **Product Catalog & Advanced Filters:** Dynamic sorting, category filtering, and real-time search functionality.
* **State-Managed Shopping Cart:** Efficient client-side cart management (Add to cart, update quantities, persistent storage).
* **Secure Token Authentication (JWT):** Handles login, signup, and maintains secure user sessions via access tokens.
* **Implicit Data Binding (Security):** Designed to match backend security compliance. Sensitive identifiers like `UserId` are never explicitly exposed or sent via client-side request bodies (payloads); instead, the app relies securely on the claims embedded inside the JWT token.
* **Role-Based Views:** Conditional rendering of administrative tools vs. standard customer features based on user roles.

---

## 🛠️ Tech Stack

* **Core Framework:** React.js / Next.js / Angular / Vue.js *(Note: Keep the one you are using)*
* **Styling & UI Components:** Tailwind CSS / Bootstrap / Material UI
* **State Management:** Redux Toolkit / Context API / Pinia
* **API Client:** Axios / Fetch API
* **Build Tool & Bundler:** Vite / Webpack

---

## 📂 Project Architecture & Directory Structure

The project follows a component-driven, modular folder structure for maximum maintainability:

```text
Netrex_ECommerce_Frontend/
│
├── public/                 # Static assets (Images, Icons, Fonts)
├── src/
│   ├── assets/             # Global styles and media files
│   ├── components/         # Reusable UI components (Navbar, Buttons, ProductCards)
│   ├── context/ /store/    # State management configurations (Auth, Cart states)
│   ├── hooks/              # Custom React hooks for shared logic
│   ├── pages/ /views/      # Routed page components (Home, Cart, Checkout, Profile)
│   ├── services/           # API service layer (Axios configurations, API endpoints)
│   ├── utils/              # Helper functions and validators
│   ├── App.js / App.tsx    # Root application component
│   └── main.js             # Application entry point
└── package.json            # Scripts and dependencies

git clone [https://github.com/Asim-AKM/Netrex_ECommerce_Frontend.git](https://github.com/Asim-AKM/Netrex_ECommerce_Frontend.git)
cd Netrex_ECommerce_Frontend

npm install
# or
yarn install

VITE_API_BASE_URL=https://localhost:xxxx/api
# or if using Next.js / React App:
# REACT_APP_API_BASE_URL=https://localhost:xxxx/api
