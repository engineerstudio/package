layui.config({
    base: "https://cdn.wwzznn.com/layuicms/js/extentions/"
}).extend({
    'ext': 'ext',
    "jqueryext": "jqueryext"
});

layui.use(['form', 'layer', 'table', 'laytpl', 'laydate', 'jqueryext', 'ext'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl,
        laydate = layui.laydate,
        ext = layui.ext,
        table = layui.table;


    laydate.render({
        elem: '#DepositTime'
        , type: 'datetime'
        , range: '~' //或 range: '~' 来自定义分割字符
    });
    laydate.render({
        elem: '#FinishTime'
        , type: 'datetime'
        , range: '~' //或 range: '~' 来自定义分割字符
    });


    form.on('submit(withdraw-order-search-submit)', function (e) {
        console.log(e.field);

        var WithdrawStartTime = '';
        var WithdrawEndTime = '';
        var finishStartTime = '';
        var finishEndTime = '';
        if (e.field['DepositTime'].length != 0) {
            var arr = e.field['DepositTime'].split('~');
            WithdrawStartTime = arr[0];
            WithdrawEndTime = arr[1];
        }
        if (e.field['FinishTime'].length != 0) {
            var arr = e.field['FinishTime'].split('~');
            finishStartTime = arr[0];
            finishEndTime = arr[1];
        }

        var pStr = {
            Id: e.field['Id'] == '' ? 0 : e.field['Id'],
            WithdrawMerchantOrderId: e.field['WithdrawMerchantOrderId'],
            AccountName: e.field['AccountName'],
            WithdrawStartTime: WithdrawStartTime,
            WithdrawEndTime: WithdrawEndTime,
            FinishStartTime: finishStartTime,
            FinishEndTime: finishEndTime
        };
        console.log(pStr);


        //return false;

        table.reload("withdraw-order-list-table", {
            contentType: 'application/x-www-form-urlencoded',
            page: {
                curr: 1 //重新从第 1 页开始
            },
            where: pStr
        });
    })

    var tableIns = table.render({
        elem: '#withdraw-order-list-table',
        url: '/mch/withdrawals/withdrawalsorderload',
        method: 'post',
        contentType: 'application/json',
        where: { 'b': 'a' },
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limits: [10, 15, 20, 25],
        limit: 15,
        id: "withdraw-order-list-table",
        cols: [[
            { type: "checkbox", fixed: "left", width: 50 },
            { field: "Id", title: '订单号', width: 80, align: "center" },
            { field: 'WithdrawMerchantOrderId', title: '商户订单号', width: 160, align: "center" },
            { field: 'AccountName', title: '会员名称', width: 120, align: "center" },
            { field: 'WithdrawMerchantName', title: '代付渠道', width: 120, align: "center" },
            { field: 'ReqWithdrawAmount', title: '订单金额', width: 160, align: "center" },
            { field: 'WithdrawAmount', title: '实际金额', width: 160, align: "center" },
            { field: 'CreateTime', title: '申请时间', width: 190, align: "center" },
            { field: 'ConfirmTime', title: '到账时间', width: 190, align: "center" },
            { field: 'StatusDes', title: '订单状态', width: 90, align: "center" },
            { title: '操作', minWidth: 80, templet: '#withdraw-order-list-table-bar', fixed: "right", align: "center" }
        ]]
    });



    //列表操作
    table.on('tool(withdraw-order-list-table)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;

        console.log(data);

        if (layEvent === 'check-order') { // 出款核对
            var index = layui.layer.open({
                title: '出款管理',
                type: 2,
                anim: 1,
                area: ['60%', '80%'],
                content: "withdraw-orderlist-review.html?p=" + encodeURI(JSON.stringify(data)),
                success: function (layero, index) {
                }
            });
        }
        else if (layEvent === 'confirm-order') {// 确认订单
            layui.layer.open({
                title: '出款渠道',
                type: 2,
                anim: 1,
                area: ['60%', '80%'],
                content: "withdraw-orderlist-review-confirm.html?p=" + encodeURI(JSON.stringify(data)),
                success: function (layero, index) {
                }
            });

        } else if (layEvent === 'cannel-order') {// 取消订单

            $.post("/mch/withdrawal/cannelorder", {
                orderId: data.field['Id']
            }, function (res) {
                console.log(res);
                res = JSON.parse(res);
                top.layer.msg(res.msg);
                //刷新父页面
                parent.location.reload();
            })
        }




    });



});
