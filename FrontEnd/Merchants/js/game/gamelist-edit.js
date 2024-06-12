layui.use(['form', 'layer', 'layedit', 'table', 'laydate', 'upload'], function () {
    var form = layui.form
    layer = parent.layer === undefined ? layui.layer : top.layer,
        laypage = layui.laypage,
        upload = layui.upload,
        layedit = layui.layedit,
        laydate = layui.laydate,
        table = layui.table,
        $ = layui.jquery;


    var rq = getQueryVariable('p');
    var rq_data = JSON.parse(decodeURI(rq));
    console.log(rq_data);
    $('[name="Name"]').val(rq_data.TypeDesc);
    $('[name="fee"]').val(rq_data.Rate);
    if (rq_data.Enabled) {
        $('[name="Enabled"]').attr('checked', true);
    }
    form.render();


    form.on("submit(gamelist-edit-submit)", function (data) {
        console.log(data);
        console.log(data.field['Enabled'] );
        $.post("/mch/game/UpdateMerchantGameStatus", { gameTypeStr: rq_data.TypeStr, enabled: data.field['Enabled'] == 'on' ? true : false }, function (res) {
            console.log(res);
            //top.layer.close(index);
            //top.layer.msg("文章添加成功！");
            layer.closeAll("iframe");
            //刷新父页面
            parent.location.reload();
        })


        return false;
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