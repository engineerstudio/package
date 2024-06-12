layui.config({
    base: "https://cdn.wwzznn.com/layuicms/js/extentions/"
}).extend({
    'ext': 'ext',
    "jqueryext": "jqueryext"
});
layui.use(['form', 'layer', 'table', 'laytpl', 'jqueryext', 'ext'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl,
        ext = layui.ext,
        table = layui.table;


    form.on('submit(protag-list-submit)', function (e) {
        console.log(e.field);

        $.post("/mch/promotions/savetag", {
            Id: e.field["Id"],
            Name: e.field['Name'],
            Sort: e.field['Sort']
        }, function (res) {
            console.log(res);
            res = JSON.parse(res);
            layer.msg(res.msg);
            if (res.code == 1) {
                //top.layer.close(index);
                //layer.closeAll("iframe");
                ////刷新父页面
                //parent.location.reload();
                tableIns.reload();
            }
        })

    });

    var tableIns = table.render({
        elem: '#protag-list-table',
        url: '/mch/promotions/tagload',
        method: 'post',
        contentType: 'application/json',
        where: { 'b': 'a' },
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limits: [10, 15, 20, 25],
        limit: 15,
        cols: [[
            { field: "Id", title: '标识', width: 80, align: "center" },
            { field: 'Name', title: '标签名称', width: 160, align: "center" },
            { field: 'Sort', title: '排序序号', width: 120, align: "center" },
            { field: 'CreateTime', title: '创建时间', width: 190, align: "center" },
            { title: '操作', minWidth: 80, templet: '#protag-list-table-bar', fixed: "right", align: "center" }
        ]]
    });


    table.on('tool(protag-list-table)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;

        console.log(layEvent);
        console.log(data);
        if (layEvent === 'edit') {
            $('[name="Name"]').val(data.Name);
            $('[name="Sort"]').val(data.Sort);
            $('[name="Id"]').val(data.Id);
        } else if (layEvent === 'del') {
            $.post("/mch/promotions/deletetag", {
                Id: $("#Id").val()
            }, function (res) {
                console.log(res);
                res = JSON.parse(res);
                layer.msg(res.msg);
                if (res.code == 1) {
                    tableIns.reload();
                }
            })
        }
    });




});


