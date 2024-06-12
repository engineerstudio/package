layui.config({
    base: "https://cdn.wwzznn.com/layuicms/js/extentions/"
}).extend({
    'ext': 'ext',
    "jqueryext": "jqueryext"
});
layui.use(['form', 'layer', 'layedit', 'table', 'laydate', 'upload', 'jqueryext', 'ext'], function () {
    var form = layui.form
    layer = parent.layer === undefined ? layui.layer : top.layer,
        laypage = layui.laypage,
        upload = layui.upload,
        layedit = layui.layedit,
        laydate = layui.laydate,
        table = layui.table,
        ext = layui.ext,
        $ = layui.jquery;


    var id = getQueryVariable('id');
    var typeId = getQueryVariable('TypeId');


    function init_form() {
        $.post('/mch/areas/gethelpcontent', { id: id }, function (d) {
            console.log(d);
            d = JSON.parse(d);
            $('[name="Id"]').val(d.info.Id);
            $('[name="Title"]').val(d.info.Title);
            $('[name="Tcontent"]').val(d.info.Tcontent);

        });
    }
    if (id)
        init_form();


    function init_helptype() {
        $('[name="TypeId"]').html(ext.getSelectOptionsByUrl('/mch/areas/helptypedic', '请选择分组'));
        if (id)
            $('[name="TypeId"]').val(typeId);
        form.render();
    }
    init_helptype();


    form.on("submit(area-help-content-submit)", function (data) {
        console.log(data.field);
        $.post("/mch/areas/savehelpcontent", {
            Id: data.field['Id'],
            TypeId: data.field['TypeId'],
            Title: data.field['Title'],
            Tcontent: data.field['Tcontent'],
        }, function (res) {
            console.log(res);
            res = JSON.parse(res);
            layer.msg(res.msg);
            if (res.code == 1) {
                //top.layer.close(index);
                //top.layer.msg("文章添加成功！");
                layer.closeAll("iframe");
                //刷新父页面
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