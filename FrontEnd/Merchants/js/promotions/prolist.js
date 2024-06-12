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

    $('[name="AType"]').html(ext.getSelectOptions('ActivityType', '请选择活动类型'));

    form.render();
    form.on('submit(promotions-list-search)', function (e) {
        console.log(e.field);
        console.log(e.field['ShowDeletedPro']);
        //return false;
        if (e.field['ShowDeletedPro'] == 'on') e.field['ShowDeletedPro'] = true;
        else e.field['ShowDeletedPro'] = false;
        table.reload("promotions-list-table", {
            contentType: 'application/x-www-form-urlencoded',
            page: {
                curr: 1 //重新从第 1 页开始
            },
            where: e.field
        });
        return false;
    });
    console.log(ShowDeletedPro())

    var tableIns = table.render({
        elem: '#promotions-list-table',
        url: '/mch/promotions/load',
        method: 'post',
        contentType: 'application/x-www-form-urlencoded',
        where: { 'ShowDeletedPro': true },
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limits: [10, 15, 20, 25],
        limit: 15,
        id: "promotions-list-table",
        cols: [[
            { type: "checkbox", fixed: "left", width: 50 },
            { field: "Id", title: '标识', width: 80, align: "center" },
            { field: 'TypeDesc', title: '活动类型', width: 160, align: "center" },
            { field: 'TagName', title: '活动标签', width: 160, align: "center" },
            { field: 'Title', title: '活动名称', width: 190, align: "center" },
            { title: '状态', templet: '#promotions-list-table-status', width: 120, align: 'center' },
            { title: '显示', templet: '#promotions-list-table-visible', width: 120, align: 'center' },
            {
                title: '活动时间', minWidth: 50, templet: function (d) {
                    return d.StartTime + '-' + d.EndTime;
                }, align: "center"
            },
            { title: '操作', minWidth: 80, templet: '#promotions-list-table-bar', fixed: "right", align: "center" }
        ]]
    });

    function ShowDeletedPro() {
        return $('[name="ShowDeletedPro"]').is(':checked');
    }

    //列表操作
    table.on('tool(promotions-list-table)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;

        console.log(data);

        localStorage.setItem('prolist-edit', encodeURI(JSON.stringify(data)))

        if (layEvent === 'edit') { // 编辑活动详细
            var index = layui.layer.open({
                title: '编辑活动',
                type: 2,
                anim: 1,
                area: ['60%', '80%'],
                content: "pro-createpage.html"
            });
        } else if (layEvent === 'details') { // 编辑活动规则

            var page_str = 'pro-config-' + data.TypeStrToLower.toLowerCase()
            console.log(page_str);
            var index = layui.layer.open({
                title: '规则设定',
                type: 2,
                anim: 1,
                area: ['60%', '80%'],
                content: page_str + ".html",
                success: function (layero, index) {
                    //var body = layui.layer.getChildFrame('body', index);
                    //body.find("#Id").val(data.Id);
                }
            });

        } else if (layEvent === 'orders') { // 订单汇总
            console.log('orders');
            var index = layui.layer.open({
                title: '订单汇总',
                type: 2,
                anim: 1,
                area: ['60%', '80%'],
                content: "pro-ordersummary.html"
            });
        } else if (layEvent === 'del') { // 删除活动
            layer.confirm('确定标记删除此活动？', { icon: 3, title: '提示信息' }, function (index) {

                // deltepro
                $.post("/mch/promotions/deltepro", {
                    id: data.Id
                }, function (res) {
                    console.log(res);
                    res = JSON.parse(res);
                    layer.msg(res.msg);
                    top.layer.close(index);
                    //parent.location.reload();
                    return false;
                })
                return false;
            });
        }
    });

    // --- 添加活动页面  
    $(".promotions-btn-create").on("click", function () {
        var index = layui.layer.open({
            title: '添加活动',
            type: 2,
            anim: 1,
            area: ['60%', '80%'],
            content: "pro-createpage.html",
            success: function (layero, index) {
            }
        });
    });
    // --- 添加活动标签页面  
    $(".promotionstag-btn-create").on("click", function () {
        var index = layui.layer.open({
            title: '活动标签管理',
            type: 2,
            anim: 1,
            area: ['60%', '80%'],
            content: "protaglist.html",
            success: function (layero, index) {
            }
        });
    });


});
