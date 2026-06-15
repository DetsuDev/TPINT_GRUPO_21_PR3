<%@ Page Language="C#" AutoEventWireup="true" 
    CodeBehind="MedicoInicio.aspx.cs" 
    Inherits="TPINT_GRUPO_21_PR3.GestionMedico.GestionMedicos" %>
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
        <a href="../GestionMedico/login.aspx" class="btn btn-primary"> Cerrar Sesion </a>
      </div>
    </div>


    <div style="padding: 50px; margin: 50px;">
        <ul class="nav nav-tabs" style=" min-width: 1000px;">
            <li class="nav-item">
                <a class="nav-link active" href="../GestionMedico/Inicio.aspx">Inicio</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="../GestionMedico/GestionTurnos.aspx">Gestionar Turnos</a>
            </li>
        </ul>
        <div class="border border-top-0 p-5" style="background-color: white;" >

            <form id="form1" runat="server">
            </form>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js" integrity="sha384-FKyoEForCGlyvwx9Hj09JcYn3nv7wiPVlz7YYwJrWVcXK/BmnVDxM+D2scQbITxI" crossorigin="anonymous"></script>
</body>
</html>
