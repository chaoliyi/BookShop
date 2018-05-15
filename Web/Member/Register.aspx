<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="BookShop.Web.Member.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <style type="text/css">
        input{width:200px; height:25px; line-height:25px; margin:5px 0;}
        input.btn{
            width: 300px;
            margin-left: 30px;
            height: 40px;
            background: #db2f2f;
            border: none;
            color: #FFF;
            font-size: 15px;
            font-weight: 700;
            cursor: pointer;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            /*校验用户名*/
            $("#txtName").blur(function () {
                if ($(this).val().length > 0) {

                    $("#validateCodeTips").hide();
                    $.post("/ashx/ValidateReg.ashx", { "action": "validateName", "userName": $(this).val() }, function (data) {
                        $("#nameTips").text(data).show();
                    });
                } else {
                    $("#nameTips").text("用户名不能为空!").show();
                }
            });
            /*校验验证码 End*/
            /*邮箱验证*/
            $("#txtEmail").blur(function () {
                if ($(this).val().length > 0) {
                    var reg = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
                    if (reg.test($(this).val())) {
                        $("#emailTips").hide();
                        $.post("/ashx/ValidateReg.ashx", { "action": "validateEmail", "userEmail": $(this).val() }, function (data) {
                            $("#emailTips").text(data).show();
                        });
                    } else {
                        $("#emailTips").text("邮箱格式不正确!").show();
                    }
                } else {
                    $("#emailTips").text("邮箱不能为空!").show();
                }
            });
            /*邮箱验证 End*/
            /*校验验证码*/
            $("#txtCode").blur(function () {
                if ($(this).val().length > 0) {
                    var reg = /^\d+$/;
                    if (reg.test($(this).val())) {
                        $("#validateCodeTips").hide();
                        $.post("/ashx/ValidateReg.ashx", { "action": "validateCode", "userVCode": $(this).val() }, function (data) {
                            $("#validateCodeTips").text(data).show();
                        });
                    } else {
                        $("#validateCodeTips").text("验证码格式错！").show();
                    }
                } else {
                    $("#validateCodeTips").text("验证不能为空!").show();
                }
            });
            /*校验验证码 End*/
            /*注册用户*/
            $("#btnRegister").click(function () {
                //if ($("#txtName").val() == "") { $("#nameTips").text("用户名不能为空"); return false; }
                //if ($("#txtPwd").val() == "") { $("#pwdTips").text("密码不能为空"); return false; }
                //if ($("#txtConfirmPwd").val() == "") { $("#confirmPwdTips").text("确认密码不能为空"); return false; }

                var pars = $("#formRegister").serializeArray();
                $.post("/ashx/AjaxRegister.ashx", pars, function (data) {
                    if (data == "ok") {
                        window.location.href = "/Default.aspx";
                    } else {
                        window.location.href = "/ShowMsg.aspx?msg=" + data + "&txt=首页&url=/Default.aspx"
                    }
                });
            });
            /*注册用户 End*/
        });
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="formRegister">
        <div id="main">
            用户名：<span id="nameTips" style="font-size: 14px; color: red"></span><br />
            <input type="text" name="txtName" id="txtName"/><br />
            真实姓名：<br />
            <input type="text" name="txtRealName" /><br />
            密码：<span id="pwdTips" style="font-size: 14px; color: red"></span><br />
            <input type="password" name="txtPwd" id="txtPwd" /><br />
            确认密码：<span id="confirmPwdTips" style="font-size: 14px; color: red"></span><br />
            <input type="password" name="txtConfirmPwd" id="txtConfirmPwd"/><br />
            Email：<span id="emailTips" style="font-size: 14px; color: red"></span><br />
            <input type="text" name="txtEmail" id="txtEmail" /><br />
            地址：<br />
            <input type="text" name="txtAddress" /><br />
            手机：<br />
            <input type="text" name="txtPhone" /><br />
            验证码：<span id="validateCodeTips" style="font-size: 14px; color: red"></span><br />
            <input type="text" name="txtCode" id="txtCode" /><img src="/ashx/ValidateCode.ashx" /><br /><br />
            <input type="button" value="注册" class="btn" id="btnRegister" />
        </div>
    </form>
</asp:Content>
