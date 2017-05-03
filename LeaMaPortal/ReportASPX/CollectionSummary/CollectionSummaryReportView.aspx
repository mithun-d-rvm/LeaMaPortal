<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollectionSummaryReportView.aspx.cs" Inherits="LeaMaPortal.ReportASPX.CollectionSummary.CollectionSummaryReportView" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="col-md-12">
            <div class="col-md-2 form-group">
                <asp:Label ID="Label1" runat="server" Text="Group"></asp:Label>
            <asp:DropDownList ID="dropDown_Group" AutoPostBack="true" OnTextChanged="groupDropdownChange" class="form-control master-form-input" runat="server">
                <asp:ListItem Selected="True">--Select--</asp:ListItem>
                <asp:ListItem>Property</asp:ListItem>
                <asp:ListItem>Region</asp:ListItem>
                <asp:ListItem>Caretaker</asp:ListItem>
            </asp:DropDownList>
            </div>
            <div class="col-md-2 form-group">
                <asp:Label ID="Label2" runat="server" Text="From"></asp:Label>
            <asp:TextBox ID="txt_fromDate" runat="server" class="form-control master-form-input" placeholder="From" type="date"></asp:TextBox>
            </div>
            <div class="col-md-2 form-group">
                <asp:Label ID="Label3" runat="server" Text="To"></asp:Label>
            <asp:TextBox ID="txt_toDate" runat="server" class="form-control master-form-input" placeholder="To" type="date"></asp:TextBox>
            </div>
            <div class="col-md-2 form-group">
                <asp:Label ID="Label4" runat="server" Text="Filter By"></asp:Label>
            <asp:DropDownList ID="dropDown_FilterBy" AutoPostBack="true" class="form-control master-form-input" OnTextChanged="filterbyChange" runat="server">
                <%--<asp:ListItem Selected="True">--Select--</asp:ListItem>
                <asp:ListItem>Property</asp:ListItem>
                <asp:ListItem>Region</asp:ListItem>
                <asp:ListItem>Caretaker</asp:ListItem>--%>
            </asp:DropDownList>
            </div>
            <div class="col-md-2 form-group">
                <asp:Label ID="Label5" runat="server" Text="Filter by value"></asp:Label>
                <asp:TextBox ID="txt_filterBy" class="form-control master-form-input" readonly="true" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-2 form-group">
                <asp:Label ID="Label7" runat="server" Text="Created User"></asp:Label>
            <asp:TextBox ID="txt_CreatedUser" class="form-control master-form-input" runat="server"></asp:TextBox>
            </div>
            <%--<asp:Label ID="Label5" runat="server" Text="Aging Days"></asp:Label>
            <asp:DropDownList ID="dropDown_AgingDays" AutoPostBack="true" OnTextChanged="agingDaysChange" runat="server" Height="19px" Width="95px">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>others</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txt_fromAging" type="number" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txt_toAging" type="number" runat="server" Visible="false"></asp:TextBox>

            <asp:Label ID="Label6" runat="server" Text="Value Range"></asp:Label>
            <asp:DropDownList ID="dropDown_ValueRange" AutoPostBack="true" OnTextChanged="valueRangeChange" runat="server" Height="19px" Width="95px">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>others</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txt_fromValue" type="number" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txt_toValue" type="number" runat="server" Visible="false"></asp:TextBox>--%>
        </div>
        <div class="col-md-12 form-group">
            <asp:Button ID="btn_showReport" runat="server" CssClass="btn btn-info btn-group pull-right" Text="Show Report" OnClick="btn_showReport_Click" />
        </div>
    <div>

        <asp:ScriptManager runat="server" ID="CollectionSummaryReportScript" ScriptMode="Release"></asp:ScriptManager>
        <div runat="server" visible="false" id="CollectionSummaryReport">
            <rsweb:ReportViewer ID="CollectionSummaryReportViewer" runat="server" width="100%" height="1033px" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            </rsweb:ReportViewer>
        </div>
    </div>
    </form>
</body>
</html>
