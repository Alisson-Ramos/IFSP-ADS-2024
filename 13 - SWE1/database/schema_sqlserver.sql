-- Arquivo de referência para criação das tabelas no SQL Server

-- Verifica e cria tabela salesman
IF OBJECT_ID('salesman', 'U') IS NULL
BEGIN
    CREATE TABLE salesman (
        salesman_id NUMERIC(5) PRIMARY KEY,
        name VARCHAR(30) NOT NULL,
        city VARCHAR(15) NOT NULL,
        commission DECIMAL(5,2) NOT NULL
    );
END

-- Verifica e cria tabela customer
IF OBJECT_ID('customer', 'U') IS NULL
BEGIN
    CREATE TABLE customer (
        customer_id NUMERIC(5) PRIMARY KEY,
        cust_name VARCHAR(30) NOT NULL,
        city VARCHAR(15) NOT NULL,
        grade NUMERIC(3) NOT NULL,
        salesman_id NUMERIC(5) NOT NULL,
        CONSTRAINT fk_customer_salesman FOREIGN KEY (salesman_id) REFERENCES salesman (salesman_id)
    );
END

-- Verifica e cria tabela orders
IF OBJECT_ID('orders', 'U') IS NULL
BEGIN
    CREATE TABLE orders (
        ord_no NUMERIC(5) PRIMARY KEY,
        purch_amt DECIMAL(8,2) NOT NULL,
        ord_date DATE NOT NULL,
        customer_id NUMERIC(5) NOT NULL,
        salesman_id NUMERIC(5) NOT NULL,
        CONSTRAINT fk_orders_customer FOREIGN KEY (customer_id) REFERENCES customer (customer_id),
        CONSTRAINT fk_orders_salesman FOREIGN KEY (salesman_id) REFERENCES salesman (salesman_id)
    );
END
