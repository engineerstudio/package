layui.use(['form', 'layer', 'laydate', 'table', 'laytpl'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laydate = layui.laydate,
        laytpl = layui.laytpl,
        table = layui.table;





    // 分组列表
    var tableIns = table.render({
        elem: '#member-viplist-list',
        url: '/mch/vips/load',
        method: 'post',
        contentType: 'application/json',
        //where: { 'b': 'a' },
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limit: 20,
        limits: [10, 15, 20, 25],
        id: "member-viplist-list",
        cols: [[
            { type: "checkbox", width: 50 },// fixed: "left",
            { field: 'Id', title: 'ID', width: 90, align: "center" },
            { field: 'GroupName', title: '分组名称', width: 180, align: 'center' },
            { field: 'Eabled', title: '是否开启', templet: '#viplist-enabled-status', align: 'center' },
            { field: 'Eabled', title: '默认分组', templet: '#viplist-isdefault-status', align: 'center' },
            { field: 'SortNo', title: '排序', width: 90, align: "center" },
            { field: 'Description', title: '备注', align: 'center' },
            { title: '操作', templet: '#viplist-listBar', align: "center" } // fixed: "right",
        ]]
    });

    $('.member-viplist-new').click(function () {
        var index = layui.layer.open({
            title: "添加分组",
            type: 2,
            maxmin: true,
            area: ['60%', '80%'],
            content: "vip-add.html"
        })
    });

    $('.member-viplist-rule-set').click(function () {
        layui.layer.open({
            title: "分组升级规则设定",
            type: 2,
            maxmin: true,
            area: ['60%', '80%'],
            content: "vip-merchant-rule.html"
        })
    });

    form.on("submit(member-viplist-search-submit)", function (e) {
        console.log(e.field);
        table.reload("member-viplist-list", {
            contentType: 'application/x-www-form-urlencoded',
            page: {
                curr: 1 //重新从第 1 页开始
            },
            where: e.field
        });

        return false;
    });




    table.on('tool(member-viplist-list)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;
        if (layEvent === 'edit') { // 编辑
            console.log('edit');

            var index = layui.layer.open({
                title: "编辑分组",
                type: 2,
                maxmin: true,
                area: ['60%', '80%'],
                content: "vip-add.html?p=" + encodeURI(JSON.stringify(data)),
                success: function (layero, index) {

                }
            })


        } else if (layEvent === 'group-rule') { // 分组规则
            console.log('group-rule');
            layui.layer.open({
                title: "分组规则",
                type: 2,
                maxmin: true,
                area: ['60%', '80%'],
                content: "vip-group-rule.html?p=" + encodeURI(JSON.stringify(data)),
                success: function (layero, index) {

                }
            })

        } else if (layEvent === 'pay-rule') { //分组的支付配置
            console.log('pay-rule');
            layui.layer.open({
                title: "分组支付",
                type: 2,
                maxmin: true,
                area: ['60%', '80%'],
                content: "vip-pay-rule.html?p=" + encodeURI(JSON.stringify(data)),
                success: function (layero, index) {

                }
            })

        } else if (layEvent === 'point-rule') {
            console.log('point-rule');

            layer.alert("此功能需要前台展示，实际开发中传入对应的必要参数进行文章内容页面访问")


        }

    });







});