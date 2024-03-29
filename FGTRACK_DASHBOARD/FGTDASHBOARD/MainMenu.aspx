﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainMenu.aspx.cs" Inherits="MainMenu" %>

<%@ Register assembly="DevExpress.Web.v12.2, Version=12.2.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard FG.Track Main Menu</title>
    <script type="text/javascript">
        window.onload = function() {
            if (document.layers)
                document.captureEvents(Event.MOUSEOVER | Event.MOUSEOUT);

            document.onmouseover = HideStatus;
            document.onmouseout = HideStatus;

            //alert(screen.width);

            if (document.all) {
                self.moveTo(0, 0);
                window.resizeTo(screen.width, screen.height - 28);

            }
            else {
                self.moveTo(0, 0);
                self.resizeTo(screen.width, screen.height - 28);

            }

            SetDivHeader();
            SetDivBody();
            SetDivFooter();
            SetButton1();
            SetButton2();
            SetButton3();
            SetButton4();
            Setfont();
            ClockDisplay();
        }
    
        function HideStatus() {
            window.status = '';
            return true;
        }

        function SetDivHeader() {
            var Hheight = 200;
            Hheight = String(((screen.height - 28) * 0.20) + "px");
            var container = document.getElementById('DivHeader');
            container.style.height = Hheight;              
        }

        function SetDivBody() {
            var Bheight = 480;
            Bheight = String(((screen.height - 28) * 0.65) + "px");
            var container = document.getElementById('DivBody');
            container.style.height = Bheight;            
        }

        function SetDivFooter() {
            var Fheight = 120;
            Fheight = String(((screen.height - 28) * 0.15) + "px");
            var container = document.getElementById('DivFooter');
            container.style.height = Fheight;            
        }

        function SetButton1() {
            var height = 200;
            
            if (screen.height > 1500) {
                height = String((200 * (screen.height / 1000)) + "px");
            }
            else {
                height = String((screen.height * 0.214) + "px");
            }
            
            var container = document.getElementById('btnCustomer');
            container.style.height = height;
            container.style.width = height;
        }
        
        function SetButton2() {
            var height = 200;
            
            if (screen.height > 1500) {
                height = String((200 * (screen.height / 1000)) + "px");
            }
            else {
                height = String((screen.height * 0.214) + "px");
            }

            var container = document.getElementById('btnMinmax');
            container.style.height = height;
            container.style.width = height;
        }
        
        function SetButton3() {
            var height = 200;
           
            if (screen.height > 1500) {
                height = String((200 * (screen.height / 1000)) + "px");
            }
            else {
                height = String((screen.height * 0.214) + "px");
            }

            var container = document.getElementById('btnMachine');
            container.style.height = height;
            container.style.width = height;
        }

        function SetButton4() {
            var height = 200;
            
            if (screen.height > 1500) {
                height = String((200 * (screen.height / 1000)) + "px");
            }
            else {
                height = String((screen.height * 0.214) + "px");
            }

            var container = document.getElementById('btnDelivery');
            container.style.height = height;
            container.style.width = height;
        }
        function Setfont() {
            var span = document.getElementById('ASPxLabel2');
            if (screen.height > 800) {                  
                    span.style.fontsize = "100 pt";
            }
            else {
                    span.style.fontsize = "36 pt";
            }
        }
        //---------------- Clock ------------------//
        var timeClock;

        var dg0 = new Image(); dg0.src = "Images/ndg0.gif";
        var dg1 = new Image(); dg1.src = "Images/ndg1.gif";
        var dg2 = new Image(); dg2.src = "Images/ndg2.gif";
        var dg3 = new Image(); dg3.src = "Images/ndg3.gif";
        var dg4 = new Image(); dg4.src = "Images/ndg4.gif";
        var dg5 = new Image(); dg5.src = "Images/ndg5.gif";
        var dg6 = new Image(); dg6.src = "Images/ndg6.gif";
        var dg7 = new Image(); dg7.src = "Images/ndg7.gif";
        var dg8 = new Image(); dg8.src = "Images/ndg8.gif";
        var dg9 = new Image(); dg9.src = "Images/ndg9.gif";
        var dgam = new Image(); dgam.src = "Images/ndgam.gif";
        var dgpm = new Image(); dgpm.src = "Images/ndgpm.gif";
        var dgc = new Image(); dgc.src = "Images/ndgc.gif";

        function ClockDisplay() {
            timeClock = setTimeout('ClockDisplay()', 1000);
            d = new Date();
            
//            hr = d.getHours() + 100;
//            mn = d.getMinutes() + 100;
//            se = d.getSeconds() + 100;

            hr = d.getUTCHours() + 107;
            mn = d.getUTCMinutes() + 100;
            se = d.getUTCSeconds() + 100;

            if (hr >= 124) {
                hr = (hr - 24);
            }  
            
            if (hr == 100) { hr = 112; am_pm = 'am'; }
            else if (hr < 112) { am_pm = 'am'; }
            else if (hr == 112) { am_pm = 'pm'; }
            else if (hr > 112) { am_pm = 'pm'; hr = (hr - 12); }
            tot = '' + hr + mn + se;
            document.hr1.src = 'Images/ndg' + tot.substring(1, 2) + '.gif';
            document.hr2.src = 'Images/ndg' + tot.substring(2, 3) + '.gif';
            document.mn1.src = 'Images/ndg' + tot.substring(4, 5) + '.gif';
            document.mn2.src = 'Images/ndg' + tot.substring(5, 6) + '.gif';
            document.se1.src = 'Images/ndg' + tot.substring(7, 8) + '.gif';
            document.se2.src = 'Images/ndg' + tot.substring(8, 9) + '.gif';
            document.ampm.src = 'Images/ndg' + am_pm + '.gif';
        }

        
        //---------------- Clock ------------------//
    </script>
    <style type="text/css">
        .style1
        {
            width: 109px;
        }
        .style2
        {
            width: 109px;
            height: 114px;
        }
        .style3
        {
            height: 114px;
        }
    </style>
    </head>
