<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminInformes.aspx.cs" Inherits="TPINT_GRUPO_21_PR3.MenuAdmin.Informes" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>
   
    <title></title>
</head>
<body style="background-color: #f8f9fa;">
   <form id="form1" runat="server">
    <div class="card" style="z-index: 999; position: fixed; right: 20px; bottom: 20px">
      <div class="card-body" >
        <p class="card-text">Bienvenido: [Usuario].</p>
        <a href="../login.aspx" class="btn btn-primary"> Cerrar Sesion </a>
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
                <a class="nav-link" href="AdminMedicos.aspx">Gestionar Medicos</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="AdminTurnos.aspx">Gestionar Turnos</a>
            </li>
            <li class="nav-item">
                <a class="nav-link active" aria-current="page" href="AdminInformes.aspx">Informes</a>
            </li>
        </ul>

        <div class="border border-top-0 p-5" style="background-color: white;" >
    <div class="card">
    <div class="card-header bg-primary text-white">
        <h4 class="mb-0">Presentismo</h4>
    </div>
            <div class="card-body p-4">
                <br />
                <br />
                <div class="row g-3">
                    <div class="col-md-3">
                        <label class="form-label" for="start-date">Fecha inicial:</label>
                        <input type="date" class="form-control" id="start-date" />
                    </div>
                        <div class="col-md-3">
                            <label class="form-label" for="end-date">Fecha final:</label>
                            <input type="date" class="form-control" id="end-date" />
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnFiltrarPorFechaInforme"  class="btn btn-primary px-4" runat="server" Text="Filtrar" Height="75px" Width="93px" />
                    </div>
                    
                </div>
                    
                
                <p>Pacientes presentes: 70</p>
                <p>Pacientes ausentes: 30</p>
                 <div class="progress-stacked">
                  <div class="progress" role="progressbar" aria-label="Segment one" aria-valuenow="15" aria-valuemin="0" aria-valuemax="100" style="width: 70%">
                    <div class="progress-bar  bg-success">70%</div>
                  </div>
                  <div class="progress" role="progressbar" aria-label="Segment two" aria-valuenow="30" aria-valuemin="0" aria-valuemax="100" style="width: 30%">
                    <div class="progress-bar  bg-danger">30%</div>
                  </div>
                </div>
                </div>

            </div>
            <div class="card">

            </div>
        </div>

    <script src="../js/bootstrap.bundle.min.js"></script>
            </form>
</body>
</html>
