<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminInformes.aspx.cs" Inherits="TPINT_GRUPO_21_PR3.MenuAdmin.Informes" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>
    <title>Informes Estadísticos</title>
</head>
<body style="background-color: #f8f9fa;">
    
    <div  class="card text-center col-1" style="z-index: 999; position: fixed; right: 20px; bottom: 20px">
      <div class="card-body" >
        <p class="card-text" style="margin: -3px -6px 5px -6px;">Bienvenid@, <br> 
          <asp:Label ID="lblNombreUsuario" runat="server" Text="[Usuario]" style="font-weight: bold;"></asp:Label>
          </p>
          <div class="text-center">
            <img src="../assets/admin-placeholder.png" alt="Administrador-placeholder" style="width:100px; height:auto; margin-bottom:5px;"/>
          </div>
        <a href="../login.aspx" class="btn btn-primary"> Cerrar Sesión </a>
      </div>
    </div>

    <div style="padding: 50px; margin: 50px;">
        <ul class="nav nav-tabs" style=" min-width: 1000px;">
            <li class="nav-item"><a class="nav-link active" aria-current="page" href="AdminInformes.aspx">Informes</a></li>
            <li class="nav-item"><a class="nav-link" href="AdminPacientes.aspx">Gestionar Pacientes</a></li>
            <li class="nav-item"><a class="nav-link" href="AdminMedicos.aspx">Gestionar Medicos</a></li>
            <li class="nav-item"><a class="nav-link" href="AdminTurnos.aspx">Gestionar Turnos</a></li>    
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
                    <div class="col-md-6">
                        <div class="card border-primary shadow-sm mb-4">
                            <div class="card-header bg-primary text-white">
                                <h5 class="mb-0">Presentismo segun fechas</h5>
                            </div>
                            <div class="card-body p-4">
                                <div class="row g-3 mb-4">
                                    <div class="col-6">
                                        <label class="form-label font-weight-bold">Fecha Inicio</label>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label font-weight-bold">Fecha Fin</label>
                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="col-12 text-end">
                                        <asp:Button ID="Button1" runat="server" Text="Filtrar" CssClass="btn btn-primary btn-sm px-4" />
                                    </div>
                                </div>
                                <div class="progress">
                                    <div class="progress-bar bg-success" id="barraPresentes" runat="server" role="progressbar" style="width: 70%" aria-valuenow="70" aria-valuemin="0" aria-valuemax="100">70%</div>
                                    <div class="progress-bar bg-danger" id="barraAusentes" runat="server" role="progressbar" style="width: 30%" aria-valuenow="30" aria-valuemin="0" aria-valuemax="100">30%</div>
                                </div>
                            </div>
                        </div>

                        <div class="card border-primary shadow-sm">
                             <div class="card-header bg-primary text-white">
                                 <h5 class="mb-0">Presentismo segun especialidad / medico</h5>
                             </div>
                             <div class="card-body p-4">
                                 <p class="fw-bold mb-3">Filtrar segun</p>
                                 <div class="row g-3 mb-4">
                                     <div class="col-6">
                                         <div class="form-group">
                                             <asp:DropDownList ID="ddlPresentismoFiltrado" runat="server" CssClass="form-control">
                                                 <asp:ListItem Value="0"> -- seleccione una opcion -- </asp:ListItem>
                                                 <asp:ListItem Value="1">Medico</asp:ListItem>
                                                 <asp:ListItem Value="2">Especialidad</asp:ListItem>
                                             </asp:DropDownList>
                                         </div>
                                     </div>
                                     <div class="col-6">
                                         <div class="form-group">
                                             <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                                 <asp:ListItem Value="0"> -- seleccione una opcion -- </asp:ListItem>
                                             </asp:DropDownList>
                                         </div>
                                     </div>
                                     <div class="col-12 text-end">
                                         <asp:Button ID="Button2" runat="server" Text="Filtrar" CssClass="btn btn-primary btn-sm px-4" />
                                     </div>
                                 </div>
                                 <div class="progress">
                                     <div class="progress-bar bg-success" id="Div1" runat="server" role="progressbar" style="width: 70%" aria-valuenow="70" aria-valuemin="0" aria-valuemax="100"> 70% </div>
                                     <div class="progress-bar bg-danger" id="Div2" runat="server" role="progressbar" style="width: 30%" aria-valuenow="30" aria-valuemin="0" aria-valuemax="100"> 30% </div>
                                 </div>
                             </div>
                        </div>
                    </div>
                </div> 
            </form>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js"></script>
    <script src="../js/bootstrap.bundle.min.js"></script>
</body>
</html>