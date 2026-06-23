<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8">
    <title>Formulário de Vendedor</title>
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
            </header>

            <div class="container-fluid">
                <div class="card" style="max-width: 600px; margin: 0 auto;">
                    <h2 style="margin-bottom: 1.5rem; border-bottom: 1px solid var(--border); padding-bottom: 1rem;">
                        <c:if test="${salesman != null}">Editar Vendedor</c:if>
                        <c:if test="${salesman == null}">Cadastrar Novo Vendedor</c:if>
                    </h2>

                    <form action="${pageContext.request.contextPath}/salesmen" method="post">
                        <c:if test="${salesman != null}">
                            <input type="hidden" name="isEdit" value="true">
                        </c:if>

                        <div class="form-group">
                            <label>Código do Vendedor</label>
                            <c:if test="${salesman != null}">
                                <input type="number" name="salesmanId" class="form-control" value="<c:out value='${salesman.salesmanId}' />" readonly />
                            </c:if>
                            <c:if test="${salesman == null}">
                                <input type="text" name="salesmanId" class="form-control" value="[Gerado Automaticamente]" readonly />
                            </c:if>
                        </div>

                        <div class="form-group">
                            <label>Nome Completo</label>
                            <input type="text" name="name" class="form-control" value="<c:out value='${salesman.name}' />" required />
                        </div>

                        <div class="form-group">
                            <label>Cidade de Atuação</label>
                            <input type="text" name="city" class="form-control" value="<c:out value='${salesman.city}' />" required />
                        </div>

                        <div class="form-group">
                            <label>Taxa de Comissão (Decimal, ex: 0.15 para 15%)</label>
                            <input type="number" step="0.01" name="commission" class="form-control" value="<c:out value='${salesman.commission}' />" required />
                        </div>

                        <div style="display: flex; gap: 1rem; margin-top: 2rem;">
                            <button type="submit" class="btn btn-primary" style="flex: 1;">Salvar Registro</button>
                            <a href="${pageContext.request.contextPath}/salesmen" class="btn" style="background-color: var(--surface-hover); color: var(--text-main); flex: 1;">Cancelar</a>
                        </div>
                    </form>
                </div>
            </div>
        </main>
    </div>
</body>
</html>
