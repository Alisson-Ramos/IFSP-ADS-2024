package br.edu.ifsp.prova2.dao;

import br.edu.ifsp.prova2.util.DbConnection;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class DashboardDAO {

    public List<Map<String, Object>> getSalesAndCommissionData() {
        List<Map<String, Object>> data = new ArrayList<>();
        
        String sql = "SELECT s.name, " +
                     "ISNULL(SUM(o.purch_amt), 0) AS total_sales, " +
                     "ISNULL(SUM(o.purch_amt * s.commission), 0) AS total_commission " +
                     "FROM salesman s " +
                     "LEFT JOIN orders o ON s.salesman_id = o.salesman_id " +
                     "GROUP BY s.name";

        try (Connection conn = DbConnection.getConnection();
             Statement stmt = conn.createStatement();
             ResultSet rs = stmt.executeQuery(sql)) {

            while (rs.next()) {
                Map<String, Object> row = new HashMap<>();
                row.put("name", rs.getString("name"));
                row.put("total_sales", rs.getDouble("total_sales"));
                row.put("total_commission", rs.getDouble("total_commission"));
                data.add(row);
            }

        } catch (Exception e) {
            e.printStackTrace();
        }

        return data;
    }
}
