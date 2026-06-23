package br.edu.ifsp.prova2.util;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.Statement;

public class DbConnection {

    // Configurações com base no enunciado
    private static final String URL = "jdbc:sqlserver://192.168.1.10:1433;databaseName=master;encrypt=true;trustServerCertificate=true;";
    private static final String USER = "sa";
    private static final String PASSWORD = "root";

    private static boolean initialized = false;

    public static Connection getConnection() throws SQLException {
        try {
            // Carrega o driver do SQL Server
            Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
        } catch (ClassNotFoundException e) {
            throw new SQLException("SQL Server Driver not found.", e);
        }

        Connection conn = DriverManager.getConnection(URL, USER, PASSWORD);
        if (!initialized) {
            initializeDatabase(conn);
            initialized = true;
        }
        return conn;
    }

    private static void initializeDatabase(Connection conn) {
        try (Statement stmt = conn.createStatement()) {
            
            // Tabela salesman
            String createSalesman = "IF OBJECT_ID('salesman', 'U') IS NULL " +
                    "BEGIN " +
                    "   CREATE TABLE salesman (" +
                    "       salesman_id NUMERIC(5) PRIMARY KEY," +
                    "       name VARCHAR(30) NOT NULL," +
                    "       city VARCHAR(15) NOT NULL," +
                    "       commission DECIMAL(5,2) NOT NULL" +
                    "   ); " +
                    "END";
            stmt.execute(createSalesman);

            // Tabela customer
            String createCustomer = "IF OBJECT_ID('customer', 'U') IS NULL " +
                    "BEGIN " +
                    "   CREATE TABLE customer (" +
                    "       customer_id NUMERIC(5) PRIMARY KEY," +
                    "       cust_name VARCHAR(30) NOT NULL," +
                    "       city VARCHAR(15) NOT NULL," +
                    "       grade NUMERIC(3) NOT NULL," +
                    "       salesman_id NUMERIC(5) NOT NULL," +
                    "       CONSTRAINT fk_customer_salesman FOREIGN KEY (salesman_id) REFERENCES salesman (salesman_id)" +
                    "   ); " +
                    "END";
            stmt.execute(createCustomer);

            // Tabela orders
            String createOrders = "IF OBJECT_ID('orders', 'U') IS NULL " +
                    "BEGIN " +
                    "   CREATE TABLE orders (" +
                    "       ord_no NUMERIC(5) PRIMARY KEY," +
                    "       purch_amt DECIMAL(8,2) NOT NULL," +
                    "       ord_date DATE NOT NULL," +
                    "       customer_id NUMERIC(5) NOT NULL," +
                    "       salesman_id NUMERIC(5) NOT NULL," +
                    "       CONSTRAINT fk_orders_customer FOREIGN KEY (customer_id) REFERENCES customer (customer_id)," +
                    "       CONSTRAINT fk_orders_salesman FOREIGN KEY (salesman_id) REFERENCES salesman (salesman_id)" +
                    "   ); " +
                    "END";
            stmt.execute(createOrders);

        } catch (SQLException e) {
            e.printStackTrace();
            System.err.println("Erro ao inicializar as tabelas do banco de dados: " + e.getMessage());
        }
    }
}
