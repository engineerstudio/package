layui.config({
    base: "https://cdn.wwzznn.com/layuicms/js/extentions/"
}).extend({
    'ext': 'ext',
    "jqueryext": "jqueryext",
    "formExt": "formext"
});
layui.use(['form', 'layer', 'layedit', 'laydate', 'upload', 'table', 'element', 'upload', 'formExt'], function () {
    var form = layui.form
    layer = parent.layer === undefined ? layui.layer : top.layer,
        laypage = layui.laypage,
        upload = layui.upload,
        layedit = layui.layedit,
        laydate = layui.laydate,
        element = layui.element,
        formExt = layui.formExt,
        $ = layui.jquery,
        table = layui.table;


    var rq = getQueryVariable('p');
    var rq_data = JSON.parse(decodeURI(rq));
    console.log(rq_data);
    formExt.upload('.upload-btn', { url: '/mch/upload/image' }, function (seclector, data) {
        seclector.prev().attr('src', data.info.src);
    });

    // 支付类型列表 

    var tablePayType = table.render({
        elem: '#pay-type-list-table',
        url: '/mch/pay/loadPayType',
        method: 'post',
        contentType: 'application/json',
        where: { 'b': 'a' },
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limits: [10, 15, 20, 25],
        limit: 15,
        id: "pay-type-list-table",
        cols: [[
            { type: "checkbox", fixed: "left", width: 50 },
            { field: "Id", title: '标识', width: 80, align: "center" },
            { field: 'Name', title: '类别名称', width: 160, align: "center" },
            { title: '图片', templet: '#pay-type-manager-list-table-img', width: 160, align: "center" },
            { title: '是否启用', width: 120, align: 'center', templet: '#pay-type-manager-list-table-status' },
            { title: '操作', minWidth: 80, templet: '#pay-type-manager-list-table-bar', fixed: "right", align: "center" }
        ]]
    });


    //列表操作
    table.on('tool(pay-type-list-table)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;

        console.log(layEvent);
        console.log(data);

        if (layEvent === 'pay-type-manager-enabled') {
            updatestatus(data.Id, false);
        }
        else if (layEvent === 'pay-type-manager-disabled') {
            updatestatus(data.Id, true);
        }
        else if (layEvent == 'edit') {
            console.log('edit');
            $('#ImgUrl').attr('src', data.PicUrl)
            $('[name="name"]').val(data.Name)
            $('[name="Id"]').val(data.Id)
        }

    });

    function updatestatus(id, enabled) {
        $.post("/mch/pay/updatecatetorytypestatus", {
            enabled: enabled,
            id: id
        }, function (res) {
            console.log(res);
            res = JSON.parse(res);
            layer.msg(res.msg);
            if (res.code == 1) {
                tablePayType.reload();
            }
        })
    }


    form.on("submit(pay-type-manager-submit)", function (data) {
        //弹出loading
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        // 实际使用时的提交信息
        $.post("/mch/pay/savepaytypecat", {
            url: $('#ImgUrl').attr('src'),
            name: $('[name="name"]').val(),
            id: $('[name="Id"]').val()
        }, function (res) {
            console.log(res);
            res = JSON.parse(res);
            top.layer.close(index);
            if (res.code == 1) {
                tablePayType.reload();
            }
            top.layer.msg(res.msg);
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