<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VacancyRegionReportView.aspx.cs" Inherits="LeaMaPortal.ReportASPX.VacancyReport.VacancyRegionReportView" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Visible="false">
        </rsweb:ReportViewer>
        <asp:ScriptManager runat="server" ID="SillyPrerequisite"></asp:ScriptManager>
    </div>
    </form>
</body>
</html>
