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




    $('[name="MemberType"]').html(ext.getSelectOptions('UserType', '请选择搜索的会员类别'));
    $('[name="GroupId"]').html(ext.getSelectOptionsByUrl('/mch/vips/groupdic', '请选择搜索的会员分组'));


    form.render();
    //角色列表
    var tableIns = table.render({
        elem: '#role-list',
        url: '/mch/account/role/load',
        method: 'post',
        contentType: 'application/json',
        //where: { 'b': 'a' },
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limit: 20,
        limits: [10, 15, 20, 25],
        id: "role-listTable",
        cols: [[
            //{ type: "checkbox", width: 50 },// fixed: "left",
            { field: 'Id', title: 'ID', width: 90, align: "center" },
            { field: 'RoleName', title: '角色名', width: 180, align: 'center' },
            { templet: '#role-type', title: '账户类型', align: 'center' },
            //{ field: 'GroupName', title: '会员分组', align: 'center' },
            { field: 'IsSystem', title: '系统默认管理', align: 'center' },
            { field: 'Remark', title: '备注', align: 'center' },
            { title: '操作', templet: '#role-listBar', align: "center" } // fixed: "right",
        ]]
    });



    // 添加站点
    $('.role-edit').click(function () {
        var index = layui.layer.open({
            title: "添加角色",
            type: 2,
            area: ['60%', '80%'],
            content: "role-edit.html",
            success: function (layero, index) {

            }
        })

        $(window).on("resize", function () {
            layui.layer.full(index);
        })
    });




    //列表操作
    table.on('tool(role-list)', function (obj) {
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
                content: "role-edit.html?p=" + encodeURI(JSON.stringify(data)),
                success: function (layero, index) {

                }
            })
        }

    });

})