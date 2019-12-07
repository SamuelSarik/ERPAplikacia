<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InvoicePage.aspx.cs" Inherits="ERPAplikacia.InvoicePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Stylesheets" runat="server">
    <link rel="stylesheet" href="/css/InvoicePage.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:Label ID="LogLabel" runat="server" Font-Bold="True" Font-Italic="True"></asp:Label>
    </p>
    <p>
        <asp:Table ID="InvoiceTable" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Caption="Objednávky">
        </asp:Table>
    </p>
    <p>
        <asp:Label ID="AuthorisationLabel" runat="server" Font-Bold="True" Text="Schváliť faktúru s ID:"></asp:Label>
        <span class="margin"><asp:TextBox ID="IdBox" runat="server" BackColor="#999999" Width="37px" ForeColor="White"></asp:TextBox></span>
        <span class="margin"><asp:Button ID="ApplyButton" runat="server" BackColor="#0066FF" ForeColor="White" Height="31px" Text="Aplikovať" Width="92px" OnClick="ApplyButton_Click" /></span>
    </p>
    <p>
        <asp:Label ID="MessageLabel" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Zobraziť faktúru s ID:"></asp:Label>
        <span class="margin"><asp:TextBox ID="ShowIdBox" runat="server" BackColor="#999999" Width="37px"></asp:TextBox></span>
        <span class="margin"><asp:Button ID="ShowButton" runat="server" BackColor="#0066FF" ForeColor="White" Height="31px" Text="Zobraziť" Width="92px" OnClick="ShowButton_Click" /></span>
    </p>
    <p>
        <asp:Label ID="PayLabel" runat="server" Font-Bold="True" Text="Zaplatiť faktúru s ID:"></asp:Label>
        <span class="margin"><asp:TextBox ID="PayIdBox" runat="server" BackColor="#999999" Width="37px"></asp:TextBox></span>
        <span><asp:Button ID="PayButton" runat="server" BackColor="#0066FF" ForeColor="White" Height="31px" Text="Zaplatiť" Width="92px" OnClick="PayButton_Click" /></span>
    </p>
    <p>
        <span class="margin"><asp:Button ID="ReturnButton" runat="server" BackColor="#ff7300" ForeColor="White" Height="31px" Text="Späť" Width="90px" OnClick="ReturnButton_Click" /></span>
    </p>
</asp:Content>
