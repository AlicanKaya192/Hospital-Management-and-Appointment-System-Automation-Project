# Hospital Management and Appointment Automation System Project

This project is an end-to-end integrated automation system that manages hospital operations. It encompasses the database layer as well as user interfaces, business logic, and deployment processes.

---

## 📌 Project Overview

The **Hospital Management and Appointment Automation System** centralizes branch, doctor, patient, secretary, and appointment management by providing:

- Branch and doctor assignments
- Patient registration, updates, and deletion
- Secretary login and session management
- Appointment scheduling, approval, and cancellation workflows
- Announcement publishing
- Unique identification control of national ID numbers via triggers

By digitizing daily patient tracking, staff administration, and appointment processes, the system minimizes manual errors and improves service quality.

---

## 🏗 System Architecture Layers

1. **Data Access Layer (DAL)**
   - Contains SQL Server tables, triggers, and stored procedures.

2. **Business Logic Layer (BLL)**
   - Validates user inputs, enforces business rules, and acts as a bridge between the DAL and UI.

3. **User Interface (UI)**
   - Built with Windows Forms or ASP.NET (configurable).
   - Main modules: Branch Management, Doctor Management, Patient Registration, Secretary Dashboard, Appointment Tracking, Announcements.

4. **Deployment & Configuration**
   - Application settings managed via `appsettings.json` or `app.config`.
   - Optional CI/CD integration with PowerShell scripts or Azure DevOps pipelines.

---

## 🛠 Technology Stack

| Layer                  | Technology / Tool              |
|------------------------|--------------------------------|
| Database               | SQL Server 2012+               |
| Data Access            | ADO.NET / Entity Framework     |
| Business Logic         | C# (.NET Framework / .NET 6)   |
| User Interface         | Windows Forms / ASP.NET MVC    |
| Development IDE        | Visual Studio 2019+            |
| Version Control        | Git & GitHub                   |
| CI/CD (Optional)       | Azure DevOps / GitHub Actions  |

---

## 🗂 Database Schema Summary

- **Tbl_Branches**: Branch information.
- **Tbl_Doctors**: Doctor records with branch associations.
- **Tbl_Patients**: Patient data.
- **Tbl_Secretary**: Secretary account details.
- **Tbl_Appointments**: Appointment information.
- **Tbl_Announcements**: Hospital announcements.

All tables implement national ID uniqueness checks using `INSTEAD OF INSERT` triggers.

---

## 🖥 Application Screenshots

<img width="586" height="357" alt="Hospital1" src="https://github.com/user-attachments/assets/6249bd2c-2134-4f7c-9fec-2a8708c8c5e5" />
<img width="428" height="336" alt="Hospital2" src="https://github.com/user-attachments/assets/9ca7c796-0330-407e-b14a-401c19942847" />
<img width="1565" height="544" alt="Hospital3" src="https://github.com/user-attachments/assets/6a054221-8917-42e6-9348-63b0a76434ff" />
<img width="432" height="335" alt="Hospital4" src="https://github.com/user-attachments/assets/2b2edc0c-58e6-40b7-bfe1-2a6a32214de2" />
<img width="1412" height="678" alt="Hospital5" src="https://github.com/user-attachments/assets/ad8c2ec8-b7fa-4b39-9a1e-ce25d19cbd33" />
<img width="421" height="337" alt="Hospital6" src="https://github.com/user-attachments/assets/7b7cb6f6-460f-4d96-a4be-41466f9e5bf7" />
<img width="1919" height="1030" alt="Hospital7" src="https://github.com/user-attachments/assets/75b7e69f-2d14-4f07-978d-34b8a221ded6" />
<img width="440" height="372" alt="Hospital8" src="https://github.com/user-attachments/assets/be48ba7c-9acb-4226-9340-892e915cfe24" />

---

## 🚀 Setup & Running the Project

1. **Prerequisites**:
   - SQL Server 2012 or later
   - Visual Studio 2019 or later (with .NET development workload)

2. **Database Initialization**:
   - Execute the `Database/HospitalProject.sql` script in SQL Server Management Studio (SSMS) to create the database and tables.

3. **Clone the Repository**:
   ```bash
   git clone https://github.com/username/HospitalManagement.git
   cd HospitalManagement
   ```

4. **Update Connection String**:
   - Modify the `ConnectionString` in `appsettings.json` or `app.config` to match your SQL Server instance.

5. **Build and Run**:
   - Open the solution in Visual Studio, select `Build` → `Rebuild Solution`, then `Start Debugging`.

---

## 🎯 Use Case Scenarios

- **Secretary**: Registers patients, schedules appointments, approves or cancels bookings.
- **Doctor**: Views pending appointments, marks completed sessions.
- **Administrator**: Adds/removes branches and doctors, publishes announcements.

---

## 🤝 Contributing

Feel free to fork the repository, commit your changes, and open a Pull Request. Please adhere to existing code conventions and project structure.

---

## 📄 License

This project is licensed under the MIT License. See the `LICENSE` file for details.
