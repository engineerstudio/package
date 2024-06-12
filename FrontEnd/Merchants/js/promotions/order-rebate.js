layui.use(['form', 'layer', 'table', 'laytpl'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl,
        table = layui.table;



    var p = getQueryVariable('p');
    var rq_data = JSON.parse(decodeURI(p));
    console.log(rq_data);
    //搜索【此功能需要后台配合，所以暂时没有动态效果演示】
    $(".search_btn").on("click", function () {
        if ($(".searchVal").val() !== '') {
            table.reload("qingdc-events-listTable", {
                page: {
                    curr: 1 //重新从第 1 页开始
                },
                where: {
                    key: $(".searchVal").val()  //搜索的关键字
                }
            });
        } else {
            layer.msg("请输入搜索的内容");
        }
    });

    var conditions = { AType: rq_data.TypeStr, OrderId: rq_data.Id };
    var tableIns = table.render({
        elem: '#promotions-order-rebate-list-table',
        url: '/mch/promotions/getrebateorderlist/',
        method: 'post',
        contentType: 'application/json',
        where: conditions,
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limits: [10, 15, 20, 25],
        limit: 15,
        id: "promotions-order-rebate-list-table",
        cols: [[
            { type: "checkbox", fixed: "left", width: 50 },
            { field: "Id", title: '标识', width: 80, align: "center" },
            { field: 'UserName', title: '用户名称', width: 190, align: "center" },
            { field: 'StatusDesc', title: '订单状态', width: 120, align: 'center' },
            { field: 'Reward', title: '订单金额', width: 120, align: 'center' },
            {
                title: '活动时间', minWidth: 50, templet: function (d) {
                    return '订单时间:' + d.CreateTime + '</br>' + '派奖时间:' + d.RewardTime;
                }, align: "center"
            },
            { title: '操作', minWidth: 80, templet: '#promotions-order-rebate-list-table-bar', fixed: "right", align: "center" }
        ]]
    });



    //列表操作
    table.on('tool(promotions-order-rebate-list-table)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;

        console.log(data);
        console.log(data.Id);
        console.log(layEvent);
        console.log(layEvent === 'details');
        if (layEvent === 'reward') { // 派奖判定

        } else if (layEvent === 'rebateorderdetails') { // 返水详细


        }
        //else if (layEvent === 'rebateorderdetails') { // 订单汇总

        //}

    });
    function getQueryVariable(variable) {
        var query = window.location.search.substring(1);
        var vars = query.split("&");
        for (var i = 0; i < vars.length; i++) {
            var pair = vars[i].split("=");
            if (pair[0] == variable) { return pair[1]; }
        }
        return (false);
    }

});
