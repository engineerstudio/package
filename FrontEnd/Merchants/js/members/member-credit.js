layui.config({
    base: "https://cdn.wwzznn.com/layuicms/js/extentions/"
}).extend({
    'ext': 'ext',
    "jqueryext": "jqueryext"
});
layui.use(['form', 'layer', 'layedit', 'laydate', 'upload', 'table', 'laydate', 'jqueryext', 'ext'], function () {
    var form = layui.form
    layer = parent.layer === undefined ? layui.layer : top.layer,
        laypage = layui.laypage,
        upload = layui.upload,
        layedit = layui.layedit,
        laydate = layui.laydate,
        table = layui.table,
        ext = layui.ext,
        $ = layui.jquery;

    var rq = getQueryVariable('p');
    var rq_data = JSON.parse(decodeURI(rq));
    console.log(rq_data);

    $('[name="FundLogType"]').html(ext.getSelectOptions('FundLogType', '请选择充值类别'));
    $('[name="TransType"]').html(ext.getSelectOptions('TransType', '请选择转账类别'));
    form.render();
    console.log(rq_data.Id)
    var conditions = { MemberId: rq_data.Id, MerchantId: 1000 };
    var tableIns = table.render({
        elem: '#member-credit-list-table',
        url: '/mch/member/getfundslog',
        method: 'post',
        //contentType: 'application/json',
        where: conditions,
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limits: [10, 15, 20, 25],
        limit: 15,
        id: "member-credit-list-table",
        cols: [[
            //{ type: "checkbox", fixed: "left", width: 50 },
            { field: "Id", title: '标识', width: 80, align: "center" },
            { field: 'FundsTypeDesc', title: '类型', width: 120, align: "center" },
            { field: 'Amount', title: '金额', width: 120, align: 'center' },
            { field: 'Balance', title: '余额', width: 120, align: 'center' },
            { field: 'CreateTime', title: '创建时间', width: 180, align: 'center' },
            { field: 'IP', title: 'IP', width: 120, align: 'center' },
            { field: 'Marks', title: '备注', align: 'center' },
        ]]
    });

    $(".search_btn").on("click", function () {
        var q_date = $('#CreateTime').val(); // 投注时间区间

        console.log(q_date.length);
        let StartAt = null, EndAt = null;
        if (q_date.length != 0) {
            StartAt = q_date.split('~')[0];
            EndAt = q_date.split('~')[1];
        } else {
            StartAt = null; EndAt = null;
        }
        var key = {
            MemberId: rq_data.Id,
            FundLogType: $('[name="FundLogType"]').val(),
            TransType: $('[name="TransType"]').val(),
            StartAt: StartAt,
            EndAt: EndAt
        };
        console.log(key);
        //return false;
        if ($(".searchVal").val() !== '') {
            table.reload("member-credit-list-table", {
                contentType: 'application/x-www-form-urlencoded',
                page: {
                    curr: 1 //重新从第 1 页开始
                },
                where: key
            });
        } else {
            layer.msg("请输入搜索的内容");
        }
    });

    laydate.render({
        elem: '#CreateTime'
        , type: 'datetime'
        , range: '~' //或 range: '~' 来自定义分割字符
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
})