Hospital Management and Appointment Automation System Project

This project is an end-to-end integrated automation system that manages hospital operations. It encompasses the database layer as well as user interfaces, business logic, and deployment processes.

📌 Project Overview

The Hospital Management and Appointment Automation System centralizes branch, doctor, patient, secretary, and appointment management by providing:

Branch and doctor assignments

Patient registration, updates, and deletion

Secretary login and session management

Appointment scheduling, approval, and cancellation workflows

Announcement publishing

Unique identification control of national ID numbers via triggers

By digitizing daily patient tracking, staff administration, and appointment processes, the system minimizes manual errors and improves service quality.

🏗 System Architecture Layers

Data Access Layer (DAL)

Contains SQL Server tables, triggers, and stored procedures.

Business Logic Layer (BLL)

Validates user inputs, enforces business rules, and acts as a bridge between the DAL and UI.

User Interface (UI)

Built with Windows Forms or ASP.NET (configurable).

Main modules: Branch Management, Doctor Management, Patient Registration, Secretary Dashboard, Appointment Tracking, Announcements.

Deployment & Configuration

Application settings managed via appsettings.json or app.config.

Optional CI/CD integration with PowerShell scripts or Azure DevOps pipelines.

🛠 Technology Stack

Layer

Technology / Tool

Database

SQL Server 2012+

Data Access

ADO.NET / Entity Framework

Business Logic

C# (.NET Framework / .NET 6)

User Interface

Windows Forms / ASP.NET MVC

Development IDE

Visual Studio 2019+

Version Control

Git & GitHub

CI/CD (Optional)

Azure DevOps / GitHub Actions

🗂 Database Schema Summary

Tbl_Branches: Branch information.

Tbl_Doctors: Doctor records with branch associations.

Tbl_Patients: Patient data.

Tbl_Secretary: Secretary account details.

Tbl_Appointments: Appointment information.

Tbl_Announcements: Hospital announcements.

All tables implement national ID uniqueness checks using INSTEAD OF INSERT triggers.

🖥 Application Screenshots

(Add project screenshots in this section.)

🚀 Setup & Running the Project

Prerequisites:

SQL Server 2012 or later

Visual Studio 2019 or later (with .NET development workload)

Database Initialization:

Execute the Database/HospitalProject.sql script in SQL Server Management Studio (SSMS) to create the database and tables.

Clone the Repository:

git clone https://github.com/username/HospitalManagement.git
cd HospitalManagement

Update Connection String:

Modify the ConnectionString in appsettings.json or app.config to match your SQL Server instance.

Build and Run:

Open the solution in Visual Studio, select Build → Rebuild Solution, then Start Debugging.

🎯 Use Case Scenarios

Secretary: Registers patients, schedules appointments, approves or cancels bookings.

Doctor: Views pending appointments, marks completed sessions.

Administrator: Adds/removes branches and doctors, publishes announcements.

🤝 Contributing

Feel free to fork the repository, commit your changes, and open a Pull Request. Please adhere to existing code conventions and project structure.

📄 License

This project is licensed under the MIT License. See the LICENSE file for details.
