<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BookShop.Web.Member.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script>
        $(function () {
            $("#btnRegister").click(function () {
                var hiddenReturnUrl=$("#hiddenReturnUrl").val();
                if (hiddenReturnUrl == "") {
                    window.location.href = "/Member/Register.aspx";
                } else {
                    window.location.href = "/Member/Register.aspx?returnUrl=" + hiddenReturnUrl;
                }
                return false;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <table width="60%" height="22" border="0" align="center" cellpadding="0" cellspacing="0" runat="server" id="tblfirst">
        <tr>
            <td width="10">
                <img src="/Images/az-tan-top-left-round-corner.gif" width="10" height="28" /></td>
            <td bgcolor="#DDDDCC"><span style="font-family: '黑体'; font-size: 16px; color: #660000;">登录网上书店</span></td>
            <td width="10">
                <img src="/Images/az-tan-top-right-round-corner.gif" width="10" height="28" /></td>
        </tr>
    </table>
    <table width="60%" height="22" border="0" align="center" cellpadding="0" cellspacing="0" runat="server" id="tblsecend">
        <tr>
            <td bgcolor="#DDDDCC" style="width: 2px">&nbsp;</td>
            <td>
                <div align="center">
                    <table height="61" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="33" colspan="6">
                                <p style="font-size: 14px; font-weight: bold; color: #FF9900; text-align: center;">已注册用户请从这里登录</p>
                            </td>
                        </tr>
                        <tr>
                            <td width="24%" height="26" rowspan="2" align="right" valign="top"><strong>用户名：</strong></td>
                            <td valign="top" width="37%">&nbsp;<input type="text" name="txtLoginID" id="txtLoginID" value="" /></td>
                        </tr>
                    </table>
                    <table height="61" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="1" colspan="2"></td>
                        </tr>
                        <tr>
                            <td width="24%" height="26" rowspan="3" align="right" valign="top"><strong>密　码：</strong></td>
                            <td valign="top" width="37%">&nbsp;<input type="text" name="txtLoginPwd" id="txtLoginPwd" value="" /></td>
                        </tr>

                        <tr>
                            <td valign="top" width="37%">
                                <input type="checkbox" name="chkAutoLogin" id="chkAutoLogin" value="1" /><label for="chkAutoLogin">记住我</label>
                            </td>
                        </tr>

                    </table>
                    <div>
                        <input type="hidden" name="hiddenReturnUrl" id="hiddenReturnUrl" value="<%=this.ReturnUrl %>" />
                        <input type="image" src="/Images/az-login-gold-3d.gif" name="btnLogin" id="btnLogin" value="登录" />
                        <input type="image" src="/Images/az-newuser-gold-3d.gif" name="btnRegister" id="btnRegister" value="新用户注册" />
                    </div>
                    <div>
                        <div align="center">
                            &nbsp;
                        </div>
                    </div>
            </td>
            <td width="2" bgcolor="#DDDDCC">&nbsp;</td>
        </tr>
    </table>
    <table width="60%" height="3" border="0" align="center" cellpadding="0" cellspacing="0" runat="server" id="tblthird">
        <tr align="center">
            <td height="3" bgcolor="#DDDDCC">&nbsp;
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
