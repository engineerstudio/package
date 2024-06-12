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
    let conf_games = JSON.parse(conf.GameConfig);
    let conf_groups = JSON.parse(conf.GroupConfig);
    console.log(conf_groups);


    $.fillSelect('[name="AType"]', ext.getEnumType('ActivityType'));
    $.fillSelect('[name="BonusType"]', ext.getEnumType('BonusType'));
    $.fillSelect('[name="BonusCalType"]', ext.getEnumType('BonusCalType'));
    $.fillSelect('[name="IPCheckType"]', ext.getEnumType('IPCheckType'));

    $('[name="AType"]').val(rq_data.TypeStr);
    $('[name="Title"]').val(rq_data.Title);


    $.get('/mch/game/configgames', function (d) {
        //console.log(d);
        var getTpl = document.getElementById('rebatetpl').innerHTML
            , view = document.getElementById('tmpt-view');
        //console.log(getTpl);
        laytpl(getTpl).render(JSON.parse(d), function (html) {
            view.innerHTML = html;
            form.render();
            // 赋值
            form.val("pro-config-rebate-form", conf_games);
        });
    })

    $.post('/mch/vips/groupdic', function (d) {
        d = JSON.parse(d);
        d = JSON.parse(d.msg);
        //console.log(d);
        console.log('conf_groups->' + conf_groups);
        let html = '';
        for (var i in d) {
            if (conf_groups != null && conf_groups.indexOf(i) != -1)
                html += '<input type="checkbox" name="group[' + i + ']" d-id=' + i + ' title="' + d[i] + '" checked>';
            else
                html += '<input type="checkbox" name="group[' + i + ']" d-id=' + i + ' title="' + d[i] + '">';
        }
        $('.groups').html(html);
        form.render();
    })

    //form.render();

    form.on('submit(pro-config-rebate-submit)', function (e) {

        e.field['Enabled'] = e.field['Enabled'] == 'on' ? true : false;
        var str = JSON.stringify(e.field);

        // 遍历配置内容
        var arr = {};
        $('#tmpt-view').find('input').each(function (i, d) {
            arr[$(d).attr('name')] = $(d).val();
        });
        // 获取Group内容
        let groupArr = [];
        for (let i in e.field) {
            if (i.startsWith('group['))
                groupArr.push($('[name="' + i + '"]').attr('d-id'))
        }
        console.log(JSON.stringify(groupArr))
        console.log(arr);
        var config = { GameConfig: JSON.stringify(arr), GroupConfig: JSON.stringify(groupArr) };
        console.log(JSON.stringify(config));
        //return false;

        //弹出loading
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        $.post("/mch/promotions/savepromoconfig", {
            aType: e.field['AType'],
            Title: e.field['Title'],
            config: JSON.stringify(config),
            id: rq_data.Id
            //Enabled: e.field['Enabled'],

        }, function (res) {
            //console.log(res);
            layer.msg(res.msg);
            top.layer.close(index);
            //layer.closeAll("iframe");
            //刷新父页面
            parent.location.reload();
        })

        //$.ajax({
        //    type: 'POST',
        //    url: '/mch/promotions/savepro',
        //    data: obj,
        //    contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        //    success: function (data) {
        //        //var data = JSON.parse(data);
        //        console.log(data);
        //        //layer.msg(data.msg);
        //    },
        //    error: function (xml, errstr, err) {
        //        layer.alert(errstr + '，系统异常！');
        //    }
        //});
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