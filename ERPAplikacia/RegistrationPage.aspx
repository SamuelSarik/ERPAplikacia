<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrationPage.aspx.cs" Inherits="ERPAplikacia.RegistrationPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Stylesheets" runat="server">
    <link rel="stylesheet" href="/Css/RegisterPage.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Krstné meno:"></asp:Label>
        <span class="offset"><asp:TextBox ID="FirstNameBox" runat="server" CssClass="offset"></asp:TextBox></span>
    </p>
    <p>
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Priezvisko:"></asp:Label>
        <span class="offset"><asp:TextBox ID="LastNameBox" runat="server" CssClass="offset"></asp:TextBox></span>
    </p>
    <p>
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Dátum narodenia:"></asp:Label>
        <span class="offset"><asp:TextBox ID="BirthBox" runat="server" CssClass="offset"></asp:TextBox></span>
    </p>
    <p>
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Ulica:"></asp:Label>
        <span class="offset"><asp:TextBox ID="StreetBox" runat="server" CssClass="offset"></asp:TextBox></span>
    </p>
    <p>
        <asp:Label ID="Label5" runat="server" Font-Bold="True" Text="Mesto:"></asp:Label>
        <span class="offset"><asp:TextBox ID="CityBox" runat="server" CssClass="offset"></asp:TextBox></span>
    </p>
    <p>
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="PSČ:"></asp:Label>
        <span class="offset"><asp:TextBox ID="PostCodeBox" runat="server" CssClass="offset"></asp:TextBox></span>
    </p>
    <p>
        <asp:Label ID="Label7" runat="server" Font-Bold="True" Text="Krajina:"></asp:Label>
        <span class="offset"><asp:TextBox ID="CountryBox" runat="server" CssClass="offset"></asp:TextBox></span>
    </p>
    <p>
        <asp:Label ID="Label8" runat="server" Font-Bold="True" Text="Prihlasovacie meno:"></asp:Label>
        <span class="offset"><asp:TextBox ID="UserBox" runat="server" CssClass="offset"></asp:TextBox></span>
    </p>
    <p>
        <asp:Label ID="Label9" runat="server" Font-Bold="True" Text="Heslo:"></asp:Label>
        <span class="offset"><asp:TextBox ID="PasswordBox" runat="server" TextMode="Password" CssClass="offset"></asp:TextBox></span>
    </p>
    <p>
        <asp:Label ID="Label10" runat="server" Font-Bold="True" Text="Potvrdiť heslo:"></asp:Label>
        <span class="offset"><asp:TextBox ID="ConfirmPasswordBox" runat="server" TextMode="Password" CssClass="offset"></asp:TextBox></span>
    </p>
    <p>
        <span class="margin"><asp:Button ID="ConfirmButton" runat="server" BackColor="#0066FF" ForeColor="White" Height="38px" Text="Potvrdiť" Width="90px" OnClick="ConfirmButton_Click" /></span>
        <span class="margin"><asp:Button ID="ReturnButton" runat="server" BackColor="#ff7300" ForeColor="White" Height="38px" Text="Späť" Width="90px" OnClick="ReturnButton_Click" /></span>
    </p>
<p>
        <asp:Label ID="WarningLabel" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
    </p>
</asp:Content>
