layui.use(['form', 'layer', 'layedit', 'laydate', 'upload', 'table', 'element'], function () {
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


    // 支付类型列表 

    var tablePayType = table.render({
        elem: '#pay-type-list-table',
        url: '/mch/areas/helptypearealist',
        method: 'post',
        contentType: 'application/json',
        where: { 'b': 'a' },
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limits: [10, 15, 20, 25],
        limit: 15,
        id: "help-type-list-table",
        cols: [[
            { type: "checkbox", fixed: "left", width: 50 },
            { field: "Id", title: '标识', width: 80, align: "center" },
            { field: 'Title', title: '名称', width: 160, align: "center" },
            { templet: '#help-type-manager-list-ishref-bar', title: '是否链接', width: 160, align: "center" },
            { field: 'CreateTime', title: '创建时间', width: 160, align: "center" },
            { title: '是否启用', width: 120, align: 'center', templet: '#help-type-manager-list-table-bar' },
            { title: '操作', minWidth: 80, templet: '#help-type-list-table-bar', fixed: "right", align: "center" }
        ]]
    });




    //列表操作
    table.on('tool(help-type-list-table)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;

        console.log(layEvent);
        console.log(data);

        if (layEvent === 'edit') {
            console.log('edit');
            var index = layui.layer.open({
                title: '编辑类别',
                type: 2,
                anim: 1,
                area: ['60%', '80%'],
                content: "help-type-edit.html?p=" + encodeURI(JSON.stringify(data)),
                success: function (layero, index) {
                    //var body = layui.layer.getChildFrame('body', index);
                    //body.find("#Id").val(data.Id);
                }
            });
        }
        else if (layEvent === 'pay-type-manager-disabled') {

        }

    });

    //function updatestatus(id, enabled) {
    //    $.post("/mch/pay/updatecatetorytypestatus", {
    //        enabled: enabled,
    //        id: id
    //    }, function (res) {
    //        console.log(res);
    //        res = JSON.parse(res);
    //        layer.msg(res.msg);
    //        if (res.code == 1) {
    //            tablePayType.reload();
    //        }
    //    })
    //}


    form.on("submit(help-type-manager-submit)", function (data) {

        console.log(data);
        var post_data = {
            Href: data.field['Href'],
            IsHref: typeof (data.field["IsHref"]) == 'undefined' ? false : true,
            IsOpen: typeof (data.field["IsOpen"]) == 'undefined' ? false : true,
            Title: data.field['Title'],
        };
        console.log(typeof (data.field["IsOpen"]) === 'undefined');
        //return false;

        //弹出loading
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        // 实际使用时的提交信息
        $.post("/mch/areas/savehelptype", {
            Href: data.field['Href'],
            IsHref: typeof (data.field["IsHref"]) == 'undefined' ? false : true,
            IsOpen: typeof (data.field["IsOpen"]) == 'undefined' ? false : true,
            Title: data.field['Title'],
        }, function (res) {
            console.log(res);
            res = JSON.parse(res);
            top.layer.close(index);
            top.layer.msg(res.msg);
            //layer.closeAll("iframe");
            ////刷新父页面
            //parent.location.reload();
        })
        //setTimeout(function () {
        //    top.layer.close(index);
        //    top.layer.msg("文章添加成功！");
        //    layer.closeAll("iframe");
        //    //刷新父页面
        //    parent.location.reload();
        //}, 500);
        return false;
    })



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