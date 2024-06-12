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

    // paytypecatedic
    $('[name="PayTypeCategoryId"]').html(ext.getSelectOptionsByUrl('/mch/pay/paytypecatedic', '请选择搜索的支付类别'));
    $('[name="PayMerchantId"]').html(ext.getSelectOptionsByUrl('/mch/pay/paymerchantdic', '请选择搜索的支付'));

    form.render();

    //搜索
    form.on('submit(pay-order-list-search-submit)', function (e) {
        console.log(e.field)
        var depositStartTime = '';
        var depositEndTime = '';
        var finishStartTime = '';
        var finishEndTime = '';
        if (e.field['DepositTime'].length != 0) {
            var arr = e.field['DepositTime'].split('~');
            depositStartTime = arr[0];
            depositEndTime = arr[1];
        }
        if (e.field['FinishTime'].length != 0) {
            var arr = e.field['FinishTime'].split('~');
            finishStartTime = arr[0];
            finishEndTime = arr[1];
        }
        var pStr = {
            MerchantId: 0,
            AccountName: e.field['AccountName'],
            PayMerchantId: e.field['PayMerchantId'] == '' ? 0 : e.field['PayMerchantId'],
            PayTypeCategoryId: e.field['PayTypeCategoryId'] == '' ? 0 : e.field['PayTypeCategoryId'],
            DepositStartTime: depositStartTime,
            DepositEndTime: depositEndTime,
            FinishStartTime: finishStartTime,
            FinishEndTime: finishEndTime
        };
        console.log(pStr);
        table.reload("pay-order-list-table", {
            contentType: 'application/x-www-form-urlencoded',
            page: {
                curr: 1 //重新从第 1 页开始
            },
            where: pStr
        });
    });

    console.log('load table ')
    var tableIns = table.render({
        elem: '#pay-order-list-table',
        url: '/mch/pay/orderload',
        method: 'post',
        contentType: 'application/json',
        //where: { 'b': 'a' },
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limits: [10, 15, 20, 25],
        limit: 15,
        id: "pay-order-list-table",
        cols: [[
            { type: "checkbox", fixed: "left", width: 50 },
            { field: "Id", title: '标识', width: 80, align: "center" },
            //{ field: 'MerchantId', title: '订单号', width: 100, align: "center" },
            { field: 'AccountName', title: '会员', width: 120, align: "center" },
            { field: 'PayTypeCategory', title: '支付类型', width: 160, align: "center" },
            { field: 'PayMerchantName', title: '支付渠道', width: 160, align: "center" },
            { field: 'ReqDepositAmount', title: '订单金额', width: 190, align: "center" },
            { field: 'DepositAmount', title: '实际金额', width: 190, align: "center" },
            { field: 'StatusStr', title: '是否到账', width: 90, align: "center" },
            { field: 'CreateTime', title: '充值时间', width: 190, align: "center" },
            { field: 'ConfirmTime', title: '到账时间', width: 190, align: "center" },
            { title: '操作', minWidth: 80, templet: '#pay-order-list-table-bar', fixed: "right", align: "center" }
        ]]
    });



    //列表操作
    table.on('tool(pay-order-list-table)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;

        console.log(data);

        if (layEvent === 'confirm-order') { // 确认订单
            var index = layui.layer.open({
                title: '编辑支付',
                type: 2,
                anim: 1,
                area: ['60%', '80%'],
                content: "order-confirm-order.html?p=" + encodeURI(JSON.stringify(data)),
                success: function (layero, index) {
                    //var body = layui.layer.getChildFrame('body', index);
                    //body.find("#Id").val(data.Id);
                }
            });
        }
    });

    // --- 同步支付类别  
    $(".pay-async").on("click", function () {
        $.get('/mch/pay/paytypelist', function (d) {
            console.log(d);
        });
    });




    //$(".pay-merchant-manager").on("click", function () {
    //    var index = layui.layer.open({
    //        title: '配置支付渠道',
    //        type: 2,
    //        anim: 1,
    //        area: ['60%', '80%'],
    //        content: "pay-merchant-manager.html?p={}",
    //        success: function (layero, index) {
    //            //var body = layui.layer.getChildFrame('body', index);
    //            //body.find("#Id").val(data.Id);
    //        }
    //    });
    //});



    //$(".pay-type-manager").on("click", function () {
    //    var index = layui.layer.open({
    //        title: '支付类型配置',
    //        type: 2,
    //        anim: 1,
    //        area: ['60%', '80%'],
    //        content: "pay-type-manager.html?p={}",
    //        success: function (layero, index) {
    //            //var body = layui.layer.getChildFrame('body', index);
    //            //body.find("#Id").val(data.Id);
    //        }
    //    });
    //});


});
