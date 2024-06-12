layui.config({
    base: "https://cdn.wwzznn.com/layuicms/js/extentions/"
}).extend({
    'ext': 'ext',
    "jqueryext": "jqueryext",
    "formExt": "formext"
});
layui.use(['form', 'element', 'layer', 'table', 'laytpl', 'jqueryext', 'ext', 'laydate', "formExt"], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl,
        element = layui.element,
        ext = layui.ext,
        laydate = layui.laydate,
        formExt = layui.formExt,
        table = layui.table;


    var rq = localStorage.getItem('prolist-edit');
    var rq_data = JSON.parse(decodeURI(rq));
    $('[name=Id]').val(rq_data.Id);
    localStorage.removeItem('prolist-edit');


    form.on('submit(promotions-order-summary-search)', function (e) {


        var betTime = e.field['betTime'];
        var rewardTime = e.field['rewardTime'];

        console.log(betTime);
        console.log(rewardTime);
        console.log(betTime.indexOf('~'))
        if (betTime.indexOf('~') != -1) {
            e.field['CreateDateStartAt'] = betTime.split(' ~ ')[0];
            e.field['CreateDateEndAt'] = betTime.split(' ~ ')[1];
        }
        if (rewardTime.indexOf('~') != -1) {
            e.field['RewardDateStartAt'] = rewardTime.split(' ~ ')[0];
            e.field['RewardDateEndAt'] = rewardTime.split(' ~ ')[1];
        }

        console.log(e.field);
        return false;
        //if (e.field['ShowDeletedPro'] == 'on') e.field['ShowDeletedPro'] = true;
        //else e.field['ShowDeletedPro'] = false;


        table.reload("promotions-list-table", {
            contentType: 'application/x-www-form-urlencoded',
            page: {
                curr: 1 //重新从第 1 页开始
            },
            where: e.field
        });
        return false;
    });

    //$.fillSelect('[name="AType"]', ext.getEnumType('ActivityType'));
    //$.fillSelect('[name="BonusType"]', ext.getEnumType('BonusType'));
    //$.fillSelect('[name="BonusCalType"]', ext.getEnumType('BonusCalType'));
    //$.fillSelect('[name="IPCheckType"]', ext.getEnumType('IPCheckType'));
    //$.fillSelect('[name="WashType"]', ext.getEnumType('WashType'));

    var tableIns = table.render({
        elem: '#promotions-order-summary-table',
        url: '/mch/promotions/ordersummary',
        method: 'post',
        contentType: 'application/x-www-form-urlencoded',
        where: { 'id': rq_data.Id },
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limits: [10, 15, 20, 25],
        limit: 15,
        id: "promotions-order-summary-table",
        cols: [[
            { field: "CreateDate", title: '创建日期', width: 80, align: "center" },
            { field: "RewardDate", title: '派奖日期', width: 80, align: "center" },
            { field: 'UserName', title: '会员名称', width: 160, align: "center" },
            { field: 'Reward', title: '派奖金额', width: 160, align: "center" },
            { title: '操作', minWidth: 80, templet: '#promotions-order-summary-table-bar', fixed: "right", align: "center" }
        ]]
    });
    laydate.render({
        elem: '#betTime'
        , type: 'date'
        , range: '~' //或 range: '~' 来自定义分割字符
    });

    laydate.render({
        elem: '#rewardTime'
        , type: 'date'
        , range: '~' //或 range: '~' 来自定义分割字符
    });



    form.render();


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