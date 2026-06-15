<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPINT_GRUPO_21_PR3.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login - Clínica</title>
    <style type="text/css">
        body {
            font-family: Arial, sans-serif;
            background-color: #eef1f5;
        }
        .contenedor {
            width: 320px;
            margin: 120px auto;
            padding: 30px;
            background-color: #ffffff;
            border: 1px solid #cccccc;
            border-radius: 8px;
            text-align: center;
            box-shadow: 0 2px 6px rgba(0,0,0,0.1);
        }
        .contenedor h2 {
            margin-top: 0;
            color: #333333;
        }
        .campo {
            width: 100%;
            padding: 8px;
            margin: 8px 0;
            box-sizing: border-box;
            border: 1px solid #aaaaaa;
            border-radius: 4px;
        }
        .boton {
            width: 100%;
            padding: 10px;
            margin-top: 10px;
            background-color: #2a6fb0;
            color: #ffffff;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }
        .error {
            color: red;
            display: block;
            margin: 4px 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="contenedor">
            <h2>Bienvenido</h2>

            <asp:TextBox ID="txtUsuario" runat="server" CssClass="campo" placeholder="Usuario"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvUsuario" runat="server"
                ControlToValidate="txtUsuario"
                ErrorMessage="Ingrese el usuario"
                CssClass="error" Display="Dynamic"></asp:RequiredFieldValidator>

            <asp:TextBox ID="txtContrasena" runat="server" CssClass="campo" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvContrasena" runat="server"
                ControlToValidate="txtContrasena"
                ErrorMessage="Ingrese la contraseña"
                CssClass="error" Display="Dynamic"></asp:RequiredFieldValidator>

            <asp:Button ID="btnTestLoginAdmin" runat="server" Text="Login Admin" CssClass="boton" OnClick="btnTestLoginAdmin_Click" />

            <br />

            <asp:Button ID="btnTestLoginMedico" runat="server" Text="Login Medico" CssClass="boton" OnClick="btnTestLoginMedico_Click" />

            <asp:Label ID="lblMensaje" runat="server" CssClass="error"></asp:Label>
        </div>
    </form>
</body>
</html>