<body style="background-color: #02022a">
    <form id="form1" runat="server">
  
    <div id="DivHeader" style="padding-left: 30px;">        
        <table style="width: 100%; height: 100%;">
            <tr>
                <td align="left" valign="bottom">
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="DASHBOARD FG-TRACKING" ForeColor="White" Font-Names="Century Gothic" Font-Size="30 pt">
                    </dx:ASPxLabel>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="right" valign="top" style="padding-top: 10px; padding-right: 10px;">
                    <img src="Images/ndg8.gif" name="hr1">
                    <img src="Images/ndg8.gif" name="hr2">
                    <img src="Images/ndgc.gif" name="c">
                    <img src="Images/ndg8.gif" name="mn1">
                    <img src="Images/ndg8.gif" name="mn2">
                    <img src="Images/ndgc.gif" name="c">
                    <img src="Images/ndg8.gif" name="se1">
                    <img src="Images/ndg8.gif" name="se2">
                    <img src="Images/ndgpm.gif" name="ampm">
                </td>
            </tr>           
        </table>
    </div>
    
    <div id="DivBody" style="padding-left: 30px;">
        <table style="float: left; width: 100%; height: 100%;">
            <tr>
                <td class="style1">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2" valign="top" style="padding-right: 10px; padding-bottom: 10px;">
                    <dx:ASPxButton ID="btnCustomer" runat="server" BackColor="#2777EC" 
                        Cursor="pointer" EnableDefaultAppearance="False" 
                        onclick="btnCustomer_Click">
                        <Image Height="200px" Url="~/Images/Customer.png" Width="200px">
                        </Image>
                        <Border BorderStyle="None" BorderWidth="0px" />
                        <FocusRectPaddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" 
                            PaddingRight="0px" PaddingTop="0px" />
                        <BorderBottom BorderStyle="None" />
                    </dx:ASPxButton>
                    
                </td>
                <td class="style3" valign="top">
                    <dx:ASPxButton ID="btnMinmax" runat="server" BackColor="#59B30B" 
                        Cursor="pointer" EnableDefaultAppearance="False" onclick="btnMinmax_Click">
                        <Image Height="200px" Url="~/Images/MinMax.png" Width="200px">
                        </Image>
                        <Border BorderStyle="None" BorderWidth="0px" />
                        <FocusRectPaddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" 
                            PaddingRight="0px" PaddingTop="0px" />
                        <BorderBottom BorderStyle="None" />
                    </dx:ASPxButton>
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td class="style1" valign="top">
                    <dx:ASPxButton ID="btnMachine" runat="server" BackColor="#613CBC" 
                        Cursor="pointer" EnableDefaultAppearance="False" 
                        onclick="btnMachine_Click">
                        <Image Height="200px" Url="~/Images/Machine.png" Width="200px">
                        </Image>
                        <Border BorderStyle="None" BorderWidth="0px" />
                        <FocusRectPaddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" 
                            PaddingRight="0px" PaddingTop="0px" />
                        <BorderBottom BorderStyle="None" />
                    </dx:ASPxButton>
                </td>
                <td valign="top">
                    <dx:ASPxButton ID="btnDelivery" runat="server" BackColor="#B80F3B" 
                        Cursor="pointer" EnableDefaultAppearance="False" 
                        onclick="btnDelivery_Click">
                        <Image Height="200px" Url="~/Images/Delivery.png" Width="200px">
                        </Image>
                        <Border BorderStyle="None" BorderWidth="0px" />
                        <FocusRectPaddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" 
                            PaddingRight="0px" PaddingTop="0px" />
                        <BorderBottom BorderStyle="None" />
                    </dx:ASPxButton>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    
    <div id="DivFooter" >
        <table style="width:100%; height: 100%;">
            <tr>
                <td>
                </td>                
                <td>
                    &nbsp;</td>
                <td align="right" style="padding-right: 30px" valign="middle">                   
                    <dx:ASPxButton ID="btnLogout" runat="server" Width="70px" BackColor="#02022A" 
                        Cursor="pointer" EnableDefaultAppearance="False" EnableTheming="False" 
                        EnableViewState="False" ClientSideEvents-Click="function(s, e) {window.close();}">                       
                        <HoverStyle BackColor="#02022A">
                        </HoverStyle>
                        <Image Height="70px" Url="~/Images/Logout.png" Width="70px">
                        </Image>
                        <Border BorderStyle="None" BorderWidth="0px" BorderColor="#02022A" />
                        <FocusRectPaddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" 
                            PaddingRight="0px" PaddingTop="0px" />                      
                        <FocusRectBorder BorderColor="#02022A" BorderStyle="None" BorderWidth="0px" />
                    </dx:ASPxButton>
                </td>
            </tr>            
        </table>
    </div>
    
    </form>
</body>
</html>
