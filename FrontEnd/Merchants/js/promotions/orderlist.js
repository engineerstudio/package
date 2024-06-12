layui.config({
    base: "https://cdn.wwzznn.com/layuicms/js/extentions/"
}).extend({
    'ext': 'ext',
    "jqueryext": "jqueryext"
});
layui.use(['form', 'layer', 'table', 'laydate', 'laytpl', 'jqueryext', 'ext'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl,
        laydate = layui.laydate,
        ext = layui.ext,
        table = layui.table;


    $('[name="AType"]').html(ext.getSelectOptions('FundLogType_Promotions', '请选择搜索的活动类型'));
    $('[name="Status"]').html(ext.getSelectOptions('ActivityOrderStatus', '请选择搜索的订单状态'));

    form.render();

    form.on('submit(promotions-orders-list-search)', function (e) {
        //return false;
        //if (e.field['AType'] == '') e.field['AType'] = 0;
        if (e.field['MemberId'] == '') e.field['MemberId'] = 0;
        if (e.field['PromotionId'] == '') e.field['PromotionId'] = 0;
        //if (e.field['Status'] == '') e.field['Status'] = 0;
        console.log(e.field)

        table.reload("promotions-orders-list-table", {
            contentType: 'application/x-www-form-urlencoded',
            page: {
                curr: 1 //重新从第 1 页开始
            },
            where: e.field
        });

        return false;
    });


    ////搜索【此功能需要后台配合，所以暂时没有动态效果演示】
    //$(".search_btn").on("click", function () {
    //    if ($(".searchVal").val() !== '') {
    //        table.reload("qingdc-events-listTable", {
    //            page: {
    //                curr: 1 //重新从第 1 页开始
    //            },
    //            where: {
    //                key: $(".searchVal").val()  //搜索的关键字
    //            }
    //        });
    //    } else {
    //        layer.msg("请输入搜索的内容");
    //    }
    //});


    var tableIns = table.render({
        elem: '#promotions-orders-list-table',
        url: '/mch/promotions/getorderlist',
        method: 'post',
        contentType: 'application/json',
        where: { 'b': 'a' },
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limits: [10, 15, 20, 25],
        limit: 15,
        id: "promotions-orders-list-table",
        cols: [[
            { type: "checkbox", fixed: "left", width: 50 },
            { field: "Id", title: '标识', width: 80, align: "center" },
            { field: 'TypeDesc', title: '活动类型', width: 160, align: "center" },
            { field: 'PromotionName', title: '活动名称', width: 190, align: "center" },
            { field: 'UserName', title: '会员名称', width: 190, align: "center" },
            { field: 'StatusDesc', title: '订单状态', width: 120, align: 'center' },
            { field: 'Reward', title: '订单金额', width: 120, align: 'center' },
            {
                title: '活动时间', minWidth: 50, templet: function (d) {
                    return '订单时间:' + d.CreateTime + '</br>' + '派奖时间:' + d.RewardTime;
                }, align: "center"
            },
            { title: '操作', minWidth: 80, templet: '#promotions-orders-list-table-bar', fixed: "right", align: "center" }
        ]]
    });



    //列表操作
    table.on('tool(promotions-orders-list-table)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;

        console.log(data);
        console.log(data.Id);
        console.log(layEvent);
        console.log(layEvent === 'details');
        if (layEvent === 'reward') { // 派奖判定
            var index = layui.layer.open({
                title: '比赛入库',
                type: 2,
                anim: 1,
                area: ['60%', '80%'],
                content: "pro-createpage.html?p=" + encodeURI(JSON.stringify(data)),
                success: function (layero, index) {
                    //var body = layui.layer.getChildFrame('body', index);
                    //body.find("#Id").val(data.Id);
                }
            });
        } else if (layEvent === 'rebateorderdetails') { // 返水详细

            var page_str = 'order-' + data.TypeStrToLower.toLowerCase()
            var index = layui.layer.open({
                title: '返水订单明细',
                type: 2,
                anim: 1,
                area: ['60%', '80%'],
                content: page_str + ".html?p=" + encodeURI(JSON.stringify(data)),
                success: function (layero, index) {
                    //var body = layui.layer.getChildFrame('body', index);
                    //body.find("#Id").val(data.Id);
                }
            });

        }
        //else if (layEvent === 'rebateorderdetails') { // 订单汇总

        //}

    });

    // --- 添加活动页面  
    $(".promotions-orders-create").on("click", function () {
        console.log(".promotions-orders-create");
        //return false;
        var index = layui.layer.open({
            title: '添加活动订单',
            type: 2,
            anim: 1,
            area: ['60%', '80%'],
            content: "order-create.html",
            success: function (layero, index) {
            }
        });
    });



});
