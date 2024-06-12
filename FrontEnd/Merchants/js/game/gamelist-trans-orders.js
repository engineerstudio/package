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
    console.log(rq_data.TypeStr);
    $('[name="GameType"]').val(rq_data.TypeStr);

    var req_where = { 'GameType': rq_data.TypeStr } ;
    console.log(req_where)
    // 转账列表
    var tableIns = table.render({
        elem: '#game-trans-orders-list',
        url: '/mch/game/transferlogs',
        method: 'post',
        contentType: 'application/x-www-form-urlencoded',
        where: req_where,
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limit: 20,
        limits: [10, 15, 20, 25],
        id: "game-trans-orders-list",
        cols: [[
            //{ type: "checkbox", width: 50 },// fixed: "left",
            { field: 'Id', title: 'ID', width: 90, align: "center" },
            { field: 'UserId', title: '用户名称', width: 90, align: 'center' },
            { field: 'TypeStr', title: '游戏名称', width: 90, align: 'center' },
            { field: 'TransTypeStr', title: '转账类型', width: 90, align: 'center' },
            { field: 'StatusStr', title: '状态', width: 120, align: 'center' },
            { field: 'Money', title: '金额', width: 90, align: 'center' },
            { field: 'CreateTime', title: '转账时间', width: 180, align: 'center' },
            { title: '操作', templet: '#trans-orders-listBar', align: "center" } // fixed: "right",
        ]]
    });




    table.on('tool(game-trans-orders-list)', function (e) {
        var layEvent = e.event,
            data = e.data;
        console.log(data);
        return false;
        if (layEvent === 'unlock-money') { // 编辑

            $.post('/mch/game/unlocktranslog', { id: data.Id }, function (d) {
                d = JSON.parse(d);
                console.log(d);
                layer.msg(d.msg);
                tableIns.reload();
            });
        }
    });





    form.on("submit(gamelist-trans-order-search-submit)", function (e) {
        console.log(e);
        table.reload("game-trans-orders-list", {
            contentType: 'application/x-www-form-urlencoded',
            page: {
                curr: 1 //重新从第 1 页开始
            },
            where: e.field
        });

        return false;

        $.post("/mch/pay/manualfunds", data.field, function (res) {
            console.log(res);
            //top.layer.close(index);
            layer.closeAll("iframe");
            //刷新父页面
            parent.location.reload();
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