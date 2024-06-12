layui.use(['form', 'layer', 'table', 'laytpl'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl,
        table = layui.table;


    form.on('submit(dGame-list-search-submit)', function (e) {
        console.log(e.field);
        //return false;
        table.reload("dGame-list-table", {
            contentType: 'application/x-www-form-urlencoded',
            page: {
                curr: 1 //重新从第 1 页开始
            },
            where: e.field
        });
    })

    var tableIns = table.render({
        elem: '#dGame-list-table',
        url: '/mch/ds/gamed',
        method: 'post',
        contentType: 'application/json',
        where: { 'b': 'a' },
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limits: [10, 15, 20, 25],
        limit: 15,
        id: "dGame-list-table",
        cols: [[
            { field: 'GameName', title: '游戏平台', width: 160, align: "center" },
            { field: 'BetOrderCount', title: '投注数量', width: 160, align: "center" },
            { field: 'SettlementOrderCount', title: '结算数量', width: 160, align: "center" },
            { field: 'TotalBet', title: '投注金额', width: 160, align: "center" },
            { field: 'TatalValidBet', title: '有效投注', width: 160, align: "center" },
            { field: 'MerchantMoney', title: '平台盈亏', width: 160, align: "center" },
            { field: 'GameMoney', title: '游戏盈亏', width: 160, align: "center" }
        ]]
    });



    //列表操作
    table.on('tool(dGame-list-table)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;

        console.log(data);

        if (layEvent === 'edit') { // 支付商户配置
            var index = layui.layer.open({
                title: '编辑支付',
                type: 2,
                anim: 1,
                area: ['60%', '80%'],
                content: "dGame-merchant-manager.html?p=" + encodeURI(JSON.stringify(data)),
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






});
