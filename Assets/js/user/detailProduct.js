// === 1. Dữ liệu sản phẩm (bạn có thể bỏ qua phần này nếu đã có trong file) ===
const products = {
    iphone: [
        { id: "iphone1", name: "iPhone 14 Pro", os: "iOS 16", cpu: "A16 Bionic", gpu: "Apple GPU", ram: "6GB", storage: "128GB", 
            price: 999, image: "../../assets/images/iphone1.jpg"},
        { id: "iphone13", name: "iPhone 13", os: "iOS 16", cpu: "A16 Bionic", gpu: "Apple GPU", ram: "6GB", storage: "256GB", 
            price: 799, image: "../../assets/images/iphone2.jpg"},
        { id: "iphone2", name: "iPhone 12", os: "iOS 16", cpu: "A16 Bionic", gpu: "Apple GPU", ram: "6GB", storage: "256GB",
            price: 699, image: "../../assets/images/iphone3.jpg" },
        { id: "iphone3", name: "iPhone SE", os: "iOS 16", cpu: "A16 Bionic", gpu: "Apple GPU", ram: "6GB", storage: "256GB",
            price: 399, image: "../../assets/images/iphone4.jpg" },
        { id: "iphone4", name: "iPhone 11", os: "iOS 16", cpu: "A16 Bionic", gpu: "Apple GPU", ram: "6GB", storage: "256GB",
            price: 599, image: "../../assets/images/iphone5.jpg" },
        { id: "iphone5", name: "iPhone XR", os: "iOS 16", cpu: "A16 Bionic", gpu: "Apple GPU", ram: "6GB", storage: "256GB",
            price: 499, image: "../../assets/images/iphone6.jpg" },
        { id: "iphone6", name: "iPhone XS", os: "iOS 16", cpu: "A16 Bionic", gpu: "Apple GPU", ram: "6GB", storage: "256GB",
            price: 899, image: "../../assets/images/iphone7.jpg" },
        { id: "iphone7", name: "iPhone X", os: "iOS 16", cpu: "A16 Bionic", gpu: "Apple GPU", ram: "6GB", storage: "256GB",
            price: 899, image: "../../assets/images/iphone8.jpg" },
        { id: "iphone8", name: "iPhone 8", os: "iOS 16", cpu: "A16 Bionic", gpu: "Apple GPU", ram: "6GB", storage: "256GB",
            price: 299, image: "../../assets/images/iphone9.jpg" },
        { id: "iphone9", name: "iPhone 7", os: "iOS 16", cpu: "A16 Bionic", gpu: "Apple GPU", ram: "6GB", storage: "256GB",
            price: 199, image: "../../assets/images/iphone10.jpg" }
    ],
    samsung: [
        { id:"samsung1", name: "Samsung Galaxy S23", os: "Android 13", cpu: "Snapdragon 8 Gen 2", gpu: "Adreno 740", ram: "8GB", storage: "256GB",
            price: 899, image: "../../assets/images/samsung1.jpg" },
        { id:"samsung2", name: "Samsung Galaxy S22", os: "Android 13", cpu: "Snapdragon 8 Gen 2", gpu: "Adreno 740", ram: "8GB", storage: "256GB",
            price: 899, image: "../../assets/images/samsung2.jpg" },
        { id: "samsung3", name: "Samsung Galaxy S21", os: "Android 13", cpu: "Snapdragon 8 Gen 2", gpu: "Adreno 740", ram: "8GB", storage: "256GB",
            price: 799, image: "../../assets/images/samsung3.jpg" },
        { id: "samsung4", name: "Samsung Galaxy Note 20", os: "Android 13", cpu: "Snapdragon 8 Gen 2", gpu: "Adreno 740", ram: "8GB", storage: "256GB",
            price: 999, image: "../../assets/images/samsung4.jpg" },
        { id: "samsung5", name: "Samsung Galaxy A54", os: "Android 13", cpu: "Snapdragon 8 Gen 2", gpu: "Adreno 740", ram: "8GB", storage: "256GB",
            price: 499, image: "../../assets/images/samsung5.jpg" },   
        { id:"samsung6", name: "Samsung Galaxy Z Flip", os: "Android 13", cpu: "Snapdragon 8 Gen 2", gpu: "Adreno 740", ram: "8GB", storage: "256GB", 
            price: 1199, image: "../../assets/images/samsung6.jpg" },
        { id:"samsung7", name: "Samsung Galaxy M52", os: "Android 13", cpu: "Snapdragon 8 Gen 2", gpu: "Adreno 740", ram: "8GB", storage: "256GB",
            price: 349, image: "../../assets/images/samsung7.jpg" },
        { id:"samsungF8", name: "Samsung Galaxy F41", os: "Android 13", cpu: "Snapdragon 8 Gen 2", gpu: "Adreno 740", ram: "8GB", storage: "256GB",
            price: 299, image: "../../assets/images/samsung8.jpg" },
        { id:"samsung9", name: "Samsung Galaxy S10", os: "Android 13", cpu: "Snapdragon 8 Gen 2", gpu: "Adreno 740", ram: "8GB", storage: "256GB", 
            price: 499, image: "../../assets/images/samsung9.jpg" },
        { id:"samsung10", name: "Samsung Galaxy A10", os: "Android 13", cpu: "Snapdragon 8 Gen 2", gpu: "Adreno 740", ram: "8GB", storage: "256GB",
            price: 199, image: "../../assets/images/samsung10.jpg" }
    ],
    xiaomi: [
        { id:"xiaomi1", name: "Xiaomi Mi 12", os: "Android 14", cpu: "MediaTek Helio G81-Ultra", gpu: "Mali-G52 MC2", ram: "4GB", storage: "128GB",
            price: 799, image: "../../assets/images/xiaomi1.jpg" },
        { id:"xiaomi2", name: "Xiaomi Redmi Note 11", os: "Android 14", cpu: "MediaTek Helio G81-Ultra", gpu: "Mali-G52 MC2", ram: "4GB", storage: "128GB", 
            price: 299, image: "../../assets/images/xiaomi2.jpg" },
        { id:"xiaomi3", name: "Xiaomi Poco X5", os: "Android 14", cpu: "MediaTek Helio G81-Ultra", gpu: "Mali-G52 MC2", ram: "4GB", storage: "128GB", 
            price: 349, image: "../../assets/images/xiaomi3.jpg" },
        { id:"xiaomi4", name: "Xiaomi Mi 11", os: "Android 14", cpu: "MediaTek Helio G81-Ultra", gpu: "Mali-G52 MC2", ram: "4GB", storage: "128GB",
            price: 699, image: "../../assets/images/xiaomi4.jpg" },
        { id:"xiaomi5", name: "Xiaomi Redmi 9", os: "Android 14", cpu: "MediaTek Helio G81-Ultra", gpu: "Mali-G52 MC2", ram: "4GB", storage: "128GB",
            price: 199, image: "../../assets/images/xiaomi5.jpg" },
        { id:"xiaomi6", name: "Xiaomi Black Shark", os: "Android 14", cpu: "MediaTek Helio G81-Ultra", gpu: "Mali-G52 MC2", ram: "4GB", storage: "128GB",
            price: 499, image: "../../assets/images/xiaomi6.jpg" },
        { id:"xiaomi7", name: "Xiaomi Mi 10T", os: "Android 14", cpu: "MediaTek Helio G81-Ultra", gpu: "Mali-G52 MC2", ram: "4GB", storage: "128GB",
            price: 599, image: "../../assets/images/xiaomi7.jpg" },
        { id:"xiaomi8", name: "Xiaomi Redmi Note 9", os: "Android 14", cpu: "MediaTek Helio G81-Ultra", gpu: "Mali-G52 MC2", ram: "4GB", storage: "128GB",
            price: 249, image: "../../assets/images/xiaomi8.jpg" },
        { id:"xiaomi9", name: "Xiaomi Mi 9", os: "Android 14", cpu: "MediaTek Helio G81-Ultra", gpu: "Mali-G52 MC2", ram: "4GB", storage: "128GB",
            price: 399, image: "../../assets/images/xiaomi9.jpg" },
        { id:"xiaomi10", name: "Xiaomi Mi A3", os: "Android 14", cpu: "MediaTek Helio G81-Ultra", gpu: "Mali-G52 MC2", ram: "4GB", storage: "128GB",
            price: 199, image: "../../assets/images/xiaomi10.jpg" }
    ],
};

