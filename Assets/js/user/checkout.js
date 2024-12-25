document.getElementById("checkout-form").addEventListener("submit", function(event) {
    event.preventDefault(); // Ngăn chặn gửi form mặc định

    // Lấy thông tin từ form
    const name = document.getElementById("name").value;
    const address = document.getElementById("address").value;
    const city = document.getElementById("city").value;
    const postalCode = document.getElementById("postal-code").value;
    const country = document.getElementById("country").value;
    const phone = document.getElementById("phone").value;
    const paymentMethod = document.querySelector('input[name="payment"]:checked').value;

    // Xử lý thanh toán (gọi API thanh toán hoặc thực hiện các bước xác thực)
    alert(`Bạn đã chọn phương thức thanh toán: ${paymentMethod}`);

    // Ví dụ: Gọi API thanh toán
    const paymentData = {
        name,
        address,
        city,
        postalCode,
        country,
        phone,
        paymentMethod
    };

    // Giả lập gọi API thanh toán
    processPayment(paymentData)
        .then(response => {
            if (response.success) {
                alert("Thanh toán thành công! Cảm ơn bạn đã mua hàng.");
                // Chuyển hướng đến trang xác nhận ```javascript
                window.location.href = "confirmation.html";
            } else {
                alert("Có lỗi xảy ra trong quá trình thanh toán. Vui lòng thử lại.");
            }
        })
        .catch(error => {
            console.error("Lỗi:", error);
            alert("Có lỗi xảy ra. Vui lòng thử lại sau.");
        });
});

// Giả lập hàm xử lý thanh toán
function processPayment(data) {
    return new Promise((resolve) => {
        setTimeout(() => {
            resolve({ success: true }); // Giả lập thanh toán thành công
        }, 1000);
    });
}