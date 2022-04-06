<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PropertyStatusReportView.aspx.cs" Inherits="LeaMaPortal.ReportASPX.PropertyStatusReport.PropertyStatusReportView" %>
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
                <asp:Label ID="Label1" runat="server" Text="Region"></asp:Label>
                <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="true" CssClass="form-control master-form-input" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"></asp:DropDownList>
            </div>

             <div class="col-md-2 form-group">    
                <asp:Label ID="Label2" runat="server" Text=" Property"></asp:Label>             
                <asp:DropDownList ID="ddlProperty" AutoPostBack="true" class="form-control master-form-input" runat="server" OnSelectedIndexChanged="dropDown_Group_SelectedIndexChanged">                   
                </asp:DropDownList>
            </div>
            
            <div class="col-md-2 form-group">
                <asp:Label ID="Label7" runat="server" Text="Created User"></asp:Label>
            <asp:TextBox ID="txt_CreatedUser" class="form-control master-form-input" runat="server"></asp:TextBox>
                </div> 
           
            
            </div>
        
        <div class="col-md-12">
           
            <div class="col-md-2 form-group">
                <asp:Button ID="btn_showReport" runat="server" CssClass="btn btn-info btn-group" Text="Show Report" OnClick="btn_showReport_Click" />
            </div>
        </div>
        <hr />
        <asp:ScriptManager runat="server" ID="PropertyStatusScript" ScriptMode="Release"></asp:ScriptManager>
        <div runat="server" visible="false" id="PropertyStatusReport">
            <rsweb:ReportViewer ID="PropertyStatusReportViewer" runat="server" width="100%" height="700px" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
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
