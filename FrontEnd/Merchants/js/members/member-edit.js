layui.config({
    base: "https://cdn.wwzznn.com/layuicms/js/extentions/"
}).extend({
    'ext': 'ext',
    "jqueryext": "jqueryext"
});
layui.use(['form', 'layer', 'layedit', 'laydate', 'upload', 'jqueryext', 'ext'], function () {
    var form = layui.form
    layer = parent.layer === undefined ? layui.layer : top.layer,
        laypage = layui.laypage,
        upload = layui.upload,
        layedit = layui.layedit,
        laydate = layui.laydate,
        ext = layui.ext,
        $ = layui.jquery;


    var rq = getQueryVariable('p');
    var rq_data = JSON.parse(decodeURI(rq));
    console.log(rq_data);
    // groupdic
    $("[name='AccountName']").val(rq_data.AccountName);
    $('[name="GroupId"]').html(ext.getSelectOptionsByUrl('/mch/vips/groupdic', '请选择分组'));
    $('[name="GroupId"]').val(rq_data.GroupId);
    form.render();

    // 保存分组信息
    $('.member-edit-save-membergroup').on('click', function () {
        //弹出loading
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        // 实际使用时的提交信息
        $.post("/mch/member/updateinfo", {
            GroupId: $("[name='GroupId']").val(), MemberId: rq_data.Id
        }, function (res) {
            console.log(res);
            res = JSON.parse(res);
            layer.msg(res.msg);
            if (res.code == 1) {
                top.layer.close(index);
                top.layer.msg("保存分组信息成功！");
                layer.closeAll("iframe");
                //刷新父页面
                parent.location.reload();
            }
        })
        return false;
    });


    $('.layui-btn-sm').on('click', function () {
        console.log($(this).attr('name'));
        if ($(this).attr('name') == 'undefined') {
            return false;
            return;
        }

        switch ($(this).attr('name')) {
            case 'forbid-login':
                break;
            case 'forbid-game':
                break;
            case 'reset-login-psw':
                break;
            case 'reset-fund-psw':
                break;
        }

    });









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
