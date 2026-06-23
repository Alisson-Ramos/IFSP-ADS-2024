package br.edu.ifsp.prova2.dao;

import br.edu.ifsp.prova2.model.Order;
import br.edu.ifsp.prova2.util.DbConnection;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class OrderDAO {

    private int getNextId() {
        String sql = "SELECT ISNULL(MAX(ord_no), 0) + 1 FROM orders";
        try (Connection conn = DbConnection.getConnection();
             Statement stmt = conn.createStatement();
             ResultSet rs = stmt.executeQuery(sql)) {
            if (rs.next()) return rs.getInt(1);
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return 1;
    }

    public void insert(Order order) {
        if (order.getOrdNo() <= 0) {
            order.setOrdNo(getNextId());
        }
        String sql = "INSERT INTO orders (ord_no, purch_amt, ord_date, customer_id, salesman_id) VALUES (?, ?, ?, ?, ?)";
        try (Connection conn = DbConnection.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setInt(1, order.getOrdNo());
            stmt.setDouble(2, order.getPurchAmt());
            stmt.setDate(3, new java.sql.Date(order.getOrdDate().getTime()));
            stmt.setInt(4, order.getCustomerId());
            stmt.setInt(5, order.getSalesmanId());
            stmt.executeUpdate();
        } catch (SQLException e) {
            throw new RuntimeException("Erro ao inserir no banco: " + e.getMessage(), e);
        }
    }

    public List<Order> findAll() {
        List<Order> list = new ArrayList<>();
        String sql = "SELECT * FROM orders";
        try (Connection conn = DbConnection.getConnection();
             Statement stmt = conn.createStatement();
             ResultSet rs = stmt.executeQuery(sql)) {
            while (rs.next()) {
                Order o = new Order();
                o.setOrdNo(rs.getInt("ord_no"));
                o.setPurchAmt(rs.getDouble("purch_amt"));
                o.setOrdDate(rs.getDate("ord_date"));
                o.setCustomerId(rs.getInt("customer_id"));
                o.setSalesmanId(rs.getInt("salesman_id"));
                list.add(o);
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return list;
    }

    public Order findById(int id) {
        String sql = "SELECT * FROM orders WHERE ord_no = ?";
        try (Connection conn = DbConnection.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setInt(1, id);
            try (ResultSet rs = stmt.executeQuery()) {
                if (rs.next()) {
                    return new Order(
                            rs.getInt("ord_no"),
                            rs.getDouble("purch_amt"),
                            rs.getDate("ord_date"),
                            rs.getInt("customer_id"),
                            rs.getInt("salesman_id")
                    );
                }
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null;
    }

    public void update(Order order) {
        String sql = "UPDATE orders SET purch_amt = ?, ord_date = ?, customer_id = ?, salesman_id = ? WHERE ord_no = ?";
        try (Connection conn = DbConnection.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setDouble(1, order.getPurchAmt());
            stmt.setDate(2, new java.sql.Date(order.getOrdDate().getTime()));
            stmt.setInt(3, order.getCustomerId());
            stmt.setInt(4, order.getSalesmanId());
            stmt.setInt(5, order.getOrdNo());
            stmt.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void delete(int id) {
        String sql = "DELETE FROM orders WHERE ord_no = ?";
        try (Connection conn = DbConnection.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setInt(1, id);
            stmt.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}
