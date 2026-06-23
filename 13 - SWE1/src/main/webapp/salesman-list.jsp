<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8">
    <title>Lista de Vendedores - Prova 2</title>
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
                <a href="${pageContext.request.contextPath}/salesmen" class="active">👔 Vendedores</a>
                <a href="${pageContext.request.contextPath}/customers">👥 Clientes</a>
                <a href="${pageContext.request.contextPath}/orders">🛒 Ordens de Venda</a>
            </nav>
        </aside>

        <main class="main-content">
            <header class="topbar">
                <h1 class="page-title">Módulo de Vendedores</h1>
                <div class="user-info">Administrador</div>
            </header>

            <div class="container-fluid">
                <div class="action-bar">
                    <h2 style="color: var(--text-main); font-weight: 600;">Vendedores Cadastrados</h2>
                    <a href="${pageContext.request.contextPath}/salesmen?action=new" class="btn btn-primary">+ Novo Vendedor</a>
                </div>

                <div class="table-container">
                    <table>
                        <thead>
                            <tr>
                                <th>Cód. (ID)</th>
                                <th>Nome</th>
                                <th>Cidade</th>
                                <th>Comissão</th>
                                <th>Ações</th>
                            </tr>
                        </thead>
                        <tbody>
                            <c:forEach var="s" items="${listSalesman}">
                                <tr>
                                    <td><strong><c:out value="${s.salesmanId}" /></strong></td>
                                    <td><c:out value="${s.name}" /></td>
                                    <td><c:out value="${s.city}" /></td>
                                    <td><c:out value="${s.commission * 100}" />%</td>
                                    <td>
                                        <a href="${pageContext.request.contextPath}/salesmen?action=edit&id=${s.salesmanId}" class="btn btn-sm btn-secondary">Editar</a>
                                        <a href="${pageContext.request.contextPath}/salesmen?action=delete&id=${s.salesmanId}" class="btn btn-sm btn-danger" onclick="return confirm('Excluir vendedor?');">Remover</a>
                                    </td>
                                </tr>
                            </c:forEach>
                            <c:if test="${empty listSalesman}">
                                <tr>
                                    <td colspan="5" style="text-align: center; color: var(--text-muted); padding: 2rem;">Nenhum vendedor encontrado no banco de dados.</td>
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
