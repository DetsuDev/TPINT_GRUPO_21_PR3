<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminMedicos.aspx.cs" Inherits="TPINT_GRUPO_21_PR3.MenuAdmin.GestionMedicos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>
    <link rel="stylesheet"
      href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css" />
    <title>Gestión de Médicos</title>
</head>
<body style="background-color: #f8f9fa;">
    
    <div class="card" style="z-index: 999; position: fixed; right: 20px; bottom: 20px">
      <div class="card-body" >
        <p class="card-text">Bienvenido: [Usuario].</p>
        <a href="../login.aspx" class="btn btn-primary"> Cerrar Sesion </a>
      </div>
    </div>

    <div style="padding: 50px; margin: 50px;">
        <ul class="nav nav-tabs" style="min-width: 1000px;">
            <li class="nav-item"><a class="nav-link" href="AdminInicio.aspx">Inicio</a></li>
            <li class="nav-item"><a class="nav-link" href="AdminPacientes.aspx">Gestionar Pacientes</a></li>
            <li class="nav-item"><a class="nav-link active" aria-current="page" href="AdminMedicos.aspx">Gestionar Medicos</a></li>
            <li class="nav-item"><a class="nav-link" href="AdminTurnos.aspx">Gestionar Turnos</a></li>
            <li class="nav-item"><a class="nav-link" href="AdminInformes.aspx">Informes</a></li>
        </ul>

        <div class="border border-top-0 p-5" style="background-color: white;">
            <form id="form1" runat="server">
                
                <div class="card border-primary mb-5 shadow-sm">
                    <div class="card-header bg-primary text-white">
                        <h4 class="mb-0">Listado de Médicos</h4>
                    </div>
                    <div class="card-body p-4">
                        <div class="table-responsive">
                            <asp:GridView ID="gvGestionMedicos" runat="server" 
                                AutoGenerateColumns="False" 
                                AllowPaging="True" 
                                PageSize="5" 
                                CssClass="table table-striped table-hover table-bordered align-middle"
                                OnPageIndexChanging="gvGestionMedicos_PageIndexChanging"
                                OnRowCancelingEdit="gvGestionMedicos_RowCancelingEdit" 
                                OnRowDeleting="gvGestionMedicos_RowDeleting" 
                                OnRowEditing="gvGestionMedicos_RowEditing" 
                                OnRowUpdating="gvGestionMedicos_RowUpdating">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-outline-warning" />
                                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" />
                                    <asp:BoundField DataField="Legajo" HeaderText="Legajo" ReadOnly="True"/>
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                                    <asp:BoundField DataField="Especialidad" HeaderText="Especialidad" />
                                    <asp:BoundField DataField="Horario" HeaderText="Horario Atención" />
                                    <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
                                    <asp:BoundField DataField="Nacionalidad" HeaderText="Nacionalidad" />
                                    <asp:BoundField DataField="FechaNac" HeaderText="Fecha Nac" />
                                    <asp:BoundField DataField="Direccion" HeaderText="Direccion" />
                                    <asp:BoundField DataField="Localidad" HeaderText="Localidad" />
                                    <asp:BoundField DataField="Provincia" HeaderText="Provincia" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" />
                                    <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
                                    <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                                    <asp:TemplateField HeaderText="Contraseña">
                                        <ItemTemplate>
                                            <span class="password-text">********</span>

                                            <button type="button"
                                                    class="btn btn-sm btn-outline-secondary toggle-password"
                                                    data-password='<%# Eval("Contrasenia") %>'>
                                                <i class="bi bi-eye-slash"></i>
                                            </button>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-outline-danger" />
                                </Columns>
                                <PagerStyle CssClass="pagination justify-content-center pt-3" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <div class="card border-primary shadow-sm">
                    <div class="card-header bg-primary text-white">
                        <h4 class="mb-0">Cargar Nuevo Médico</h4>
                    </div>
                    <div class="card-body p-4">
                        <div class="row g-3">
                            <div class="col-md-3">
                                <label class="form-label font-weight-bold">Legajo Médico</label>
                                <asp:TextBox ID="txtLegajo" runat="server" CssClass="form-control" placeholder="Ej: MED-999"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label">DNI</label>
                                <asp:TextBox ID="txtDni" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label">Nombre</label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label">Apellido</label>
                                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label">Especialidad</label>
                                <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select"></asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label">Horario de Disponibilidad</label>
                                <asp:DropDownList ID="ddlHorario" runat="server" CssClass="form-select"></asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label class="form-label">Sexo</label>
                                <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="M">Masculino</asp:ListItem>
                                    <asp:ListItem Value="F">Femenino</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label class="form-label">Nacionalidad</label>
                                <asp:TextBox ID="txtNacionalidad" runat="server" CssClass="form-control" placeholder="Ej: Argentina"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label">Fecha de Nacimiento</label>
                                <asp:TextBox ID="txtFechaNac" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label">Teléfono</label>
                                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">Dirección</label>
                                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label">Provincia</label>
                                <asp:DropDownList ID="ddlProvincia" runat="server" CssClass="form-select"></asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label">Localidad</label>
                                <asp:DropDownList ID="ddlLocalidad" runat="server" CssClass="form-select"></asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">Correo Electrónico</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="medico@clinica.com"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label">Usuario de Login</label>
                                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label">Contraseña</label>
                                <asp:TextBox ID="txtContrasenia" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            </div>
                               <div class="col-md-3">
                                <label class="form-label">Confirmar contraseña</label>
                                <asp:TextBox ID="txtConfirmarContrasenia" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="col-12 text-end pt-3">
                                <asp:Button ID="btnCargar" runat="server" Text="Cargar Médico" CssClass="btn btn-primary px-4" />
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