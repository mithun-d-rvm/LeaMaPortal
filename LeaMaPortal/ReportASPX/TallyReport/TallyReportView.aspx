<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TallyReportView.aspx.cs" Inherits="LeaMaPortal.ReportASPX.TallyReport.TallyReportView" %>

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
            <div >
                                
                <table>
                    <tr>
                        <td class="auto-style1">
                             <asp:Label ID="Label1" runat="server" Text="Report Name"></asp:Label>

                        </td>
                        &nbsp
                        <td class="auto-style1">
                             &nbsp;&nbsp; :&nbsp;&nbsp;
                             <asp:DropDownList ID="dropDown_Group" AutoPostBack="true" OnTextChanged="groupDropdownChange"  runat="server" Height="29px" Width="178px" >
                <asp:ListItem Selected="True">--Select--</asp:ListItem>
                <asp:ListItem >Customer</asp:ListItem>
                <asp:ListItem>Invoice</asp:ListItem>
                <asp:ListItem>Receipt</asp:ListItem>
                <asp:ListItem>Payment</asp:ListItem>
            </asp:DropDownList>
                        </td>
                          
                        <td class="auto-style1">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="Label2" runat="server" Text="From Date"></asp:Label>
                            &nbsp;:&nbsp;&nbsp;&nbsp;
                        </td>
                        
                    
                        <td class="auto-style1">
                          &nbsp;<asp:TextBox ID="txt_fromDate" runat="server"  placeholder="From" type="date" Width="163px"></asp:TextBox>
                        </td>
                        <td>
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Text="To Date"></asp:Label>
                        &nbsp;:</td>
                        <td class="auto-style1">
                             &nbsp;&nbsp;<asp:TextBox ID="txt_toDate" runat="server"  placeholder="To" type="date" Width="161px"></asp:TextBox>
                        </td>
                        <td class="auto-style1">
                            &nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="btn_showReport" runat="server" CssClass="btn btn-info btn-group" Text="Show Report" OnClick="btn_showReport_Click" />
                        </td>
                        <td class="auto-style1">
                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btn_loadReport" runat="server" CssClass="btn btn-info btn-group" Text="Load Report" Visible ="false"  OnClick="btn_loadReport_Click"  />
                        </td>
                    </tr>
                
                           <%-- <td class="auto-style1" >
&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="countryname" Text ="Country" runat ="server" ></asp:Label>
                             :<asp:DropDownList ID="dropdowncountry" AutoPostBack="true"   runat="server" Height="26px" Width="155px" OnSelectedIndexChanged="dropdowncountry_SelectedIndexChanged">
                
            </asp:DropDownList>
                        </td>--%>
                       <%-- <td class="auto-style1" >
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                Region :<asp:DropDownList ID="dropdownregion" AutoPostBack="true"   runat="server" Height="30px" Width="142px">
                
            </asp:DropDownList>
                        </td>
                        </tr>--%>
                    </table>
                <br />
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                   
                                  
                <br />
            </div>

            <div >
<asp:GridView ID ="gvtally" runat ="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="673px" >
    <AlternatingRowStyle BackColor="White" />
    <EditRowStyle BackColor="#7C6F57" />
    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#E3EAEB" />
    <SelectedRowStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
    <SortedAscendingCellStyle BackColor="#F8FAFA" />
    <SortedAscendingHeaderStyle BackColor="#246B61" />
    <SortedDescendingCellStyle BackColor="#D4DFE1" />
    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
           
            </div>
            
            
        </div>

        <div runat="server" visible="false" id="TallyReport">
        </div>
           
    </form>
</body>
</html>
