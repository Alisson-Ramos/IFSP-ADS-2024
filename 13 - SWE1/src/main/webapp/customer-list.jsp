<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8">
    <title>Lista de Clientes - Prova 2</title>
    <link rel="stylesheet" href="${pageContext.request.contextPath}/css/style-corp.css">
</head>
<body>
    <div class="layout">
        <aside class="sidebar">
            <div class="sidebar-header">
                <h2 class="logo">CBTSWE1</h2>
            </div>
            <nav class="sidebar-nav">
                <a href="${pageContext.request.contextPath}/dashboard">📊 Dashboard</a>
                <a href="${pageContext.request.contextPath}/salesmen">👔 Vendedores</a>
                <a href="${pageContext.request.contextPath}/customers" class="active">👥 Clientes</a>
                <a href="${pageContext.request.contextPath}/orders">🛒 Ordens de Venda</a>
            </nav>
        </aside>

        <main class="main-content">
            <header class="topbar">
                <h1 class="page-title">Módulo de Clientes</h1>
                <div class="user-info">Administrador</div>
            </header>

            <div class="container-fluid">
                <div class="action-bar">
                    <h2 style="color: var(--text-main); font-weight: 600;">Clientes Cadastrados</h2>
                    <a href="${pageContext.request.contextPath}/customers?action=new" class="btn btn-primary">+ Novo Cliente</a>
                </div>

                <div class="table-container">
                    <table>
                        <thead>
                            <tr>
                                <th>Cód. (ID)</th>
                                <th>Nome do Cliente</th>
                                <th>Cidade</th>
                                <th>Categoria (Grade)</th>
                                <th>ID Vendedor Resp.</th>
                                <th>Ações</th>
                            </tr>
                        </thead>
                        <tbody>
                            <c:forEach var="c" items="${listCustomer}">
                                <tr>
                                    <td><strong><c:out value="${c.customerId}" /></strong></td>
                                    <td><c:out value="${c.custName}" /></td>
                                    <td><c:out value="${c.city}" /></td>
                                    <td><c:out value="${c.grade}" /></td>
                                    <td><c:out value="${c.salesmanId}" /></td>
                                    <td>
                                        <a href="${pageContext.request.contextPath}/customers?action=edit&id=${c.customerId}" class="btn btn-sm btn-secondary">Editar</a>
                                        <a href="${pageContext.request.contextPath}/customers?action=delete&id=${c.customerId}" class="btn btn-sm btn-danger" onclick="return confirm('Excluir cliente?');">Remover</a>
                                    </td>
                                </tr>
                            </c:forEach>
                            <c:if test="${empty listCustomer}">
                                <tr>
                                    <td colspan="6" style="text-align: center; color: var(--text-muted); padding: 2rem;">Nenhum cliente encontrado no banco de dados.</td>
                                </tr>
                            </c:if>
                        </tbody>
                    </table>
                </div>
            </div>
        </main>
    </div>
</body>
</html>
