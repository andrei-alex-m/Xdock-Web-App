<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="booking_mockUp._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Booking </title>
    <link rel="shortcut icon" type="image/x-icon" href="imagini/favico.ico" />
    <script type="text/javascript">
        window.onerror = function (msg, url, line) {
            alert("S-a produs o eroare: \n" + msg + '\n' + url + '\n' + line);
            return false;
        }
    </script>
    <style type="text/css">
        .labels {
            display: block;
            float: left;
            margin-right: 5px;
            margin-left: 22px;
        }


        /* Locks the left column */
        td.locked, th.locked {
            position: relative;
            cursor: default;
            left: expression(document.getElementById("div_gridholder").scrollLeft); /* div_gridholder : name of the div which holds gridview*/
        }


        /* Locks the header */
        .FreezeHeader {
            position: relative;
            cursor:default;
            top: expression(document.getElementById("GridView1").scrollTop-2);
            z-index: 20;
        }

        .FreezeHeader.locked{
            z-index : 100;
        }

        .label_clear {
            clear: both;
            display: block;
            margin-bottom: 0px;
            text-decoration: underline;
            text-align: center;
            font-weight: 700;
            background-color: #3399FF;
        }

        .lbl_info {
            border: 1px solid red;
            padding: 10px;
            font-size: larger;
            background-color: #000000;
            color: #ff6a00;
            width: 30%;
            height: 50px;
        }

        .calendarul {
            color: red;
            text-align: center;
        }

        .laber_head {
            display: block;
            float: left;
            margin-left: 10px;
            margin-right: 0px;
        }

        .elements_head {
            float: left;
            margin-left: 0px;
        }

        .validator {
            float: left;
            margin-left: 0px;
        }

        .contact_buton {
            border: 1px solid blue;
            border-radius: 15px;
            color: white;
            background-color: #808080;
            font-style: italic;
            font-size: medium;
            float: right;
        }

            .contact_buton:hover {
                box-shadow: 3px 3px 5px 6px #ccc;
            }

        .tabelul td {
            height: 50px;
            align-content: center;
            text-align: center;
            align-self: center;
            align-items: center;
            border: 1px thin dashed;
        }

        .auto-style3 {
            width: 222px;
        }

        .label_tabel {
            font-size: large;
            border: 1px solid black;
            color: red;
            background-color: #ccc;
            border-radius: 20px;
            margin-left: 134px;
            align-content: center;
            text-align: center;
            align-items: center;
            vertical-align: central;
            padding: 10px;
        }

        #Button3:hover {
            box-shadow: 3px 3px 5px 6px #ccc;
            color: blue;
        }

        #Button3:disabled {
            background-color: grey;
            color: darkblue;
        }

        .auto-style6 {
            width: 222px;
            height: 50px;
        }

        .special_label {
            margin-left: 14px;
            display: block;
            float: left;
            margin-right: 9px;
        }

        .auto-style11 {
            width: 142px;
        }

        .auto-style12 {
            width: 142px;
            height: 50px;
        }

        .auto-style15 {
            width: 149px;
        }

        .auto-style16 {
            width: 149px;
            height: 50px;
        }

        .auto-style29 {
            width: 161px;
        }

        .auto-style30 {
            width: 161px;
            height: 50px;
        }

        .auto-style31 {
            width: 148px;
        }

        .auto-style32 {
            width: 148px;
            height: 50px;
        }

        .auto-style33 {
            width: 146px;
        }

        .auto-style34 {
            width: 146px;
            height: 50px;
        }

        .auto-style35 {
            width: 121px;
        }

        .auto-style36 {
            width: 120px;
        }

        .auto-style37 {
            width: 211px;
        }
        .auto-style38 {
            font-size: xx-large;
            font-weight: bold;
            color: #CC3300;
        }
        </style>

