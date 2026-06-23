package br.edu.ifsp.prova2.servlet;

import br.edu.ifsp.prova2.dao.DashboardDAO;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.util.List;
import java.util.Map;

@WebServlet(urlPatterns = {"", "/dashboard"})
public class DashboardServlet extends HttpServlet {

    private DashboardDAO dao = new DashboardDAO();

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        List<Map<String, Object>> metrics = dao.getSalesAndCommissionData();
        
        double totalSalesGeneral = 0;
        double totalCommissionsGeneral = 0;

        for (Map<String, Object> m : metrics) {
            totalSalesGeneral += (Double) m.get("total_sales");
            totalCommissionsGeneral += (Double) m.get("total_commission");
        }

        req.setAttribute("metrics", metrics);
        req.setAttribute("totalSalesGeneral", totalSalesGeneral);
        req.setAttribute("totalCommissionsGeneral", totalCommissionsGeneral);

        req.getRequestDispatcher("/index.jsp").forward(req, resp);
    }
}
