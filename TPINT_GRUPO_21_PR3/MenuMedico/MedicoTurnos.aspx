<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MedicoTurnos.aspx.cs" Inherits="TPINT_GRUPO_21_PR3.MenuMedico.MedicoTurnos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />

    <title></title>
</head>
<body style="background-color: #f8f9fa;">

<div  class="card" style="z-index: 999; position: fixed; right: 20px; bottom: 20px">
  <div class="card-body" >
        <p class="card-text">Bienvenido: 
          <asp:Label ID="lblNombreUsuario" runat="server" Text="[Usuario]"></asp:Label>
          </p>
        <a href="../login.aspx" class="btn btn-primary"> Cerrar Sesion </a>
  </div>
</div>

    <div style="padding: 50px; margin: 50px;">
        <ul class="nav nav-tabs" style="min-width: 1000px;">
            <li class="nav-item">
                <a class="nav-link active" href="../GestionMedico/GestionTurnos.aspx">Gestionar Turnos</a>
            </li>
        </ul>
        <div class="border border-top-0 p-5" style="background-color: white;">

            <form id="form1" runat="server">
                <div class="card border-primary mb-5 shadow-sm">
                    <div class="card-header bg-primary text-white">
                        <h4 class="mb-0">Buscar Turnos</h4>
                    </div>
                    <div class="card-body p-4">
                        <div class="row g-3">
                            <div class="col-md-4">
                                <label class="form-label">DNI</label>
                                <asp:TextBox ID="txtBuscarDni" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label">Paciente</label>
                                <asp:TextBox ID="txtBuscarPaciente" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label">Fecha</label>
                                <asp:TextBox ID="txtBuscarFecha" runat="server" CssClass="form-control" placeholder="Ej: 15/06/2026"></asp:TextBox>
                            </div>
                            <div class="col-12 text-end pt-3">
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary px-4" OnClick="btnBuscar_Click" />
                                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary px-4" OnClick="btnLimpiar_Click" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="card border-primary mb-5 shadow-sm">
                    <div class="card-header bg-primary text-white">
                        <h4 class="mb-0">Listado de Turnos</h4>
                    </div>
                    <div class="card-body p-4">
                        <div class="table-responsive">
                            <asp:GridView ID="gvMedicoTurnos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered align-middle">
                                <Columns>
                                    <asp:BoundField DataField="DNI" HeaderText="DNI" />
                                    <asp:BoundField DataField="Paciente" HeaderText="Paciente" />
                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                                    <asp:BoundField DataField="Hora" HeaderText="Hora" />
                                    <asp:BoundField DataField="Observacion" HeaderText="Observacion" />
                                    <asp:TemplateField HeaderText="Estado">
                                        <ItemTemplate>
                                            <asp:RadioButtonList ID="rblPresentismo" runat="server">
                                                <asp:ListItem Value="1">Presente</asp:ListItem>
                                                <asp:ListItem Value="2">Ausente</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:Button ID="btnConfirmarPresentismo" runat="server" Text="Confirmar seleccion" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <script src="../js/bootstrap.bundle.min.js"></script>
</body>
</html>
