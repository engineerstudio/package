layui.use(['form', 'layer', 'table', 'laytpl'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl,
        table = layui.table;


    form.on('submit(agent-list-search-submit)', function (e) {
        console.log(e.field);
        //return false;
        table.reload("agent-list-table", {
            contentType: 'application/x-www-form-urlencoded',
            page: {
                curr: 1 //重新从第 1 页开始
            },
            where: e.field
        });
    })

    var tableIns = table.render({
        elem: '#agent-list-table',
        url: '/mch/member/agentload',
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
            { field: 'AccountName', title: '账户名', width: 160, align: "center" },
            { field: 'RebatePlan', title: '返点方案', width: 160, align: "center" },
            { field: 'ContractPlan', title: '合约方案', width: 160, align: "center" },
            { field: 'AgentId', title: '上级代理', width: 160, align: "center" },
            { field: 'Code', title: '推广码', width: 160, align: "center" },
            { field: 'IsDefault', title: '默认代理', width: 160, align: "center" },
            { field: 'CreateTime', title: '创建时间', width: 190, align: "center" },
            { title: '操作', minWidth: 80, templet: '#agent-list-table-bar', fixed: "right", align: "center" }
        ]]
    });



    //列表操作
    table.on('tool(agent-list-table)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;

        console.log(data);
        localStorage.setItem('agent-modify-data', encodeURI(JSON.stringify(data)));
        if (layEvent === 'edit') { // 支付商户配置
            var index = layui.layer.open({
                title: '代理编辑',
                type: 2,
                anim: 1,
                area: ['60%', '80%'],
                content: "list-modify.html"
            });
        } else if (layEvent === 'del') { // 删除活动
            // layer.confirm('确定删除此菜单？', { icon: 3, title: '提示信息' }, function (index) {
            //     del(data.Id);
            // });
            layer.msg('未实现del');
        }
    });


    $(".modify-agent").on("click", function () {
        console.log('代理编辑');
        localStorage.setItem('agent-modify-data', '{}');
        var index = layui.layer.open({
            title: '编辑代理',
            type: 2,
            anim: 1,
            area: ['60%', '80%'],
            content: "list-modify.html"
        });
    });



});
