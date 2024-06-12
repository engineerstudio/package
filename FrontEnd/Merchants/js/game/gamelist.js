layui.use(['form', 'layer', 'laydate', 'table', 'laytpl'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laydate = layui.laydate,
        laytpl = layui.laytpl,
        table = layui.table;


    form.on('submit(game-gamelist-search-form)', function (e) {
        console.log(e.field);
        //return false;
        table.reload("game-gamelist-list", {
            contentType: 'application/x-www-form-urlencoded',
            page: {
                curr: 1 //重新从第 1 页开始
            },
            where: e.field
        });
    });

    // 游戏列表
    var tableIns = table.render({
        elem: '#game-gamelist-list',
        url: '/mch/game/merchantsconfig',
        method: 'post',
        contentType: 'application/json',
        where: { 'b': 'a' },
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limit: 20,
        limits: [10, 15, 20, 25],
        id: "game-gamelist-list",
        cols: [[
            { type: "checkbox", width: 50 },// fixed: "left",
            { field: 'Id', title: '序号', width: 90, align: "center" },
            { field: 'TypeDesc', title: '游戏名称', width: 180, align: 'center' },
            { templet: '#gamelist-gamestatus', title: '是否开启', align: 'center' },
            { field: 'Rate', title: '交收费率', align: 'center' },
            { title: '操作', templet: '#gamelist-listBar', align: "center" } // fixed: "right",
        ]]
    });

    $('.game-gamelist-new').click(function () {

        //var index = layui.layer.open({
        //    title: "添加分组",
        //    type: 2,
        //    maxmin: true,
        //    area: ['60%', '80%'],
        //    content: "vip-add.html",
        //    success: function (layero, index) {

        //    }
        //})


    });

    table.on('tool(game-gamelist-list)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;
        if (layEvent === 'edit') { // 编辑
            console.log('edit');
            layui.layer.open({
                title: "编辑",
                type: 2,
                maxmin: true,
                area: ['60%', '80%'],
                content: "gamelist-edit.html?p=" + encodeURI(JSON.stringify(data)),
                success: function (layero, index) {

                }
            })
        }
        else if (layEvent === 'gamelist-trans-orders') { // 分组规则
            console.log('gamelist-trans-orders');
            layui.layer.open({
                title: "转账日志",
                type: 2,
                maxmin: true,
                area: ['60%', '80%'],
                content: "gamelist-trans-orders.html?p=" + encodeURI(JSON.stringify(data)),
                success: function (layero, index) {

                }
            })
        }

        //} else if (layEvent === 'look') { //预览
        //    layer.alert("此功能需要前台展示，实际开发中传入对应的必要参数进行文章内容页面访问")
        //}
    });







});