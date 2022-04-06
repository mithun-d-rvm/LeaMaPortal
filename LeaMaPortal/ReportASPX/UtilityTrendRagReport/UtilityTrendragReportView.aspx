<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UtilityTrendragReportView.aspx.cs" Inherits="LeaMaPortal.ReportASPX.UtilityTrendRagReport.UtilityTrendragReportView" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Content/bootstrap.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            height: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
                <div class="col-md-12">
                    <br />

                    <div>
                       
                      
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       
                      
                            <asp:Label ID="Label1" runat="server" Text="Report Name"></asp:Label>
                            &nbsp;&nbsp;:
            <asp:DropDownList ID="dropDown_Group" AutoPostBack="true"   runat="server" Height="29px" Width="444px" OnSelectedIndexChanged="dropDown_Group_SelectedIndexChanged" >
                <asp:ListItem Selected="True">--Select--</asp:ListItem>
                <asp:ListItem>Monthly Trend Report</asp:ListItem>
                <asp:ListItem>Yearly Trend Report</asp:ListItem>
                <asp:ListItem>Property wise Summary Report</asp:ListItem>
                <asp:ListItem >RAG Report</asp:ListItem>
                <asp:ListItem >Utility Payments RAG property Report</asp:ListItem>
            </asp:DropDownList>
                        
                    </div>
                    <br />
                    <div>
                     
                    </div>
            <div >

                <table>
                    
                    <tr><td></td></tr>
                    <tr>
                        <td class="auto-style1">
&nbsp;&nbsp;
                            <asp:Label ID ="Utility" Text ="Utility Name" runat ="server" ></asp:Label>
               :<asp:DropDownList ID="dropdown_utility" AutoPostBack="true"   runat="server" Height="30px" Width="148px">
                
            </asp:DropDownList>
                        </td>
                        <td class="auto-style1">
&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="countryname" Text ="Country" runat ="server" ></asp:Label>
                             :<asp:DropDownList ID="dropdowncountry" AutoPostBack="true"   runat="server" Height="26px" Width="155px" OnSelectedIndexChanged="dropdowncountry_SelectedIndexChanged">
                
            </asp:DropDownList>
                        </td>
                        <td class="auto-style1">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                Region :<asp:DropDownList ID="dropdownregion" AutoPostBack="true"   runat="server" Height="30px" Width="142px">
                
            </asp:DropDownList>
                        </td>
                        <td class="auto-style1">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID ="property" runat ="server" Text ="Property"></asp:Label>
                 :<asp:DropDownList ID="dropdownproperty" AutoPostBack="true"   runat="server" Height="30px" Width="142px">
                
            </asp:DropDownList>
                        </td>
                      <td class="auto-style1">

                      </td>
                    </tr>
                    <tr>
<td></td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    </table>
                <br />
                <table >
                      <tr>
                        <td>
                             &nbsp;&nbsp;
                                <asp:Label ID="Label2" runat="server" Text="From Date"></asp:Label>
                            &nbsp;&nbsp;:&nbsp;
            <asp:TextBox ID="txt_fromDate" runat="server"  placeholder="From" type="date" Width="144px"></asp:TextBox>
            
                        </td>
                        <td>
                             &nbsp;&nbsp;
                            <asp:Label ID="Label3" runat="server" Text="To Date"></asp:Label>
                            &nbsp
            <asp:TextBox ID="txt_toDate" runat="server"  placeholder="To" type="date" Width="144px"></asp:TextBox>
                        </td>
                         
                           <td>
                                 &nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btn_showReport" runat="server" CssClass="btn btn-info btn-group pull-right" Text="Show Report" OnClick="btn_showReport_Click1"  />
                        </td>
                    </tr>
                <tr>
                    <td></td>
                </tr>
                    <tr>
                       
                    </tr>
                </table>
                  
                
            </div>
            
     
            <div class="col-md-2 form-group">
                <asp:Label ID="Label7" runat="server" Text="Created User" Visible ="false" ></asp:Label>
            <asp:TextBox ID="txt_CreatedUser"  runat="server" Visible ="false" ></asp:TextBox>
            </div>
                  </div>
        <div class="col-md-12 form-group">
            
        </div>
    <div>

        <asp:ScriptManager runat="server" ID="UtilityTrendRagReportScript" ScriptMode="Release"></asp:ScriptManager>
        <div runat="server" visible="true" id="UtilityTrenddiv">
            <rsweb:ReportViewer ID="UtilityTrendRagReportViewer" runat="server" width="100%" height="700px" AsyncRendering="False" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            </rsweb:ReportViewer>
        </div>
    </div>
    </form>
</body>
</html>
