<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EBWaterReportView.aspx.cs" Inherits="LeaMaPortal.ReportASPX.Utility.EBWaterReportView" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Group"></asp:Label>
            <asp:DropDownList ID="dropDown_Group" AutoPostBack="true" OnTextChanged="groupDropdownChange" runat="server" Height="19px" Width="95px">
                <asp:ListItem Selected="True">--Select--</asp:ListItem>
                <asp:ListItem>Property</asp:ListItem>
                <asp:ListItem>Region</asp:ListItem>
                <asp:ListItem>Caretaker</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="Label2" runat="server" Text="From"></asp:Label>
            <asp:TextBox ID="txt_fromDate" runat="server" placeholder="From" type="date"></asp:TextBox>
            <asp:Label ID="Label3" runat="server" Text="To"></asp:Label>
            <asp:TextBox ID="txt_toDate" runat="server" placeholder="To" type="date"></asp:TextBox>
            <asp:Label ID="Label4" runat="server" Text="Filter By"></asp:Label>
            <asp:DropDownList ID="dropDown_FilterBy" AutoPostBack="true" OnTextChanged="filterbyChange" runat="server" Height="19px" Width="95px">
                <%--<asp:ListItem Selected="True">--Select--</asp:ListItem>
                <asp:ListItem>Property</asp:ListItem>
                <asp:ListItem>Region</asp:ListItem>
                <asp:ListItem>Caretaker</asp:ListItem>--%>
            </asp:DropDownList>
            <asp:TextBox ID="txt_filterBy" Visible="false" runat="server"></asp:TextBox>
            <asp:Label ID="Label5" runat="server" Text="Aging Days"></asp:Label>
            <asp:DropDownList ID="dropDown_AgingDays" AutoPostBack="true" OnTextChanged="agingDaysChange" runat="server" Height="19px" Width="95px">
                <%--<asp:ListItem Selected="True">--Select--</asp:ListItem>--%>
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>others</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txt_fromAging" type="number" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txt_toAging" type="number" runat="server" Visible="false"></asp:TextBox>

            <asp:Label ID="Label6" runat="server" Text="Value Range"></asp:Label>
            <asp:DropDownList ID="dropDown_ValueRange" AutoPostBack="true" OnTextChanged="valueRangeChange" runat="server" Height="19px" Width="95px">
                <%--<asp:ListItem Selected="True">--Select--</asp:ListItem>--%>
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>others</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txt_fromValue" type="number" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txt_toValue" type="number" runat="server" Visible="false"></asp:TextBox>
            <asp:Label ID="Label7" runat="server" Text="Created User"></asp:Label>
            <asp:TextBox ID="txt_CreatedUser" runat="server"></asp:TextBox>

        <br />
            <asp:Button ID="btn_showReport" runat="server" Text="Show Report" OnClick="btn_showReport_Click" />
        </div>
    <div>

        <asp:ScriptManager runat="server" ID="EBWaterReportScript" ScriptMode="Release"></asp:ScriptManager>
        <div runat="server" visible="false" id="EBWaterReport">
            <rsweb:ReportViewer ID="EBWaterReportViewer" runat="server" width="100%" height="1033px" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            </rsweb:ReportViewer>
        </div>
    </div>
    </form>
</body>
</html>
