<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VacancyCaretakerReportView.aspx.cs" Inherits="LeaMaPortal.ReportASPX.VacancyReport.VacancyCaretakerReportView" %>
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
            <asp:DropDownList ID="dropDown_Group" runat="server" Height="19px" Width="95px">
                <asp:ListItem>Property</asp:ListItem>
                <asp:ListItem>Region</asp:ListItem>
                <asp:ListItem>Caretaker</asp:ListItem>
            </asp:DropDownList>
        <br />
            <asp:Button ID="btn_showReport" runat="server" Text="Show Report" OnClick="btn_showReport_Click" />
        </div>
        <asp:ScriptManager runat="server" ID="VacancyCaretakerScript" ScriptMode="Release"></asp:ScriptManager>
        <div runat="server" visible="false" id="vacancyReport">
            <rsweb:ReportViewer ID="VacancyReportViewer" runat="server" width="100%" height="1033px" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            </rsweb:ReportViewer>
        </div>

    <%--<div runat="server" visible="false" id="VacancyCaretaker">
        <rsweb:ReportViewer ID="VacancyCaretakerReport" runat="server" width="100%" height="1033px" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            <LocalReport ReportPath="ReportRDLC\Vacancy\vacancycaretaker.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="VacancyReportEntityDataSource" Name="DataSet1" />
                </DataSources>
            </LocalReport>
            
        </rsweb:ReportViewer>
        <asp:EntityDataSource ID="VacancyCaretakerEntityDataSource" runat="server" ConnectionString="name=LeamaEntities" DefaultContainerName="LeamaEntities" EnableFlattening="False" EntitySetName="vacancy_caretaker_report" EntityTypeFilter="vacancy_caretaker_report" Select="it.[Caretaker_id], it.[Caretaker_Name], it.[Region_Name], it.[Country], it.[Aging_days], it.[Noof_properties], it.[Aging_Range], it.[Amont_Range], it.[Loss_Amt], it.[user]">
        </asp:EntityDataSource>
    </div>
        <div runat="server" visible="false" id="VacancyRegion">
            <rsweb:ReportViewer ID="VacancyRegionReport" runat="server" width="100%" height="1033px" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="ReportRDLC\Vacancy\vacancyregion.rdlc">
                    <DataSources>
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
        </div>--%>
    </form>
</body>
</html>
