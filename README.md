# N5.Permissions

This project uses Docker Compose to set up a full development environment with all required services.

## Requirements

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

## How to Run the Project

1. Clone this repository:

   ```bash
   git clone https://github.com/your-user/your-project.git
   cd your-project

2. Start the services using Docker Compose:
     ```bash
     docker compose up
3. Once the services are running, you can access the application at:
   http://localhost:5000/swagger/index.html
4. The database loads a default record in the PermissionType table with the following value, which is used for creating and modifying permissions:
     ```code
       Id = 1
       Description = "Vacation"

## Architectural diagram

   ![Diagram](https://github.com/user-attachments/assets/b7f30672-4e52-4fdb-b527-e8f99cbd2d5d)


