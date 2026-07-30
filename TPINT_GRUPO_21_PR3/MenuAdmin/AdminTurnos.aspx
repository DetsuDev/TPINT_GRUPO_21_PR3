<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminTurnos.aspx.cs" Inherits="TPINT_GRUPO_21_PR3.MenuAdmin.GestionTurnos" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>
    <title>Gestión de Turnos</title>
    <style>
        #fullscreenOverlay {
            display: none;
            position: fixed;       
            top: 0;
            left: 0;
            width: 100%;           
            height: 100vh;         
            background-color: rgba(0, 0, 0, 0.7); 
            z-index: 1000;
        }

        #divFormulario {
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            min-width: 80%;
            max-width: 80%;
            z-index: 1001;
            background-color: white;
            border-radius: 8px;
            box-shadow: 0px 4px 15px rgba(0, 0, 0, 0.2);
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
          <div class="card-body">
            <p class="card-text" style="margin: -3px -6px 5px -6px;"> <asp:Label ID="lblWelcomePlaceholder" runat="server" Text="<%$ Resources:lang, lblWelcomePlaceholder %>"> </asp:Label> <br/> 
              <asp:Label ID="lblNombreUsuario" runat="server" Text="[Usuario]" style="font-weight: bold;"></asp:Label>
            </p>
            <div class="text-center">
                 <img src="../assets/admin-placeholder.png" alt="Administrador-placeholder" style="width:100px; height:auto; margin-bottom:5px;"/>
            </div>
            <a href="../login.aspx" class="btn btn-primary"><asp:Literal runat="server" Text="<%$ Resources:lang, btnLogout %>" /></a>
          </div>
        </div>

        <div style="padding: 50px; margin: 50px;">
                <ul class="nav nav-tabs" style="min-width: 1000px;">
                <li class="nav-item">
                    <a class="nav-link" href="AdminInformes.aspx"><asp:Literal runat="server" Text="<%$ Resources:lang, navInformes %>" /></a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="AdminPacientes.aspx"><asp:Literal runat="server" Text="<%$ Resources:lang, navPacientes %>" /></a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="AdminMedicos.aspx"><asp:Literal runat="server" Text="<%$ Resources:lang, navMedicos %>" /></a>
                </li>
                <li class="nav-item">
                    <a class="nav-link active" aria-current="page" href="AdminTurnos.aspx"><asp:Literal runat="server" Text="<%$ Resources:lang, navTurnos %>" /></a>
                </li>
            </ul>
            
            <div class="border border-top-0 p-5" style="background-color: white;">
                
                <div class="card" runat="server" id="divEliminar" style="z-index: 9999; width: 320px; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); text-align: center; padding: 10px;">
                    <div class="card-body">
                        <p class="card-text"><asp:Literal runat="server" Text="<%$ Resources:lang, msgDeleteRecord %>" /></p>
                        <div style="text-align: right">
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" class="btn btn-danger" OnClick="btnEliminar_Click"/>
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-secondary" OnClick="btnCancelar_Click"/>
                        </div>
                    </div>
                </div>
              
                <div class="card border-primary mb-5 shadow-sm">
                    <div class="card-header bg-primary text-white">
                        <h4 class="mb-0"><asp:Literal runat="server" Text="<%$ Resources:lang, headerSearchAppointments %>" /></h4>
                    </div>
                    <div class="card-body p-4">
                        <div class="row g-3">
                            <div class="col-md-3">
                                <label class="form-label">DNI</label>
                                <asp:TextBox ID="txtBuscarDni" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label"><asp:Literal runat="server" Text="<%$ Resources:lang, lblPatient %>" /></label>
                                <asp:TextBox ID="txtBuscarPaciente" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label"><asp:Literal runat="server" Text="<%$ Resources:lang, lblDate %>" /></label>
                                 <asp:RegularExpressionValidator ID="revFecha" runat="server" ErrorMessage="* dd/mm/aaaa" ControlToValidate="txtBuscarFecha" ForeColor="Red" ValidationExpression="^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>         
                                <asp:TextBox ID="txtBuscarFecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label"><asp:Literal runat="server" Text="<%$ Resources:lang, lblStatus %>" /></label>
                                <asp:DropDownList ID="ddlBuscarEstado" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="<%$ Resources:lang, All %>" Value=""></asp:ListItem>
                                    <asp:ListItem Text="<%$ Resources:lang, Present %>" Value="Presente"></asp:ListItem>
                                    <asp:ListItem Text="<%$ Resources:lang, Pending %>" Value="Pendiente"></asp:ListItem>
                                    <asp:ListItem Text="<%$ Resources:lang, Absent %>"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-12 text-end pt-3">
                                <asp:Button ID="btnBuscar" runat="server" Text="<%$ Resources:lang, btnSearch %>" CssClass="btn btn-primary px-4" OnClick="btnBuscar_Click" />
                                <asp:Button ID="btnLimpiar" runat="server" Text="<%$ Resources:lang, btnClear %>" CssClass="btn btn-outline-secondary px-4" OnClick="btnLimpiar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                
                <div class="mb-3 text-center">
                    <asp:Label ID="lblMensajeGeneral" runat="server" CssClass="fw-bold fs-5" Visible="false"></asp:Label>
                </div>

                <div class="mb-3 text-center">
                    <asp:Label ID="lblMensajeErrorPopup" runat="server" CssClass="fw-bold fs-6" ForeColor="Red" Visible="false"></asp:Label>
                </div>
                <div class="card border-primary mb-5 shadow-sm">
                    <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">

                        <h4 class="mb-0"><asp:Literal runat="server" Text="<%$ Resources:lang, headerAppointmentList %>" /></h4>

                        <asp:Button ID="btnNuevoTurno" runat="server" Text="<%$ Resources:lang, btnNewAppointment %>" CssClass="btn btn-light" OnClick="btnNuevoTurno_Click" CausesValidation="false" />
                        
                    </div>
                    <div class="card-body p-4">
                        <div class="table-responsive">
                            <asp:GridView ID="gvGestionTurnos" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="5"
                                CssClass="table table-striped table-hover table-bordered align-middle"
                                OnPageIndexChanging="gvGestionTurnos_PageIndexChanging"
                                OnRowDeleting="gvGestionTurnos_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="true" />
                                    <asp:BoundField DataField="Medico" HeaderText="<%$ Resources:lang, headerDoctor %>" ReadOnly="true" />
                                    <asp:BoundField DataField="Especialidad" HeaderText="<%$ Resources:lang, headerSpecialty %>" ReadOnly="true" />
                                    <asp:BoundField DataField="DNI" HeaderText="DNI" ReadOnly="true" />
                                    <asp:BoundField DataField="Paciente" HeaderText="<%$ Resources:lang, headerPatient %>" ReadOnly="true" />
                                    <asp:BoundField DataField="Fecha" HeaderText="<%$ Resources:lang, headerDate %>" ReadOnly="true" />
                                    <asp:BoundField DataField="Hora" HeaderText="<%$ Resources:lang, headerTime %>" ReadOnly="true" />
                                    <asp:BoundField DataField="Observacion" HeaderText="<%$ Resources:lang, headerObservation %>" ReadOnly="true" />
                                    <asp:TemplateField HeaderText="<%$ Resources:lang, headerStatus %>">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEstadoTurno" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-outline-danger">
                                        <ControlStyle CssClass="btn btn-sm btn-outline-danger"></ControlStyle>
                                    </asp:CommandField>
                                </Columns>
                                <PagerStyle CssClass="pagination justify-content-center pt-3" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>


                <div id="fullscreenOverlay" runat="server"></div>
                <asp:HiddenField ID="hdnIdTurnoEliminar" runat="server" />
                <asp:HiddenField ID="hdnIdTurnoEditar" runat="server" />
                <div id="divFormulario" class="card border-primary shadow-sm" runat="server">
                    <div class="card-header bg-primary text-white">
                        <h4 id="hCargarTurno" runat="server" class="mb-0">Cargar Nuevo Turno</h4>
                    </div>
                    <div class="card-body p-4">
                        <div class="row g-4 align-items-start">
                            <div class="col-md-4">
                                <div class="mb-3">
                                    <label class="form-label fw-bold"><asp:Literal runat="server" Text="<%$ Resources:lang, lblPatientsDni %>" /></label><asp:RequiredFieldValidator ID="rfvDni" runat="server" ErrorMessage="*" ControlToValidate="txtPaciente" ForeColor="Red" ValidationGroup="vgAltaTurno" /><asp:RegularExpressionValidator ID="revDni" runat="server" ErrorMessage="* Solo números" ValidationExpression="^\d+$" ControlToValidate="txtPaciente" ForeColor="Red" Display="Dynamic" ValidationGroup="vgAltaTurno" /><asp:TextBox ID="txtPaciente" runat="server" CssClass="form-control" placeholder="Ej: 45123456" OnTextChanged="txtPaciente_TextChanged" /></div>
                                <div class="mb-3">
                                    <label class="form-label fw-bold"><asp:Literal runat="server" Text="<%$ Resources:lang, lblSpecialty %>" /></label><asp:RequiredFieldValidator ID="rfvEspecialidad" runat="server" ErrorMessage="*" ControlToValidate="ddlAltaEspecialidad" InitialValue="0" ForeColor="Red" ValidationGroup="vgAltaTurno" /><asp:DropDownList ID="ddlAltaEspecialidad" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="ddlAltaEspecialidad_SelectedIndexChanged" /></div>
                                <div class="mb-3">
                                    <label class="form-label fw-bold"><asp:Literal runat="server" Text="<%$ Resources:lang, lblAssignedDoctor %>" /></label><asp:RequiredFieldValidator ID="rfvMedico" runat="server" ErrorMessage="*" ControlToValidate="ddlAltaMedico" InitialValue="0" ForeColor="Red" ValidationGroup="vgAltaTurno" /><asp:DropDownList ID="ddlAltaMedico" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="ddlAltaMedico_SelectedIndexChanged" /></div>
                            </div>

                            <div class="col-md-4">
                                <label class="form-label fw-bold"><asp:Literal runat="server" Text="<%$ Resources:lang, lblDate %>" /></label>
                                <asp:Calendar ID="cFechasTurnos" runat="server" OnDayRender="cFechasTurnos_DayRender" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="100%" OnSelectionChanged="cFechasTurnos_SelectionChanged">
                                    <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                    <OtherMonthDayStyle ForeColor="#999999" />
                                    <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                    <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                    <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                                    <WeekendDayStyle BackColor="#CCCCFF" />
                                </asp:Calendar>
                            </div>

                            <div class="col-md-4">
                                <div class="mb-3">
                                    <label class="form-label fw-bold"><asp:Literal runat="server" Text="<%$ Resources:lang, lblTime %>" /></label>
                                    <asp:RequiredFieldValidator ID="rfvHora" runat="server" ErrorMessage="* Seleccione Horario" ControlToValidate="ddlHora" InitialValue="0" ForeColor="Red" ValidationGroup="vgAltaTurno" /><asp:DropDownList ID="ddlHora" runat="server" CssClass="form-select" />
                                </div>
                            </div>

                            <div class="col-12">
                                <label class="form-label"><asp:Literal runat="server" Text="<%$ Resources:lang, lblObservation %>" /></label><asp:TextBox ID="txtObservacionAlta" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" /></div>

                            <div class="col-12 text-end pt-2">
                                <asp:Button ID="btnCargar" runat="server" Text="<%$ Resources:lang, btnAppointment %>" CssClass="btn btn-primary px-4" OnClick="btnCargar_Click" ValidationGroup="vgAltaTurno" />
                                <asp:Button ID="btnCancelarEdicion" runat="server" Text="<%$ Resources:lang, btnCancel %>" CssClass="btn btn-outline-secondary px-4" OnClick="btnCancelarEdicion_Click" CausesValidation="false" /></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="../js/bootstrap.bundle.min.js"></script>
</body>
</html>