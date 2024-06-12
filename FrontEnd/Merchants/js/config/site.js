layui.config({
    base: "https://cdn.wwzznn.com/layuicms/js/extentions/"
}).extend({
    'ext': 'ext',
    "jqueryext": "jqueryext",
    "formExt": "formext"
});
layui.use(['form', 'layer', 'layedit', 'laydate', 'upload', 'table', 'element', 'upload', 'formExt'], function () {
    var form = layui.form
    layer = parent.layer === undefined ? layui.layer : top.layer,
        laypage = layui.laypage,
        upload = layui.upload,
        layedit = layui.layedit,
        laydate = layui.laydate,
        element = layui.element,
        formExt = layui.formExt,
        $ = layui.jquery,
        table = layui.table;


    //var rq = getQueryVariable('p');
    //var rq_data = JSON.parse(decodeURI(rq));
    //console.log(rq_data);
    //$('[name="Name"]').val(rq_data.TypeDesc);
    //$('[name="fee"]').val(rq_data.Rate);
    //if (rq_data.Enabled) {
    //    $('[name="Enabled"]').attr('checked', true);
    //}

    loadInitd();

    function loadInitd() {
        $.post("/mch/merchant/getcsconfig", function (res) {
            console.log(res);
            res = JSON.parse(res);
            var info = res.info;// JSON.parse(res.info);
            console.log(info);
            formExt.fill('#config-site-table-form', info);
        })
    }



    formExt.upload('.upload-btn', { url: '/mch/upload/image' }, function (seclector, data) {
        seclector.prev().attr('src', data.info.src);
    });
    form.render();


    form.on("submit(config-site-table-form-submit)", function (data) {
        var enabledH5 = data.field['EnabledH5'] == 'undefined' ? false : true;
        var agentModel = data.field['EnabledAgentModel'] == 'undefined' ? false : true;
        var d = { SiteName: "", WebLogo: $('[name="WebLogo"]').attr('src'), MobileLogo: $('[name="MobileLogo"]').attr('src'), ServiceLink: $('[name="ServiceLink"]').val(), QRCode: $('[name="QRCode"]').attr('src'), EnabledH5: enabledH5, EnabledAgentModel: agentModel };

        $.post("/mch/merchant/savecsconfig",d, function (res) {
            console.log(res);
        })

        return false;
    });




})