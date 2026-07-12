<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MedicoTurnos.aspx.cs" Inherits="TPINT_GRUPO_21_PR3.MenuMedico.MedicoTurnos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />

    <title></title>
</head>
<body style="background-color: #f8f9fa;">

    <div  class="card text-center col-1" style="z-index: 999; position: fixed; right: 20px; bottom: 20px">
      <div class="card-body" >
        <p class="card-text" style="margin: -3px -6px 5px -6px;">Bienvenid@, <br/> 
          <asp:Label ID="lblNombreUsuario" runat="server" Text="[Usuario]" style="font-weight: bold;"></asp:Label>
          </p>
          <div class="text-center">
            <img src="../assets/medico-placeholder.png" alt="medico-placeholder" style="width:100px; height:auto; margin-bottom:5px;"/>
          </div>
        <a href="../login.aspx" class="btn btn-primary"> Cerrar Sesión </a>
      </div>
    </div>

    <div style="padding: 50px; margin: 50px;">
        <ul class="nav nav-tabs" style="min-width: 1000px;">
            <li class="nav-item">
                <a class="nav-link active" href="MedicoTurnos.aspx">Gestionar Turnos</a>
            </li>
        </ul>
        <div class="border border-top-0 p-5" style="background-color: white;">

            <form id="form1" runat="server">
                <asp:Label ID="lblMensaje" runat="server" Font-Bold="true"></asp:Label>
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
                                <asp:TextBox ID="txtBuscarFecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        
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
                            <asp:GridView ID="gvMedicoTurnos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered align-middle"
                                DataKeyNames="ID">
                                <Columns>
                                    <asp:BoundField DataField="DNI" HeaderText="DNI" />
                                    <asp:BoundField DataField="Paciente" HeaderText="Paciente" />
                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                                    <asp:BoundField DataField="Hora" HeaderText="Hora" />
                                    <asp:BoundField DataField="Observacion" HeaderText="Observacion" />
                                    <asp:TemplateField HeaderText="Estado">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEstadoActual" runat="server" Text='<%# Eval("Estado") %>' CssClass="fw-bold d-block mb-1"></asp:Label>
                                            <asp:RadioButtonList ID="rblPresentismo" runat="server">
                                                <asp:ListItem Value="Presente">Presente</asp:ListItem>
                                                <asp:ListItem Value="Ausente">Ausente</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:TextBox ID="txtObsPresentismo" runat="server" CssClass="form-control form-control-sm mt-1" placeholder="Observación" Text='<%# Eval("Observacion") %>'></asp:TextBox>
                                            <asp:Button ID="btnConfirmarPresentismo" runat="server" Text="Confirmar" CssClass="btn btn-sm btn-primary mt-1" OnClick="btnConfirmarPresentismo_Click" />
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
