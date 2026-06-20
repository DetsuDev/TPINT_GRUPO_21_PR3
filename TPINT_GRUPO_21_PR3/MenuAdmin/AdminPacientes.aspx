<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPacientes.aspx.cs" Inherits="TPINT_GRUPO_21_PR3.MenuAdmin.AdminPacientes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <title>Gestión de Pacientes</title>
</head>
<body style="background-color: #f8f9fa;">

    <div class="card" style="z-index: 999; position: fixed; right: 20px; bottom: 20px">
        <div class="card-body">
            <p class="card-text">Bienvenido: [Usuario].</p>
            <a href="../login.aspx" class="btn btn-primary">Cerrar Sesion </a>
        </div>
    </div>

    <div style="padding: 50px; margin: 50px;">
        <ul class="nav nav-tabs" style="min-width: 1000px;">
            <li class="nav-item"><a class="nav-link" href="AdminInformes.aspx">Informes</a></li>
            <li class="nav-item"><a class="nav-link active" aria-current="page" href="AdminPacientes.aspx">Gestionar Pacientes</a></li>
            <li class="nav-item"><a class="nav-link" href="AdminMedicos.aspx">Gestionar Medicos</a></li>
            <li class="nav-item"><a class="nav-link" href="AdminTurnos.aspx">Gestionar Turnos
                </a></li>
        </ul>

        <div class="border border-top-0 p-5" style="background-color: white;">
            <form id="form1" runat="server">
                
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
                        <h4 class="mb-0">Listado de Pacientes</h4>
                    </div>
                    <div class="card-body p-4">
                        <div class="table-responsive">
                            <asp:GridView ID="gvGestionPacientes" runat="server" 
                                AutoGenerateColumns="False" 
                                AllowPaging="True" 
                                PageSize="5" 
                                CssClass="table table-striped table-hover table-bordered align-middle"
                                OnPageIndexChanging="gvGestionPacientes_PageIndexChanging"
                                OnRowCancelingEdit="gvGestionPacientes_RowCancelingEdit" 
                                OnRowDeleting="gvGestionPacientes_RowDeleting" 
                                OnRowEditing="gvGestionPacientes_RowEditing" 
                                OnRowUpdating="gvGestionPacientes_RowUpdating">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-outline-warning" />
                                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" />
                                    <asp:BoundField DataField="DNI" HeaderText="DNI" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                                    <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
                                    <asp:BoundField DataField="Nacionalidad" HeaderText="Nacionalidad" />
                                    <asp:BoundField DataField="FechaNac" HeaderText="Fecha Nac" DataFormatString="{0:dd/MM/yyyy}"/>
                                    <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                                    <asp:BoundField DataField="Localidad" HeaderText="Localidad" />
                                    <asp:BoundField DataField="Provincia" HeaderText="Provincia" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" />
                                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-outline-danger" />
                                </Columns>
                                <PagerStyle CssClass="pagination justify-content-center pt-3" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <div class="card border-primary shadow-sm">
                    <div class="card-header bg-primary text-white">
                        <h4 class="mb-0">Cargar Nuevo Paciente</h4>
                    </div>
                    <div class="card-body p-4">
                        <div class="row g-3">
                            <div class="col-md-3">
                                <label class="form-label font-weight-bold">DNI</label>
                                <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" placeholder="Ej: 45123456"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label">Nombre</label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-5">
                                <label class="form-label">Apellido</label>
                                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label class="form-label">Sexo</label>
                                <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="M">Masculino</asp:ListItem>
                                    <asp:ListItem Value="F">Femenino</asp:ListItem>
                                    <asp:ListItem Value="O">Otro</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
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
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="nombre@correo.com"></asp:TextBox>
                            </div>
                            <div class="col-12 text-end pt-3">
                                <asp:Button ID="btnCargar" runat="server" Text="Cargar Paciente" CssClass="btn btn-primary px-4" OnClick="btnCargar_Click" />
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
