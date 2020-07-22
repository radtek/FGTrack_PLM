<%@ page language="C#" autoeventwireup="true" inherits="frmSTKMinmax, App_Web_-8b7cqw1" %>

<%@ Register assembly="DevExpress.Web.v12.2, Version=12.2.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stock By Min & Max</title>
    
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

             ClockDisplay();

             SetDivHeader();
             SetDivBody();
             //SetDivFooter()
             SetDivGridview();
             // ClearGridCookies()
             ReadConfigFile();
         }

         window.onunload = function() {
             KillTimer();
         }
    
        function HideStatus() {
            window.status = '';
            return true;
        }

        function SetDivHeader() {
            var Hheight = 200;
            Hheight = String(((screen.height - 28) * 0.08) + "px");
            var container = document.getElementById('DivHeader');
            container.style.height = Hheight;
        }

        function SetDivBody() {
            var Bheight = 480;
            Bheight = String(((screen.height - 28) * 0.87) + "px");
            var container = document.getElementById('DivBody');
            container.style.height = Bheight;
        }

        function SetDivGridview() {
            var Bheight = 480;
            Bheight = String(((screen.height - 28) * 0.85) + "px");
            var container = document.getElementById('DivGridview');
            container.style.height = Bheight;
        }

        function ClearGridCookies() {
            _aspxDelCookie('grvSTK_Minmax');
            window.location.reload();
        }

        //---------------- Setting Auto Refresh Page ----------------//

        var isauto = true;
        var timeout;
        var timerefresh = 1000;

        function scheduleGridUpdate(grid) {
            if (isauto) {
                window.clearTimeout(timeout);
                timeout = window.setTimeout(
                function() { grid.PerformCallback(); },
                timerefresh
                );
            }

        }

        function grid_Init(s, e) {
            scheduleGridUpdate(s);
        }

        function grid_BeginCallback(s, e) {
            window.clearTimeout(timeout);
        }
        function grid_EndCallback(s, e) {
            scheduleGridUpdate(s);
        }

        //---------------- Setting Auto Refresh Page ----------------//
        //---------------- Clock ------------------//
        var timeClock;

        var dg0 = new Image(); dg0.src = "Images/dg0.gif";
        var dg1 = new Image(); dg1.src = "Images/dg1.gif";
        var dg2 = new Image(); dg2.src = "Images/dg2.gif";
        var dg3 = new Image(); dg3.src = "Images/dg3.gif";
        var dg4 = new Image(); dg4.src = "Images/dg4.gif";
        var dg5 = new Image(); dg5.src = "Images/dg5.gif";
        var dg6 = new Image(); dg6.src = "Images/dg6.gif";
        var dg7 = new Image(); dg7.src = "Images/dg7.gif";
        var dg8 = new Image(); dg8.src = "Images/dg8.gif";
        var dg9 = new Image(); dg9.src = "Images/dg9.gif";
        var dgam = new Image(); dgam.src = "Images/dgam.gif";
        var dgpm = new Image(); dgpm.src = "Images/dgpm.gif";
        var dgc = new Image(); dgc.src = "Images/dgc.gif";

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
            document.hr1.src = 'Images/dg' + tot.substring(1, 2) + '.gif';
            document.hr2.src = 'Images/dg' + tot.substring(2, 3) + '.gif';
            document.mn1.src = 'Images/dg' + tot.substring(4, 5) + '.gif';
            document.mn2.src = 'Images/dg' + tot.substring(5, 6) + '.gif';
            document.se1.src = 'Images/dg' + tot.substring(7, 8) + '.gif';
            document.se2.src = 'Images/dg' + tot.substring(8, 9) + '.gif';
            document.ampm.src = 'Images/dg' + am_pm + '.gif';
        }
        //---------------- Clock ------------------//

        function KillTimer() {
            if (timeClock) {
                clearTimeout(timeClock);
                timeClock = null;
            }

            if (timeout) {
                clearTimeout(timeout);
                timeout = null;
            }

        }

        
        
    </script>
    
    <style type="text/css">
        .text_overflow
        {
            white-space:nowrap;
            overflow:hidden;
            text-overflow:ellipsis;            
        }
        .Emptystyle td {
            padding: 4px 20px!important;
            height:13px;          
        }
        .RowStyle {
            height: 50px;
        }
        .AlternateRowStyle {
            height: 50px;
        }        
         .HeaderStyle 
        {
            height: 50px;
        }
        
        .dxpControl 
        {
            margin-left: auto;
            margin-right: auto;
        }
        .style1
        {
            width: 90px;
        }      
        .style2
        {
            width: 90px;
        }      
        .style3
        {
            width: 160px;
        }
        .style4
        {
            width: 72px;
        }
        .style7
        {
            width: 150px;
        }   
        .action_button
        {
           font-family: Century Gothic;
           font-size:11pt;
           color:#FFFFFF;
           padding-left:1px;
        } 
        
        .action_button_active
        {
           font-family: Century Gothic;
           font-size:11pt;
           color:#0C88FF;
           padding-left:1px;
        } 
        .label
        {
           font-family: Century Gothic;
           font-size:11pt;
           color:#FFFFFF;
           padding-left:1px;
        }
        .style5
        {
            width: 111px;
        }
        .style6
        {
            width: 131px;
        }
          
    </style>
    