// Hàm cập nhật số lượng sản phẩm trong giỏ hàng
function updateCartCount() {
    const cart = JSON.parse(localStorage.getItem("cart")) || [];
    const totalItems = cart.reduce((sum, item) => sum + item.quantity, 0);
    const cartCountElement = document.getElementById("cart-count");
    cartCountElement.textContent = totalItems > 0 ? totalItems : ""; // Ẩn nếu không có sản phẩm
}

// Gọi hàm cập nhật số lượng giỏ hàng khi trang được tải
document.addEventListener("DOMContentLoaded", () => {
    const productId = localStorage.getItem("selectedProductId");
    const productInfoDiv = document.getElementById("product-info");

    // Tìm sản phẩm trong danh sách sản phẩm
    let productData = null;
    Object.values(products).forEach((brand) => {
        const found = brand.find((item) => item.id === productId);
        if (found) productData = found;
    });

    if (productData) {
        productInfoDiv.innerHTML = `
            <img src="${productData.image}" alt="${productData.name}">
            <h1>${productData.name}</h1>
            <p><strong>OS:</strong> ${productData.os}</p>
            <p><strong>CPU:</strong> ${productData.cpu}</p>
            <p><strong>GPU:</strong> ${productData.gpu}</p>
            <p><strong>RAM:</strong> ${productData.ram}</p>
            <p><strong>Storage:</strong> ${productData.storage}</p>
            <p><strong>Price:</strong> ${productData.price}$</p>
            <button id="add-to-cart-btn">Add to cart</button>
        `;

        // Gắn sự kiện cho nút "Add to cart"
        document.getElementById("add-to-cart-btn").addEventListener("click", () => {
            addToCart(productData); // Gọi hàm thêm sản phẩm vào giỏ hàng
        });
    } else {
        productInfoDiv.innerHTML = "<p>Thông tin sản phẩm không khả dụng.</p>";
    }

    // Cập nhật số lượng sản phẩm trong giỏ hàng
    updateCartCount(); // Gọi hàm cập nhật số lượng giỏ hàng
});

// Hàm thêm sản phẩm vào giỏ hàng
function addToCart(product) {
    let cart = JSON.parse(localStorage.getItem("cart")) || [];
    const existingProduct = cart.find(item => item.id === product.id);

    if (existingProduct) {
        existingProduct.quantity += 1; // Tăng số lượng nếu sản phẩm đã có
    } else {
        cart.push({ ...product, quantity: 1 }); // Thêm mới nếu chưa có
    }
    
    localStorage.setItem("cart", JSON.stringify(cart)); // Lưu giỏ hàng vào localStorage
    alert(`${product.name} đã được thêm vào giỏ hàng!`);
    
    // Cập nhật số lượng sản phẩm trong giỏ hàng
    updateCartCount(); // Gọi hàm cập nhật số lượng giỏ hàng
}