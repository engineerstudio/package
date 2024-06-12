layui.use(['form', 'layer', 'layedit', 'laydate', 'upload'], function () {
    var form = layui.form
    layer = parent.layer === undefined ? layui.layer : top.layer,
        laypage = layui.laypage,
        upload = layui.upload,
        layedit = layui.layedit,
        laydate = layui.laydate,
        $ = layui.jquery;

    var rq = getQueryVariable('p');
    var rq_data = JSON.parse(decodeURI(rq));
    console.log(rq_data);
    if (rq_data) {
        $('[name="GroupName"]').val(rq_data.GroupName);
        //$('[name="Enabled"]').val(rq_data.Enabled);
        $('[name="Enabled"]').attr("checked", rq_data.Enabled);
        //$('[name="IsDefault"]').val(rq_data.IsDefault);
        $('[name="IsDefault"]').attr("checked", rq_data.IsDefault);
        $('[name="Description"]').val(rq_data.Description);
        $('[name="SortNo"]').val(rq_data.SortNo);
        $('[name="Id"]').val(rq_data.Id);
        form.render();
    }

    form.on("submit(member-vip-add-submit)", function (data) {
        var field = data.field;
        field.Enabled = field.Enabled == 'on' ? true : false;
        field.IsDefault = field.IsDefault == 'on' ? true : false;
        console.log(field);
        //var s = JSON.parse(field)
        //return false;
        //弹出loading
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        // 实际使用时的提交信息
        $.ajax({
            url: "/mch/vips/add",
            data: field,
            type: 'POST',
            success: function (res) {
                console.log(res);
                res = JSON.parse(res);
                if (res.code == 1) {
                    setTimeout(function () {
                        top.layer.close(index);
                        top.layer.msg(res.msg);
                        layer.closeAll("iframe");
                        //刷新父页面
                        parent.location.reload();
                    }, 500);
                } else {
                    top.layer.close(index);
                    top.layer.msg(res.msg);
                }
            },
            error: function (res) {
                console.log(res);
                if (res.code == 1) {
                    setTimeout(function () {
                        top.layer.close(index);
                        top.layer.msg(res.msg);
                        layer.closeAll("iframe");
                        //刷新父页面
                        parent.location.reload();
                    }, 500);
                } else {
                    top.layer.close(index);
                    top.layer.msg(res.msg);
                }

            }
        })

        return false;
    })


    function getQueryVariable(variable) {
        var query = window.location.search.substring(1);
        var vars = query.split("&");
        for (var i = 0; i < vars.length; i++) {
            var pair = vars[i].split("=");
            if (pair[0] == variable) { return pair[1]; }
        }
        return (false);
    }
})