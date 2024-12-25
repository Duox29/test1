// Khởi tạo danh sách đơn hàng, sản phẩm và danh mục
let orders = [];
let products = [];
let categories = [];

// Hàm khởi tạo trang
function init() {
    loadProductsFromLocalStorage();
    loadOrdersFromLocalStorage();
    loadCategoriesFromLocalStorage();
    renderProducts();
    renderOrders();
    renderCategories();
    renderRevenue();
}

// Hàm tải sản phẩm từ localStorage
function loadProductsFromLocalStorage() {
    const storedProducts = localStorage.getItem('products');
    if (storedProducts) {
        products = JSON.parse(storedProducts);
    }
}

// Hàm tải đơn hàng từ localStorage
function loadOrdersFromLocalStorage() {
    const storedOrders = localStorage.getItem('orders');
    if (storedOrders) {
        orders = JSON.parse(storedOrders);
    }
}

// Hàm tải danh mục từ localStorage
function loadCategoriesFromLocalStorage() {
    const storedCategories = localStorage.getItem('categories');
    if (storedCategories) {
        categories = JSON.parse(storedCategories);
    }
}

// Hàm render danh sách sản phẩm
function renderProducts() {
    const productTableBody = document.getElementById('product-table-body');
    productTableBody.innerHTML = '';
    products.forEach(product => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${product.name}</td>
            <td>${product.price}</td>
            <td><img src="${product.image}" alt="${product.name}" width="50"></td>
            <td>${product.details}</td>
            <td>${product.category}</td>
            <td>
                <button onclick="editProduct('${product.id}')">Sửa</button>
                <button onclick="deleteProduct('${product.id}')">Xóa</button>
            </td>
        `;
        productTableBody.appendChild(row);
    });
}

// Hàm render danh sách đơn hàng
function renderOrders() {
    const orderTableBody = document.getElementById('order-table-body');
    orderTableBody.innerHTML = '';
    orders.forEach(order => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${order.id}</td>
            <td>${order.customerName}</td>
            <td>${order.totalPrice}</td>
            <td>${order.status}</td>
            <td>
                <button onclick="deleteOrder('${order.id}')">Xóa</button>
                <button onclick="updateOrderStatus('${order.id}')">Cập nhật trạng thái</button>
            </td>
        `;
        orderTableBody.appendChild(row);
    });
}

// Hàm render doanh thu
function renderRevenue() {
    const revenueDisplay = document.getElementById('revenue-display');
    const totalRevenue = orders.reduce((total, order) => total + order.totalPrice, 0);
    revenueDisplay.textContent = `Tổng doanh thu: ${totalRevenue.toLocaleString()} VNĐ`;
}

// Hàm render danh sách danh mục
function renderCategories() {
    const categoryList = document.getElementById('category-list');
    categoryList.innerHTML = '';
    categories.forEach(category => {
        const listItem = document.createElement('li');
        listItem.textContent = category.name;
        categoryList.appendChild(listItem);
    });
}

// Hàm thêm sản phẩm
document.getElementById('product-form').addEventListener('submit', function(event) {
    event.preventDefault();
    const productId = document.getElementById('product-id').value;
    const productName = document.getElementById('product-name').value;
    const productPrice = parseFloat(document.getElementById('product-price').value);
    const productImage = document.getElementById('product-image').value;
    const productDetails = document.getElementById('product-details').value;
    const productCategory = document.getElementById('product-category').value;

    if (productId) {
        // Cập nhật sản phẩm
        const productIndex = products.findIndex(product => product.id === productId);
        products[productIndex] = { id: productId, name: productName, price: productPrice, image: productImage, details: productDetails, category: productCategory };
    } else {
        // Thêm sản phẩm mới
        const newProduct = { id: Date.now().toString(), name: productName, price: productPrice, image: productImage, details: productDetails, category : productCategory };
        products.push(newProduct);
    }

    saveProductsToLocalStorage();
    renderProducts();
    this.reset(); // Reset form fields
});

// Hàm xóa sản phẩm
function deleteProduct(productId) {
    products = products.filter(product => product.id !== productId);
    saveProductsToLocalStorage();
    renderProducts();
}

// Hàm chỉnh sửa sản phẩm
function editProduct(productId) {
    const product = products.find(product => product.id === productId);
    document.getElementById('product-id').value = product.id;
    document.getElementById('product-name').value = product.name;
    document.getElementById('product-price').value = product.price;
    document.getElementById('product-image').value = product.image;
    document.getElementById('product-details').value = product.details;
    document.getElementById('product-category').value = product.category;
}

// Hàm lưu sản phẩm vào localStorage
function saveProductsToLocalStorage() {
    localStorage.setItem('products', JSON.stringify(products));
}

// Hàm xóa đơn hàng
function deleteOrder(orderId) {
    orders = orders.filter(order => order.id !== orderId);
    saveOrdersToLocalStorage();
    renderOrders();
}

// Hàm cập nhật trạng thái đơn hàng
function updateOrderStatus(orderId) {
    const order = orders.find(order => order.id === orderId);
    order.status = order.status === 'Đang xử lý' ? 'Đã giao' : 'Đang xử lý';
    saveOrdersToLocalStorage();
    renderOrders();
}

// Hàm lưu đơn hàng vào localStorage
function saveOrdersToLocalStorage() {
    localStorage.setItem('orders', JSON.stringify(orders));
}

// Hàm khởi động trang
window.onload = init;