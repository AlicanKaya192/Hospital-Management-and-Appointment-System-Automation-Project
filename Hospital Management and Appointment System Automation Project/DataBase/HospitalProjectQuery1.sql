--CREATE DATABASE HospitalProject
--USE HospitalProject

--CREATE TABLE Tbl_Branches (
--    Branchid TINYINT IDENTITY(1,1) PRIMARY KEY,
--    BranchName VARCHAR(30) NOT NULL
--)

--CREATE TABLE Tbl_Patients (
--    Patientid SMALLINT IDENTITY(1,1),
--    PatientName VARCHAR(10) NOT NULL,
--    PatientSurname VARCHAR(10) NOT NULL,
--    PatientTC CHAR(11) NOT NULL,
--    PatientPhone VARCHAR(15) NOT NULL,
--    PatientPw VARCHAR(10) NOT NULL,
--    PatientGender VARCHAR(6) NOT NULL
--)

--CREATE TABLE Tbl_Doctors (
--    Doctorid TINYINT IDENTITY(1,1),
--    DoctorName VARCHAR(10) NOT NULL,
--    DoctorSurname VARCHAR(10) NOT NULL,
--    Branchid TINYINT NOT NULL,
--    DoctorTC CHAR(11) NOT NULL,
--    DoctorPw VARCHAR(10) NOT NULL,
--    FOREIGN KEY (Branchid) REFERENCES Tbl_Branches(Branchid)
--)

--CREATE TABLE Tbl_Secretary (
--    Secretaryid TINYINT IDENTITY(1,1),
--    SecretaryNameSurname VARCHAR(20) NOT NULL,
--    SecretaryTC CHAR(11) NOT NULL,
--    SecretaryPw VARCHAR(10) NOT NULL
--)

--CREATE TABLE Tbl_Appointments (
--    Appointmentid INT IDENTITY(1,1),
--    AppointmentDate VARCHAR(20) NOT NULL,
--    AppointmentTime VARCHAR(5) NOT NULL,
--    AppointmentBranch VARCHAR(30) NOT NULL,
--    AppointmentDoctor VARCHAR(20) NOT NULL,
--    AppointmentStatus BIT DEFAULT 0,
--    PatientTC CHAR(15),
--    PatientComplaint VARCHAR(250) NOT NULL
--)

-- Trigger: When adding a doctor, TC checks if it is present in other tables.
--GO
--CREATE TRIGGER trg_Doctor_TcControl
--ON Tbl_Doctors
--INSTEAD OF INSERT
--AS
--BEGIN
--    IF EXISTS (
--        SELECT 1 FROM Tbl_Doctors
--        WHERE DoctorTC IN (SELECT DoctorTC FROM inserted)
--    ) OR EXISTS (
--        SELECT 1 FROM Tbl_Patients
--        WHERE PatientTC IN (SELECT DoctorTC FROM inserted)
--    ) OR EXISTS (
--        SELECT 1 FROM Tbl_Secretary
--        WHERE SecretaryTC IN (SELECT DoctorTC FROM inserted)
--    )
--    BEGIN
--        RAISERROR('Bu TC numarasý baþka bir tabloda mevcut (Doktor).', 16, 1)
--        RETURN
--    END

--    INSERT INTO Tbl_Doctors (DoctorName, DoctorSurname, Branchid, DoctorTC, DoctorPw)
--    SELECT DoctorName, DoctorSurname, Branchid, DoctorTC, DoctorPw
--    FROM inserted
--END

---- Trigger: When adding a patient, it checks whether there is a TC in other tables.
--GO
--CREATE TRIGGER trg_Patient_TcControl
--ON Tbl_Patients
--INSTEAD OF INSERT
--AS
--BEGIN
--    IF EXISTS (
--        SELECT 1 FROM Tbl_Patients
--        WHERE PatientTC IN (SELECT PatientTC FROM inserted)
--    ) OR EXISTS (
--        SELECT 1 FROM Tbl_Doctors
--        WHERE DoctorTC IN (SELECT PatientTC FROM inserted)
--    ) OR EXISTS (
--        SELECT 1 FROM Tbl_Secretary
--        WHERE SecretaryTC IN (SELECT PatientTC FROM inserted)
--    )
--    BEGIN
--        RAISERROR('Bu TC numarasý baþka bir tabloda mevcut (Hasta).', 16, 1)
--        RETURN
--    END

