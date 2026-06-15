<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminMedicos.aspx.cs" Inherits="TPINT_GRUPO_21_PR3.MenuAdmin.GestionMedicos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous">

    <title></title>
</head>
<body style="background-color: #f8f9fa;">
    
    <div class="card" style="z-index: 999; position: fixed; right: 20px; bottom: 20px">
      <div class="card-body" >
        <p class="card-text">Bienvenido: [Usuario].</p>
        <a href="login.aspx" class="btn btn-primary"> Cerrar Sesion </a>
      </div>
    </div>


    <div style="padding: 50px; margin: 50px;">
        <ul class="nav nav-tabs" style=" min-width: 1000px;">
            <li class="nav-item">
                <a class="nav-link" href="AdminInicio.aspx">Inicio</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="AdminPacientes.aspx">Gestionar Pacientes</a>
            </li>
            <li class="nav-item">
                <a class="nav-link active" aria-current="page" href="AdminMedicos.aspx">Gestionar Medicos</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="AdminTurnos.aspx">Gestionar Turnos</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="AdminInformes.aspx">Informes</a>
            </li>
        </ul>
        <div class="border border-top-0 p-5" style="background-color: white;" >

            <form id="form1" runat="server">
                <asp:GridView ID="gvGestionMedicos" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="gvGestionMedicos_RowCancelingEdit" OnRowDeleting="gvGestionMedicos_RowDeleting" OnRowEditing="gvGestionMedicos_RowEditing" OnRowUpdating="gvGestionMedicos_RowUpdating">
                    <Columns>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:BoundField DataField="ID" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                        <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
                        <asp:BoundField DataField="Nacionalidad" HeaderText="Nacionalidad" />
                        <asp:BoundField DataField="FechaNac" HeaderText="Fecha Nac" />
                        <asp:BoundField DataField="Direccion" HeaderText="Direccion" />
                        <asp:BoundField DataField="Localidad" HeaderText="Localidad" />
                        <asp:BoundField DataField="Provincia" HeaderText="Provincia" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
                        <asp:BoundField DataField="Horario" HeaderText="Horario Atencion" />
                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                        <asp:BoundField DataField="Contrasenia" HeaderText="Contrasenia" />
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
            </form>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js" integrity="sha384-FKyoEForCGlyvwx9Hj09JcYn3nv7wiPVlz7YYwJrWVcXK/BmnVDxM+D2scQbITxI" crossorigin="anonymous"></script>
</body>
</html>
