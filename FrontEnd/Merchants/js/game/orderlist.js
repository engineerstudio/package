layui.config({
    base: "https://cdn.wwzznn.com/layuicms/js/extentions/"
}).extend({
    'ext': 'ext',
    "jqueryext": "jqueryext"
});

layui.use(['form', 'layer', 'laydate', 'table', 'laytpl', 'jqueryext', 'ext'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laydate = layui.laydate,
        laytpl = layui.laytpl,
        ext = layui.ext,
        table = layui.table;

    $('[name="GameCategoryStr"]').html(ext.getGameCategorySelect('请选择游戏类别'));
    $('[name="OrderStatusStr"]').html(ext.getSelectOptions('OrderStatus', '请选择订单状态'));
    $('[name="GameTypeStr"]').html(ext.getSelectOptionsByUrl('/mch/game/merchantgametype', '请选择游戏'));

    form.render();
    // 游戏列表
    var tableIns = table.render({
        elem: '#game-orderlist-list',
        url: '/mch/game/gameorders',
        method: 'post',
        contentType: 'application/json',
        totalRow: true,
        totalRowText: '合计：',
        //where: { 'b': 'a' },
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limit: 20,
        limits: [10, 15, 20, 25],
        id: "game-orderlist-list",
        cols: [[
            { type: "checkbox", width: 50 },// fixed: "left",
            { field: 'SourceId', title: '订单号', width: 180, align: "center" },
            { field: 'GameCategoryDes', title: '游戏类别', width: 90, align: 'center' },
            { field: 'GameTypeDes', title: '游戏平台', width: 90, align: 'center' },
            { field: 'MemberId', title: '会员名称', width: 100, align: 'center' },
            { field: 'PlayerName', title: '游戏会员', width: 160, align: 'center' },
            { field: 'StatusDes', title: '订单状态', width: 90, align: 'center' },
            { field: 'BetAmount', title: '投注金额', width: 90, align: 'center' },
            { field: 'ValidBet', title: '有效投注', width: 90, align: 'center' },
            { field: 'Money', title: '盈亏金额', width: 90, align: 'center' },
            { field: 'AwardAmount', title: '派奖金额', width: 90, align: 'center' },
            { field: 'OrderCreateTimeUtc8', title: '投注时间', width: 180, align: 'center' },
            { field: 'OrderAwardTimeUtc8', title: '派奖时间', width: 180, align: 'center' },
            { title: '操作', templet: '#orderlist-listBar', align: "center" } // fixed: "right",
        ]]
    });




    //搜索
    $(".search_btn").on("click", function () {
        var q_date = $('#betTime').val(); // 投注时间区间

        console.log(q_date.length);
        let CreateStartTime = null, CreateEndTime = null;
        if (q_date.length != 0) {
            CreateStartTime = q_date.split('~')[0];
            CreateEndTime = q_date.split('~')[1];
        } else {
            CreateStartTime = null; CreateEndTime = null;
        }
        var key = {
            GameCategoryStr: $('[name="GameCategoryStr"]').val(),
            GameTypeStr: $('[name="GameTypeStr"]').val(),
            CreateStartTime: CreateStartTime,
            CreateEndTime: CreateEndTime,
            SourceId: $('[name="SourceId"]').val(),
            GameAccount: $('[name="GameAccount"]').val(),
            AccountName: $('[name="AccountName"]').val(),
            OrderStatusStr: $('[name="OrderStatusStr"]').val(),
            MemberId: 0,
            BetStartTime: '',
            BetEndTime: ''
        };
        console.log(key);
        //return false;
        if ($(".searchVal").val() !== '') {
            table.reload("game-orderlist-list", {
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


    table.on('tool(game-orderlist-list)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;
        if (layEvent === 'details') { // 编辑
            console.log(data);
            layui.layer.open({
                title: "订单明细",
                type: 2,
                maxmin: true,
                area: ['60%', '80%'],
                content: "orderlist-detail.html?orderId=" + data.Id + "&cat=" + data.GameCategory,
                success: function (layero, index) {
                }
            })
        }
    });

    laydate.render({
        elem: '#betTime'
        , type: 'datetime'
        , range: '~' //或 range: '~' 来自定义分割字符
    });






});