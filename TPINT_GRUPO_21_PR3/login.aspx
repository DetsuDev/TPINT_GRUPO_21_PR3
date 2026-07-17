<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPINT_GRUPO_21_PR3.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>
   
    <title>Login - Clínica</title>
    <style type="text/css">
        .error {
            color: red;
            display: block;
            margin: 4px 0;
        }

        #language-switch{
              position: fixed;
              top: 1em;
              right: 5em;
        }
    </style>
</head>
<body style=" font-family: Arial, sans-serif; background-color: #eef1f5;">
    <form id="form1" runat="server">
    <div id="language-switch">
        <input type="radio" class="btn-check" name="options-outlined" id="english-btn" autocomplete="off" checked/>
        <label class="btn btn-outline-primary" for="english-btn">EN</label>

        <input type="radio" class="btn-check" name="options-outlined" id="spanish-btn" autocomplete="off"/>
        <label class="btn btn-outline-primary" for="spanish-btn">ES</label>
    </div>

        <div class="card" style="width: 320px; margin: 120px auto; padding: 30px; text-align: center; top: 0px; left: 0px;">
            <h2>Bienvenido</h2>

            <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Usuario"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvUsuario" runat="server"
                ControlToValidate="txtUsuario"
                ErrorMessage="Ingrese el usuario"
                class="alert alert-danger" Style="margin-top: 10px" Display="Dynamic"></asp:RequiredFieldValidator>

            <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control" Style="margin: 8px 0;"
                TextMode="Password" placeholder="Contraseña"></asp:TextBox>
            
            <asp:RequiredFieldValidator ID="rfvContrasena" runat="server"
                ControlToValidate="txtContrasena"
                ErrorMessage="Ingrese la contraseña"
                class="alert alert-danger" Style="margin-top: 10px;" Display="Dynamic" ></asp:RequiredFieldValidator>
            <asp:Button ID="btnLoguearse" runat="server" Text="Ingresar" CssClass="btn btn-primary" Style="margin-top: 10px;" OnClick="btnLogearse_Click" />

            <asp:Label ID="lblMensaje" runat="server" CssClass="error" ></asp:Label>
        </div>
    </form>
    
        <script src="../js/bootstrap.bundle.min.js"></script>
</body>
</html>