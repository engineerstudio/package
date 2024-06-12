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
    console.log(rq);
    console.log(rq_data);


    $('[name="KeyId"]').html(ext.getSelectOptionsByUrl('/mch/areas/sectiondic', '请选择分组'));
    function init_form(d) {
        console.log('init_form');
        $('[name="Id"]').val(d.Id);
        $('[name="Alias"]').val(d.Alias);
        if (d.Enabled) $('[name="Enabled"]').attr('checked', true);
        if (d.HasSubMenu) $('[name="HasSubMenu"]').attr('checked', true);
        $('[name="ImgUrl"]').val(d.SKey);
        $('[name="ImgUrl"]').val(d.ImgUrl);
        $('[name="PageUrl"]').val(d.PageUrl);
        $('[name="KeyId"]').val(d.KeyId);
    }
    if (rq_data)
        init_form(rq_data);

    form.render();

    form.on("submit(area-section-key-detail-submit)", function (data) {
    

        if (data.field['HasSubMenu'] == 'on') data.field['HasSubMenu'] = true;
        else data.field['HasSubMenu'] = false;
        if (data.field['Enabled'] == 'on') data.field['Enabled'] = true;
        else data.field['Enabled'] = false;
        console.log(data.field);

        //return false;
        $.post("/mch/areas/sectionsave", data.field, function (res) {
            console.log(res);
            //top.layer.close(index);
            //top.layer.msg("文章添加成功！");
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