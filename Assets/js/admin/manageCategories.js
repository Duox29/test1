document.addEventListener("DOMContentLoaded", () => {
    const categoryList = document.getElementById("category-list");

    // Mock data for categories
    const categories = [
        { id: 1, name: "Electronics" },
        { id: 2, name: "Clothing" },
        { id: 3, name: "Home Appliances" },
    ];

    // Render category list
    const renderCategories = () => {
        categoryList.innerHTML = categories
            .map(
                (category) => `
            <tr>
                <td>${category.id}</td>
                <td>${category.name}</td>
                <td>
                    <button class="edit-btn" onclick="editCategory(${category.id})">Edit</button>
                    <button class="delete-btn" onclick="deleteCategory(${category.id})">Delete</button>
                </td>
            </tr>
        `
            )
            .join("");
    };

    // Function: Edit category
    window.editCategory = (id) => {
        const category = categories.find((c) => c.id === id);
        const newName = prompt("Enter new name for the category:", category.name);
        if (newName) {
            category.name = newName;
            renderCategories();
        }
    };

    // Function: Delete category
    window.deleteCategory = (id) => {
        const confirmDelete = confirm(`Are you sure you want to delete category ID: ${id}?`);
        if (confirmDelete) {
            categories = categories.filter((c) => c.id !== id);
            renderCategories();
        }
    };

    // Initial render
    renderCategories();

    // Add new category button event
    document.getElementById("add-category-btn").addEventListener("click", () => {
        const newCategoryName = prompt("Enter new category name:");
        if (newCategoryName) {
            const newCategory = {
                id: categories.length + 1,
                name: newCategoryName,
            };
            categories.push(newCategory);
            renderCategories();
        }
    });
});