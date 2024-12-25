document.addEventListener("DOMContentLoaded", () => {
    const revenueList = document.getElementById("revenue-list");
    const totalRevenueElement = document.getElementById("total-revenue");

    // Mock data for revenue
    const revenueData = [
        { month: "January", totalSales: 1500, details: "Detailed report for January" },
        { month: "February", totalSales: 2000, details: "Detailed report for February" },
        { month: "March", totalSales: 2500, details: "Detailed report for March" },
    ];

    let totalRevenue = 0;

    // Render revenue list and calculate total revenue
    const renderRevenue = () => {
        revenueList.innerHTML = revenueData
            .map(
                (item) => `
            <tr>
                <td>${item.month}</td>
                <td>$${item.totalSales}</td>
                <td><button class="details-btn" onclick="viewDetails('${item.details}')">View</button></td>
            </tr>
        `
            )
            .join("");

        // Calculate total revenue
        totalRevenue = revenueData.reduce((sum, item) => sum + item.totalSales, 0);
        totalRevenueElement.textContent = `$${totalRevenue}`;
    };

    // Function: View revenue details
    window.viewDetails = (details) => {
        alert(details);
    };

    // Initial render
    renderRevenue();
});
