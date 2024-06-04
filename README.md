# Ecommerce Web Application

This repository contains the source code for an Ecommerce Web Application built using ASP.NET MVC Core. The application provides functionality for both customers and administrators to interact with the ecommerce platform.

## Features

### For Customers:

- **Home Page**: Displays a category menu and featured products.
- **View Products by Category**: Allows customers to browse products by category.
- **View Product Details**: Provides detailed information about a specific product.
- **Product Rating**: Allows customers to rate products.
- **Register**: New customers can register for an account.
- **Login/Logout**: Authentication system using Identity Core ASP.
- **Optional Features**: Includes shopping cart, ordering, and payment options (Cash on Delivery and Card Payment). Utilizes IdentityServer4 for enhanced security.

### For Admins:

- **Login/Logout**: Administrators can securely log in and out of the system.
- **Manage Product Categories**: Admins can add, edit, or delete product categories, along with their descriptions. Additionally, sub-categories can be managed under each category.
- **Manage Products**: Administrators have full control over product management, including details such as name, category, description, price, images, creation date, and last updated date. Products can also have variations such as colors (e.g., red, green, blue).
- **View Customers**: Admins can view customer information.

## Architecture

The project follows ASP.NET MVC Core architecture and incorporates various techniques to enhance functionality and maintainability:

- **TagHelpers**: Used to create reusable components in Razor views, improving readability and reducing redundancy.
- **Razor Pages**: Employed for simpler page-based UI interactions and routing.
- **ViewComponents**: Utilized to create self-contained components that can be reused across multiple views.
- **Unit Testing**: Includes unit tests for common components such as controllers, view components, and services. While the coverage may not be exhaustive, it demonstrates the ability to write effective unit tests.

![Screenshot 2024-06-04 174641](https://github.com/Vawnndv/Assignment-Ecommerce/assets/90141979/ab53b8a0-7e1c-49a8-9752-ebae7279ea87)

## Requirements

To run the application locally, ensure you have the following prerequisites installed:

- .NET Core SDK
- Visual Studio or Visual Studio Code
- SQL Server (for database storage)

## Getting Started

1. Clone this repository to your local machine.
2. Open the solution in Visual Studio or Visual Studio Code.
3. Restore the necessary NuGet packages.
4. Update the connection string in `appsettings.json` to point to your SQL Server instance.
5. Run the database migrations to create the required tables:
    ```bash
    dotnet ef database update
    ```
6. Build and run the application.

## Contributing

Contributions are welcome! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE). Feel free to use, modify, and distribute the code for your own projects.
