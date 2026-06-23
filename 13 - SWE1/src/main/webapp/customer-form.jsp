<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8">
    <title>Formulário de Cliente</title>
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
            </header>

            <div class="container-fluid">
                <div class="card" style="max-width: 600px; margin: 0 auto;">
                    <h2 style="margin-bottom: 1.5rem; border-bottom: 1px solid var(--border); padding-bottom: 1rem;">
                        <c:if test="${customer != null}">Editar Cliente</c:if>
                        <c:if test="${customer == null}">Cadastrar Novo Cliente</c:if>
                    </h2>

                    <form action="${pageContext.request.contextPath}/customers" method="post">
                        <c:if test="${customer != null}">
                            <input type="hidden" name="isEdit" value="true">
                        </c:if>

                        <div class="form-group">
                            <label>Código do Cliente</label>
                            <c:if test="${customer != null}">
                                <input type="number" name="customerId" class="form-control" value="<c:out value='${customer.customerId}' />" readonly />
                            </c:if>
                            <c:if test="${customer == null}">
                                <input type="text" name="customerId" class="form-control" value="[Gerado Automaticamente]" readonly />
                            </c:if>
                        </div>

                        <div class="form-group">
                            <label>Nome do Cliente</label>
                            <input type="text" name="custName" class="form-control" value="<c:out value='${customer.custName}' />" required />
                        </div>

                        <div class="form-group">
                            <label>Cidade</label>
                            <input type="text" name="city" class="form-control" value="<c:out value='${customer.city}' />" required />
                        </div>

                        <div class="form-group">
                            <label>Classificação (Grade Numérica)</label>
                            <input type="number" name="grade" class="form-control" value="<c:out value='${customer.grade}' />" required />
                        </div>

                        <div class="form-group">
                            <label>Vendedor Responsável pela Conta</label>
                            <select name="salesmanId" class="form-control" required>
                                <option value="">[ Selecione um vendedor ]</option>
                                <c:forEach var="s" items="${salesmen}">
                                    <option value="${s.salesmanId}" <c:if test="${customer.salesmanId == s.salesmanId}">selected</c:if>>
                                        Cód: ${s.salesmanId} - ${s.name}
                                    </option>
                                </c:forEach>
                            </select>
                        </div>

                        <div style="display: flex; gap: 1rem; margin-top: 2rem;">
                            <button type="submit" class="btn btn-primary" style="flex: 1;">Salvar Registro</button>
                            <a href="${pageContext.request.contextPath}/customers" class="btn" style="background-color: var(--surface-hover); color: var(--text-main); flex: 1;">Cancelar</a>
                        </div>
                    </form>
                </div>
            </div>
        </main>
    </div>
</body>
</html>
