// Lấy các phần tử DOM
const cartTableBody = document.querySelector(".cart-table tbody");
const cartTotalElement = document.getElementById("cart-total");

// Giỏ hàng
let cart = JSON.parse(localStorage.getItem("cart")) || [];

// Hàm render giỏ hàng
function renderCart() {
    cartTableBody.innerHTML = ""; // Xóa nội dung cũ
    let totalPrice = 0;

    cart.forEach((item, index) => {
        const row = document.createElement("tr");
        const itemTotalPrice = item.price * item.quantity;
        totalPrice += itemTotalPrice;

        row.innerHTML = `
            <td><img src="${item.image}" alt="${item.name}" class="cart-img"></td>
            <td>${item.name}</td>
            <td>
                <button class="decrease-qty" data-index="${index}">-</button>
                ${item.quantity}
                <button class="increase-qty" data-index="${index}">+</button>
            </td>
            <td>${item.price}$</td>
            <td>${itemTotalPrice}$</td>
            <td>
                <button class="remove-btn" data-index="${index}">Xóa</button>
            </td>
        `;
        cartTableBody.appendChild(row);
    });

    cartTotalElement.textContent = totalPrice.toFixed(2);
    localStorage.setItem("cart", JSON.stringify(cart)); // Lưu giỏ hàng vào localStorage
}

// Hàm thêm sản phẩm vào giỏ hàng
function addToCart(product) {
    const existingProduct = cart.find((item) => item.name === product.name);
    if (existingProduct) {
        existingProduct.quantity += product.quantity; // Cập nhật số lượng nếu sản phẩm đã tồn tại
    } else {
        cart.push(product); // Thêm sản phẩm mới
    }
    renderCart();
}

// Hàm xóa sản phẩm khỏi giỏ hàng
function removeFromCart(index) {
    cart.splice(index, 1); // Xóa sản phẩm tại vị trí index
    renderCart(); // Render lại giao diện
}

// Hàm cập nhật số lượng sản phẩm
function updateQuantity(index, change) {
    cart[index].quantity += change;
    if (cart[index].quantity <= 0) {
        removeFromCart(index);
    } else {
        renderCart();
    }
}

// Gắn sự kiện cho các nút trong bảng
cartTableBody.addEventListener("click", (e) => {
    const index = e.target.getAttribute("data-index");

    if (e.target.classList.contains("remove-btn")) {
        removeFromCart(index);
    } else if (e.target.classList.contains("increase-qty")) {
        updateQuantity(index, 1);
    } else if (e.target.classList.contains("decrease-qty")) {
        updateQuantity(index, -1);
    }
});

// Hàm khởi tạo giỏ hàng ban đầu
document.addEventListener("DOMContentLoaded", () => {
    renderCart();
});
