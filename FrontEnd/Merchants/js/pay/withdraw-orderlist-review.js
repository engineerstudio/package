layui.use(['form', 'layer', 'layedit', 'laydate', 'upload', 'element', 'table'], function () {
    var form = layui.form
    layer = parent.layer === undefined ? layui.layer : top.layer,
        laypage = layui.laypage,
        upload = layui.upload,
        layedit = layui.layedit,
        laydate = layui.laydate,
        element = layui.element,
        $ = layui.jquery,
        table = layui.table;


    var rq = getQueryVariable('p');
    var rq_data = JSON.parse(decodeURI(rq));
    console.log(rq_data);
    var withdrawinfo = 0, history = 0, betamount = 0;
    //  1. 获取用户的出款信息

    function load_withdrawinfo() {
        if (withdrawinfo == 1) return;
        var id = rq_data.Id;
        $.post("/mch/withdrawals/withdrawalsorderinfo", {
            id: id
        }, function (res) {
            console.log(res);
            res = JSON.parse(res);
            if (res.code == 1) {
                var d = res.info;
                $('.memberId').text(d.MemberId);
                $('.memberName').text(d.MemberName);
                $('.accountName').text(d.AccountName);
                $('.accountNo').text(d.AccountNo);
                $('.reqMoney').text(d.ReqWithdrawAmount);


                $('.sameBankCard').text(d.SameBankCard);
                $('.totalRecharge').text(d.TotalRecharge);
                $('.totalWithdrawals').text(d.TotalWithdrawals);
                $('.gameMoney').text(d.GameMoney);

                $('.isMemberProfit').text(d.MemberProfit);

                $('.fullBetAmount').text(d.FullBetAmount);

            }
            else if (res.code == 0) {
                layer.msg(res.msg);
            }
        });
        withdrawinfo = 1;
    }

    //  2. 监听tab页面切换

    element.on('tab(withdraw-orderlist-review-tabs)', function () {
        var layId = this.getAttribute('lay-id');
        console.log(layId);
        switch (layId) {
            case 'withraw-info':
                load_withdrawinfo();
                break;
            case 'withraw-record':
                load_withdrawhistory();
                break;
            case 'withraw-betamount':
                load_withdrawbetamount();
                break;
            default: break;
        }
    });

    //  3. 加载出款类型


    // 4.  确认出款,更改订单状态为可以出款状态
    $('.withdraw-confirm-submit').click(function (data) {
        $.post("/mch/withdrawals/confirmwithdrawalsorder", {
            orderId: rq_data.Id
        }, function (res) {
            console.log(res);
            res = JSON.parse(res);
            layer.msg(res.msg);
            layer.closeAll("iframe");
            //刷新父页面
            parent.location.reload();
        })
    });


    // 5.  拒绝出款
    $('.withdraw-cannel-submit').click(function () {
        console.log('cannel')
    });


    function load_withdrawhistory() {

        if (history == 1) return;

        var tableIns = table.render({
            elem: '#withdraw-orderlist-history-table',
            url: '/mch/withdrawals/history/',
            method: 'post',
            contentType: 'application/json',
            where: { memberId: rq_data.Id },
            cellMinWidth: 95,
            page: true,
            height: "full-125",
            limits: [10, 15, 20, 25],
            limit: 15,
            id: "withdraw-orderlist-history-table",
            cols: [[
                { field: "Id", title: '订单号', width: 80, align: "center" },
                { field: 'CreateTime', title: '申请时间', width: 190, align: "center" },
                { field: 'ConfirmTime', title: '到账时间', width: 190, align: "center" },
                { field: 'WithdrawMerchantName', title: '代付渠道', align: "center" },
                { field: 'ReqWithdrawAmount', title: '订单金额', width: 100, align: "center" },
                { field: 'WithdrawAmount', title: '实际金额', width: 100, align: "center" },
                { field: 'StatusDes', title: '订单状态', width: 100, align: "center" },
                { field: 'Marks', title: '备注', width: 190, align: "center" }
            ]]
        });

        history = 1;
    }

    function load_withdrawbetamount() {

    }


    load_withdrawinfo();
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