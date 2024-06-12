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

    var conf = JSON.parse(rq_data.Config);


    let conf_games = {};
    if (typeof (conf.GameConfig) != 'undefined')
        conf_games = JSON.parse(conf.GameConfig);
    console.log(conf_games);

    $.fillSelect('[name="AType"]', ext.getEnumType('ActivityType'));
    $.fillSelect('[name="BonusType"]', ext.getEnumType('BonusType'));
    $.fillSelect('[name="BonusCalType"]', ext.getEnumType('BonusCalType'));
    $.fillSelect('[name="IPCheckType"]', ext.getEnumType('IPCheckType'));

    $('[name="AType"]').val(rq_data.TypeStr);
    $('[name="Title"]').val(rq_data.Title);
    form.render();


    $.get('/mch/game/configgames', function (d) {
        var getTpl = document.getElementById('commissiontpl').innerHTML
            , view = document.getElementById('tmpt-view');
        laytpl(getTpl).render(JSON.parse(d), function (html) {
            view.innerHTML = html;
            form.render();
            form.val("pro-config-commission-form", conf_games);
        });
    })



    form.on('submit(pro-config-commission-submit)', function (e) {

        //弹出loading
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        e.field['Enabled'] = e.field['Enabled'] == 'on' ? true : false;
        var str = JSON.stringify(e.field);

        // 遍历配置内容
        var arr = {};
        $('#tmpt-view').find('input').each(function (i, d) {
            arr[$(d).attr('name')] = $(d).val();
        });

        //console.log(arr);
        //return false;
        $.post("/mch/promotions/savepromoconfig", {
            aType: e.field['AType'],
            Title: e.field['Title'],
            config: JSON.stringify(arr),
            id: rq_data.Id
            //Enabled: e.field['Enabled'],
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