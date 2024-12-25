document.addEventListener("DOMContentLoaded", () => {
    const productList = document.getElementById("product-list");

    // Mock data for products
    const products = [
        { id: 1, name: "iPhone 14", category: "Phones", price: 999, stock: 10 },
        { id: 2, name: "Samsung Galaxy S23", category: "Phones", price: 899, stock: 15 },
        { id: 3, name: "MacBook Pro", category: "Laptops", price: 1999, stock: 5 },
    ];

    // Render product list
    const renderProducts = () => {
        productList.innerHTML = products
            .map(
                (product) => `
            <tr>
                <td>${product.id}</td>
                <td>${product.name}</td>
                <td>${product.category}</td>
                <td>$${product.price}</td>
                <td>${product.stock}</td>
                <td>
                    <button class="edit-btn" onclick="editProduct(${product.id})">Edit</button>
                    <button class="delete-btn" onclick="deleteProduct(${product.id})">Delete</button>
                </td>
            </tr>
        `
            )
            .join("");
    };

    // Event: Add new product
    document.getElementById("add-product-btn").addEventListener("click", () => {
        const newProduct = {
            id: products.length + 1,
            name: "New Product",
            category: "Category",
            price: 0,
            stock: 0,
        };
        products.push(newProduct);
        renderProducts();
    });

    // Function: Edit product
    window.editProduct = (id) => {
        const product = products.find((p) => p.id === id);
        if (product) {
            const newName = prompt("Enter new name:", product.name);
            if (newName) product.name = newName;
            renderProducts();
        }
    };

    // Function: Delete product
    window.deleteProduct = (id) => {
        const index = products.findIndex((p) => p.id === id);
        if (index !== -1) {
            products.splice(index, 1);
            renderProducts();
        }
    };

    // Initial render
    renderProducts();
});