</head>
<body style="background-color: #02022a">
    <script type="text/javascript">
        function ReadConfigFile() {
            timerefresh = '<%=ConfigurationManager.AppSettings["TimeRefresh"] %>';
        }
        //---------------- Control Gridview -----------------//

        function btnTimer_Click(s, e) {

            var el = s.GetMainElement();
            var img = el.getElementsByTagName('img');

            var fname = img[0].src.split('/').pop()

            if (fname == 'Icon_Timer.png') {
                isauto = false;
                window.clearTimeout(timeout);
                img[0].src = '<%=Page.ResolveClientUrl("~/Images/Icon_Timer2.png")%>';

                document.getElementById("btnTimer").className = 'action_button_active';

            } else {
                isauto = true;
                scheduleGridUpdate(grid);
                img[0].src = '<%=Page.ResolveClientUrl("~/Images/Icon_Timer.png")%>';

                document.getElementById("btnTimer").className = 'action_button';

            }

        }

        function btnCustom_Click(s, e) {
            var el = s.GetMainElement();
            var img = el.getElementsByTagName('img');

            if (grid.IsCustomizationWindowVisible()) {
                grid.HideCustomizationWindow();
                img[0].src = '<%=Page.ResolveClientUrl("~/Images/Icon_Custom.png")%>';

                document.getElementById("btnCustom").className = 'action_button';
            }
            else {
                grid.ShowCustomizationWindow();
                img[0].src = '<%=Page.ResolveClientUrl("~/Images/Icon_Custom2.png")%>';

                document.getElementById("btnCustom").className = 'action_button_active';
            }
        }

        function grid_CustomizationWindowCloseUp(s, e) {
            var el = btnCustom.GetMainElement();
            var img = el.getElementsByTagName('img');
            img[0].src = '<%=Page.ResolveClientUrl("~/Images/Icon_Custom.png")%>';

            document.getElementById("btnCustom").className = 'action_button';
        }

        function ClearGridCookies() {
            _aspxDelCookie('grvSTK_Minmax');
            window.location.reload();
        }
        
    </script>
    <form id="form1" runat="server">
     <div id="DivHeader" style="padding-left: 10px;">        
        <table style="width: 100%; height: 100%;">
            <tr>
                <td valign="middle" class="style4">
                    <dx:ASPxButton ID="btnBack" runat="server" Cursor="pointer" 
                        EnableDefaultAppearance="False" Height="40px" 
                        Width="40px" onclick="btnBack_Click">
                        <Image Height="40px" Url="~/Images/Back.png" Width="40px">
                        </Image>
                        <FocusRectPaddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" 
                            PaddingRight="0px" PaddingTop="0px" />
                    </dx:ASPxButton>
                </td>
                <td valign="middle">
                    <img src="Images/dg8.gif" name="hr1">
                    <img src="Images/dg8.gif" name="hr2">
                    <img src="Images/dgc.gif" name="c">
                    <img src="Images/dg8.gif" name="mn1">
                    <img src="Images/dg8.gif" name="mn2">
                    <img src="Images/dgc.gif" name="c">
                    <img src="Images/dg8.gif" name="se1">
                    <img src="Images/dg8.gif" name="se2">
                    <img src="Images/dgpm.gif" name="ampm">
                </td>
                <td width="55px" valign="middle">
                    <dx:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="Images/MinMax.png" Height="40px" Width="40px">
                    </dx:ASPxImage>
                </td>
                <td width="275px" valign="middle">
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="STOCK BY MIN & MAX" ForeColor="White" Font-Names="Century Gothic" Font-Size="18 pt">
                    </dx:ASPxLabel>
                </td>
            </tr>           
        </table>
    </div>
    
    <div id="DivBody">
          <asp:ScriptManager ID="ScriptManager1" runat="server">
          </asp:ScriptManager>
          
          <asp:Timer ID="Timer1" runat="server" Enabled="False" Interval="10000" 
              ontick="Timer1_Tick">
          </asp:Timer>
          
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            
                <div id="DivControl" style="width: 100%; height: 40px;">
                    <table style="width: 100%; height: 38px;">
                        <tr>
                            <td class="style5" align="center" valign="middle">
                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="STATUS DELAY :" CssClass="label">
                                </dx:ASPxLabel>
                            </td>
                            <td align="left" valign="middle" class="style6">
                                <dx:ASPxCheckBoxList ID="cblstatus" runat="server" ClientInstanceName="cblstatus" 
                                    RepeatDirection="Horizontal" CssClass="label" AutoPostBack="True" 
                                    onselectedindexchanged="cblstatus_SelectedIndexChanged" 
                                    SelectedIndex="0" BackColor="#02022A">
                                    <Border BorderColor="#02022A" BorderStyle="None" />
                                    <Items>
                                        <dx:ListEditItem Text="YES" Value="Y" Selected="True" />
                                        <dx:ListEditItem Text="NO" Value="N" Selected="True" />
                                        <dx:ListEditItem Text="EXPECTED" Value="E" Selected="True" />
                                    </Items>
                                </dx:ASPxCheckBoxList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td valign="middle" class="style7">
                                <dx:ASPxButton ID="btnUpdate" runat="server" Cursor="pointer" 
                                 EnableDefaultAppearance="False" Height="30px" Width="140px" AutoPostBack="False"
                                 CssClass="action_button" Text="Refresh Data" onclick="btnUpdate_Click">
                                <Image Height="30px" Url="~/Images/Icon_Update.png" Width="30px">
                                </Image>
                                <FocusRectPaddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" 
                                 PaddingRight="0px" PaddingTop="0px" />
                                </dx:ASPxButton>
                            </td>
                            <td valign="middle" class="style1">
                                <dx:ASPxButton ID="btnReset" runat="server" Cursor="pointer" 
                                    EnableDefaultAppearance="False" Height="30px" Width="75px" AutoPostBack="False"
                                    CssClass="action_button" Text="Reset">
                                    <ClientSideEvents Click="ClearGridCookies" />
                                    <Image Height="30px" Url="~/Images/Icon_Reset.png" Width="30px">
                                    </Image>
                                    <FocusRectPaddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" 
                                        PaddingRight="0px" PaddingTop="0px" />
                                </dx:ASPxButton>
                                
                            </td>                            
                            <td valign="middle" class="style2">
                                <dx:ASPxButton ID="btnTimer" runat="server" Cursor="pointer" 
                                    EnableDefaultAppearance="False" Height="30px" Width="75px" 
                                    AutoPostBack="False" ClientInstanceName="btnTimer"
                                    CssClass="action_button" Text="Timer">
                                    <ClientSideEvents Click="btnTimer_Click" />
                                    <Image Height="30px" Url="~/Images/Icon_Timer.png" Width="30px">
                                    </Image>
                                    <FocusRectPaddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" 
                                        PaddingRight="0px" PaddingTop="0px" />
                                </dx:ASPxButton>
                            </td>                            
                            <td valign="middle" class="style3">
                                <dx:ASPxButton ID="btnCustom" runat="server" Cursor="pointer" 
                                    EnableDefaultAppearance="False" Height="30px" Width="155px" 
                                    AutoPostBack="False" ClientInstanceName="btnCustom" CssClass="action_button" 
                                    Text="Custom Column">
                                    <ClientSideEvents Click="btnCustom_Click" />
                                    <Image Height="30px" Url="~/Images/Icon_Custom.png" Width="30px">
                                    </Image>
                                    <FocusRectPaddings Padding="0px" PaddingBottom="0px" PaddingLeft="0px" 
                                        PaddingRight="0px" PaddingTop="0px" />
                                </dx:ASPxButton>
                            </td>                            
                        </tr>                        
                    </table>
                </div>
            
                <div id="DivGridview">
                    
                    <dx:ASPxGridView ID="grvSTK_Minmax" runat="server" AutoGenerateColumns="False" 
                            ClientInstanceName="grid" KeyFieldName="WH_ID" Width="100%" 
                        onhtmldatacellprepared="grvSTK_Minmax_HtmlDataCellPrepared" 
                        onafterperformcallback="grvSTK_Minmax_AfterPerformCallback">
            
                    <Styles>
                            <Header Font-Bold="True" HorizontalAlign="Center" Font-Size="13pt" ForeColor="#121EBC" Wrap="True" CssClass="HeaderStyle"></Header>
                            <Row BackColor="#02022A" CssClass="RowStyle"></Row>                               
                            <AlternatingRow  Enabled="True" BackColor="#03033F" CssClass="AlternateRowStyle"></AlternatingRow>
                            <GroupRow HorizontalAlign="Left" Font-Bold="true" BackColor="#fefece"></GroupRow>
                            <EmptyDataRow CssClass="Emptystyle" Font-Bold="True" ForeColor="Black" 
                                HorizontalAlign="Left" VerticalAlign="Middle">
                            </EmptyDataRow>
                            <GroupPanel HorizontalAlign="Left" Font-Bold="true"></GroupPanel>
                    </Styles>
                        
                    <Columns>
                        
                        <dx:GridViewDataDateColumn Caption="CUSTOMER ID" FieldName="PARTY_ID" Name="PARTY_ID" 
                         VisibleIndex="0" Width="6%" Visible="false" 
                         CellStyle-ForeColor="White" CellStyle-HorizontalAlign="Left" CellStyle-VerticalAlign="Middle" CellStyle-Wrap="False" CellStyle-Font-Size="18pt" CellStyle-Font-Bold="False">
                        <CellStyle CssClass="text_overflow"/>
                        </dx:GridViewDataDateColumn>
                        
                        <dx:GridViewDataTextColumn Caption="CUSTOMER" FieldName="PARTY_NAME" Name="PARTY_NAME" 
                         VisibleIndex="1" Width="11%"
                         CellStyle-ForeColor="White" CellStyle-HorizontalAlign="Left" CellStyle-VerticalAlign="Middle" CellStyle-Wrap="False" CellStyle-Font-Size="18pt" CellStyle-Font-Bold="False">
                        <CellStyle CssClass="text_overflow"/>
                        </dx:GridViewDataTextColumn>
                        
                        <dx:GridViewDataTextColumn Caption="PRODUCT TYPE CODE" FieldName="PRODUCT_TYPE_ID" Name="PRODUCT_TYPE_ID" 
                         VisibleIndex="2" Width="8%" Visible="false"
                         CellStyle-ForeColor="White" CellStyle-HorizontalAlign="Left" CellStyle-VerticalAlign="Middle" CellStyle-Wrap="False" CellStyle-Font-Size="18pt" CellStyle-Font-Bold="False">
                        <CellStyle CssClass="text_overflow"/>
                        </dx:GridViewDataTextColumn>
                        
                        <dx:GridViewDataTextColumn Caption="PRODUCT TYPE" FieldName="PRODUCT_TYPE_NAME" Name="PRODUCT_TYPE_NAME" 
                         VisibleIndex="3" Width="7%"
                         CellStyle-ForeColor="White" CellStyle-HorizontalAlign="Center" CellStyle-VerticalAlign="Middle" CellStyle-Wrap="False" CellStyle-Font-Size="18pt" CellStyle-Font-Bold="False">                        
                        <CellStyle CssClass="text_overflow"/>
                        </dx:GridViewDataTextColumn>
                        
                        <dx:GridViewDataTextColumn Caption="W/H" FieldName="WH_ID" Name="WH_ID" 
                         VisibleIndex="4" Width="4%"
                         CellStyle-ForeColor="White" CellStyle-HorizontalAlign="Center" CellStyle-VerticalAlign="Middle" CellStyle-Wrap="False" CellStyle-Font-Size="18pt" CellStyle-Font-Bold="False">
                        <CellStyle CssClass="text_overflow"/>
                        </dx:GridViewDataTextColumn>                       
          
                        <dx:GridViewDataTextColumn Caption="PRODUCT#" FieldName="PRODUCT_NO" Name="PRODUCT_NO" 
                         VisibleIndex="5" Width="18%"
                         CellStyle-ForeColor="White" CellStyle-HorizontalAlign="Left" CellStyle-VerticalAlign="Middle" CellStyle-Wrap="False" CellStyle-Font-Size="18pt" CellStyle-Font-Bold="False">
                        <CellStyle CssClass="text_overflow"/>
                        </dx:GridViewDataTextColumn>
                        
                         <dx:GridViewDataTextColumn Caption="PRODUCT NO" FieldName="PRODUCT_SEQ_NO" Name="PRODUCT_SEQ_NO" 
                         VisibleIndex="6" Width="10%" Visible="false"
                         CellStyle-ForeColor="White" CellStyle-HorizontalAlign="Left" CellStyle-VerticalAlign="Middle" CellStyle-Wrap="False" CellStyle-Font-Size="18pt" CellStyle-Font-Bold="False">
                        <CellStyle CssClass="text_overflow"/>
                        </dx:GridViewDataTextColumn>
                        
                        <dx:GridViewDataTextColumn Caption="PRODUCT NAME" FieldName="PRODUCT_NAME" Name="PRODUCT_NAME" 
                         VisibleIndex="7" Width="17%" Visible="false"
                         CellStyle-ForeColor="White" CellStyle-HorizontalAlign="Left" CellStyle-VerticalAlign="Middle" CellStyle-Wrap="False" CellStyle-Font-Size="18pt" CellStyle-Font-Bold="False">
                        <CellStyle CssClass="text_overflow"/>
                        </dx:GridViewDataTextColumn> 
                        
                        <dx:GridViewDataTextColumn Caption="FORECAST" FieldName="FORECAST" Name="FORECAST" 
                         VisibleIndex="8" Width="8%"
                         CellStyle-ForeColor="White" CellStyle-HorizontalAlign="Right" CellStyle-VerticalAlign="Middle" CellStyle-Wrap="False" CellStyle-Font-Size="18pt" CellStyle-Font-Bold="False">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0}">
                        </PropertiesTextEdit>
                        <CellStyle CssClass="text_overflow"/>
                        </dx:GridViewDataTextColumn>                       
   
                        <dx:GridViewDataTextColumn Caption="QTY/BOX (PCS)" FieldName="BOX_QTY" Name="BOX_QTY" 
                         VisibleIndex="9" Width="7%"
                         CellStyle-ForeColor="White" CellStyle-HorizontalAlign="Right" CellStyle-VerticalAlign="Middle" CellStyle-Wrap="False" CellStyle-Font-Size="18pt" CellStyle-Font-Bold="true">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0}">
                        </PropertiesTextEdit>
                        <CellStyle CssClass="text_overflow"/>
                        </dx:GridViewDataTextColumn>
                        
                        <dx:GridViewDataTextColumn Caption="MIN(BOX)" FieldName="MIN_BOX" Name="MIN_BOX" 
                         VisibleIndex="10" Width="5%"
                         CellStyle-ForeColor="White" CellStyle-HorizontalAlign="Right" CellStyle-VerticalAlign="Middle" CellStyle-Wrap="False" CellStyle-Font-Size="18pt" CellStyle-Font-Bold="true">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0}">
                        </PropertiesTextEdit>
                        <CellStyle CssClass="text_overflow"/>
                        </dx:GridViewDataTextColumn>
                            
                        <dx:GridViewDataTextColumn Caption="MAX(BOX)" FieldName="MAX_BOX" Name="MAX_BOX" 
                         VisibleIndex="11" Width="5%"
                         CellStyle-ForeColor="White" CellStyle-HorizontalAlign="Right" CellStyle-VerticalAlign="Middle" CellStyle-Wrap="False" CellStyle-Font-Size="18pt" CellStyle-Font-Bold="true">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0}">
                        </PropertiesTextEdit>
                        <CellStyle CssClass="text_overflow"/>
                        </dx:GridViewDataTextColumn> 
                        
                        <dx:GridViewDataTextColumn Caption="STOCK AS ON (BOX)" FieldName="STOCK_BOX" Name="STOCK_BOX" 
                         VisibleIndex="12" Width="8%"
                         CellStyle-ForeColor="White" CellStyle-HorizontalAlign="Right" CellStyle-VerticalAlign="Middle" CellStyle-Wrap="False" CellStyle-Font-Size="18pt" CellStyle-Font-Bold="true">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0}">
                        </PropertiesTextEdit>
                        <CellStyle CssClass="text_overflow"/>
                        </dx:GridViewDataTextColumn> 

                        <dx:GridViewDataTextColumn Caption="STOCK AS ON (PCS)" FieldName="STOCK_PCS" Name="STOCK_PCS" 
                         VisibleIndex="13" Width="8%"
                         CellStyle-ForeColor="White" CellStyle-HorizontalAlign="Right" CellStyle-VerticalAlign="Middle" CellStyle-Wrap="False" CellStyle-Font-Size="18pt" CellStyle-Font-Bold="true">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0}">
                        </PropertiesTextEdit>
                        <CellStyle CssClass="text_overflow"/>
                        </dx:GridViewDataTextColumn>  
                        
                        <dx:GridViewDataTextColumn Caption="STATUS RUNNING M/C" FieldName="STATUS_RUNNING_MC" Name="STATUS_RUNNING_MC" 
                         VisibleIndex="14" Width="7%"
                         CellStyle-ForeColor="White" CellStyle-HorizontalAlign="Center" CellStyle-VerticalAlign="Middle" CellStyle-Wrap="False" CellStyle-Font-Size="18pt" CellStyle-Font-Bold="false">
                        <CellStyle CssClass="text_overflow"/>
                        </dx:GridViewDataTextColumn>
                        
                        <dx:GridViewDataTextColumn Caption="DELAY(PCS)" FieldName="PICK_PENDING" Name="PICK_PENDING" 
                         VisibleIndex="15" Width="5%"
                         CellStyle-ForeColor="White" CellStyle-HorizontalAlign="Right" CellStyle-VerticalAlign="Middle" CellStyle-Wrap="False" CellStyle-Font-Size="18pt" CellStyle-Font-Bold="true">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0}">
                        </PropertiesTextEdit>
                        <CellStyle CssClass="text_overflow"/>
                        </dx:GridViewDataTextColumn> 
                        
                        <dx:GridViewDataTextColumn Caption="EXPECTED DELAY  (3days)" FieldName="EXPECTED_DELAY" Name="EXPECTED_DELAY" 
                         VisibleIndex="16" Width="9%"
                         CellStyle-ForeColor="Yellow" CellStyle-HorizontalAlign="Right" CellStyle-VerticalAlign="Middle" CellStyle-Wrap="False" CellStyle-Font-Size="18pt" CellStyle-Font-Bold="true">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0}">
                        </PropertiesTextEdit>
                        <CellStyle CssClass="text_overflow"/>
                        </dx:GridViewDataTextColumn>                      

                        <dx:GridViewDataTextColumn Caption="STATUS" FieldName="STATUS" Name="STATUS" 
                         VisibleIndex="17" Width="6%" Visible="false"
                         CellStyle-ForeColor="White" CellStyle-HorizontalAlign="Center" CellStyle-VerticalAlign="Middle" CellStyle-Wrap="False" CellStyle-Font-Size="18pt" CellStyle-Font-Bold="true">
                        <CellStyle CssClass="text_overflow"/>
                        </dx:GridViewDataTextColumn>                    

                   </Columns>
                   
                   <Settings ShowGroupPanel="False"
                             GridLines="Horizontal" 
                             ShowGroupButtons="False" 
                             ShowHorizontalScrollBar="False"   
                             ShowHeaderFilterButton="true" 
                             ShowFilterBar="Auto" />
                             
                   <ClientSideEvents Init="grid_Init" BeginCallback="grid_BeginCallback" EndCallback="grid_EndCallback" CustomizationWindowCloseUp="grid_CustomizationWindowCloseUp" />                             
                   <SettingsPager PageSize="10" ShowEmptyDataRows="True" AlwaysShowPager="true" />
                   <SettingsBehavior AllowSelectByRowClick="True" ColumnResizeMode = "NextColumn" />  
                   <SettingsLoadingPanel Mode="Disabled" />  
                   <SettingsCustomizationWindow Enabled="True"/>  
                   <Images HeaderActiveFilter-Url ="Images/Filter_Orange.png" HeaderFilter-Url ="Images/Filter_Gray.png"></Images> 
                   
                   <SettingsCookies Enabled="true" 
                        StoreColumnsVisiblePosition="true" 
                        StoreColumnsWidth="true"
                        StoreFiltering="true" 
                        StoreGroupingAndSorting="true"
                        StorePaging="false" 
                        CookiesID="grvSTK_Minmax" />
                        
                   </dx:ASPxGridView>
                     
                </div> 
                       
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            </Triggers>
          </asp:UpdatePanel>   
        
    </div>
    </form>
</body>
</html>
