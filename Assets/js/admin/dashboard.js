document.addEventListener("DOMContentLoaded", () => {
    // Fetch data from backend (mocked here for now)
    const stats = {
        totalProducts: 120,
        totalOrders: 250,
        totalRevenue: 15000,
        totalUsers: 80,
        pendingOrders: 30,
        completedOrders: 220,
        averageOrderValue: 60,
        newUsersToday: 5,
    };

    // Update stats on the page
    document.getElementById("total-products").textContent = stats.totalProducts;
    document.getElementById("total-orders").textContent = stats.totalOrders;
    document.getElementById("total-revenue").textContent = `$${stats.totalRevenue}`;
    document.getElementById("total-users").textContent = stats.totalUsers;
    document.getElementById("pending-orders").textContent = stats.pendingOrders;
    document.getElementById("completed-orders").textContent = stats.completedOrders;
    document.getElementById("average-order-value").textContent = `$${stats.averageOrderValue}`;
    document.getElementById("new-users-today").textContent = stats.newUsersToday;
});