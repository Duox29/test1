document.addEventListener("DOMContentLoaded", () => {
    const orderList = document.getElementById("order-list");

    // Mock data for orders
    const orders = [
        { id: 1, user: "User1", totalAmount: 250, status: "Pending" },
        { id: 2, user: "User2", totalAmount: 180, status: "Shipped" },
        { id: 3, user: "User3", totalAmount: 320, status: "Delivered" },
    ];

    // Render order list
    const renderOrders = () => {
        orderList.innerHTML = orders
            .map(
                (order) => `
            <tr>
                <td>${order.id}</td>
                <td>${order.user}</td>
                <td>$${order.totalAmount}</td>
                <td>${order.status}</td>
                <td>
                    <button class="view-btn" onclick="viewOrder(${order.id})">View</button>
                    <button class="update-btn" onclick="updateOrder(${order.id})">Update</button>
                </td>
            </tr>
        `
            )
            .join("");
    };

    // Function: View order
    window.viewOrder = (id) => {
        alert("Viewing details for Order ID: " + id);
    };

    // Function: Update order status
    window.updateOrder = (id) => {
        const order = orders.find((o) => o.id === id);
        if (order) {
            const newStatus = prompt("Enter new status for order:", order.status);
            if (newStatus) order.status = newStatus;
            renderOrders();
        }
    };

    // Initial render
    renderOrders();
});
