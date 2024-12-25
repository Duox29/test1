document.addEventListener("DOMContentLoaded", () => {
    const userList = document.getElementById("user-list");

    // Mock data for users
    const usersData = [
        { username: "john_doe", email: "john@example.com", role: "Admin", status: "Active" },
        { username: "jane_doe", email: "jane@example.com", role: "User", status: "Inactive" },
        { username: "alice_smith", email: "alice@example.com", role: "User", status: "Active" },
    ];

    // Render user list
    const renderUsers = () => {
        userList.innerHTML = usersData
            .map(
                (user) => `
            <tr>
                <td>${user.username}</td>
                <td>${user.email}</td>
                <td>${user.role}</td>
                <td>${user.status}</td>
                <td>
                    <div class="action-btns">
                        <button class="edit-btn" onclick="editUser('${user.username}')">Edit</button>
                        <button class="delete-btn" onclick="deleteUser('${user.username}')">Delete</button>
                        <button class="activate-btn" onclick="activateUser('${user.username}')">${user.status === "Active" ? "Deactivate" : "Activate"}</button>
                    </div>
                </td>
            </tr>
        `
            )
            .join("");
    };

    // Function: Edit user
    window.editUser = (username) => {
        alert(`Editing user: ${username}`);
    };

    // Function: Delete user
    window.deleteUser = (username) => {
        const confirmDelete = confirm(`Are you sure you want to delete user: ${username}?`);
        if (confirmDelete) {
            alert(`User ${username} deleted`);
        }
    };

    // Function: Activate/Deactivate user
    window.activateUser = (username) => {
        const user = usersData.find((user) => user.username === username);
        user.status = user.status === "Active" ? "Inactive" : "Active";
        renderUsers();
    };

    // Initial render
    renderUsers();
});
