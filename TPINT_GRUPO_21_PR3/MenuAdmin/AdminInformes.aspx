<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminInformes.aspx.cs" Inherits="TPINT_GRUPO_21_PR3.MenuAdmin.Informes" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<<<<<<< Updated upstream
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous">

    <title></title>
=======
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>
    <title>Informes Estadísticos</title>
>>>>>>> Stashed changes
</head>
<body style="background-color: #f8f9fa;">
    
    <div class="card" style="z-index: 999; position: fixed; right: 20px; bottom: 20px">
      <div class="card-body" >
        <p class="card-text">Bienvenido: [Usuario].</p>
        <a href="login.aspx" class="btn btn-primary"> Cerrar Sesion </a>
      </div>
    </div>

    <div style="padding: 50px; margin: 50px;">
        <ul class="nav nav-tabs" style=" min-width: 1000px;">
            <li class="nav-item"><a class="nav-link" href="AdminInicio.aspx">Inicio</a></li>
            <li class="nav-item"><a class="nav-link" href="AdminPacientes.aspx">Gestionar Pacientes</a></li>
            <li class="nav-item"><a class="nav-link" href="AdminMedicos.aspx">Gestionar Medicos</a></li>
            <li class="nav-item"><a class="nav-link" href="AdminTurnos.aspx">Gestionar Turnos</a></li>
            <li class="nav-item"><a class="nav-link active" aria-current="page" href="AdminInformes.aspx">Informes</a></li>
        </ul>
        
        <div class="border border-top-0 p-5" style="background-color: white;" >
            <form id="form1" runat="server">
                <div class="row">     
                    <div class="col-md-6">
                        <div class="card border-primary shadow-sm">
                            <div class="card-header bg-primary text-white">
                                <h5 class="mb-0">Informe Productividad Médica</h5>
                            </div>
                            <div class="card-body p-4">
                                <div class="row g-3 mb-4">
                                    <div class="col-6">
                                        <label class="form-label font-weight-bold">Fecha Inicio</label>
                                        <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label font-weight-bold">Fecha Fin</label>
                                        <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="col-12 text-end">
                                        <asp:Button ID="btnFiltrarRanking" runat="server" Text="Filtrar" CssClass="btn btn-primary btn-sm px-4" />
                                    </div>
                                </div>

                                <div class="table-responsive">
                                    <asp:GridView ID="gvRankingEspecialidades" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered align-middle">
                                        <Columns>
                                            <asp:BoundField DataField="Especialidad" HeaderText="Especialidad" />
                                            <asp:BoundField DataField="CantidadTurnos" HeaderText="Cantidad de Turnos" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div> 
            </form>
        </div>
    </div>
<<<<<<< Updated upstream
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js" integrity="sha384-FKyoEForCGlyvwx9Hj09JcYn3nv7wiPVlz7YYwJrWVcXK/BmnVDxM+D2scQbITxI" crossorigin="anonymous"></script>
=======
    <script src="../js/bootstrap.bundle.min.js"></script>
>>>>>>> Stashed changes
</body>
</html>