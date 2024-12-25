document.addEventListener("DOMContentLoaded", () => {
    // Mock data for statistics
    const stats = {
        totalSales: 50000,
        salesThisMonth: 15000,
        salesLastMonth: 20000,
        monthlySales: [12000, 15000, 13000, 17000, 16000, 18000, 20000] // Dữ liệu doanh thu hàng tháng
    };

    // Load statistics
    const loadStatistics = () => {
        document.getElementById("total-sales").textContent = `$${stats.totalSales}`;
        document.getElementById("sales-this-month").textContent = `$${stats.salesThisMonth}`;
        document.getElementById("sales-last-month").textContent = `$${stats.salesLastMonth}`;

        drawSalesChart(stats.monthlySales); // Vẽ đồ thị doanh thu hàng tháng
    };

    // Vẽ đồ thị doanh thu hàng tháng
    const drawSalesChart = (monthlySales) => {
        const ctx = document.getElementById('salesChart').getContext('2d');
        const salesChart = new Chart(ctx, {
            type: 'line', // Loại đồ thị
            data: {
                labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul'], // Nhãn cho các tháng
                datasets: [{
                    label: 'Monthly Sales ($)',
                    data: monthlySales, // Dữ liệu doanh thu hàng tháng
                    borderColor: 'rgba(75, 192, 192, 1)', // Màu đường
                    backgroundColor: 'rgba(75, 192, 192, 0.2)', // Màu nền
                    borderWidth: 2,
                    fill: true // Đổ màu dưới đường
                }]
            },
            options: {
                responsive: true,
                scales: {
                    y: {
                        beginAtZero: true // Bắt đầu trục y từ 0
                    }
                }
            }
        });
    };

    // Initial load
    loadStatistics();
});