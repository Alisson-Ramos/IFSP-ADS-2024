package br.edu.ifsp.prova2.servlet;

import br.edu.ifsp.prova2.dao.CustomerDAO;
import br.edu.ifsp.prova2.dao.OrderDAO;
import br.edu.ifsp.prova2.dao.SalesmanDAO;
import br.edu.ifsp.prova2.model.Order;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

@WebServlet("/orders")
public class OrderServlet extends HttpServlet {
    private OrderDAO dao = new OrderDAO();
    private CustomerDAO customerDAO = new CustomerDAO();
    private SalesmanDAO salesmanDAO = new SalesmanDAO();

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        String action = req.getParameter("action");
        if (action == null) action = "list";

        switch (action) {
            case "new":
                req.setAttribute("customers", customerDAO.findAll());
                req.setAttribute("salesmen", salesmanDAO.findAll());
                req.getRequestDispatcher("/order-form.jsp").forward(req, resp);
                break;
            case "edit":
                int id = Integer.parseInt(req.getParameter("id"));
                Order existing = dao.findById(id);
                req.setAttribute("order", existing);
                req.setAttribute("customers", customerDAO.findAll());
                req.setAttribute("salesmen", salesmanDAO.findAll());
                req.getRequestDispatcher("/order-form.jsp").forward(req, resp);
                break;
            case "delete":
                int deleteId = Integer.parseInt(req.getParameter("id"));
                dao.delete(deleteId);
                resp.sendRedirect("orders");
                break;
            default:
                req.setAttribute("listOrder", dao.findAll());
                req.getRequestDispatcher("/order-list.jsp").forward(req, resp);
                break;
        }
    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        int id = 0;
        try {
            id = Integer.parseInt(req.getParameter("ordNo"));
        } catch (NumberFormatException e) {
            id = 0;
        }
        double amt = Double.parseDouble(req.getParameter("purchAmt"));
        String dateStr = req.getParameter("ordDate");
        int customerId = Integer.parseInt(req.getParameter("customerId"));
        int salesmanId = Integer.parseInt(req.getParameter("salesmanId"));

        Date ordDate = new Date();
        try {
            ordDate = new SimpleDateFormat("yyyy-MM-dd").parse(dateStr);
        } catch (ParseException e) {
            e.printStackTrace();
        }

        Order order = new Order(id, amt, ordDate, customerId, salesmanId);

        if (req.getParameter("isEdit") != null && req.getParameter("isEdit").equals("true")) {
            dao.update(order);
        } else {
            dao.insert(order);
        }
        resp.sendRedirect("orders");
    }
}
