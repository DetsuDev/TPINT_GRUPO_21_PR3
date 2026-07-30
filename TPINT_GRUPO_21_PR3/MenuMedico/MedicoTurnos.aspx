<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MedicoTurnos.aspx.cs" Inherits="TPINT_GRUPO_21_PR3.MenuMedico.MedicoTurnos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />

    <title></title>
    <style>
        .table-separada {
            border-collapse: separate !important;
            border-spacing: 0 12px;
        }

        #languageswitch {
              position: absolute;
              top: 1em;
              right: 5em;
        }
        #languageswitch input[type="radio"] { display: none; }
        #languageswitch label { cursor: pointer; }
        #languageswitch input[type="radio"]:checked + label {
            background-color: #0d6efd;
            color: #fff;
            border-color: #0d6efd;
        }

    </style>
</head>
<body style="background-color: #f8f9fa;">
    <form id="form1" runat="server">
    <div id="languageswitch" runat="server">
        <asp:RadioButton ID="rbtnEn" runat="server" GroupName="lang" AutoPostBack="true" OnCheckedChanged="rblLanguage_SelectedIndexChanged" ClientIDMode="Static" />
        <label for="rbtnEn" class="btn btn-outline-primary" style="margin-right:0.25rem;">EN</label>

        <asp:RadioButton ID="rbtnEs" runat="server" GroupName="lang" AutoPostBack="true" OnCheckedChanged="rblLanguage_SelectedIndexChanged" ClientIDMode="Static" />
        <label for="rbtnEs" class="btn btn-outline-primary">ES</label>
    </div>
    <div class="card text-center col-1" style="z-index: 999; position: fixed; right: 20px; bottom: 20px">
      <div class="card-body" >
        <p class="card-text" style="margin: -3px -6px 5px -6px;"> <asp:Label ID="lblWelcomePlaceholder" runat="server" Text="<%$ Resources:lang, lblWelcomePlaceholder %>"> </asp:Label> <br/> 
          <asp:Label ID="lblNombreUsuario" runat="server" Text="[Usuario]" style="font-weight: bold;"></asp:Label>
          </p>
          <div class="text-center">
            <img src="../assets/medico-placeholder.png" alt="medico-placeholder" style="width:100px; height:auto; margin-bottom:5px;"/>
          </div>
        <a href="../login.aspx" class="btn btn-primary"><asp:Literal runat="server" Text="<%$ Resources:lang, btnLogout %>" /></a>
      </div>
    </div>

        <div style="padding: 50px; margin: 50px;">
        <ul class="nav nav-tabs" style="min-width: 1000px;">
            <li class="nav-item">
                <a class="nav-link active" href="MedicoTurnos.aspx"><asp:Literal runat="server" Text="<%$ Resources:lang, navTurnos %>" /></a>
            </li>
        </ul>
        <div class="border border-top-0 p-5" style="background-color: white;">
                <asp:Label ID="lblMensaje" runat="server" Font-Bold="true"></asp:Label>
                <div class="card border-primary mb-5 shadow-sm">
                    <div class="card-header bg-primary text-white">
                        <h4 class="mb-0"><asp:Literal runat="server" Text="<%$ Resources:lang, headerSearchAppointments %>" /></h4>
                    </div>
                    <div class="card-body p-4">
                        <div class="row g-3">
                            <div class="col-md-4">
                                <label class="form-label"><asp:Literal runat="server" Text="<%$ Resources:lang, lblDNI %>" /></label>
                                <asp:TextBox ID="txtBuscarDni" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label"><asp:Literal runat="server" Text="<%$ Resources:lang, lblPatient %>" /></label>
                                <asp:TextBox ID="txtBuscarPaciente" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label"><asp:Literal runat="server" Text="<%$ Resources:lang, lblDate %>" /></label>
                                
                                <asp:RegularExpressionValidator ID="revFecha" runat="server" ErrorMessage="* dd/mm/aaaa" ControlToValidate="txtBuscarFecha" ForeColor="Red" ValidationExpression="^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
                                        
                                <asp:TextBox ID="txtBuscarFecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        
                            </div>
                            <div class="col-12 text-end pt-3">
                                <asp:Button ID="btnBuscar" runat="server" Text="<%$ Resources:lang, btnSearch %>" CssClass="btn btn-primary px-4" OnClick="btnBuscar_Click" />
                                <asp:Button ID="btnLimpiar" runat="server" Text="<%$ Resources:lang, btnClear %>" CssClass="btn btn-outline-secondary px-4" OnClick="btnLimpiar_Click" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="card border-primary mb-5 shadow-sm">
                    <div class="card-header bg-primary text-white">
                        <h4 class="mb-0"><asp:Literal runat="server" Text="<%$ Resources:lang, headerAppointmentList %>" /></h4>
                    </div>
                    <div class="card-body p-4">
                        <div class="table-responsive">
                            <asp:GridView ID="gvMedicoTurnos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered align-middle table-separada"
                                DataKeyNames="ID" OnRowDataBound="gvMedicoTurnos_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="DNI" HeaderText="DNI" />
                                    <asp:BoundField DataField="Paciente" HeaderText="<%$ Resources:lang, lblPatient %>" />
                                    <asp:BoundField DataField="Fecha" HeaderText="<%$ Resources:lang, lblDate %>" />
                                    <asp:BoundField DataField="Hora" HeaderText="<%$ Resources:lang, lblTime %>" />
                                   <asp:TemplateField HeaderText="<%$ Resources:lang, lblObservation %>">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtObservacion" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control form-control-sm mt-1" Text='<%# Eval("Observacion") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="<%$ Resources:lang, lblStatus %>">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEstadoActual" runat="server" Text='<%# Eval("Estado") %>' CssClass="fw-bold d-block mb-1"></asp:Label>
        
                                            <asp:RadioButtonList ID="rblPresentismo" runat="server" RepeatDirection="Horizontal" CssClass="d-inline-block">
                                                <asp:ListItem Value="Presente">Presente</asp:ListItem>
                                                <asp:ListItem Value="Ausente">Ausente</asp:ListItem>
                                            </asp:RadioButtonList>
        
                                            <asp:Button ID="btnConfirmarPresentismo" runat="server" Text="<%$ Resources:lang, btnSaveChanges %>" CssClass="btn btn-sm btn-primary mt-1 d-block" OnClick="btnConfirmarPresentismo_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
        </div>
    </div>

    <script src="../js/bootstrap.bundle.min.js"></script>
    </form>
</body>
</html>