</head>
<body style="overflow: auto; background-color: #dcd7d7; height: auto; width: auto;">

    <form id="form1" runat="server" style="height: auto;">
        
            <div style="height: 90px; width: auto; align-content: center;">
                <div style="float: left; background-color: #dcd7d7; height: 60px; width: 195px; align-content: center; text-align: center; ; margin: 2px;">
                    <span style="text-align: center; font-size: large;">You are authenticated as: </span>
                    <br />
                    <asp:Label ID="lbl_utilizator" Font-Size="Larger" runat="server"></asp:Label>
                </div>
                <h1 style="align-content: center; color: #ff6a00; width: 446px; margin-top: 9px; margin-left: 321px; float: left; height: 36px;"><span class="auto-style38" lang="EN-GB" style="font-family: &quot;Calibri&quot;,sans-serif; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-bidi-font-family: &quot;Times New Roman&quot;; mso-ansi-language: EN-GB; mso-fareast-language: EN-US; mso-bidi-language: AR-SA">Slot time booking management</span></h1>
                <asp:Button ID="Button4" runat="server" CssClass="contact_buton" Text="Contact" Height="44px" Style="margin-top: 7px" Width="77px" OnClick="Button4_Click" />
                <span style="margin-left:20px;"> V 0.74</span>&nbsp;
                <hr style="clear: both;" />

            </div>
            <!-- detalii contact -->
            <div id="dtl_contact" runat="server" style="align-content: center;">
                
                <asp:GridView ID="GridView2" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="5" CellSpacing="6" EnableModelValidation="True" GridLines="None" Style="margin-left: 82px">
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                &nbsp;<br />
                <hr />
                <br />

            </div>

            <div style="width: auto; align-content: center; height: 334px;">




                <h3 style="color: red; margin-left: 16px;">Details </h3>
                <hr />
                <table class="tabelul" style="height: 247px; width: 1169px">
                    <tr>
                        <td class="auto-style11">
                            <asp:Label ID="Label14" runat="server" CssClass="laber_head" Text="Trip id "></asp:Label>
                        </td>
                        <td class="auto-style3">
                            <asp:TextBox ID="txt_id_identificare" MaxLength="20" runat="server" Enabled="false" CssClass="elements_head" Width="180px"></asp:TextBox>
                        </td>
                        <td class="auto-style15">
                            <asp:Label ID="Label5" runat="server" CssClass="laber_head" Text="Truck number"></asp:Label>
                        </td>
                        <td class="auto-style31">
                            <asp:TextBox ID="txt_numar_camion" runat="server" MaxLength="14" CssClass="elements_head"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_numar_camion" CssClass="validator" Display="Dynamic" ErrorMessage="Empty truck number || " Text="*" ValidationGroup="header"></asp:RequiredFieldValidator>
                        </td>
                        <td class="auto-style29">
                            <asp:Label ID="Label6" runat="server" CssClass="laber_head" Text="Trailer number" Width="108px"></asp:Label>
                        </td>
                        <td class="auto-style33">
                            <asp:TextBox ID="txt_numar_remorca" runat="server" MaxLength="14" CssClass="elements_head"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_numar_remorca" CssClass="validator" ErrorMessage="Empty trailer number  || " Text="*" ValidationGroup="header"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style11">
                            <asp:Label ID="Label7" runat="server" CssClass="laber_head" Text="Driver name"></asp:Label>
                        </td>
                        <td class="auto-style3">
                            <asp:TextBox ID="txt_nume_sofer" MaxLength="40" runat="server" CssClass="elements_head" Width="180px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_nume_sofer" CssClass="validator" Display="Dynamic" ErrorMessage="Empty driver name   || " Text="*" ValidationGroup="header"></asp:RequiredFieldValidator>
                        </td>
                        <td class="auto-style15">
                            <asp:Label ID="Label8" runat="server" CssClass="laber_head" Text="Phone number"></asp:Label>
                        </td>
                        <td class="auto-style31">
                            <asp:TextBox ID="txt_numar_telefon" MaxLength="12" runat="server" CssClass="elements_head"></asp:TextBox>
                        </td>
                        <td class="auto-style29">
                            <asp:Label ID="Label9" runat="server" CssClass="laber_head" Text="Number of pallets"></asp:Label>
                        </td>
                        <td class="auto-style33">
                            <asp:TextBox ID="txt_numar_paleti_total" runat="server" MaxLength="4" placeholder="ex: 78" CssClass="elements_head"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_numar_paleti_total" CssClass="validator" Display="Dynamic" ErrorMessage="Empty number of pallets   || " Text="*" ValidationGroup="header"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style12">
                            <asp:Label ID="Label10" runat="server" CssClass="laber_head" Text="Pallets type"></asp:Label>
                        </td>
                        <td class="auto-style6">
                            <asp:DropDownList ID="drop_tip_paleti" runat="server" Style="font-size: large; height: auto;" CssClass="elements_head" Width="185px">
                                <asp:ListItem Value="Euro">Euro (120*80)</asp:ListItem>
                                <asp:ListItem Value="Non-euro">Non-euro (euro-size)</asp:ListItem>
                                <asp:ListItem Value="Industriali">Industriali (120*100)</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="auto-style16">
                            <asp:Label ID="Label11" runat="server" CssClass="laber_head" Text="Total weight"></asp:Label>
                        </td>
                        <td class="auto-style32">
                            <asp:TextBox ID="txt_greutate_totala" MaxLength="6" runat="server" CssClass="elements_head" placeholder="in kg ex: 12342"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_greutate_totala" CssClass="validator" Display="Dynamic" ErrorMessage="Empty weight   || " Text="*" ValidationGroup="header"></asp:RequiredFieldValidator>
                        </td>
                        <td class="auto-style30">
                            <asp:Label ID="Label12" runat="server" CssClass="laber_head" Text="Truck seal code"></asp:Label>
                        </td>
                        <td class="auto-style34">
                            <asp:TextBox ID="txt_numar_sigiliu" MaxLength="20" runat="server" CssClass="elements_head"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <br />
                            <asp:Label ID="Label13" runat="server" CssClass="special_label" Text="Invoice and notice codes " Width="262px" Height="19px"></asp:Label>
                            <asp:TextBox ID="txt_numerele_avizelor" TextMode="MultiLine" runat="server" CssClass="elements_head" Width="290px" Height="52px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_numerele_avizelor" CssClass="validator" Display="Dynamic" ErrorMessage="Empty invoice and notice codes   || " Text="*" ValidationGroup="header"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="Label15" runat="server" Text="Action type"></asp:Label>
                        </td>
                        <td>

                            <asp:DropDownList ID="DropDownList5" runat="server" Font-Bold="True" Font-Size="Medium" Height="46px" Width="132px">
                                <asp:ListItem Value="unloading">Unloading</asp:ListItem>
                                <asp:ListItem Value="loading">Loading</asp:ListItem>
                            </asp:DropDownList>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="height: auto;">
                            <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="SingleParagraph" ValidationGroup="header" runat="server" />
                        </td>
                    </tr>
                </table>
                <hr />

            </div>
            <!--detalii -->
            <div style="height: auto; width: auto;">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" style="height: auto; width: auto;">
                    <ContentTemplate>
                        <div style="clear: both; align-content: center; text-align: center; align-self: center; align-items: center; vertical-align: central; height: 95px;">
                            <br />

                            <asp:Label ID="lbl_info" runat="server" CssClass="lbl_info"></asp:Label>
                            <br />
                            <br />
                            <hr />
                            <br />

                        </div>
                        <!-- aside-->
                        <div style="width: auto; height: auto; text-align: left; display:block;" >
                            <div style="height: auto; align-content: center; float: left; padding: 10px; width: 24%;">
                                <h3 style="color: red; width: 100%; text-align: center;">Rezervation </h3>
                                <hr />



                                <br />
                                <br />
                                <asp:Label ID="Label1" runat="server" Text="Depo:" CssClass="labels" Width="38px"></asp:Label>
                                <asp:DropDownList ID="DropDownList1" runat="server" Width="168px" CssClass="labels" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Value="DC1_Ploiesti">DC1 PLOIESTI</asp:ListItem>
                                    <asp:ListItem Value="DC2_Tureni">DC2 TURENI</asp:ListItem>
                                    <asp:ListItem Value="DC3_Aricesti">DC3 ARICESTI</asp:ListItem>
                                    <asp:ListItem Value="nestle_ul">Nestle&amp;Ul</asp:ListItem>
                                </asp:DropDownList>


                                <br />
                                <br />
                                <asp:Label ID="Label2" runat="server" Text="Select a date for unload! " CssClass="label_clear" Width="280px"></asp:Label>

                                <asp:Calendar ID="Calendar1" runat="server" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="205px" ShowGridLines="True" Width="282px" Style="margin-top: 12px" OnSelectionChanged="Calendar1_SelectionChanged">
                                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                    <OtherMonthDayStyle ForeColor="#CC9966" />
                                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                    <SelectorStyle BackColor="#FFCC66" />
                                    <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                                </asp:Calendar>

                                <br />

                                Unload start time:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Ramp:<br />
                                <asp:DropDownList ID="DropDownList2" runat="server" Width="123px" Height="19px" ToolTip="Valoarea default este de o ora">
                                    <asp:ListItem>08:00</asp:ListItem>
                                    <asp:ListItem>09:00</asp:ListItem>
                                    <asp:ListItem>10:00</asp:ListItem>
                                    <asp:ListItem>11:00</asp:ListItem>
                                    <asp:ListItem>12:00</asp:ListItem>
                                    <asp:ListItem>13:00</asp:ListItem>
                                    <asp:ListItem>14:00</asp:ListItem>
                                    <asp:ListItem>15:00</asp:ListItem>
                                    <asp:ListItem>16:00</asp:ListItem>
                                    <asp:ListItem>17:00</asp:ListItem>
                                    <asp:ListItem>18:00</asp:ListItem>
                                    <asp:ListItem>19:00</asp:ListItem>
                                    <asp:ListItem>20:00</asp:ListItem>
                                    <asp:ListItem>21:00</asp:ListItem>
                                    <asp:ListItem>22:00</asp:ListItem>
                                    <asp:ListItem>23:00</asp:ListItem>
                                    <asp:ListItem>00:00</asp:ListItem>
                                    <asp:ListItem>01:00</asp:ListItem>
                                    <asp:ListItem>02:00</asp:ListItem>
                                    <asp:ListItem>03:00</asp:ListItem>
                                    <asp:ListItem>04:00</asp:ListItem>
                                    <asp:ListItem>05:00</asp:ListItem>
                                    <asp:ListItem>06:00</asp:ListItem>
                                    <asp:ListItem>07:00</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="DropDownList3" runat="server" Width="123px" Height="19px" Style="margin-left: 24px" ToolTip="Valoarea default este de o ora">
                                    <asp:ListItem Value="2">rampa 2</asp:ListItem>
                                    <asp:ListItem Value="3">rampa 3</asp:ListItem>
                                    <asp:ListItem Value="4">rampa 4</asp:ListItem>
                                    <asp:ListItem Value="5">rampa 5</asp:ListItem>
                                    <asp:ListItem Value="6">rampa 6</asp:ListItem>
                                    <asp:ListItem Value="7">rampa 7</asp:ListItem>
                                    <asp:ListItem Value="9">rampa 9</asp:ListItem>
                                    <asp:ListItem Value="10">rampa 10</asp:ListItem>
                                    <asp:ListItem Value="11">rampa 11</asp:ListItem>
                                    <asp:ListItem Value="15">rampa 15</asp:ListItem>
                                </asp:DropDownList>
                                <br />

                                <br />
                                Unload duration<br />
                                <asp:DropDownList ID="DropDownList4" runat="server" Width="108px" Height="19px">
                                    <asp:ListItem Value="1">1 hour</asp:ListItem>
                                    <asp:ListItem Value="2">2 hours</asp:ListItem>
                                    <asp:ListItem Value="3">3 hours</asp:ListItem>
                                    <asp:ListItem Value="4">4 hours</asp:ListItem>
                                    <asp:ListItem Value="5">5 hours</asp:ListItem>
                                    <asp:ListItem Value="6">6 hours</asp:ListItem>
                                    <asp:ListItem Value="7">7 hours</asp:ListItem>
                                    <asp:ListItem Value="8">8 hours</asp:ListItem>
                                    <asp:ListItem Value="9">9 hours</asp:ListItem>
                                    <asp:ListItem Value="10">10 hours</asp:ListItem>
                                    <asp:ListItem Value="11">11 hours</asp:ListItem>
                                    <asp:ListItem Value="12">12 hours</asp:ListItem>
                                </asp:DropDownList>

                                <br />



                                <asp:Button ID="Button3" runat="server" ValidationGroup="header" Style="margin-top: 27px; border: 1px solid red; border-radius: 10px; font-size: large; color: white; background-color: #808080" Text="Save trip" Width="280px" Height="46px" OnClick="Button1_Click" />
                                <br />
                                <br />
                                <asp:Button ID="Button5" runat="server" Text="New trip" OnClick="Button5_Click" Width="143px" />
                                <br />
                                <br />
                                &nbsp;Delete trip with id:
                            


                            <br />
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txt_id_stergere" runat="server" Width="148px" Style="margin-left: 0px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="Button1" runat="server" Text="Delete" Style="margin-left: 18px" OnClick="Button1_Click1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lbl_stergere_cursa" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                </table>
                                <br />
                                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Style="margin-left: 19px" Text="Show all trips" /> 
                                <table id="tabel_date" runat="server" style="width: 313px; height: 214px; align-content:center; text-align:center; ">
                                    <tr>
                                        <td>
                                            From date: 
                                        </td>
                                        <td class="auto-style37">
                                            To date: 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                             <asp:Calendar ID="Calendar2" runat="server" Height="200px" Width="184px" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" ShowGridLines="True">
                                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                    <OtherMonthDayStyle ForeColor="#CC9966" />
                                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                    <SelectorStyle BackColor="#FFCC66" />
                                    <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                                </asp:Calendar>
                                        </td>
                                        <td class="auto-style37">
                                             <asp:Calendar ID="Calendar3" runat="server" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" ShowGridLines="True" Width="187px">
                                                 <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                                                 <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                                 <OtherMonthDayStyle ForeColor="#CC9966" />
                                                 <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                                 <SelectorStyle BackColor="#FFCC66" />
                                                 <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                                                 <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                                             </asp:Calendar>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                              <br />
                               
                            </div>
                            <!--Calendarul-->
                            <div style="float: left; height: 660px; width: 73%; padding-top: 40px; overflow: auto; padding-left: 5px; padding-right: 5px; padding-bottom: 10px;">
                                <asp:GridView ID="GridView1" runat="server" CellPadding="4" CssClass="calendarul" EnableModelValidation="True" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="0px">
                                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                    <HeaderStyle BackColor="#990000" CssClass="FreezeHeader" Font-Bold="True" ForeColor="#FFFFCC" />
                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                    <RowStyle BackColor="White" ForeColor="#330099" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                </asp:GridView>
                                <asp:Label ID="lbl_tabel" runat="server" Visible="False" BorderStyle="Solid" CssClass="label_tabel" Text="No trips have been booked for today, pressing 'Save trip' a new day will be created starting with your trip!" Height="56px" Width="548px"></asp:Label>
                                <br />
                                <div style="align-content: center; text-align: center;" id="divlegena" runat="server">
                                    <table style="align-content: center; text-align: center; align-self: center">
                                        <tr>
                                            <td>
                                                <button disabled="disabled" style="background-color: lightcyan; height: 50px; width: 50px"></button>
                                            </td>
                                            <td class="auto-style35">Ambient ramps</td>
                                            <td>
                                                <button disabled="disabled" style="background-color: lightblue; height: 50px; width: 50px"></button>
                                            </td>
                                            <td class="auto-style36">Food ramps 
                                            </td>
                                        </tr>

                                    </table>

                                </div>
                            </div>
                             <div style="clear: both; width: auto; height: 400px; overflow: scroll;" id="div_curse_client" runat="server">
                                   
                                    <br />
                                    <br />

                                    <asp:GridView ID="grid_toate_cursele" runat="server" CellPadding="5" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#990000" CssClass="FreezeHeader" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                        <SortedDescendingHeaderStyle BackColor="#820000" />
                                    </asp:GridView>
                                </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
    </form>
</body>
</html>
