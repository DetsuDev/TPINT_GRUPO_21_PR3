<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPacientes.aspx.cs" Inherits="TPINT_GRUPO_21_PR3.MenuAdmin.AdminPacientes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <title>Gestión de Pacientes</title>
        <style>
        .form-select{
            min-width:140px;
        }
        .form-control{
            min-width:120px;
        }
    </style>
</head>
<body style="background-color: #f8f9fa;">

  <div  class="card" style="z-index: 999; position: fixed; right: 20px; bottom: 20px">
  <div class="card-body" >
    <p class="card-text">Bienvenido: </p>
      <asp:Label ID="lblNombreUsuario" runat="server" Text="[Usuario]"></asp:Label>
    <a href="../login.aspx" class="btn btn-primary"> Cerrar Sesion </a>
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
                <asp:HiddenField ID="hdnIdEliminar" runat="server" />
                <div class="card" runat="server"
                    id="divEliminar"
                    style="z-index: 9999; width: 320px; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); text-align: center; padding: 10px;">
                    <div class="card-body">
                        <p class="card-text">¿Está seguro que desea eliminar el registro?</p>
                        <div style="text-align: right">
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" class="btn btn-danger" OnClick="btnEliminar_Click"/>
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-secondary" OnClick="btnCancelar_Click"/>
                        </div>
                    </div>
                </div>
                <div class="card border-primary mb-5 shadow-sm">
                    <div class="card-header bg-primary text-white">
                        <h4 class="mb-0">Buscar Pacientes</h4>
                    </div>
                    <div class="card-body p-4">
                        <div class="row g-3">
                            <div class="col-md-5">
                                <label class="form-label">Búsqueda (DNI, nombre o apellido)</label>
                                <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label">Provincia</label>
                                <asp:DropDownList ID="ddlFiltroProvincia" runat="server" CssClass="form-select"></asp:DropDownList>
                            </div>
                            <div class="col-12 text-end pt-3">
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary px-4" OnClick="btnBuscar_Click" CausesValidation="false" />
                                <asp:Button ID="btnLimpiarBusqueda" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary px-4" OnClick="btnLimpiarBusqueda_Click" CausesValidation="false" />
                            </div>
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
                                DataKeyNames="ID"
                                CssClass="table table-striped table-hover table-bordered align-middle"
                                OnPageIndexChanging="gvGestionPacientes_PageIndexChanging"
                                OnRowCancelingEdit="gvGestionPacientes_RowCancelingEdit" 
                                OnRowDeleting="gvGestionPacientes_RowDeleting" 
                                OnRowEditing="gvGestionPacientes_RowEditing" 
                                OnRowUpdating="gvGestionPacientes_RowUpdating">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-outline-warning" />
                                    <asp:TemplateField HeaderText="ID">
                                        <ItemTemplate><asp:Label ID="lbl_it_Id" runat="server" Text='<%# Eval("ID") %>'></asp:Label></ItemTemplate>
                                        <EditItemTemplate><asp:Label ID="lbl_eit_Id" runat="server" Text='<%# Eval("ID") %>'></asp:Label></EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="DNI">
                                        <ItemTemplate><asp:Label ID="lbl_it_Dni" runat="server" Text='<%# Eval("DNI") %>'></asp:Label></ItemTemplate>
                                        <EditItemTemplate><asp:Label ID="lbl_eit_Dni" runat="server" Text='<%# Eval("DNI") %>'></asp:Label></EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nombre">
                                        <ItemTemplate><asp:Label ID="lbl_it_Nombre" runat="server" Text='<%# Eval("Nombre") %>'></asp:Label></ItemTemplate>
                                        <EditItemTemplate><asp:TextBox ID="txt_eit_Nombre" runat="server" CssClass="form-control form-control-sm" Text='<%# Bind("Nombre") %>'></asp:TextBox></EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Apellido">
                                        <ItemTemplate><asp:Label ID="lbl_it_Apellido" runat="server" Text='<%# Eval("Apellido") %>'></asp:Label></ItemTemplate>
                                        <EditItemTemplate><asp:TextBox ID="txt_eit_Apellido" runat="server" CssClass="form-control form-control-sm" Text='<%# Bind("Apellido") %>'></asp:TextBox></EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sexo">
                                        <ItemTemplate><asp:Label ID="lbl_it_Sexo" runat="server" Text='<%# Eval("Sexo") %>'></asp:Label></ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlGridSexo" runat="server" CssClass="form-select form-select-sm">
                                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                                                <asp:ListItem Value="F">Femenino</asp:ListItem>
                                                <asp:ListItem Value="O">Otro</asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nacionalidad">
                                        <ItemTemplate><asp:Label ID="lbl_it_Nacionalidad" runat="server" Text='<%# Eval("Nacionalidad") %>'></asp:Label></ItemTemplate>
                                        <EditItemTemplate><asp:TextBox ID="txt_eit_Nacionalidad" runat="server" CssClass="form-control form-control-sm" Text='<%# Bind("Nacionalidad") %>'></asp:TextBox></EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fecha Nac">
                                        <ItemTemplate><asp:Label ID="lbl_it_FechaNac" runat="server" Text='<%# Eval("FechaNac", "{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_eit_FechaNac" runat="server" CssClass="form-control form-control-sm" TextMode="Date" Text='<%# Bind("FechaNac", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dirección">
                                        <ItemTemplate><asp:Label ID="lbl_it_Direccion" runat="server" Text='<%# Eval("Direccion") %>'></asp:Label></ItemTemplate>
                                        <EditItemTemplate><asp:TextBox ID="txt_eit_Direccion" runat="server" CssClass="form-control form-control-sm" Text='<%# Bind("Direccion") %>'></asp:TextBox></EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Localidad">
                                        <ItemTemplate><asp:Label ID="lbl_it_Localidad" runat="server" Text='<%# Eval("Localidad") %>'></asp:Label></ItemTemplate>
                                        <EditItemTemplate><asp:DropDownList ID="ddlGridLocalidad" runat="server" CssClass="form-select form-select-sm"></asp:DropDownList></EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Provincia">
                                        <ItemTemplate><asp:Label ID="lbl_it_Provincia" runat="server" Text='<%# Eval("Provincia") %>'></asp:Label></ItemTemplate>
                                        <EditItemTemplate><asp:DropDownList ID="ddlGridProvincia" runat="server" CssClass="form-select form-select-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlGridProvincia_SelectedIndexChanged"></asp:DropDownList></EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate><asp:Label ID="lbl_it_Email" runat="server" Text='<%# Eval("Email") %>'></asp:Label></ItemTemplate>
                                        <EditItemTemplate><asp:TextBox ID="txt_eit_Email" runat="server" CssClass="form-control form-control-sm" Text='<%# Bind("Email") %>'></asp:TextBox></EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Teléfono">
                                        <ItemTemplate><asp:Label ID="lbl_it_Telefono" runat="server" Text='<%# Eval("Telefono") %>'></asp:Label></ItemTemplate>
                                        <EditItemTemplate><asp:TextBox ID="txt_eit_Telefono" runat="server" CssClass="form-control form-control-sm" Text='<%# Bind("Telefono") %>'></asp:TextBox></EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-outline-danger" />
                                </Columns>
                                <PagerStyle CssClass="pagination justify-content-center pt-3" />
                            </asp:GridView>
                        </div>
                        <div class="text-center pt-3">
                            <asp:Label ID="lblMensajeGrid" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="card border-primary shadow-sm">
                    <div class="card-header bg-primary text-white">
                        <h4 class="mb-0">Cargar Nuevo Paciente</h4>
                    </div>
                    <div class="card-body p-4">
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <div class="row g-3">
                            <div class="col-md-3">
                                <label class="form-label font-weight-bold">DNI</label>
                                <asp:RequiredFieldValidator ID="rfvNombrePaciente" runat="server" ErrorMessage="*" ControlToValidate="txtDni" ForeColor="Red" ValidationGroup="GrupoAlta"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revDniNumerico" runat="server" ErrorMessage="* Solo números" ControlToValidate="txtDni" ValidationExpression="^\d{7,9}$" ForeColor="Red" Display="Dynamic" ValidationGroup="GrupoAlta"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" placeholder="Ej: 45123456"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label">Nombre</label>
                                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="*" ControlToValidate="txtNombre" ForeColor="Red" ValidationGroup="GrupoAlta"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-5">
                                <label class="form-label">Apellido</label>
                                <asp:RequiredFieldValidator ID="rfvApellidoPaciente" runat="server" ErrorMessage="*" ControlToValidate="txtApellido" ForeColor="Red" ValidationGroup="GrupoAlta"></asp:RequiredFieldValidator>
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
                                <asp:RequiredFieldValidator ID="rfvNacionalidadPaciente" runat="server" ErrorMessage="*" ControlToValidate="txtNacionalidad" ForeColor="Red" ValidationGroup="GrupoAlta"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtNacionalidad" runat="server" CssClass="form-control" placeholder="Ej: Argentina"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label">Fecha de Nacimiento</label>
                                <asp:RequiredFieldValidator ID="rfvFechaNacimientoPaciente" runat="server" ErrorMessage="*" ControlToValidate="txtFechaNac" ForeColor="Red" ValidationGroup="GrupoAlta"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtFechaNac" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label">Teléfono</label>
                                <asp:RequiredFieldValidator ID="rfvTelefonoPaciente" runat="server" ErrorMessage="*" ControlToValidate="txtTelefono" ForeColor="Red" ValidationGroup="GrupoAlta"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revTelefonoNumerico" runat="server" ErrorMessage="* Solo números" ControlToValidate="txtTelefono" ValidationExpression="^\d+$" ForeColor="Red" Display="Dynamic" ValidationGroup="GrupoAlta"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">Dirección</label>
                                <asp:RequiredFieldValidator ID="rfvDireccionPaciente" runat="server" ErrorMessage="*" ControlToValidate="txtDireccion" ForeColor="Red" ValidationGroup="GrupoAlta"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label">Provincia</label>
                                <asp:UpdatePanel ID="upProvincia" runat="server" RenderMode="Inline">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlProvincia" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label">Localidad</label>
                                <asp:UpdatePanel ID="upLocalidad" runat="server" RenderMode="Inline">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlLocalidad" runat="server" CssClass="form-select"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">Correo Electrónico</label>
                                <asp:RegularExpressionValidator ID="revEmailPaciente" runat="server" ErrorMessage="*" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" ValidationGroup="GrupoAlta"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="nombre@correo.com"></asp:TextBox>
                            </div>
                            <div class="col-12 text-end pt-3">
                                <asp:Label ID="lblMensaje" runat="server" ForeColor="Green" Font-Bold="true" CssClass="me-3"></asp:Label>
                                <asp:Button ID="Button1" runat="server" Text="Cargar Paciente" CssClass="btn btn-primary px-4" OnClick="btnCargar_Click" ValidationGroup="GrupoAlta" />
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
