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


    var rq = getQueryVariable('p');
    var rq_data = JSON.parse(decodeURI(rq));
    console.log(rq_data);


    function init_form(d) {
        console.log('init_form');
        $('[name="Id"]').val(d.Id);
        $('[name="Title"]').val(d.Title);
        $('[name="Href"]').val(d.Href);
        if (d.IsHref)
            $('[name="IsHref"]').attr('checked', 'checked');
        if (d.IsOpen)
            $('[name="IsOpen"]').attr('checked', 'checked');
        form.render();
    }
    init_form(rq_data);

    form.on("submit(help-type-edit-submit)", function (data) {
        console.log(data.field);
        console.log(typeof (data.field["IsHref"]) == 'undefined')
        console.log(data.field["IsHref"] == 'undefined')
        var post_data = {
            Href: data.field['Href'],
            IsHref: typeof (data.field["IsHref"]) == 'undefined' ? false : true,
            IsOpen: typeof (data.field["IsOpen"]) == 'undefined' ? false : true,
            Title: data.field['Title'],
            Id: data.field['Id']
        };
        console.log(post_data);

        $.post("/mch/areas/savehelptype", post_data, function (res) {
            console.log(res);
            res = JSON.parse(res);
            //top.layer.close(index);
            top.layer.msg(res.msg);
            layer.closeAll("iframe");
            //刷新父页面
            parent.location.reload();
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