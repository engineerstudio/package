layui.config({
    base: "https://cdn.wwzznn.com/layuicms/js/extentions/"
}).extend({
    'ext': 'ext',
    "jqueryext": "jqueryext",
    "authtree": "authtree"
});
layui.use(['form', 'layer', 'layedit', 'table', 'laydate', 'upload', 'jqueryext', 'ext', "authtree"], function () {
    var form = layui.form
    layer = parent.layer === undefined ? layui.layer : top.layer,
        laypage = layui.laypage,
        upload = layui.upload,
        layedit = layui.layedit,
        laydate = layui.laydate,
        table = layui.table,
        ext = layui.ext,
        authtree = layui.authtree,
        $ = layui.jquery;


    var rq = getQueryVariable('p');
    var rq_data = JSON.parse(decodeURI(rq));

    var id = rq_data.Id;
    console.log(id);
    console.log(rq_data);


    $('[name="RoleId"]').html(ext.getSelectOptionsByUrl('/mch/account/role/roledic', '请选择角色'));


    // 初始化
    function init_form() {
        $('[name="Id"]').val(rq_data.Id);
        $('[name="RoleName"]').val(rq_data.RoleName);
        $('[name="Remark"]').val(rq_data.Remark);
        $('[name="RoleType"]').val(rq_data.RoleType);
    }
    if (id)
        init_form();

    console.log(rq_data.Id);


    form.render();
    form.on("submit(role-edit-submit)", function (data) {
        var json = {
            Id: $("#Id").val(),  //主键
            RoleName: $('[name="RoleName"]').val(),  //角色名称
            RoleType: $('[name="RoleType"]').val(),  //角色类型
            MenuIds: authtree.getChecked('#yilezhu-auth-tree'),
            Remark: $('[name="Remark"]').val()  //备注
        };
        console.log(json);
        $.post("/mch/account/role/addormodify", json, function (res) {
            console.log(res);
            res = JSON.parse(res);
            layer.msg(res.msg);
            if (res.code == 1) {
                layer.closeAll("iframe");
                parent.location.reload();
            }
        })

        return false;
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