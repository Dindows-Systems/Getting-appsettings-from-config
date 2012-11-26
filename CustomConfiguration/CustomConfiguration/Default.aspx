<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>
            Custom Configuration Settings Demo</h2>
        <h3>
            ASP.NET 1.x-Style Configuration Setting Values:</h3>
        <p>
            <asp:Label ID="aspnet1Values" runat="server"></asp:Label>&nbsp;</p>
        <p>
            <asp:Button ID="RefreshButton" runat="server" Text="Refresh" />&nbsp;</p>
    
    </div>
    </form>
</body>
</html>
