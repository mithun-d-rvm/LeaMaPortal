<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VacancyReportView.aspx.cs" Inherits="LeaMaPortal.ReportASPX.VacancyReport.VacancyCaretakerReportView" %>
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
                <asp:DropDownList ID="dropDown_Group" AutoPostBack="true" class="form-control master-form-input" OnTextChanged="groupDropdownChange" runat="server">
                    <asp:ListItem Selected="True">--Select--</asp:ListItem>
                    <asp:ListItem>Property</asp:ListItem>
                    <asp:ListItem>Region</asp:ListItem>
                    <asp:ListItem>Caretaker</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-3 form-group">
                <asp:Label ID="Label2" runat="server" Text="From"></asp:Label>
            <asp:TextBox ID="txt_fromDate" runat="server" class="form-control master-form-input" placeholder="From" type="date"></asp:TextBox>
            </div>
            <div class="col-md-3 form-group">
                <asp:Label ID="Label3" runat="server" Text="To"></asp:Label>
            <asp:TextBox ID="txt_toDate" runat="server" class="form-control master-form-input" placeholder="To" type="date"></asp:TextBox>
            </div>
            <div class="col-md-2 form-group">
                <asp:Label ID="Label4" runat="server" Text="Filter By"></asp:Label>
            <asp:DropDownList ID="dropDown_FilterBy" AutoPostBack="true" OnTextChanged="filterbyChange" class="form-control master-form-input" runat="server">
                <%--<asp:ListItem Selected="True">--Select--</asp:ListItem>
                <asp:ListItem>Property</asp:ListItem>
                <asp:ListItem>Region</asp:ListItem>
                <asp:ListItem>Caretaker</asp:ListItem>--%>
            </asp:DropDownList>
            </div>
            <div class="col-md-2 form-group">
                <asp:Label ID="Label8" runat="server" Text="Filter Value"></asp:Label>
                <asp:TextBox ID="txt_filterBy" readonly="true" class="form-control master-form-input" runat="server"></asp:TextBox>
            </div>
            </div>
        <div class="col-md-12">
            <div class="col-md-2 form-group">
                <asp:Label ID="Label5" runat="server" Text="Aging Days"></asp:Label>
            <asp:DropDownList ID="dropDown_AgingDays" class="form-control master-form-input" AutoPostBack="true" OnTextChanged="agingDaysChange" runat="server">
                <%--<asp:ListItem Selected="True">--Select--</asp:ListItem>--%>
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>others</asp:ListItem>
            </asp:DropDownList>
            </div>
            <div class="col-md-2 form-group">
                 <asp:Label ID="Label9" runat="server" Text="Aging Days From"></asp:Label>
                <asp:TextBox ID="txt_fromAging" type="number" class="form-control master-form-input" runat="server" readonly="true"></asp:TextBox>
                </div>
            <div class="col-md-2 form-group">
                 <asp:Label ID="Label10" runat="server" Text="Aging Days To"></asp:Label>
                <asp:TextBox ID="txt_toAging" type="number" class="form-control master-form-input" runat="server" readonly="true"></asp:TextBox>
                </div>
            <div class="col-md-2 form-group">
                <asp:Label ID="Label6" runat="server" Text="Value Range"></asp:Label>
            <asp:DropDownList ID="dropDown_ValueRange" AutoPostBack="true" class="form-control master-form-input" OnTextChanged="valueRangeChange" runat="server">
                <%--<asp:ListItem Selected="True">--Select--</asp:ListItem>--%>
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>others</asp:ListItem>
            </asp:DropDownList>
            </div>
            <div class="col-md-2 form-group">
                 <asp:Label ID="Label11" runat="server" Text="Value Range From"></asp:Label>
                <asp:TextBox ID="txt_fromValue" type="number" class="form-control master-form-input" runat="server" readonly="true"></asp:TextBox>
                </div>
            <div class="col-md-2 form-group">
                 <asp:Label ID="Label12" runat="server" Text="Value Range To"></asp:Label>
                <asp:TextBox ID="txt_toValue" type="number" class="form-control master-form-input" runat="server" readonly="true"></asp:TextBox>
                </div>
            </div>
        <div class="col-md-12">
            <div class="col-md-2 form-group">
                <asp:Label ID="Label7" runat="server" Text="Created User"></asp:Label>
            <asp:TextBox ID="txt_CreatedUser" class="form-control master-form-input" runat="server"></asp:TextBox>
                </div>
            <div class="col-md-2 form-group">
                <asp:Button ID="btn_showReport" runat="server" CssClass="btn btn-info btn-group" Text="Show Report" OnClick="btn_showReport_Click" />
            </div>
        </div>
        <hr />
        <asp:ScriptManager runat="server" ID="VacancyCaretakerScript" ScriptMode="Release"></asp:ScriptManager>
        <div runat="server" visible="false" id="vacancyReport">
            <rsweb:ReportViewer ID="VacancyReportViewer" runat="server" width="100%" height="700px" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
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
