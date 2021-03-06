﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessImage_imageareaselect.aspx.cs" Inherits="BookShop.Web.ProcessImage_imageareaselect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>imageareaselect-demo</title>
    <link href="css/imgareaselect/imgareaselect-default.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/imgareaselect/jquery.imgareaselect.min.js"></script>
    <script src="SWFUpload/swfupload.js"></script>
    <script src="SWFUpload/handlers.js"></script>

    <script type="text/javascript">
        $(function () {
            $("img").hide();
            $("#btnCut").hide();
            /*上传图片设置*/
            var swfu = new SWFUpload({
                // Backend Settings
                upload_url: "/ashx/upload.ashx?action=upload",
                post_params: {
                    "ASPSESSID": "<%=Session.SessionID %>"
                },

                // File Upload Settings
                file_size_limit: "10 MB",
                file_types: "*.jpg;*.gif",
                file_types_description: "JPG Images",
                file_upload_limit: 0,    // Zero means unlimited

                // Event Handler Settings - these functions as defined in Handlers.js
                //  The handlers are not part of SWFUpload but are part of my website and control how
                //  my website reacts to the SWFUpload events.
                swfupload_preload_handler: preLoad,
                swfupload_load_failed_handler: loadFailed,
                file_queue_error_handler: fileQueueError,
                file_dialog_complete_handler: fileDialogComplete,
                upload_progress_handler: uploadProgress,
                upload_error_handler: uploadError,
                upload_success_handler: showImage,//uploadSuccess,
                upload_complete_handler: uploadComplete,

                // Button settings
                button_image_url: "/SWFUpload/images/btnbg.png",//设置按钮样式
                button_placeholder_id: "spanButtonPlaceholder",
                button_width: 137,
                button_height: 35,
                button_text: '<span class="button">选择图片<span class="buttonSmall">(2 MB Max)</span></span>',
                button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt;color:white; } .buttonSmall { font-size: 10pt; }',
                button_text_top_padding: 6,
                button_text_left_padding: 6,

                // Flash Settings
                flash_url: "/SWFUpload/swfupload.swf",	// Relative to this file
                flash9_url: "/SWFUpload/swfupload_FP9.swf",	// Relative to this file

                custom_settings: {
                    upload_target: "divFileProgressContainer"
                },

                // Debug Settings
                debug: false
            });

            /*选择截图范围*/
            $('#imgShow').imgAreaSelect({
                selectionColor: 'blue', x1: 0, y1: 0, x2: 100,
                maxWidth: 200, minWidth: 100, y2: 100, minHeight: 100, maxHeight: 200,
                selectionOpacity: 0.2, onSelectEnd: preview
            });
            /*保存截图*/
            $("#btnCut").click(function () {
                saveCutImage();
            });
        });
        /*截图范围预览*/
        function preview(img, selection) {

            $('#imgShow').data('x', selection.x1);
            $('#imgShow').data('y', selection.y1);
            $('#imgShow').data('w', selection.width);
            $('#imgShow').data('h', selection.height);

        }
        /*图片上传完毕处理函数*/
        function showImage(file, serverData) {
            //动态调整img大小
            //var data = $.parseJSON(serverData);
            //$("#imgShow").css("background", "url('" + data.url + "') no-repeat").css("height",data.h).css("width",data.w);
            //显示图片缩略图
            $("#imgShow").attr("src", serverData).show();
            $("#btnCut").show();
        }
        function saveCutImage() {
            /*计算截取头像的范围*/
            var pars = {
                "x": $('#imgShow').data('x'),
                "y": $('#imgShow').data('y'),
                "w": $('#imgShow').data('w'),
                "h": $('#imgShow').data('h'),
                "action": "cut",
                "path": $("#imgShow").attr("src")
            }
            $.post("/ashx/upload.ashx", pars, function (data) {
                var serverData = $.parseJSON(data);
                if (serverData.msg == "ok") {
                    $("#imgCut").attr("src", serverData.path).show();
                } else {
                    alert(serverData.msg);
                }

            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="content">
            <div id="swfu_container" style="margin: 0px 10px;">
                <div>
                    <span id="spanButtonPlaceholder"></span>
                    <div id="divFileProgressContainer"></div>
                </div>
            </div>
            <img id="imgShow" src="#" style="height:300px;"/>
            <img id="imgCut" src="#" />
            <input type="button" id="btnCut" name="btnCut" value="保存截图" />
        </div>
    </form>
</body>
</html>

