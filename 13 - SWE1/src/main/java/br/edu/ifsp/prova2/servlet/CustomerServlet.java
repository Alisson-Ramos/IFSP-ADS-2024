package br.edu.ifsp.prova2.servlet;

import br.edu.ifsp.prova2.dao.CustomerDAO;
import br.edu.ifsp.prova2.dao.SalesmanDAO;
import br.edu.ifsp.prova2.model.Customer;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

@WebServlet("/customers")
public class CustomerServlet extends HttpServlet {
    private CustomerDAO dao = new CustomerDAO();
    private SalesmanDAO salesmanDAO = new SalesmanDAO();

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        String action = req.getParameter("action");
        if (action == null) action = "list";

        switch (action) {
            case "new":
                req.setAttribute("salesmen", salesmanDAO.findAll());
                req.getRequestDispatcher("/customer-form.jsp").forward(req, resp);
                break;
            case "edit":
                int id = Integer.parseInt(req.getParameter("id"));
                Customer existing = dao.findById(id);
                req.setAttribute("customer", existing);
                req.setAttribute("salesmen", salesmanDAO.findAll());
                req.getRequestDispatcher("/customer-form.jsp").forward(req, resp);
                break;
            case "delete":
                int deleteId = Integer.parseInt(req.getParameter("id"));
                dao.delete(deleteId);
                resp.sendRedirect("customers");
                break;
            default:
                req.setAttribute("listCustomer", dao.findAll());
                req.getRequestDispatcher("/customer-list.jsp").forward(req, resp);
                break;
        }
    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        int id = 0;
        try {
            id = Integer.parseInt(req.getParameter("customerId"));
        } catch (NumberFormatException e) {
            id = 0;
        }
        String name = req.getParameter("custName");
        String city = req.getParameter("city");
        int grade = Integer.parseInt(req.getParameter("grade"));
        int salesmanId = Integer.parseInt(req.getParameter("salesmanId"));

        Customer customer = new Customer(id, name, city, grade, salesmanId);

        if (req.getParameter("isEdit") != null && req.getParameter("isEdit").equals("true")) {
            dao.update(customer);
        } else {
            dao.insert(customer);
        }
        resp.sendRedirect("customers");
    }
}
