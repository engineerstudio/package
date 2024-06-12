﻿layui.use(['form', 'layer', 'table', 'laytpl'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl,
        table = layui.table;


    form.on('submit(withdrawal-list-search-submit)', function (e) {
        console.log(e.field);
        //return false;
        table.reload("withdrawal-list-table", {
            contentType: 'application/x-www-form-urlencoded',
            page: {
                curr: 1 //重新从第 1 页开始
            },
            where: e.field
        });
    })

    var tableIns = table.render({
        elem: '#withdrawal-list-table',
        url: '/mch/withdrawals/loadmerchants',
        method: 'post',
        contentType: 'application/json',
        where: { 'q': 'a' },
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limits: [10, 15, 20, 25],
        limit: 15,
        id: "withdrawal-list-table",
        cols: [[
            { type: "checkbox", fixed: "left", width: 50 },
            { field: "Id", title: '标识', width: 80, align: "center" },
            { field: 'Name', title: '渠道名称', width: 160, align: "center" },
            { field: 'Description', title: '备注', width: 190, align: "center" },
            { title: '状态', templet: '#withdrawal-list-table-status', width: 120, align: 'center' },
            { title: '操作', minWidth: 80, templet: '#withdrawal-list-table-bar', fixed: "right", align: "center" }
        ]]
    });



    //列表操作
    table.on('tool(withdrawal-list-table)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;

        console.log(data);

        if (layEvent === 'edit') { // 支付商户配置
            var index = layui.layer.open({
                title: '编辑支付',
                type: 2,
                anim: 1,
                area: ['60%', '80%'],
                content: "withdrawal-merchant-manager.html?p=" + encodeURI(JSON.stringify(data)),
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

    // --- 同步支付类别  
    $(".withdrawal-async").on("click", function () {
        $.get('/mch/pay/paytypelist', function (d) {
            console.log(d);
        });
    });




    $(".withdrawal-merchant-manager").on("click", function () {
        var index = layui.layer.open({
            title: '配置支付渠道',
            type: 2,
            anim: 1,
            area: ['60%', '80%'],
            content: "withdrawal-merchant-manager.html?p={}",
            success: function (layero, index) {
                //var body = layui.layer.getChildFrame('body', index);
                //body.find("#Id").val(data.Id);
            }
        });
    });



    $(".withdrawal-type-manager").on("click", function () {
        var index = layui.layer.open({
            title: '支付类型配置',
            type: 2,
            anim: 1,
            area: ['60%', '80%'],
            content: "withdrawal-type-manager.html?p={}",
            success: function (layero, index) {
                //var body = layui.layer.getChildFrame('body', index);
                //body.find("#Id").val(data.Id);
            }
        });
    });


});
