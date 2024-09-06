-- CREATE DATABASE
CREATE DATABASE BankMegaTechTest;

-- USE DATABASE
USE BankMegaTechTest;

-- CREATE TABLE MS_STORAGE_LOCATION
CREATE TABLE ms_storage_location (
    location_id VARCHAR(10) PRIMARY KEY,
    location_name VARCHAR(100) NOT NULL
);

-- CREATE TABLE MS_USER
CREATE TABLE ms_user (
    user_id BIGINT PRIMARY KEY,
    user_name VARCHAR(20) NOT NULL,
    password VARCHAR(50) NOT NULL,
    is_active BIT NOT NULL
);

-- CREATE TABLE TR_BPKB
CREATE TABLE tr_bpkb (
    agreement_number VARCHAR(100) PRIMARY KEY,
    bpkb_no VARCHAR(100) NOT NULL,
    branch_id VARCHAR(10),
    bpkb_date DATETIME NOT NULL,
    faktur_no VARCHAR(100),
    faktur_date DATETIME,
    location_id VARCHAR(10),
    police_no VARCHAR(20),
    bpkb_date_in DATETIME,
    user_id BIGINT,
    FOREIGN KEY (location_id) REFERENCES ms_storage_location(location_id),
    FOREIGN KEY (user_id) REFERENCES ms_user(user_id)
);
