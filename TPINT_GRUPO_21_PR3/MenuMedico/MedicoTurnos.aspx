<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MedicoTurnos.aspx.cs" Inherits="TPINT_GRUPO_21_PR3.MenuMedico.MedicoTurnos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous"/>

    <title></title>
</head>
<body style="background-color: #f8f9fa;">
    
    <div class="card" style="z-index: 999; position: fixed; right: 20px; bottom: 20px">
      <div class="card-body" >
        <p class="card-text">Bienvenido: [Usuario].</p>
        <a href="../login.aspx" class="btn btn-primary"> Cerrar Sesion </a>
      </div>
    </div>


    <div style="padding: 50px; margin: 50px;">
        <ul class="nav nav-tabs" style=" min-width: 1000px;">
            <li class="nav-item">
                <a class="nav-link" href="../MenuMedico/MedicoInicio.aspx">Inicio</a>
            </li>
            <li class="nav-item">
                <a class="nav-link active" href="../GestionMedico/GestionTurnos.aspx">Gestionar Turnos</a>
            </li>
        </ul>
        <div class="border border-top-0 p-5" style="background-color: white;" >

            <form id="form1" runat="server">
                <asp:GridView ID="gvMedicoTurnos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered align-middle">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" />
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
            </form>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js" integrity="sha384-FKyoEForCGlyvwx9Hj09JcYn3nv7wiPVlz7YYwJrWVcXK/BmnVDxM+D2scQbITxI" crossorigin="anonymous"></script>
</body>
</html>
