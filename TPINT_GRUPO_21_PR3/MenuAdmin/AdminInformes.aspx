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
        <p class="card-text" style="margin: -3px -6px 5px -6px;">Bienvenid@, <br/> 
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

        <div class="border border-top-0 p-5" style="background-color: white;">
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
                                        <asp:RequiredFieldValidator ID="rfvFechaInicioProductividad" runat="server" ErrorMessage="*" ControlToValidate="txtFechaInicioProductividad" ForeColor="Red" ValidationGroup="informeProductividad"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revFechaInicioProductividad" runat="server" ErrorMessage="* dd/mm/aaaa" ControlToValidate="txtFechaInicioProductividad" ForeColor="Red" ValidationExpression="^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$" ValidationGroup="informeProductividad"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtFechaInicioProductividad" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label font-weight-bold">Fecha Fin</label>
                                        <asp:RequiredFieldValidator ID="rfvFechaFinProductividad" runat="server" ErrorMessage="*" ControlToValidate="txtFechaFinProductividad" ForeColor="Red" ValidationGroup="informeProductividad"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revFechaFinProductividad" runat="server" ErrorMessage="* dd/mm/aaaa" ControlToValidate="txtFechaFinProductividad" ForeColor="Red" ValidationExpression="^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$" ValidationGroup="informeProductividad"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtFechaFinProductividad" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    
                                    <asp:CompareValidator ID="cvFechaProd" runat="server" ControlToValidate="txtFechaFinProductividad" ControlToCompare="txtFechaInicioProductividad" Operator="GreaterThan" Type="Date" ErrorMessage="* La fecha de fin debe ser mayor que la fecha de inicio." ForeColor="Red" ValidationGroup="informeProductividad" />

                                    <div class="col-12 text-end">

                                        <asp:Button ID="btnLimpiarRanking" runat="server" Text="Limpiar" CssClass="btn btn-secondary btn-sm px-4" OnClick="btnLimpiarRanking_Click" />

                                        <asp:Button ID="btnFiltrarRanking" runat="server" Text="Filtrar" CssClass="btn btn-primary btn-sm px-4" OnClick="btnFiltrarRanking_Click" ValidationGroup="informeProductividad" />
                                    </div>
                                </div>

                                <div class="table-responsive">
                                    <asp:GridView ID="gvRankingEspecialidades" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered align-middle">
                                        <Columns>
                                            <asp:BoundField DataField="Especialidad" HeaderText="Especialidad" />
                                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad de Turnos" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="card border-primary shadow-sm mb-4">
                            <div class="card-header bg-primary text-white">
                                <h5 class="mb-0">Informe por intervalo de tiempo</h5>
                            </div>

                            <div runat="server" id="informeSegunFecha" class="card-body p-4">
                                <div class="row g-3 mb-4">
                                    <div class="col-6">
                                        <label class="form-label font-weight-bold">Fecha Inicio</label>
                                        <asp:RequiredFieldValidator ID="rfvFechaInicioPresentismo" runat="server" ErrorMessage="*" ControlToValidate="txtFechaInicioPresentismo" ForeColor="Red" ValidationGroup="informeFecha"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revFechaInicioPresentismo" runat="server" ErrorMessage="* dd/mm/aaaa" ControlToValidate="txtFechaInicioPresentismo" ForeColor="Red" ValidationExpression="^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$" ValidationGroup="informeFecha"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtFechaInicioPresentismo" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <label class="form-label font-weight-bold">Fecha Fin</label>
                                        <asp:RequiredFieldValidator ID="rfvFinalPresentismo" runat="server" ErrorMessage="*" ControlToValidate="txtFechaFinPresentismo" ForeColor="Red" ValidationGroup="informeFecha"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revFechaFinalPresentismo" runat="server" ErrorMessage="* dd/mm/aaaa" ControlToValidate="txtFechaFinPresentismo" ForeColor="Red" ValidationExpression="^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$" ValidationGroup="informeFecha"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtFechaFinPresentismo" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <asp:CompareValidator ID="cvFecha" runat="server" ControlToValidate="txtFechaFinPresentismo" ControlToCompare="txtFechaInicioPresentismo" Operator="GreaterThan" Type="Date" ErrorMessage="* La fecha de fin debe ser mayor que la fecha de inicio." ForeColor="Red" ValidationGroup="informeFecha" />

                                    <div class="col-12 text-end">

                                        <asp:Button ID="btnLimpiarRanking0" runat="server" Text="Limpiar" CssClass="btn btn-secondary btn-sm px-4" OnClick="btnLimpiarInforme_Click" />
                                        <asp:Button ID="btnInformeFechas" runat="server" Text="Generar" CssClass="btn btn-primary btn-sm px-4" OnClick="btnInformeFechas_Click" ValidationGroup="informeFecha" />
                                    </div>
                                    <div class="card-body p-4">
                                        <div runat="server" id="barraDeInforme" class="progress ">
                                            <div class="progress-bar bg-success" id="barraVerde" runat="server" role="progressbar"></div>
                                            <div class="progress-bar bg-warning" id="barraAmarilla" runat="server" role="progressbar"></div>
                                            <div class="progress-bar bg-danger" id="barraRoja" runat="server" role="progressbar"></div>
                                        </div>
                                    </div>
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