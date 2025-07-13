-- Drop tables if they exist to ensure a clean setup
IF OBJECT_ID('dbo.service_requests', 'U') IS NOT NULL DROP TABLE dbo.service_requests;
IF OBJECT_ID('dbo.technicians', 'U') IS NOT NULL DROP TABLE dbo.technicians;

-- =============================================
-- Table: technicians
-- Stores information about the technicians.
-- =============================================
CREATE TABLE technicians (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(255) NOT NULL,
    specialization NVARCHAR(100) NOT NULL,
    experience_years INT,
    is_available BIT DEFAULT 1,
    created_at DATETIME DEFAULT GETDATE()
);

-- =============================================
-- Table: service_requests
-- Stores all incoming service requests.
-- =============================================
CREATE TABLE service_requests (
    id INT IDENTITY(1,1) PRIMARY KEY,
    description NVARCHAR(MAX) NOT NULL,
    address NVARCHAR(255) NOT NULL,
    service_type NVARCHAR(100) NOT NULL,
    status NVARCHAR(50) NOT NULL DEFAULT 'pending',
    scheduled_at DATETIME NULL,
    technician_id INT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (technician_id) REFERENCES technicians(id) ON DELETE SET NULL
);

-- =============================================
-- Insert Sample Data
-- Populates the tables with the example data.
-- =============================================

-- Insert sample technicians
INSERT INTO technicians (name, specialization, experience_years, is_available) VALUES
('Mario Rossi', 'elettrico', 10, 1),
('Luigi Verdi', 'idraulico', 5, 1),
('Giovanni Bianchi', 'edile', 15, 0);

-- Insert sample service requests
INSERT INTO service_requests (description, address, service_type, status, scheduled_at, technician_id) VALUES
('Presa elettrica non funzionante in cucina.', 'Via Roma 1, Pordenone', 'elettrico', 'pending', NULL, NULL),
('Perdita d''acqua dal rubinetto del bagno.', 'Viale della Vittoria 25, Udine', 'idraulico', 'assigned', '2023-07-20 09:00:00', 2);
