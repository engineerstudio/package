layui.use(['form', 'layer', 'laydate', 'table', 'laytpl'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laydate = layui.laydate,
        laytpl = layui.laytpl,
        table = layui.table;


    form.on('submit(area-section-search-form)', function (e) {
        console.log(e.field);
        //return false;
        table.reload("area-section-list", {
            contentType: 'application/x-www-form-urlencoded',
            page: {
                curr: 1 //重新从第 1 页开始
            },
            where: e.field
        });
    });

    // 游戏列表
    var tableIns = table.render({
        elem: '#area-section-list',
        url: '/mch/areas/section',
        method: 'post',
        contentType: 'application/json',
        where: { 'b': 'a' },
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limit: 20,
        limits: [10, 15, 20, 25],
        id: "area-section-list",
        cols: [[
            { type: "checkbox", width: 50 },// fixed: "left",
            { field: 'Id', title: '序号', width: 90, align: "center" },
            { field: 'TypeDesc', title: '区域类别', width: 180, align: 'center' },
            { field: 'Description', title: '描述', align: 'center' },
            { title: '操作', templet: '#area-listBar', align: "center" } // fixed: "right",
        ]]
    });

    $('.area-section-new-key').click(function () {

        var index = layui.layer.open({
            title: "添加Key",
            type: 2,
            maxmin: true,
            area: ['60%', '80%'],
            content: "section-key.html",
            success: function (layero, index) {

            }
        })

    });

    $('.area-section-new-keydetails').click(function () {

        var index = layui.layer.open({
            title: "添加Key子菜单",
            type: 2,
            maxmin: true,
            area: ['60%', '80%'],
            content: "section-key-detail.html",
            success: function (layero, index) {

            }
        })

    });



    table.on('tool(area-section-list)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;
        if (layEvent === 'edit') { // 编辑
            console.log('edit');
            layui.layer.open({
                title: "编辑",
                type: 2,
                maxmin: true,
                area: ['60%', '80%'],
                content: "section-edit.html?p=" + encodeURI(JSON.stringify(data)),
                success: function (layero, index) {

                }
            })
        }
        //else if (layEvent === 'area-trans-orders') { // 分组规则
        //    console.log('area-trans-orders');
        //    layui.layer.open({
        //        title: "转账日志",
        //        type: 2,
        //        maxmin: true,
        //        area: ['60%', '80%'],
        //        content: "area-trans-orders.html?p=" + encodeURI(JSON.stringify(data)),
        //        success: function (layero, index) {

        //        }
        //    })


        //} else if (layEvent === 'look') { //预览
        //    layer.alert("此功能需要前台展示，实际开发中传入对应的必要参数进行文章内容页面访问")
        //}
    });







});