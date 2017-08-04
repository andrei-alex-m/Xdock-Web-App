<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Scanning.aspx.cs" Inherits="xDock_scanners_app._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" type="image/x-icon" href="imagini/favico.ico" />
    <title>Scanning </title>
    <script type="text/javascript">
        'use strict';
        window.onerror = function (msg, url, line) {
            alert('Cursa nu a fost gasita!');
            return false;
        }

        function fct_onblur() {
            var b = document.getElementById('btn_palet');
            b.focus();
        }

        function fct_blr() {
            var b = document.getElementById('Button1');
            b.focus();
        }

        function fct() {
            var b = document.getElementById('TextBox3');
            b.focus();
        }

        function check_if_enter(event, optiune) {
            if (event.keyCode == 13) {
                if (optiune == 2) {
                    document.getElementById('btn_palet').click();
                    return false;
                }
                else if (optiune == 1) {
                    document.getElementById('Button1').click();
                    return false;
                }
            }
            else {
                return true;
            }
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 53px;
        }

        .auto-style7 {
            height: 26px;
            width: 147px;
        }

        .auto-style12 {
            width: 147px;
        }

        .auto-style13 {
            width: 127px;
        }

        .auto-style14 {
            height: 26px;
            width: 127px;
        }

        .auto-style15 {
            width: 254px;
        }

        .auto-style16 {
            height: 48px;
        }

        .auto-style18 {
            width: 221px;
        }

        .auto-style23 {
            width: 57px;
        }
        .auto-style25 {
            width: 79px;
        }
        .auto-style26 {
            width: 144px;
        }
        .auto-style27 {
            width: 125px;
        }
        .auto-style29 {
            width: 220px;
        }
    </style>
