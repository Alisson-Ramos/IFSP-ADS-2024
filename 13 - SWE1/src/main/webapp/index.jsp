<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/fmt" prefix="fmt" %>
<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Enterprise Dashboard - Prova 2</title>
    <link rel="stylesheet" href="${pageContext.request.contextPath}/css/style-corp.css">
    <!-- Chart.js CDN -->
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
</head>
<body>
    <div class="layout">
        <!-- Sidebar Corporativa -->
        <aside class="sidebar">
            <div class="sidebar-header">
                <h2 class="logo">CBTSWE1</h2>
            </div>
            <nav class="sidebar-nav">
                <a href="${pageContext.request.contextPath}/dashboard" class="active">📊 Dashboard</a>
                <a href="${pageContext.request.contextPath}/salesmen">👔 Vendedores</a>
                <a href="${pageContext.request.contextPath}/customers">👥 Clientes</a>
                <a href="${pageContext.request.contextPath}/orders">🛒 Ordens de Venda</a>
            </nav>
        </aside>

        <!-- Main Content -->
        <main class="main-content">
            <header class="topbar">
                <h1 class="page-title">Visão Geral Corporativa</h1>
                <div class="user-info">Administrador</div>
            </header>

            <div class="container-fluid">
                <!-- KPIs -->
                <div class="kpi-grid">
                    <div class="kpi-card">
                        <h3>Volume de Vendas Global</h3>
                        <p class="kpi-value">
                            R$ <fmt:formatNumber value="${totalSalesGeneral}" minFractionDigits="2" maxFractionDigits="2"/>
                        </p>
                    </div>
                    <div class="kpi-card highlight">
                        <h3>Total de Comissões Pagas</h3>
                        <p class="kpi-value">
                            R$ <fmt:formatNumber value="${totalCommissionsGeneral}" minFractionDigits="2" maxFractionDigits="2"/>
                        </p>
                    </div>
                    <div class="kpi-card secondary">
                        <h3>Força de Vendas Ativa</h3>
                        <p class="kpi-value">${metrics.size()} Colaboradores</p>
                    </div>
                </div>

                <!-- Gráfico Chart.js -->
                <div class="card chart-container">
                    <h2 style="margin-bottom: 1rem; color: var(--text-main);">Performance de Vendas x Comissões por Vendedor</h2>
                    <canvas id="salesChart"></canvas>
                </div>
            </div>
        </main>
    </div>

    <!-- Script de Renderização do Gráfico -->
    <script>
        const ctx = document.getElementById('salesChart').getContext('2d');
        
        // Arrays vazios populados pelo JSTL
        const labels = [];
        const dataSales = [];
        const dataCommissions = [];

        <c:forEach var="item" items="${metrics}">
            labels.push('${item.name}');
            dataSales.push(${item.total_sales});
            dataCommissions.push(${item.total_commission});
        </c:forEach>

        new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [
                    {
                        label: 'Total de Vendas (R$)',
                        data: dataSales,
                        backgroundColor: 'rgba(79, 70, 229, 0.8)', // Primary Color
                        borderColor: 'rgba(79, 70, 229, 1)',
                        borderWidth: 1,
                        borderRadius: 4
                    },
                    {
                        label: 'Comissões (R$)',
                        data: dataCommissions,
                        backgroundColor: 'rgba(16, 185, 129, 0.8)', // Secondary Color
                        borderColor: 'rgba(16, 185, 129, 1)',
                        borderWidth: 1,
                        borderRadius: 4
                    }
                ]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: { position: 'top' },
                    tooltip: {
                        callbacks: {
                            label: function(context) {
                                let label = context.dataset.label || '';
                                if (label) label += ': ';
                                if (context.parsed.y !== null) {
                                    label += new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(context.parsed.y);
                                }
                                return label;
                            }
                        }
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        ticks: {
                            callback: function(value) {
                                return 'R$ ' + value;
                            }
                        }
                    }
                }
            }
        });
    </script>
</body>
</html>
