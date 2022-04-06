<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetRegions.aspx.cs" Inherits="LeaMaPortal.GetRegions" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <form id="form1" runat="server">
                                                    
            <div>
                <table>                                    
                    <tr>                        
                        <td class="auto-style1">

               <asp:DropDownList ID="dropdownregion" AutoPostBack="true"   runat="server" Height="30px" Width="142px" OnSelectedIndexChanged="dropdownregion_SelectedIndexChanged">
                
            </asp:DropDownList>
                        </td>                    
                    </tr>                  
                    </table>
         
            </div>
            
     
          
               
       
    </form>
</body>
</html>
