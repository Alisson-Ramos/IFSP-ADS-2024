package br.edu.ifsp.prova2.servlet;

import br.edu.ifsp.prova2.dao.SalesmanDAO;
import br.edu.ifsp.prova2.model.Salesman;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

@WebServlet("/salesmen")
public class SalesmanServlet extends HttpServlet {
    private SalesmanDAO dao = new SalesmanDAO();

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        String action = req.getParameter("action");
        if (action == null) action = "list";

        switch (action) {
            case "new":
                req.getRequestDispatcher("/salesman-form.jsp").forward(req, resp);
                break;
            case "edit":
                int id = Integer.parseInt(req.getParameter("id"));
                Salesman existing = dao.findById(id);
                req.setAttribute("salesman", existing);
                req.getRequestDispatcher("/salesman-form.jsp").forward(req, resp);
                break;
            case "delete":
                int deleteId = Integer.parseInt(req.getParameter("id"));
                dao.delete(deleteId);
                resp.sendRedirect("salesmen");
                break;
            default:
                req.setAttribute("listSalesman", dao.findAll());
                req.getRequestDispatcher("/salesman-list.jsp").forward(req, resp);
                break;
        }
    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        int id = 0;
        try {
            id = Integer.parseInt(req.getParameter("salesmanId"));
        } catch (NumberFormatException e) {
            id = 0;
        }
        String name = req.getParameter("name");
        String city = req.getParameter("city");
        double commission = Double.parseDouble(req.getParameter("commission"));

        Salesman salesman = new Salesman(id, name, city, commission);

        // Se veio de um edit, vamos tentar atualizar. Senão inserir.
        if (req.getParameter("isEdit") != null && req.getParameter("isEdit").equals("true")) {
            dao.update(salesman);
        } else {
            dao.insert(salesman);
        }
        resp.sendRedirect("salesmen");
    }
}
