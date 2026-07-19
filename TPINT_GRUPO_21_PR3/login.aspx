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

        #languageswitch{
              position: fixed;
              top: 1em;
              right: 5em;
        }
        #languageswitch input[type="radio"] { display: none; }
        #languageswitch label { cursor: pointer; }
        #languageswitch input[type="radio"]:checked + label {
            background-color: #0d6efd;
            color: #fff;
            border-color: #0d6efd;
        }
    </style>
</head>
<body style=" font-family: Arial, sans-serif; background-color: #eef1f5;">
    <form id="form1" runat="server">
    <div id="languageswitch" runat="server">
        <asp:RadioButton ID="rbtnEn" runat="server" GroupName="lang" AutoPostBack="true" OnCheckedChanged="rblLanguage_SelectedIndexChanged" ClientIDMode="Static" />
        <label for="rbtnEn" class="btn btn-outline-primary" style="margin-right:0.25rem;">EN</label>

        <asp:RadioButton ID="rbtnEs" runat="server" GroupName="lang" AutoPostBack="true" OnCheckedChanged="rblLanguage_SelectedIndexChanged" ClientIDMode="Static" />
        <label for="rbtnEs" class="btn btn-outline-primary">ES</label>
    </div>

        <div class="card" style="width: 320px; margin: 120px auto; padding: 30px; text-align: center; top: 0px; left: 0px;">
            <div>
                            <asp:Label ID="lblBienvenido" runat="server" Text="<%$ Resources:lang, lblWelcome %>" Font-Bold="True" Font-Size="X-Large"></asp:Label>

            </div>

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
            <asp:Button ID="btnLoguearse" runat="server" Text="<%$ Resources:lang, btnLoguearse %>" CssClass="btn btn-primary" Style="margin-top: 10px;" OnClick="btnLogearse_Click" />

            <asp:Label ID="lblMensaje" runat="server" CssClass="error" ></asp:Label>
        </div>
    </form>
    
        <script src="../js/bootstrap.bundle.min.js"></script>
</body>
</html>