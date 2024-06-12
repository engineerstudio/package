layui.use(['form', 'layer', 'layedit', 'laydate', 'upload'], function () {
    var form = layui.form
    layer = parent.layer === undefined ? layui.layer : top.layer,
        laypage = layui.laypage,
        upload = layui.upload,
        layedit = layui.layedit,
        laydate = layui.laydate,
        $ = layui.jquery;


    form.on("submit(member-add-submit)", function (data) {
        //弹出loading
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });

        console.log(data.field);

        // 实际使用时的提交信息
        $.post("/mch/member/add", {
            AccountName: $("[name='AccountName']").val()
        }, function (res) {
            console.log(res);
            res = JSON.parse(res);
            layer.msg(res.msg);
            if (res.code == 1) {
                top.layer.close(index);
                top.layer.msg("用户添加成功！");
                layer.closeAll("iframe");
                //刷新父页面
                parent.location.reload();
            }
            //setTimeout(function () {
            //    top.layer.close(index);
            //    top.layer.msg("用户添加成功！");
            //    layer.closeAll("iframe");
            //    //刷新父页面
            //    parent.location.reload();
            //}, 500);

        })
        return false;
    })


}) 
