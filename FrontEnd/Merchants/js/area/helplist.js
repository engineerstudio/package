layui.use(['form', 'layer', 'table', 'laytpl'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl,
        table = layui.table;


    form.on('submit(area-helplist-search-submit)', function (e) {
        console.log(e.field);
        //return false;
        table.reload("area-helplist-table", {
            contentType: 'application/x-www-form-urlencoded',
            page: {
                curr: 1 //重新从第 1 页开始
            },
            where: e.field
        });
    })

    var tableIns = table.render({
        elem: '#area-helplist-table',
        url: '/mch/areas/helparealist',
        method: 'post',
        contentType: 'application/json',
        where: { 'b': 'a' },
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limits: [10, 15, 20, 25],
        limit: 15,
        id: "area-helplist-table",
        cols: [[
            { type: "checkbox", fixed: "left", width: 50 },
            { field: "Id", title: '标识', width: 80, align: "center" },
            { field: 'TypeName', title: '类别', width: 160, align: "center" },
            { field: 'Title', title: '标题', width: 160, align: "center" },
            { field: 'CreateTime', title: '创建时间', width: 190, align: "center" },
            { title: '操作', minWidth: 80, templet: '#area-helplist-table-bar', fixed: "right", align: "center" }
        ]]
    });



    //列表操作
    table.on('tool(area-helplist-table)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;

        console.log(data);

        if (layEvent === 'edit') { // 支付商户配置
            var index = layui.layer.open({
                title: '编辑支付',
                type: 2,
                anim: 1,
                area: ['60%', '80%'],
                content: "help-content.html?id=" + data.Id + '&TypeId=' + data.TypeId,
                success: function (layero, index) {
                    //var body = layui.layer.getChildFrame('body', index);
                    //body.find("#Id").val(data.Id);
                }
            });
        } else if (layEvent === 'orders') { // 订单汇总

            layer.msg('未实现orders');

        } else if (layEvent === 'del') { // 删除活动
            // layer.confirm('确定删除此菜单？', { icon: 3, title: '提示信息' }, function (index) {
            //     del(data.Id);
            // });
            layer.msg('未实现del');
        }
    });


    $(".help-content").on("click", function () {
        console.log('help-content');
        var index = layui.layer.open({
            title: '添加区域内容',
            type: 2,
            anim: 1,
            area: ['60%', '80%'],
            content: "help-content.html?p={}",
            success: function (layero, index) {
                //var body = layui.layer.getChildFrame('body', index);
                //body.find("#Id").val(data.Id);
            }
        });
    });


    $(".help-type-manager").on("click", function () {
        var index = layui.layer.open({
            title: '帮助类型配置',
            type: 2,
            anim: 1,
            area: ['60%', '80%'],
            content: "help-type-manager.html?p={}",
            success: function (layero, index) {
                //var body = layui.layer.getChildFrame('body', index);
                //body.find("#Id").val(data.Id);
            }
        });
    });


});
