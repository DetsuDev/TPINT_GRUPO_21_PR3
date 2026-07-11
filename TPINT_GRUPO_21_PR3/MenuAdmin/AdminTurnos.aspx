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
    </style>
</head>
<body style="background-color: #f8f9fa;">
    
    <form id="form1" runat="server">
        <div class="card text-center col-1" style="z-index: 999; position: fixed; right: 20px; bottom: 20px">
          <div class="card-body">
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
            <ul class="nav nav-tabs" style="min-width: 1000px;">
                <li class="nav-item">
                    <a class="nav-link" href="AdminInformes.aspx">Informes</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="AdminPacientes.aspx">Gestionar Pacientes</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="AdminMedicos.aspx">Gestionar Medicos</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link active" aria-current="page" href="AdminTurnos.aspx">Gestionar Turnos</a>
                </li>
            </ul>
            
            <div class="border border-top-0 p-5" style="background-color: white;">
                
                <div class="card" runat="server" id="divEliminar" style="z-index: 9999; width: 320px; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); text-align: center; padding: 10px;">
                    <div class="card-body">
                        <p class="card-text">Desea Eliminar el turno?</p>
                        <div style="text-align: right">
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" class="btn btn-danger" OnClick="btnEliminar_Click"/>
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-secondary" OnClick="btnCancelar_Click"/>
                        </div>
                    </div>
                </div>
              
                <div class="card border-primary mb-5 shadow-sm">
                    <div class="card-header bg-primary text-white">
                        <h4 class="mb-0">Buscar Turnos</h4>
                    </div>
                    <div class="card-body p-4">
                        <div class="row g-3">
                            <div class="col-md-3">
                                <label class="form-label">DNI</label>
                                <asp:TextBox ID="txtBuscarDni" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label">Paciente</label>
                                <asp:TextBox ID="txtBuscarPaciente" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label">Fecha</label>
                                <asp:TextBox ID="txtBuscarFecha" runat="server" CssClass="form-control" placeholder="Ej: 15/06/2026"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label">Estado</label>
                                <asp:DropDownList ID="ddlBuscarEstado" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="">Todos</asp:ListItem>
                                    <asp:ListItem Value="Presente">Presente</asp:ListItem>
                                    <asp:ListItem Value="Ausente">Ausente</asp:ListItem>
                                    <asp:ListItem Value="Pendiente">Pendiente</asp:ListItem>
                                </asp:DropDownList>
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
                            <asp:GridView ID="gvGestionTurnos" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="5"
                                CssClass="table table-striped table-hover table-bordered align-middle"
                                OnPageIndexChanging="gvGestionTurnos_PageIndexChanging"
                                OnRowDeleting="gvGestionTurnos_RowDeleting"
                                OnRowEditing="gvGestionTurnos_RowEditing">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-outline-warning" EditText="Editar" />
                                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="true" />
                                    <asp:BoundField DataField="Medico" HeaderText="Médico" ReadOnly="true" />
                                    <asp:BoundField DataField="Especialidad" HeaderText="Especialidad" ReadOnly="true" />
                                    <asp:BoundField DataField="DNI" HeaderText="DNI" ReadOnly="true" />
                                    <asp:BoundField DataField="Paciente" HeaderText="Paciente" ReadOnly="true" />
                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" ReadOnly="true" />
                                    <asp:BoundField DataField="Hora" HeaderText="Hora" ReadOnly="true" />
                                    <asp:BoundField DataField="Observacion" HeaderText="Observacion" ReadOnly="true" />
                                    <asp:TemplateField HeaderText="Estado">
                                        <ItemTemplate>
                                             <asp:Label ID="lblEstadoTurno" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-outline-danger" />
                                </Columns>
                                <PagerStyle CssClass="pagination justify-content-center pt-3" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
               
                <div class="mb-3 text-center">
                    <asp:Label ID="lblMensajeGeneral" runat="server" CssClass="fw-bold fs-5" Visible="false"></asp:Label>
                </div>

                <asp:Button ID="btnNuevoTurno" runat="server" Text="Nuevo Turno" CssClass="btn btn-primary" OnClick="btnNuevoTurno_Click" CausesValidation="false" />

                <div id="fullscreenOverlay" runat="server"></div>
                    <asp:HiddenField ID="hdnIdTurnoEliminar" runat="server" />
                    <asp:HiddenField ID="hdnIdTurnoEditar" runat="server" />
                    <div id="divFormulario" class="card border-primary shadow-sm" runat="server">
                        <div class="card-header bg-primary text-white">
                            <h4 id="hCargarTurno" runat="server" class="mb-0">Cargar Nuevo Turno</h4>
                        </div>
                        <div class="card-body p-4">
                            <div class="mb-3 text-center">
                                <asp:Label ID="lblMensajeErrorPopup" runat="server" CssClass="fw-bold fs-6" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                            <div class="row g-3">
                                <div class="col-md-3">
                                    <label class="form-label font-weight-bold">DNI Paciente</label>
                                    <asp:RequiredFieldValidator ID="rfvDni" runat="server" ErrorMessage="*" ControlToValidate="txtPaciente" ForeColor="Red" ValidationGroup="vgAltaTurno"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revDni" runat="server" ErrorMessage="* Solo números" ValidationExpression="^\d+$" ControlToValidate="txtPaciente" ForeColor="Red" Display="Dynamic" ValidationGroup="vgAltaTurno"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtPaciente" runat="server" CssClass="form-control" placeholder="Ej: 45123456"></asp:TextBox>
                                     <label class="form-label">Especialidad</label>
                                    <asp:RequiredFieldValidator ID="rfvEspecialidad" runat="server" ErrorMessage="*" ControlToValidate="ddlAltaEspecialidad" InitialValue="0" ForeColor="Red" ValidationGroup="vgAltaTurno"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlAltaEspecialidad" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="ddlAltaEspecialidad_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <label class="form-label">Médico Asignado</label>
                                    <asp:RequiredFieldValidator ID="rfvMedico" runat="server" ErrorMessage="*" ControlToValidate="ddlAltaMedico" InitialValue="0" ForeColor="Red" ValidationGroup="vgAltaTurno"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlAltaMedico" runat="server" CssClass="form-select" OnSelectedIndexChanged="ddlAltaMedico_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:Calendar ID="cFechasTurnos" runat="server" OnDayRender="cFechasTurnos_DayRender" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="220px">
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
                                <div class="col-md-3">
                                    <label class="form-label">Hora</label>
                                    <asp:RequiredFieldValidator ID="rfvHora" runat="server" ErrorMessage="*" ControlToValidate="txtHora" ForeColor="Red" ValidationGroup="vgAltaTurno"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revHora" runat="server" ErrorMessage="* Formato HH:MM" ValidationExpression="^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$" ControlToValidate="txtHora" ForeColor="Red" Display="Dynamic" ValidationGroup="vgAltaTurno"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtHora" runat="server" CssClass="form-control" placeholder="Ej: 10:30" AutoPostBack="True" OnTextChanged="txtFechaHora_TextChanged"></asp:TextBox>
                                    <asp:DropDownList ID="ddlHora" CssClass="form-select" runat="server"></asp:DropDownList>
                                </div>
                                </div>
                                <div class="col-md-8">
                                    <label class="form-label">Observación</label>
                                    <asp:TextBox ID="txtObservacionAlta" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                </div>
                                <div class="col-12 text-end pt-3">
                                    <asp:Button ID="btnCargar" runat="server" Text="Agendar Turno" CssClass="btn btn-primary px-4" OnClick="btnCargar_Click" ValidationGroup="vgAltaTurno" />
                                    <asp:Button ID="btnCancelarEdicion" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary px-4" OnClick="btnCancelarEdicion_Click" CausesValidation="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </form>
    
    <script src="../js/bootstrap.bundle.min.js"></script>
</body>
</html>