package br.edu.ifsp.prova2.dao;

import br.edu.ifsp.prova2.model.Customer;
import br.edu.ifsp.prova2.util.DbConnection;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class CustomerDAO {

    private int getNextId() {
        String sql = "SELECT ISNULL(MAX(customer_id), 0) + 1 FROM customer";
        try (Connection conn = DbConnection.getConnection();
             Statement stmt = conn.createStatement();
             ResultSet rs = stmt.executeQuery(sql)) {
            if (rs.next()) return rs.getInt(1);
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return 1;
    }

    public void insert(Customer customer) {
        if (customer.getCustomerId() <= 0) {
            customer.setCustomerId(getNextId());
        }
        String sql = "INSERT INTO customer (customer_id, cust_name, city, grade, salesman_id) VALUES (?, ?, ?, ?, ?)";
        try (Connection conn = DbConnection.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setInt(1, customer.getCustomerId());
            stmt.setString(2, customer.getCustName());
            stmt.setString(3, customer.getCity());
            stmt.setInt(4, customer.getGrade());
            stmt.setInt(5, customer.getSalesmanId());
            stmt.executeUpdate();
        } catch (SQLException e) {
            throw new RuntimeException("Erro ao inserir no banco: " + e.getMessage(), e);
        }
    }

    public List<Customer> findAll() {
        List<Customer> list = new ArrayList<>();
        String sql = "SELECT * FROM customer";
        try (Connection conn = DbConnection.getConnection();
             Statement stmt = conn.createStatement();
             ResultSet rs = stmt.executeQuery(sql)) {
            while (rs.next()) {
                Customer c = new Customer();
                c.setCustomerId(rs.getInt("customer_id"));
                c.setCustName(rs.getString("cust_name"));
                c.setCity(rs.getString("city"));
                c.setGrade(rs.getInt("grade"));
                c.setSalesmanId(rs.getInt("salesman_id"));
                list.add(c);
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return list;
    }

    public Customer findById(int id) {
        String sql = "SELECT * FROM customer WHERE customer_id = ?";
        try (Connection conn = DbConnection.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setInt(1, id);
            try (ResultSet rs = stmt.executeQuery()) {
                if (rs.next()) {
                    return new Customer(
                            rs.getInt("customer_id"),
                            rs.getString("cust_name"),
                            rs.getString("city"),
                            rs.getInt("grade"),
                            rs.getInt("salesman_id")
                    );
                }
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null;
    }

    public void update(Customer customer) {
        String sql = "UPDATE customer SET cust_name = ?, city = ?, grade = ?, salesman_id = ? WHERE customer_id = ?";
        try (Connection conn = DbConnection.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setString(1, customer.getCustName());
            stmt.setString(2, customer.getCity());
            stmt.setInt(3, customer.getGrade());
            stmt.setInt(4, customer.getSalesmanId());
            stmt.setInt(5, customer.getCustomerId());
            stmt.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void delete(int id) {
        String sql = "DELETE FROM customer WHERE customer_id = ?";
        try (Connection conn = DbConnection.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setInt(1, id);
            stmt.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}
