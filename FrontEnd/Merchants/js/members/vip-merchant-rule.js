layui.config({
    base: "https://cdn.wwzznn.com/layuicms/js/extentions/"
}).extend({
    'formExt': 'formExt',
    "jqueryext": "jqueryext"
});

layui.use(['form', 'layer', 'layedit', 'laydate', 'upload', 'formExt'], function () {
    var form = layui.form
    layer = parent.layer === undefined ? layui.layer : top.layer,
        laypage = layui.laypage,
        upload = layui.upload,
        layedit = layui.layedit,
        laydate = layui.laydate,
        formExt = layui.formExt,
        $ = layui.jquery;

    $.ajax({
        url: '/mch/merchant/getvipconfig',
        type: 'POST',
        success: function (d) {
            console.log(d);
            d = JSON.parse(d);
            if (d.code == 1) {
                if (d.info == '') d.info = "{}";
                formExt.fill('#vip-merchant-rule-form', JSON.parse(d.info));
                form.render();
            }
        }
    });

    formExt.submit('vip-merchant-rule-submit', "/mch/merchant/savevipconfig", function (d) {
        var field = d.field;
        field.EnabledAutoGroup = field.EnabledAutoGroup == 'on' ? true : false;
        field.EnableMonthSalary = field.EnableMonthSalary == 'on' ? true : false;
        field.EnableBirthAmount = field.EnableBirthAmount == 'on' ? true : false;
        field.EnableProAmount = field.EnableProAmount == 'on' ? true : false;
        console.log(d);
        return d;
    })
})