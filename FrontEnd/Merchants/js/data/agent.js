layui.use(['form', 'layer', 'table', 'laytpl'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl,
        table = layui.table;


    form.on('submit(dAgent-list-search-submit)', function (e) {
        console.log(e.field);
        //return false;
        table.reload("dAgent-list-table", {
            contentType: 'application/x-www-form-urlencoded',
            page: {
                curr: 1 //重新从第 1 页开始
            },
            where: e.field
        });
    })

    var tableIns = table.render({
        elem: '#dAgent-list-table',
        url: '/mch/ds/agentd',
        method: 'post',
        contentType: 'application/json',
        where: { 'b': 'a' },
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limits: [10, 15, 20, 25],
        limit: 15,
        id: "dAgent-list-table",
        cols: [[
            { field: "Id", title: '标识', width: 80, align: "center" },
            { field: 'AgentName', title: '代理名称', width: 160, align: "center" },
            { field: 'SubMemberCount', title: '下级数量', width: 160, align: "center" },
            { field: 'NewMemberCount', title: '新增注册', width: 160, align: "center" },
            { field: 'TotalRecharge', title: '总充值', width: 160, align: "center" },
            { field: 'TotalWithdrawal', title: '总提现', width: 160, align: "center" },
            { field: 'TotalBet', title: '总投注', width: 160, align: "center" },
            { field: 'TatalValidBet', title: '总有效投注', width: 160, align: "center" },
            { field: 'Money', title: '游戏盈亏', width: 160, align: "center" }
        ]]
    });



    //列表操作
    table.on('tool(dAgent-list-table)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;

        console.log(data);

        if (layEvent === 'edit') { // 支付商户配置
            var index = layui.layer.open({
                title: '编辑支付',
                type: 2,
                anim: 1,
                area: ['60%', '80%'],
                content: "dAgent-merchant-manager.html?p=" + encodeURI(JSON.stringify(data)),
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
    $(".dAgent-async").on("click", function () {
        $.get('/mch/dAgent/dAgenttypelist', function (d) {
            console.log(d);
        });
    });




    $(".dAgent-merchant-manager").on("click", function () {
        var index = layui.layer.open({
            title: '配置支付渠道',
            type: 2,
            anim: 1,
            area: ['60%', '80%'],
            content: "dAgent-merchant-manager.html?p={}",
            success: function (layero, index) {
                //var body = layui.layer.getChildFrame('body', index);
                //body.find("#Id").val(data.Id);
            }
        });
    });



    $(".dAgent-type-manager").on("click", function () {
        var index = layui.layer.open({
            title: '支付类型配置',
            type: 2,
            anim: 1,
            area: ['60%', '80%'],
            content: "dAgent-type-manager.html?p={}",
            success: function (layero, index) {
                //var body = layui.layer.getChildFrame('body', index);
                //body.find("#Id").val(data.Id);
            }
        });
    });


});