--    INSERT INTO Tbl_Patients (PatientName, PatientSurname, PatientTC, PatientPhone, PatientPw, PatientGender)
--    SELECT PatientName, PatientSurname, PatientTC, PatientPhone, PatientPw, PatientGender
--    FROM inserted
--END

---- Trigger: When adding a secretary, it checks whether the TC exists in other tables.
--GO
--CREATE TRIGGER trg_Secretary_TcControl
--ON Tbl_Secretary
--INSTEAD OF INSERT
--AS
--BEGIN
--    IF EXISTS (
--        SELECT 1 FROM Tbl_Secretary
--        WHERE SecretaryTC IN (SELECT SecretaryTC FROM inserted)
--    ) OR EXISTS (
--        SELECT 1 FROM Tbl_Doctors
--        WHERE DoctorTC IN (SELECT SecretaryTC FROM inserted)
--    ) OR EXISTS (
--        SELECT 1 FROM Tbl_Patients
--        WHERE PatientTC IN (SELECT SecretaryTC FROM inserted)
--    )
--    BEGIN
--        RAISERROR('Bu TC numarasý baþka bir tabloda mevcut (Sekreter).', 16, 1)
--        RETURN
--    END

--    INSERT INTO Tbl_Secretary (SecretaryNameSurname, SecretaryTC, SecretaryPw)
--    SELECT SecretaryNameSurname, SecretaryTC, SecretaryPw
--    FROM inserted
--END

--SELECT name, parent_id, OBJECT_NAME(parent_id) AS TableName
--FROM sys.triggers
--WHERE OBJECT_NAME(parent_id) IN ('Tbl_Patients', 'Tbl_Doctors', 'Tbl_Secretary')

--INSERT INTO Tbl_Branches (BranchName) VALUES 
--('Immunology'),         -- 1 (Anthony Fauci)
--('Psychoanalysis'),     -- 2 (Sigmund Freud)
--('Cardiac Surgery'),    -- 3 (Christiaan Barnard)
--('Neurosurgery'),       -- 4 (Ben Carson)
--('Ophthalmology');      -- 5 (Patricia Bath)

--INSERT INTO Tbl_Doctors (DoctorName, DoctorSurname, Branchid, DoctorTC, DoctorPw) VALUES
--('Anthony', 'Fauci', 1, '10000000001', 'fauci123'),
--('Sigmund', 'Freud', 2, '10000000002', 'freud123'),
--('Christiaan', 'Barnard', 3, '10000000003', 'heart123'),
--('Ben', 'Carson', 4, '10000000004', 'brain123'),
--('Patricia', 'Bath', 5, '10000000005', 'eye123'),
--('Lisa', 'Monroe', 1, '10000000006', 'im123'),
--('Carl', 'Jung', 2, '10000000007', 'ps123'),
--('Michael', 'DeBakey', 3, '10000000008', 'cs123'),
--('Wilder', 'Penfield', 4, '10000000009', 'ns123'),
--('Harold', 'Ridley', 5, '10000000010', 'op123')

--INSERT INTO Tbl_Secretary (SecretaryNameSurname, SecretaryTC, SecretaryPw) VALUES
--('Mary Johnson', '10000000006', 'mary123'),
--('John Smith', '10000000007', 'john123'),
--('Laura Brown', '10000000008', 'laura123'),
--('David Wilson', '10000000009', 'david123');

--CREATE TABLE Tbl_Announcements (
--    Announcementid SMALLINT IDENTITY(1,1) PRIMARY KEY,
--    Announcement VARCHAR(200) NOT NULL
--)