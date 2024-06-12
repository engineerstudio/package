layui.config({
    base: "https://cdn.wwzznn.com/layuicms/js/extentions/"
}).extend({
    'ext': 'ext',
    "jqueryext": "jqueryext"
});
layui.use(['form', 'element', 'layer', 'table', 'laytpl', 'jqueryext', 'ext', 'laydate'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl,
        element = layui.element,
        ext = layui.ext,
        laydate = layui.laydate,
        table = layui.table;


    var rq = localStorage.getItem('prolist-edit');
    var rq_data = JSON.parse(decodeURI(rq));
    localStorage.removeItem('prolist-edit');

    let conf = JSON.parse(rq_data.Config);
    console.log(rq_data);
    console.log(conf);

    let conf_groups = null;
    console.log(typeof (conf.GroupConfig));
    if (conf.GroupConfig != null)
        conf_groups = JSON.parse(conf.GroupConfig);
    else
        conf_groups = "";

    // 充值金额 / 充值比例
    $.fillSelect('[name="AType"]', ext.getEnumType('ActivityType'));

    $('[name="AType"]').val(rq_data.TypeStr);
    $('[name="Title"]').val(rq_data.Title);

    $.post('/mch/vips/groupdic', function (d) {
        d = JSON.parse(d);
        d = JSON.parse(d.msg);
        let html = '';
        console.log(conf_groups)
        for (var i in d) {
            if (conf_groups.indexOf(i) != -1)
                html += '<input type="checkbox" name="group[' + i + ']" d-id=' + i + ' title="' + d[i] + '" checked>';
            else
                html += '<input type="checkbox" name="group[' + i + ']" d-id=' + i + ' title="' + d[i] + '">';
        }
        $('.groups').html(html);
        form.render();
    })

    form.on('submit(pro-config-recharge-submit)', function (e) {
        e.field['Enabled'] = e.field['Enabled'] == 'on' ? true : false;
        var str = JSON.stringify(e.field);

        // 获取Group内容
        let groupArr = [];
        for (let i in e.field) {
            if (i.startsWith('group['))
                groupArr.push($('[name="' + i + '"]').attr('d-id'))
        }
        console.log(JSON.stringify(groupArr))
        var config = {
            GroupConfig: JSON.stringify(groupArr),
            //Wash: e.field['Wash'],
            //WashValue: e.field['WashValue']
        };
        console.log(JSON.stringify(config));

        //弹出loading
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        $.post("/mch/promotions/savepromoconfig", {
            aType: e.field['AType'],
            Title: e.field['Title'],
            config: JSON.stringify(config),
            id: rq_data.Id,
            enabled: e.field['Enabled']
        }, function (res) {
            res = JSON.parse(res);
            layer.msg(res.msg);
            if (res.code == 1) {
                top.layer.close(index);
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

});