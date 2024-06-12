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
    // 初始化
    $.ajax({
        url: '/mch/account/menu/loaddatawithparentroleid',
        dataType: 'json',
        type: 'POST',
        data: { roleId: rq_data.Id, ParentId: -1 },
        success: function (data) {
            console.log(data);
            // 渲染时传入渲染目标ID，树形结构数据（具体结构看样例，checked表示默认选中），以及input表单的名字
            var trees = authtree.listConvert(data, {
                primaryKey: 'Id'
                , startPid: 0
                , parentKey: 'ParentId'
                , nameKey: 'DisplayName'
                , valueKey: 'Id'
                , checkedKey: 'Checked'
            });
            authtree.render('#yilezhu-auth-tree', trees, {
                inputname: 'ids[]'
                , layfilter: 'yilezhu-check-auth'
                , autowidth: true
            });

            authtree.on('change(yilezhu-check-auth)', function (data) {
                console.log('监听 authtree 触发事件数据', data);
            });
            authtree.on('dblclick(yilezhu-check-auth)', function (data) {
                console.log('监听到双击事件', data);
            });
        },
        error: function (xml, errstr, err) {
            layer.alert(errstr + '，系统异常！');
        }
    });
    function strToIntArr(str) {
        if (str) {
            var strArr = str.split(',');
            var dataIntArr = [];//保存转换后的整型字符串
            //方法一
            strArr.forEach(function (data, index, arr) {
                dataIntArr.push(+data);
            });
            return dataIntArr;
        }
    }

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