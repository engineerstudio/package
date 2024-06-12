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




    //$('[name="MemberType"]').html(ext.getSelectOptions('UserType', '请选择搜索的会员类别'));
    //$('[name="GroupId"]').html(ext.getSelectOptionsByUrl('/mch/vips/groupdic', '请选择搜索的会员分组'));
    //form.render();


    //角色列表
    var tableIns = table.render({
        elem: '#account-list',
        url: '/mch/account/manager/load',
        method: 'post',
        contentType: 'application/json',
        //where: { 'b': 'a' },
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limit: 20,
        limits: [10, 15, 20, 25],
        id: "account-listTable",
        cols: [[
            //{ type: "checkbox", width: 50 },// fixed: "left",
            { field: 'Id', title: 'ID', width: 70, align: "center" },
            { field: 'UserName', title: '账户名称', width: 120, align: 'center' },
            { templet: '#account-manager-roleName', title: '角色名称', width: 110, align: 'center' },
            { field: 'IsLockStr', title: '状态', width: 90, align: 'center' },
            { field: 'LoginLastTime', title: '登陆时间', width: 160, align: 'center' },
            { field: 'Remark', title: '备注', align: 'center' },
            { title: '操作', templet: '#account-listBar', align: "center" } // fixed: "right",
        ]]
    });



    // 添加站点
    $('.account-edit').click(function () {
        var index = layui.layer.open({
            title: "添加角色",
            type: 2,
            area: ['60%', '80%'],
            content: "account-edit.html",
            success: function (layero, index) {

            }
        })

        $(window).on("resize", function () {
            layui.layer.full(index);
        })
    });




    //列表操作
    table.on('tool(account-list)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;

        console.log(data);
        data.Pasw = '';
        if (layEvent === 'edit') { //编辑
            layui.layer.open({
                title: "编辑",
                type: 2,
                maxmin: true,
                area: ['60%', '80%'],
                content: "account-edit.html?p=" + encodeURI(JSON.stringify(data)),
                success: function (layero, index) {

                }
            })
        }

    });

})