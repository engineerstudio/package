
layui.use(['form', 'layer', 'table', 'laytpl'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl,
        table = layui.table;


    form.on('submit(area-noticelist-search-submit)', function (e) {
        console.log(e.field);
        //return false;
        table.reload("area-noticelist-table", {
            contentType: 'application/x-www-form-urlencoded',
            page: {
                curr: 1 //重新从第 1 页开始
            },
            where: e.field
        });
    })

    var tableIns = table.render({
        elem: '#area-noticelist-table',
        url: '/mch/areas/noticeload',
        method: 'post',
        contentType: 'application/json',
        where: { 'b': 'a' },
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limits: [10, 15, 20, 25],
        limit: 15,
        cols: [[
            { type: "checkbox", fixed: "left", width: 50 },
            { field: "Id", title: '标识', width: 80, align: "center" },
            { field: 'TypeDes', title: '类别', width: 160, align: "center" },
            { field: 'Title', title: '标题', width: 160, align: "center" },
            { field: 'CreateTime', title: '创建时间', width: 190, align: "center" },
            { title: '操作', minWidth: 80, templet: '#area-noticelist-table-bar', fixed: "right", align: "center" }
        ]]
    });


    //列表操作
    table.on('tool(area-noticelist-table)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;

        console.log(data);
        localStorage.setItem('area-notice-entity', encodeURI(JSON.stringify(data)));
        if (layEvent === 'edit') { // 支付商户配置
            var index = layui.layer.open({
                title: '编辑内容',
                type: 2,
                anim: 1,
                area: ['60%', '80%'],
                content: "notice-edit.html",
            });
        } else if (layEvent === 'del') { // 删除活动
            layer.confirm('确定删除此菜单？', { icon: 3, title: '提示信息' }, function (index) {
                del(data.Id);
            });

        }
    });

    function del(id) {
        $.post("/mch/areas/noticedel", { id: id }, function (res) {
            console.log(res);
            res = JSON.parse(res);
            top.layer.msg(res.msg);
            if (res.code == 0) {
                tableIns.reload();
                layer.closeAll("iframe");
            }
        })
    }

    $(".notice-content").on("click", function () {
        console.log('notice-content');
        var index = layui.layer.open({
            title: '添加内容',
            type: 2,
            anim: 1,
            area: ['60%', '80%'],
            content: "notice-edit.html",
        });
    });


});
