layui.config({
    base: "https://cdn.wwzznn.com/layuicms/js/extentions/"
}).extend({
    'ext': 'ext',
    "jqueryext": "jqueryext",
    "formExt": "formext"
});

layui.use(['form', 'layer', 'layedit', 'table', 'laydate', 'upload', 'formExt'], function () {
    var form = layui.form
    layer = parent.layer === undefined ? layui.layer : top.layer,
        laypage = layui.laypage,
        upload = layui.upload,
        layedit = layui.layedit,
        laydate = layui.laydate,
        table = layui.table,
        formExt = layui.formExt,
        $ = layui.jquery;


    var rq = getQueryVariable('p');
    var rq_data = JSON.parse(decodeURI(rq));

    function init_form(d) {
        $('[name="Id"]').val(d.Id);
        $('[name="Alias"]').val(d.Alias);
        if (d.Enabled) $('[name="Enabled"]').attr('checked', true);
        if (d.HasSubMenu) $('[name="HasSubMenu"]').attr('checked', true);
        $('[name="SKey"]').val(d.SKey);
        $('[name="PcImgUrl"]').attr('src', d.PcImgUrl);
        $('[name="H5ImgUrl"]').attr('src', d.H5ImgUrl);
        $('[name="PageUrl"]').val(d.PageUrl);
        $('[name="SKey"]').val(d.SKey);

        formExt.upload('.upload-btn', { url: '/mch/upload/image' }, function (seclector, data) {
            seclector.prev().attr('src', data.info.src);
        });
        form.render();
    }
    init_form(rq_data);

    form.on("submit(area-section-edit-detail-submit)", function (data) {

        if (data.field['HasSubMenu'] == 'on') data.field['HasSubMenu'] = true;
        else data.field['HasSubMenu'] = false;
        if (data.field['Enabled'] == 'on') data.field['Enabled'] = true;
        else data.field['Enabled'] = false;

        data.field['PcImgUrl'] = $('#PcImgUrl').attr('src');
        data.field['H5ImgUrl'] = $('#H5ImgUrl').attr('src');

        $.post("/mch/areas/sectionsave", data.field, function (res) {
            res = JSON.parse(res);
            layer.msg(res.msg);
            layer.closeAll("iframe");
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