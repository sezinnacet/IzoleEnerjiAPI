# IzoleEnerjiAPI

# Project Setup

## Database Configuration

1. Update the connection string in `appsettings.json` with your own database details.

## Running Migrations

1. Open a terminal in the project directory.
2. Run the following command to apply migrations and create the database:

    ```bash
    dotnet ef database update
    ```

## Starting the Application

1. Run the application using your preferred method:

    ```bash
    dotnet run
    ```

2. The application should now be connected to your local database.
