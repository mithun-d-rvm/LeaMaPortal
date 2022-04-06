<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PDCReportView.aspx.cs" Inherits="LeaMaPortal.ReportASPX.PDC.PDCReportView" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="col-md-12 ">

            <div class="col-md-2 form-group">
                 <asp:Label ID="Label9" runat="server" Text="Group"></asp:Label>
                <asp:DropDownList ID="dropDown_Group" AutoPostBack="true" class="form-control master-form-input" OnTextChanged="groupDropdownChange" runat="server" OnSelectedIndexChanged="dropDown_Group_SelectedIndexChanged">
                    <asp:ListItem Selected="True">--Select--</asp:ListItem>
                    <asp:ListItem>Property</asp:ListItem>
                    <asp:ListItem>Region</asp:ListItem>
                    <asp:ListItem>Caretaker</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-3 form-group">
                <asp:Label ID="Label2" runat="server" Text="From"></asp:Label>
            <asp:TextBox ID="txt_fromDate" runat="server" placeholder="From" class="form-control master-form-input" type="date"></asp:TextBox>
            </div>

            <div class="col-md-3 form-group">
                <asp:Label ID="Label3" runat="server" Text="To"></asp:Label>
            <asp:TextBox ID="txt_toDate" runat="server" class="form-control master-form-input" placeholder="To" type="date"></asp:TextBox>
            </div>

            <div class="col-md-3 form-group">
               <asp:Label ID="Label4" runat="server" Text="Filter By"></asp:Label>
            <asp:DropDownList ID="dropDown_FilterBy" AutoPostBack="true" OnTextChanged="filterbyChange" class="form-control master-form-input" runat="server">
                <%--<asp:ListItem Selected="True">--Select--</asp:ListItem>
                <asp:ListItem>Property</asp:ListItem>
                <asp:ListItem>Region</asp:ListItem>
                <asp:ListItem>Caretaker</asp:ListItem>--%>
            </asp:DropDownList>
            </div>

          



            </div>
            <%--<asp:Label ID="Label5" runat="server" Text="Aging Days"></asp:Label>
            <asp:DropDownList ID="dropDown_AgingDays" AutoPostBack="true" OnTextChanged="agingDaysChange" runat="server" Height="19px" Width="95px">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>others</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txt_fromAging" type="number" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txt_toAging" type="number" runat="server" Visible="false"></asp:TextBox>--%>
        <div class="col-md-12">
              <div class="col-md-2 form-group">
                <asp:Label ID="Label1" runat="server" Text="Filterby value"></asp:Label>
            <asp:TextBox ID="txt_filterBy" class="form-control master-form-input" readonly="true" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3 form-group">
                <asp:Label ID="Label6" runat="server" Text="Value Range"></asp:Label>
            <asp:DropDownList ID="dropDown_ValueRange" AutoPostBack="true" class="form-control master-form-input" OnTextChanged="valueRangeChange" runat="server">
                <%--<asp:ListItem Selected="True">--Select--</asp:ListItem>--%>
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>others</asp:ListItem>
            </asp:DropDownList>
            </div>
           <%-- <div class="col-md-3 form-group">
                <asp:Label ID="Label9" runat="server" Text="Country"></asp:Label>
           --%> 
            <%--<asp:DropDownList ID="dropdowncountry" AutoPostBack="true" class="form-control master-form-input"  runat="server" OnSelectedIndexChanged="dropdowncountry_SelectedIndexChanged">--%>
                <%--<asp:ListItem Selected="True">--Select--</asp:ListItem>--%>
                
            <%--</asp:DropDownList>--%>
            <%--</div>--%>
            <%--<div class="col-md-3 form-group">
                 <asp:Label ID="Label10" runat="server" Text="Region"></asp:Label>
            <asp:DropDownList ID="dropdownregion" AutoPostBack="true" class="form-control master-form-input"  runat="server">
            --%>    <%--<asp:ListItem Selected="True">--Select--</asp:ListItem>--%>
              
            <%--</asp:DropDownList>
            </div>--%>
            <div class="col-md-3 form-group">
                <asp:Label ID="Label5" runat="server" Text="Value From"></asp:Label>
            <asp:TextBox ID="txt_fromValue" type="number" class="form-control master-form-input" runat="server" readonly="true"></asp:TextBox>
            </div>
            <div class="col-md-3 form-group">
                <asp:Label ID="Label8" runat="server" Text="Value To"></asp:Label>
            <asp:TextBox ID="txt_toValue" type="number" class="form-control master-form-input" runat="server" readonly="true"></asp:TextBox>
            </div>
            </div>
            
             <div class="col-md-12">
            <div class="col-md-3 form-group">
                <asp:Label ID="Label7" runat="server" Text="Created User"></asp:Label>
            <asp:TextBox ID="txt_CreatedUser" class="form-control master-form-input" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-2 form-group">
                <asp:Button ID="btn_showReport" runat="server" CssClass="btn btn-info btn-group" Text="Show Report" OnClick="btn_showReport_Click" />
            </div>
        </div>


        
    <div>

        <asp:ScriptManager runat="server" ID="PDCReportScript" ScriptMode="Release"></asp:ScriptManager>
        <div runat="server" visible="false" id="PDCReport">
            <rsweb:ReportViewer ID="PDCReportViewer" runat="server" width="100%" height="700px" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            </rsweb:ReportViewer>
        </div>
    </div>
    </form>
</body>
</html>
      