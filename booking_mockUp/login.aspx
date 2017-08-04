<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="booking_mockUp.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" type="image/x-icon" href="imagini/favico.ico" />
    <title>Login Xdock Booking </title>
    <style>
        header section {
            display: block;
        }

        .labels {
            display: block;
            font-size: large;
        }

        .special_label {
            color: red;
            font-family: Arial;
            font-style: italic;
        }

        table td {
            align-content: center;
            height: 50px;
        }

        .auto-style1 {
            width: 461px;
        }

        .auto-style2 {
            width: 128px;
        }

        .auto-style3 {
            width: 139px;
        }

        .auto-style4 {
            width: 413px;
        }

        .auto-style6 {
            color: #0a39f3;
            width: 485px;
            font-weight: normal;
            padding: 5px;
        }
    </style>
    <script type="text/javascript">
        window.onerror = function (msg, url, line) {
            alert('S-a produs o eroare : \n' + msg + '\n' + url + '\n' + line);
            return false;
        }
    </script>
</head>
<body style="overflow: auto; height: auto; width: auto; background-color:#e2e0e0" >
    <form id="form1" runat="server">
        <div style="border-style: none; display: block; border-color: inherit; background-image: url('./imagini/logistica.jpg'); background-repeat: no-repeat; background-size: cover; width: 1200px; height: 490px; margin-top:5%; margin-left:3%;" >

            <header style="text-align: center;">
                <h1 class="auto-style6"><strong>AQUILA X-DOCK SLOT TIME BOOKING PLATFORM</strong></h1>
            </header>
            <table style="width: 450px; align-content: center; background-color: #e0dede; align-self: center; text-align: center; align-items: center; margin-left: 17px;">

                <tr>
                    <td class="auto-style3" style="text-align: center; align-content: center; align-items: center; align-self: center;">
                        <asp:Label CssClass="labels" ID="Nume" runat="server" Text="Name" Width="55px"></asp:Label></td>
                    <td class="auto-style4">
                        <asp:TextBox ID="TextBox1" MaxLength="20" CssClass="labels" Style="float: left;" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" Style="display: block;" ValidationGroup="grup" ControlToValidate="TextBox1" runat="server" ErrorMessage="Must insert a name" Height="20px" Width="134px"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td class="auto-style3" style="text-align: center; align-content: center; align-items: center; align-self: center;">
                        <asp:Label ID="Label2" CssClass="labels" runat="server" Text="Password" Width="55px"></asp:Label></td>
                    <td class="auto-style4">
                        <asp:TextBox ID="TextBox2" MaxLength="10" TextMode="Password" CssClass="labels" Style="float: left;" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" Style="display: block;" ValidationGroup="grup" ControlToValidate="TextBox2" runat="server" ErrorMessage="Must insert password" Height="19px" Width="138px"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: left;">

                        <asp:RadioButton ID="rb_client" BorderStyle="None" GroupName="choice" Text="Client" runat="server" Style="margin-left: 14px; font-size: large;" Width="131px " />
                        <asp:RadioButton ID="rb_operator" GroupName="choice" BorderStyle="None" Text="Operator" runat="server" Style="margin-left: 0px; font-size: large;" />


                    </td>
                </tr>
                <tr>

                    <td class="auto-style4" colspan="2" style="text-align: left;">
                        <asp:Button ID="Button1" ValidationGroup="grup" runat="server" Text="Log In" OnClick="Button1_Click" Height="38px" Style="margin-left: 10px" Width="86px" />
                        <asp:Button ID="Button2" ValidationGroup="grup" runat="server" CausesValidation="false" Text="Register" OnClick="Button2_Click" Height="38px" Style="margin-left: 46px" Width="86px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">

                        <asp:Label ID="lbl_message" CssClass="special_label" runat="server" Text=""></asp:Label>
                        <hr id="hr_hr" runat="server" />
                    </td>
                </tr>
            </table>

            <br />
            <br />


            <br />
            <br />


        </div>
        <br />

        <!-- registration here -->
        <div id="register_div" runat="server" style="clear: both; padding-left: 5%; text-align: center;">
            <header>
                <h1 style="text-align: left; color: #ff6a00; width: 235px; height: 36px; margin-left: 78px; margin-bottom: 4px;">Register</h1>
            </header>
            <section>

                <div>
                    <table>
                        <tr>
                            <td class="auto-style2">
                                <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label></td>
                            <td class="auto-style1" style="text-align: left;">
                                <asp:TextBox ID="TextBox3" MaxLength="20" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="regis" Display="Dynamic" ControlToValidate="TextBox3" runat="server" ErrorMessage="Name missing"></asp:RequiredFieldValidator>
                            </td>

                        </tr>
                        <tr>
                            <td class="auto-style2">
                                <asp:Label ID="Label7" runat="server" Text="Company"></asp:Label>
                            </td>
                            <td class="auto-style1" style="text-align: left;">
                                <asp:TextBox ID="TextBox5" MaxLength="30" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="regis" Display="Dynamic" ControlToValidate="TextBox5" runat="server" ErrorMessage="Company name empty"></asp:RequiredFieldValidator>
                            </td>

                        </tr>
                        <tr>
                            <td class="auto-style2">
                                <asp:Label ID="Label3" runat="server" Text="Password"></asp:Label></td>
                            <td class="auto-style1" style="text-align: left;">
                                <asp:TextBox ID="TextBox4" TextMode="Password" MaxLength="10" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="regis" Display="Dynamic" runat="server" ControlToValidate="TextBox4" ErrorMessage="Password empty"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="Dynamic" ValidationGroup="regis" ControlToValidate="TextBox4" ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{6,}$" runat="server" ErrorMessage="Must be al least 6 characters"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">
                                <asp:Label ID="Label4" runat="server" Text="Confirm password"></asp:Label></td>
                            <td class="auto-style1" style="text-align: left;">
                                <asp:TextBox ID="TextBox6" TextMode="Password" MaxLength="15" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="regis" Display="Dynamic" ControlToValidate="TextBox6" runat="server" ErrorMessage="Password empty"></asp:RequiredFieldValidator>

                                <asp:CompareValidator ID="CompareValidator1" Display="Dynamic" ValidationGroup="regis" ControlToValidate="TextBox6" ControlToCompare="TextBox4" runat="server" ErrorMessage="It doesn't match the password"></asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td class="auto-style2">
                                <asp:Label ID="Label5" runat="server" Text="Email"></asp:Label></td>
                            <td class="auto-style1" style="text-align: left;">
                                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="regis" runat="server" Display="Dynamic" ErrorMessage="Email empty" ControlToValidate="TextBox7"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="Dynamic" ValidationGroup="regis" ID="RegularExpressionValidator1" ControlToValidate="TextBox7" runat="server" ErrorMessage="Wrong email format " ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="auto-style2">
                                <asp:Label ID="Label6" runat="server" Text="Merchandise type"></asp:Label></td>
                            <td class="auto-style1" style="text-align: left;">
                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" Height="16px" Width="252px" RepeatDirection="Horizontal" Style="margin-left: 19px">
                                    <asp:ListItem>Food</asp:ListItem>
                                    <asp:ListItem>Ambient</asp:ListItem>
                                    <asp:ListItem>Adr</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>

                    </table>
                    <div style="text-align: left;">
                        <asp:Button ID="Button3" runat="server" ValidationGroup="regis" Text="Sign up" Height="36px" Style="margin-left: 17px" Width="150px" OnClick="Button3_Click" />
                        <br />
                        <br />

                        <asp:Label ID="Label8" ForeColor="Red" Font-Size="Medium" runat="server" BorderStyle="None" Style="margin-left: 40px; margin-bottom: 4px;"></asp:Label>
                    </div>
                </div>

            </section>
        </div>


    </form>
</body>
</html>
