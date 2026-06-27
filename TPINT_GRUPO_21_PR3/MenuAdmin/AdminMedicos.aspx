<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminMedicos.aspx.cs" Inherits="TPINT_GRUPO_21_PR3.MenuAdmin.GestionMedicos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>
    <link rel="stylesheet"
      href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css" />
    <title>Gestión de Médicos</title>
    <style>
        .custom-checkboxlist label {
            margin-right: 6px;
            margin-left: 2px;
            font-size: 15px;
        }
        .custom-checkboxlist  {

            transform: translateY(3px);
        }
        #fullscreenOverlay {
              display: none;
              position: fixed;       
              top: 0;
              left: 0;
              width: 100%;           
              height: 100vh;         
              background-color: rgba(0, 0, 0, 0.7); 
              z-index: 1000;         
              justify-content: center;
              align-items: center;
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
            <li class="nav-item"><a class="nav-link" href="AdminPacientes.aspx">Gestionar Pacientes</a></li>
            <li class="nav-item"><a class="nav-link active" aria-current="page" href="AdminMedicos.aspx">Gestionar Medicos</a></li>
            <li class="nav-item"><a class="nav-link" href="AdminTurnos.aspx">Gestionar Turnos</a></li>
        </ul>


        <div class="border border-top-0 p-5" style="background-color: white;">

            <form id="form1" runat="server">
                    <div id="fullscreenOverlay" runat="server"></div>
                <asp:HiddenField ID="hdnIdMedico" runat="server" />
                <asp:HiddenField ID="hdnIdPersona" runat="server" />
                
                    <asp:Label ID="lblMensaje" runat="server" Font-Bold="true" CssClass="me-3"></asp:Label>
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
                        <h4 class="mb-0">Listado de Médicos</h4>
                    </div>
                    <div class="card-body p-4">
                        <div class="table-responsive">
                            <asp:GridView ID="gvGestionMedicos" runat="server"
                                AutoGenerateColumns="False"
                                AllowPaging="True"
                                PageSize="5"
                                DataKeyNames="Id_Medico"
                                CssClass="table table-striped table-hover table-bordered align-middle"
                                OnPageIndexChanging="gvGestionMedicos_PageIndexChanging" 
                                OnRowDeleting="gvGestionMedicos_RowDeleting">
                                <Columns>
                                    <%--<asp:BoundField DataField="Horario" HeaderText="Horario Atención" /> --%>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEditar" runat="server" class="btn btn-outline-warning" CommandArgument='<%# Eval("Id_Medico") %>' OnClick="btnEditar_Click" Text="Editar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Id_Medico" HeaderText="ID" ReadOnly="True" />
                                    <asp:BoundField DataField="Legajo_Medico" HeaderText="Legajo" ReadOnly="True"/>
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                                    <asp:BoundField DataField="Especialidad" HeaderText="Especialidad" />
                                    <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
                                    <asp:BoundField DataField="Nacionalidad" HeaderText="Nacionalidad" />
                                    
                                    <asp:BoundField DataField="FechaNac" HeaderText="Fecha Nac" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="Direccion" HeaderText="Direccion" />
                                    <asp:BoundField DataField="Localidad" HeaderText="Localidad" />
                                    <asp:BoundField DataField="Provincia" HeaderText="Provincia" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" />
                                    <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
                                    <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                                    <asp:TemplateField HeaderText="Contraseña">
                                        <ItemTemplate>
                                            <asp:Label ID="pass" runat="server" Text="********"></asp:Label>
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
                <asp:Button ID="btnMostrarForm" runat="server" Text="Nuevo Medico" OnClick="btnMostrarForm_Click" class="btn btn-primary" />
                <div class="card" runat="server"
                    id="divFormulario"
                    style="z-index: 9999; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); min-width: 80%">
                    <div class="card-body">

                        <div class="card border-primary shadow-sm">
                            <div class="card-header bg-primary text-white">
                                <h4 class="mb-0" id="hCargarMedico" runat="server">Cargar Nuevo Médico</h4>
                            </div>
                            <div class="card-body p-4">
                                <div class="row g-3">
                                    <div class="col-md-3">
                                        <label class="form-label font-weight-bold">Legajo Médico</label>
                                        <asp:RequiredFieldValidator ID="rfvLegajoMedico" runat="server" ErrorMessage="*" ControlToValidate="txtLegajo" ForeColor="Red" ValidationGroup="GrupoMedico"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revLegajoMedico" runat="server" ErrorMessage="* Ingrese un valor valido" ValidationExpression="^[A-Z]{3}-\d{3}$" ControlToValidate="txtDni" ForeColor="Red" Display="Dynamic" ValidationGroup="GrupoMedico"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtLegajo" runat="server" CssClass="form-control" placeholder="Ej: MED-999"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">DNI</label>
                                        <asp:RequiredFieldValidator ID="rfvDNI" runat="server" ErrorMessage="*" ControlToValidate="txtDni" ForeColor="Red" ValidationGroup="GrupoMedico"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revDNI" runat="server" ErrorMessage="* Ingrese 8 digitos numericos" ValidationExpression="^\d{8}$" ControlToValidate="txtDni" ForeColor="Red" Display="Dynamic" ValidationGroup="GrupoMedico"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtDni" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">Nombre</label>

                                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="*" ControlToValidate="txtNombre" ForeColor="Red" ValidationGroup="GrupoMedico"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revNombre" runat="server" ErrorMessage="* Solo letras" ValidationExpression="^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$" ControlToValidate="txtNombre" ForeColor="Red" Display="Dynamic" ValidationGroup="GrupoMedico"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">Apellido</label>

                                        <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ErrorMessage="*" ControlToValidate="txtApellido" ForeColor="Red" ValidationGroup="GrupoMedico"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revApellido" runat="server" ErrorMessage="* Solo letras" ValidationExpression="^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$" ControlToValidate="txtApellido" ForeColor="Red" Display="Dynamic" ValidationGroup="GrupoMedico"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label class="form-label">Especialidad</label>
                                        <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="form-label">Horario de Disponibilidad</label>
                                        <asp:DropDownList ID="ddlHorario" runat="server" CssClass="form-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">Dias disponibles</label>
                                        <asp:CheckBoxList ID="cblDiasDisponibles" runat="server" RepeatDirection="Horizontal" CssClass="custom-checkboxlist">
                                            <asp:ListItem Value="L">Lunes</asp:ListItem>
                                            <asp:ListItem Value="M">Martes</asp:ListItem>
                                            <asp:ListItem Value="X">Miercoles</asp:ListItem>
                                            <asp:ListItem Value="J">Jueves</asp:ListItem>
                                            <asp:ListItem Value="V">Viernes</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                    <div class="col-md-1">
                                        <label class="form-label">Sexo</label>
                                        <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-select" Style="font-size: 14px;">
                                            <asp:ListItem Value="M">Masculino</asp:ListItem>
                                            <asp:ListItem Value="F">Femenino</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="form-label">Nacionalidad</label>

                                        <asp:RequiredFieldValidator ID="rfvNacionalidad" runat="server" ErrorMessage="*" ControlToValidate="txtNacionalidad" ForeColor="Red" ValidationGroup="GrupoMedico"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revNacionalidad" runat="server" ErrorMessage="* Solo letras" ValidationExpression="^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$" ControlToValidate="txtNacionalidad" ForeColor="Red" Display="Dynamic" ValidationGroup="GrupoMedico"></asp:RegularExpressionValidator>

                                        &nbsp;<asp:TextBox ID="txtNacionalidad" runat="server" CssClass="form-control" placeholder="Ej: Argentina"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">Fecha de Nacimiento</label>
                                        <asp:TextBox ID="txtFechaNac" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">Telefono</label>
                                        <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ErrorMessage="*" ControlToValidate="txtTelefono" ForeColor="Red" ValidationGroup="GrupoMedico"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revTelefono" runat="server" ErrorMessage="* Ingrese 10 digitos numericos" ValidationExpression="^\d{10}$" ControlToValidate="txtTelefono" ForeColor="Red" Display="Dynamic" ValidationGroup="GrupoMedico"></asp:RegularExpressionValidator>
                                        &nbsp;<asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="form-label">Dirección</label>
                                        <asp:RequiredFieldValidator ID="rfvDireccion" runat="server" ErrorMessage="*" ControlToValidate="txtDireccion" ForeColor="Red" ValidationGroup="GrupoMedico"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">Provincia</label>
                                        <asp:RequiredFieldValidator ID="rfvProvincia" runat="server" ErrorMessage="*" ControlToValidate="ddlProvincia" ForeColor="Red" ValidationGroup="GrupoMedico" InitialValue="-- Elija una provincia --"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlProvincia" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">Localidad</label>
                                        <asp:RequiredFieldValidator ID="rfvLocalidad" runat="server" ErrorMessage="*" ControlToValidate="ddlLocalidad" ForeColor="Red" ValidationGroup="GrupoMedico"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlLocalidad" runat="server" CssClass="form-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="*" ControlToValidate="txtEmail" ForeColor="Red" ValidationGroup="GrupoMedico"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="* Correo no válido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ForeColor="Red" Display="Dynamic" ValidationGroup="GrupoMedico"></asp:RegularExpressionValidator>
                                        &nbsp;<asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="medico@clinica.com"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">
                                            Usuario de Login       
                                            <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">Contraseña</label>
                                        <asp:TextBox ID="txtContrasenia" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">Confirmar contraseña</label>
                                        <asp:CompareValidator ID="cvContrasenia" runat="server" ControlToValidate="txtConfirmarContrasenia" ControlToCompare="txtContrasenia" ErrorMessage="* No coinciden" ForeColor="Red" Display="Dynamic" ValidationGroup="GrupoMedico"></asp:CompareValidator>
                                        <asp:TextBox ID="txtConfirmarContrasenia" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                    </div>
                                    <div class="col-12 text-end pt-3">
                                        <asp:Button ID="btnCargar" runat="server" Text="Cargar Médico" CssClass="btn btn-primary px-4" OnClick="btnCargar_Click" ValidationGroup="GrupoMedico" />
                                        <asp:Button ID="btnCancelarEdicion" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary px-4" OnClick="btnCancelarEdicion_Click" CausesValidation="false" Visible="true" />
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
            </form>
        </div>
    </div>

        <script src="../js/bootstrap.bundle.min.js">
        </script>
    <script>


        </script>


</body>
</html>