</head>
<body style="margin: 0px;">
   
    <form id="form1" runat="server" style="overflow: scroll; margin: 0px;">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="width: 240px; height: auto; background-color: #d8d0d0; padding: 5px;">



                    <table style="width: 242px; align-content:center; text-align:center">
                        <tr>
                            <td class="auto-style1">Cursa: </td>
                            <td>
                                <asp:TextBox ID="txt_id_cursa" onkeypress="return check_if_enter(event, 1);"   runat="server" Style="margin-left: 1px; text-align: center;" Width="141px" BackColor="#66FFFF"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" BorderStyle="None" ControlToValidate="txt_id_cursa" ValidationGroup="cursa" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Introduceti un id cursa"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="Button1" runat="server" Text="Incarca cursa" ValidationGroup="cursa" Style="margin-bottom:3px;" Width="225px" BackColor="#CC0000" ForeColor="White" OnClick="Button1_Click"/>
                                <br />
                                <button onclick="fct();" style="background-color:#CC0000; color:white; width: 225px">Detalii cursa</button>
                            </td>
                        </tr>
                    </table>
                    <hr />
                     <span>Actiune scanare paleti: </span>
                    <br />
                    <asp:ListBox ID="lst_actiune_paleti" runat="server" AutoPostBack="True" Height="42px" OnSelectedIndexChanged="lst_actiune_paleti_SelectedIndexChanged" Style="text-align: center;" Width="167px" BackColor="#CCFFCC">
                    </asp:ListBox>
                    <br />
                    <br />
                    <span>Lista comenzi pe cursa </span>
                    <br />
                    <asp:ListBox ID="lst_lista_comenzi" runat="server" AutoPostBack="True" Height="70px" OnSelectedIndexChanged="lst_lista_comenzi_SelectedIndexChanged" Style="text-align: center;" ToolTip="selectati o cursa pentru a arata paletii aferenti acestei curse" Width="214px" BackColor="#CCFFCC"></asp:ListBox>
                    <br />

                    <br />
                    <table style="text-align:center;">
                        <tr>
                            <td class="auto-style25">Locatie incarcare: </td>
                            <td class="auto-style26">
                                <asp:TextBox ID="txt_locInc" runat="server" BackColor="#CCFF66"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style25">Locatie descarcare: </td>
                            <td class="auto-style26">
                                <asp:TextBox ID="txt_locDesc" runat="server" BackColor="#CCFF66"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <span>Lista paleti pe comanda </span>
                    <br />
                    <asp:ListBox ID="lst_lista_paleti" runat="server" Height="94px" OnSelectedIndexChanged="lst_lista_paleti_SelectedIndexChanged" Style="text-align: center;" Width="214px" BackColor="#CCFFCC"></asp:ListBox>
                    <br />
                    <table>
                        <tr>
                            <td class="auto-style27" style="text-align: right; vertical-align: central;">
                                <label style="display: block; margin-right: 10px;">
                                Id palet</label> </td>
                            <td class="auto-style18">
                                <asp:TextBox ID="txt_id_palet" runat="server" onkeypress="return check_if_enter(event, 2);" Style="float: left; text-align: center; margin-left: 5px;" Width="126px" BackColor="#66FFFF"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_id_palet" Display="Dynamic" ErrorMessage="camp gol" Style="display: block;" ValidationGroup="palet" Width="71px"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style23" colspan="2" style="align-content:center; text-align:center; align-items:center; align-self:center;" >
                                <asp:Button ID="btn_palet" style="background-color:#CC0000; color:white;" runat="server" OnClick="btn_palet_Click"  Text="Verifica" ValidationGroup="palet" Width="215px"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style16" style="height:auto;" colspan="2">
                                <asp:Label ID="lbl_paleti_info" runat="server" Text="" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">Lista paletilor scanati<br />
                                <asp:ListBox ID="lst_paleti_scanati" runat="server" AutoPostBack="True" Height="121px" Style="text-align: center;" Width="210px" BackColor="#CCFFCC"></asp:ListBox>
                            </td>
                        </tr>
                    </table>
                    
                    <br />
                    <hr />
                    <table style="padding: 10px;">
                        <tr>
                            <td class="auto-style29"><span>Modificare status comanda </span></td>
                        </tr>
                        
                        <tr>
                            <td class="auto-style29">Status special<br />
                                <asp:ListBox ID="lst_status_special_comanada" runat="server" AutoPostBack="True" Height="120px" Width="180px" BackColor="#CCFFCC">
                                </asp:ListBox>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <span>Observatii speciale </span>

                    <br />

                    <asp:TextBox ID="txt_observatii_speciale" runat="server" Height="34px" TextMode="MultiLine" Width="217px" BackColor="#CCFFCC"></asp:TextBox>
                    <br />
                    <asp:Label ID="lbl_status" runat="server" Style="color:darkblue; background-color:grey;" Text=""></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="btn_salveaaza_status_comanda" runat="server" Height="36px" OnClick="btn_salveaaza_status_comanda_Click" Style="margin-left: 16px" Text="Salveaza status" Width="195px" BackColor="#CC0000" ForeColor="White" />
                    <hr />

                    <span style="width:100%; display:block; text-align:center;" > Detalii cursa </span>
                    
                    <br />

                    <table>
                        <tr>
                            <td class="auto-style13"><span>Sigiliu camion </span></td>
                            <td class="auto-style12">
                                <asp:TextBox ID="txt_sigiliu_camion" Style="text-align: center;" Enabled="false" runat="server" Width="115px" BackColor="#CCFF66"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style13"><span>Rampa incarcare </span></td>
                            <td class="auto-style12">
                                <asp:TextBox ID="TextBox1" Style="text-align: center;" runat="server" Width="115px" BackColor="#CCFF66"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style13"><span>Rampa descarcare </span></td>
                            <td class="auto-style12">
                                <asp:TextBox ID="TextBox2" Style="text-align: center;" runat="server" Width="115px" BackColor="#CCFF66"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button3" runat="server" Style="font-size:small;" Text="Schimba" OnClick="Button3_Click" Width="86px" BackColor="#CC0000" ForeColor="White" />
                            </td>
                            <td>
                                <asp:Label ID="lbl_rampi_schimbate" Style="color:darkblue;" runat="server"  Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style13"><span>Numar paleti pe cursa </span></td>
                            <td class="auto-style12">
                                <asp:TextBox ID="txt_numar_paleti_total" Style="text-align: center;" Enabled="false" runat="server" Width="115px" BackColor="#CCFF66"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style14"><span>Numar paleti incarcati </span></td>
                            <td class="auto-style7">
                                <asp:TextBox ID="txt_numar_paleti_incarcati" Style="text-align: center;" Enabled="false" runat="server" Width="115px" BackColor="#CCFF66"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="auto-style13"><span>Numar paleti descarcati </span></td>
                            <td class="auto-style12">
                                <asp:TextBox ID="txt_numar_paleti_descarcati" Style="text-align: center;" Enabled="false" runat="server" Width="115px" BackColor="#CCFF66"></asp:TextBox></td>
                        </tr>
                    </table>
                    <br />
                    Adaugare sigiliu camion
            <table>
                <tr>
                    <td class="auto-style15">
                        <asp:TextBox ID="TextBox3" Style="text-align: center;" MaxLength="39" runat="server" Width="115px" BackColor="#00CCFF"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" ControlToValidate="TextBox3" ValidationGroup="sigiliu" runat="server" ErrorMessage="Camp necesar"></asp:RequiredFieldValidator>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="Button2"  runat="server" Text="Adauga sigiliu" ValidationGroup="sigiliu" OnClick="Button2_Click" Style="margin-left: 0px" Width="226px" BackColor="#CC0000" ForeColor="White" /> 
                        <asp:Label  ID="lbl_sigiliu" Style="display:block; margin-left:5px; color:darkblue; width:auto;" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>

                    
                    <br />
                </div>
                

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
