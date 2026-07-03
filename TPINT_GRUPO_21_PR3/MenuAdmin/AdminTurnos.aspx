<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminTurnos.aspx.cs" Inherits="TPINT_GRUPO_21_PR3.MenuAdmin.GestionTurnos" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>
   
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
        <ul class="nav nav-tabs" style=" min-width: 1000px;">
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

            <form id="form1" runat="server">

                <asp:Label ID="lblMensaje" runat="server" Font-Bold="true"></asp:Label>
                    <div class="card" runat="server"
                        id="divEliminar"
                        style="z-index: 9999; width: 320px; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); text-align: center; padding: 10px;">
                        <div class="card-body">
                            <p class="card-text">Desea Eliminar a: [elemento]?</p>
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
                            <asp:GridView ID="gvGestionTurnos" runat="server"
                                AutoGenerateColumns="False"
                                AllowPaging="True"
                                PageSize="5"
                                DataKeyNames="ID"
                                CssClass="table table-striped table-hover table-bordered align-middle"
                                OnPageIndexChanging="gvGestionTurnos_PageIndexChanging"
                                OnRowDeleting="gvGestionTurnos_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="true" />
                                    <asp:BoundField DataField="DNI" HeaderText="DNI" />
                                    <asp:BoundField DataField="Paciente" HeaderText="Paciente" />
                                    <asp:BoundField DataField="Medico" HeaderText="Medico" />
                                    <asp:BoundField DataField="Especialidad" HeaderText="Especialidad" />
                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                                    <asp:BoundField DataField="Hora" HeaderText="Hora" />
                                    <asp:BoundField DataField="Observacion" HeaderText="Observacion" />
                                    <asp:BoundField DataField="Estado" HeaderText="Estado" />
                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-outline-danger" >
<ControlStyle CssClass="btn btn-sm btn-outline-danger"></ControlStyle>
                                    </asp:CommandField>
                                </Columns>
                                <PagerStyle CssClass="pagination justify-content-center pt-3" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <div class="card border-primary shadow-sm">
    <div class="card-header bg-primary text-white">
        <h4 class="mb-0">Cargar Nuevo Turno</h4>
    </div>
    <div class="card-body p-4">
        <div class="row g-3">
            <div class="col-md-3">
                <label class="form-label">Especialidad</label>
                <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-md-3">
                <label class="form-label">Médico</label>
                <asp:DropDownList ID="ddlMedico" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>
            <div class="col-md-3">
                <label class="form-label">Paciente</label>
                <asp:DropDownList ID="ddlPaciente" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>
            <div class="col-md-3">
                <label class="form-label">Fecha</label>
                <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label class="form-label">Hora</label>
                <asp:DropDownList ID="ddlHora" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>

            <div class="col-12 text-end pt-3">
                <asp:Button ID="btnCargar" runat="server" Text="Cargar Turno" CssClass="btn btn-primary px-4" OnClick="btnCargar_Click" />
            </div>
        </div>
    </div>
</div>

            </form>
        </div>
    </div>

        <script src="../js/bootstrap.bundle.min.js"></script>

</body>
</html>
