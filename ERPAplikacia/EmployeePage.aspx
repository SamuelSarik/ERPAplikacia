<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeePage.aspx.cs" Inherits="ERPAplikacia.EmployeePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Stylesheets" runat="server">
    <link rel="stylesheet" href="/css/InvoicePage.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:Label ID="LogLabel" runat="server" Font-Bold="True" Font-Italic="True"></asp:Label>
    </p>
    <p>
        <asp:Table ID="EmployeeTable" runat="server" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px" Caption="Zamestnanci">
        </asp:Table>
    </p>
    <p>
        <asp:Label ID="MessageLabel" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
    </p>
    <p>
        <span class="margin"><asp:Button ID="ReturnButton" runat="server" BackColor="#ff7300" ForeColor="White" Height="31px" Text="Späť" Width="90px" OnClick="ReturnButton_Click" /></span>
    </p>
</asp:Content>
