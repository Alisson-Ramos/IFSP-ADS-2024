<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/fmt" prefix="fmt" %>
<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8">
    <title>Lista de Vendas - Prova 2</title>
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
                <a href="${pageContext.request.contextPath}/customers">👥 Clientes</a>
                <a href="${pageContext.request.contextPath}/orders" class="active">🛒 Ordens de Venda</a>
            </nav>
        </aside>

        <main class="main-content">
            <header class="topbar">
                <h1 class="page-title">Módulo de Vendas (Orders)</h1>
                <div class="user-info">Administrador</div>
            </header>

            <div class="container-fluid">
                <div class="action-bar">
                    <h2 style="color: var(--text-main); font-weight: 600;">Registro de Transações</h2>
                    <a href="${pageContext.request.contextPath}/orders?action=new" class="btn btn-primary">+ Nova Venda</a>
                </div>

                <div class="table-container">
                    <table>
                        <thead>
                            <tr>
                                <th>Cód. (Nº Ordem)</th>
                                <th>Valor da Compra</th>
                                <th>Data da Ordem</th>
                                <th>ID Cliente</th>
                                <th>ID Vendedor</th>
                                <th>Ações</th>
                            </tr>
                        </thead>
                        <tbody>
                            <c:forEach var="o" items="${listOrder}">
                                <tr>
                                    <td><strong><c:out value="${o.ordNo}" /></strong></td>
                                    <td>R$ <fmt:formatNumber value="${o.purchAmt}" minFractionDigits="2" maxFractionDigits="2"/></td>
                                    <td><fmt:formatDate value="${o.ordDate}" pattern="dd/MM/yyyy"/></td>
                                    <td><c:out value="${o.customerId}" /></td>
                                    <td><c:out value="${o.salesmanId}" /></td>
                                    <td>
                                        <a href="${pageContext.request.contextPath}/orders?action=edit&id=${o.ordNo}" class="btn btn-sm btn-secondary">Editar</a>
                                        <a href="${pageContext.request.contextPath}/orders?action=delete&id=${o.ordNo}" class="btn btn-sm btn-danger" onclick="return confirm('Excluir venda?');">Remover</a>
                                    </td>
                                </tr>
                            </c:forEach>
                            <c:if test="${empty listOrder}">
                                <tr>
                                    <td colspan="6" style="text-align: center; color: var(--text-muted); padding: 2rem;">Nenhuma venda registrada no sistema.</td>
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
