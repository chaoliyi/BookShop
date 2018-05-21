<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FindPwd.aspx.cs" Inherits="BookShop.Web.Member.FindPwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <style>
        #divForm{
            width:300px;height:300px;margin:0 auto;
        }
        ul li{display:block;list-style:none;margin-bottom:13px;}
        ul li label{width:86px;height:34px;line-height:34px;display:block;float:left;}
        ul li input{width:137px;height:32px;line-height:32px;}
        ul li input.text{text-indent:10px;}
    </style>
    <script>
        $(function () {
            $("#btnFindPwd").click(function () {
                var userName = $("#txtName").val();
                var userEmail = $("#txtEmail").val();
                if (userName!=""&&userEmail!="") {
                    $.post("/ashx/FindPwd.ashx", { "userName": userName, "userEmail": userEmail }, function (data) {
                        alert(data);
                        window.location.href("Login.aspx");
                    });
                } else {
                    alert("用户名或邮箱不能为空！");
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div id="divForm">
            <ul class="formtitle">
                <li><label>用户名：</label><input type="text" class="text" name="txtName" id="txtName" value="" /></li>
                <li><label>邮箱：</label><input type="text" class="text" name="txtEmail" id="txtEmail" value="" /></li>
                <li><label>&nbsp;</label><input type="button" name="btnFindPwd" id="btnFindPwd" value="找回密码" /></li>
            </ul>
        </div>
    </form>
</asp:Content>
