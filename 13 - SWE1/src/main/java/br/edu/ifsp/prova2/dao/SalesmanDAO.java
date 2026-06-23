package br.edu.ifsp.prova2.dao;

import br.edu.ifsp.prova2.model.Salesman;
import br.edu.ifsp.prova2.util.DbConnection;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class SalesmanDAO {

    private int getNextId() {
        String sql = "SELECT ISNULL(MAX(salesman_id), 0) + 1 FROM salesman";
        try (Connection conn = DbConnection.getConnection();
             Statement stmt = conn.createStatement();
             ResultSet rs = stmt.executeQuery(sql)) {
            if (rs.next()) return rs.getInt(1);
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return 1;
    }

    public void insert(Salesman salesman) {
        if (salesman.getSalesmanId() <= 0) {
            salesman.setSalesmanId(getNextId());
        }
        String sql = "INSERT INTO salesman (salesman_id, name, city, commission) VALUES (?, ?, ?, ?)";
        try (Connection conn = DbConnection.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setInt(1, salesman.getSalesmanId());
            stmt.setString(2, salesman.getName());
            stmt.setString(3, salesman.getCity());
            stmt.setDouble(4, salesman.getCommission());
            stmt.executeUpdate();
        } catch (SQLException e) {
            throw new RuntimeException("Erro ao inserir no banco: " + e.getMessage(), e);
        }
    }

    public List<Salesman> findAll() {
        List<Salesman> list = new ArrayList<>();
        String sql = "SELECT * FROM salesman";
        try (Connection conn = DbConnection.getConnection();
             Statement stmt = conn.createStatement();
             ResultSet rs = stmt.executeQuery(sql)) {
            while (rs.next()) {
                Salesman s = new Salesman();
                s.setSalesmanId(rs.getInt("salesman_id"));
                s.setName(rs.getString("name"));
                s.setCity(rs.getString("city"));
                s.setCommission(rs.getDouble("commission"));
                list.add(s);
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return list;
    }

    public Salesman findById(int id) {
        String sql = "SELECT * FROM salesman WHERE salesman_id = ?";
        try (Connection conn = DbConnection.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setInt(1, id);
            try (ResultSet rs = stmt.executeQuery()) {
                if (rs.next()) {
                    return new Salesman(
                            rs.getInt("salesman_id"),
                            rs.getString("name"),
                            rs.getString("city"),
                            rs.getDouble("commission")
                    );
                }
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null;
    }

    public void update(Salesman salesman) {
        String sql = "UPDATE salesman SET name = ?, city = ?, commission = ? WHERE salesman_id = ?";
        try (Connection conn = DbConnection.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setString(1, salesman.getName());
            stmt.setString(2, salesman.getCity());
            stmt.setDouble(3, salesman.getCommission());
            stmt.setInt(4, salesman.getSalesmanId());
            stmt.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void delete(int id) {
        String sql = "DELETE FROM salesman WHERE salesman_id = ?";
        try (Connection conn = DbConnection.getConnection();
             PreparedStatement stmt = conn.prepareStatement(sql)) {
            stmt.setInt(1, id);
            stmt.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}
