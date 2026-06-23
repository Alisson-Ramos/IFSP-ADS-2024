<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8">
    <title>Formulário de Venda</title>
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
            </header>

            <div class="container-fluid">
                <div class="card" style="max-width: 600px; margin: 0 auto;">
                    <h2 style="margin-bottom: 1.5rem; border-bottom: 1px solid var(--border); padding-bottom: 1rem;">
                        <c:if test="${order != null}">Editar Venda Transacional</c:if>
                        <c:if test="${order == null}">Registrar Nova Venda</c:if>
                    </h2>

                    <form action="${pageContext.request.contextPath}/orders" method="post">
                        <c:if test="${order != null}">
                            <input type="hidden" name="isEdit" value="true">
                        </c:if>

                        <div class="form-group">
                            <label>Nº da Ordem (Código)</label>
                            <c:if test="${order != null}">
                                <input type="number" name="ordNo" class="form-control" value="<c:out value='${order.ordNo}' />" readonly />
                            </c:if>
                            <c:if test="${order == null}">
                                <input type="text" name="ordNo" class="form-control" value="[Gerado Automaticamente]" readonly />
                            </c:if>
                        </div>

                        <div class="form-group">
                            <label>Valor da Compra (R$)</label>
                            <input type="number" step="0.01" name="purchAmt" class="form-control" value="<c:out value='${order.purchAmt}' />" required />
                        </div>

                        <div class="form-group">
                            <label>Data da Venda</label>
                            <input type="date" name="ordDate" class="form-control" value="<c:out value='${order.ordDate}' />" required />
                        </div>

                        <div class="form-group">
                            <label>Cliente Comprador</label>
                            <select name="customerId" class="form-control" required>
                                <option value="">[ Selecione um cliente ]</option>
                                <c:forEach var="c" items="${customers}">
                                    <option value="${c.customerId}" <c:if test="${order.customerId == c.customerId}">selected</c:if>>
                                        Cód: ${c.customerId} - ${c.custName}
                                    </option>
                                </c:forEach>
                            </select>
                        </div>

                        <div class="form-group">
                            <label>Vendedor Responsável (Comissionado)</label>
                            <select name="salesmanId" class="form-control" required>
                                <option value="">[ Selecione um vendedor ]</option>
                                <c:forEach var="s" items="${salesmen}">
                                    <option value="${s.salesmanId}" <c:if test="${order.salesmanId == s.salesmanId}">selected</c:if>>
                                        Cód: ${s.salesmanId} - ${s.name}
                                    </option>
                                </c:forEach>
                            </select>
                        </div>

                        <div style="display: flex; gap: 1rem; margin-top: 2rem;">
                            <button type="submit" class="btn btn-primary" style="flex: 1;">Salvar Registro</button>
                            <a href="${pageContext.request.contextPath}/orders" class="btn" style="background-color: var(--surface-hover); color: var(--text-main); flex: 1;">Cancelar</a>
                        </div>
                    </form>
                </div>
            </div>
        </main>
    </div>
</body>
</html>
