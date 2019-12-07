<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewInvoicePage.aspx.cs" Inherits="ERPAplikacia.NewInvoicePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Stylesheets" runat="server">
    <link rel="stylesheet" href="/css/NewInvoicePage.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
    <asp:Label ID="LogLabel" runat="server" Font-Bold="True" Font-Italic="True"></asp:Label>
    </p>
    <p>
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Vytvoriť novú objednávku produktu:"></asp:Label>
    </p>
    <p>
    <asp:Label ID="Label2" runat="server" Text="Vybrať produkt:"></asp:Label>
    <asp:DropDownList ID="ProductList" runat="server">
        <asp:ListItem>Farebný papier žltý</asp:ListItem>
        <asp:ListItem>Klasický papier</asp:ListItem>
        <asp:ListItem>Kartón</asp:ListItem>
        <asp:ListItem>Farebný papier červený</asp:ListItem>
        <asp:ListItem>Farebný papier zelený</asp:ListItem>
        <asp:ListItem>Farebný papier modrý</asp:ListItem>
        <asp:ListItem>Farebný papier čierny</asp:ListItem>
        <asp:ListItem>Kartón</asp:ListItem>
        <asp:ListItem>Guličkové pero modré</asp:ListItem>
        <asp:ListItem>Guličkové pero červené</asp:ListItem>
        <asp:ListItem>Plniace pero</asp:ListItem>
        <asp:ListItem>Sada zvýrazňovačov</asp:ListItem>
        <asp:ListItem>Ceruzka HB1</asp:ListItem>
        <asp:ListItem>Ceruzka HB2</asp:ListItem>
        <asp:ListItem>Ceruzka HB3</asp:ListItem>
        <asp:ListItem>Kalkulačka CASIO500</asp:ListItem>
        <asp:ListItem>Euroobaly 100ks</asp:ListItem>
        <asp:ListItem>Šanón A4</asp:ListItem>
        <asp:ListItem>Zošívačka</asp:ListItem>
        <asp:ListItem>Nožnice</asp:ListItem>
        <asp:ListItem>Stojan</asp:ListItem>
        <asp:ListItem>Zošit 540</asp:ListItem>
        <asp:ListItem>Zošit 520</asp:ListItem>
        <asp:ListItem>Obaly zošit</asp:ListItem>
        <asp:ListItem>Guma</asp:ListItem>
        <asp:ListItem>Poznámkový blokA4</asp:ListItem>
        <asp:ListItem>Poznámkový blokA5</asp:ListItem>
        <asp:ListItem>Diár 2020</asp:ListItem>
        <asp:ListItem>Stolový kalendár</asp:ListItem>
        <asp:ListItem>Fixa flipchart modrá</asp:ListItem>
        <asp:ListItem>Fixa flipchart červená</asp:ListItem>
        <asp:ListItem>Fixa flipchart čierna</asp:ListItem>
    </asp:DropDownList>
    </p>
    <p>
    <asp:Label ID="Label3" runat="server" Text="Množstvo:"></asp:Label>
    <asp:TextBox ID="AmountBox" runat="server" Width="47px">0</asp:TextBox>
    </p>
    <p>
    <span class="margin"><asp:Button ID="ConfirmButton" runat="server" BackColor="#0066FF" ForeColor="White" Height="38px" Text="Potvrdiť" Width="90px" OnClick="ConfirmButton_Click" /></span>
    <span class="margin"><asp:Button ID="ReturnButton" runat="server" BackColor="#ff7300" ForeColor="White" Height="38px" Text="Späť" Width="90px" OnClick="ReturnButton_Click" /></span>
    </p>
</asp:Content>
