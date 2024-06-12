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
    let config = JSON.parse(rq_data.Config);
    let ruleConfig = '';
    console.log(config.RuleConfig != null)
    if (config.RuleConfig != null) {
        ruleConfig = JSON.parse(config.RuleConfig);
        console.log('ruleConfig->' + ruleConfig);
        let html = '';
        for (var i in ruleConfig) {
            console.log(i)
            html += tableTrInit(ruleConfig[i].DayCount, ruleConfig[i].RechargeAmount, ruleConfig[i].WashAmount, ruleConfig[i].Reward)
        }
        $('.weeklyCheckin-rule-tbody').append(html);
    }
    console.log(config)
    $.fillSelect('[name="AType"]', ext.getEnumType('ActivityType'));
    function init_data() {
        //console.log(rq_data);
        let conf = JSON.parse(rq_data.Config);
        $('[name="Id"]').val(rq_data.Id);
        $('[name="AType"]').val(rq_data.TypeStr);
        $('[name="Title"]').val(rq_data.Title);
        if (rq_data.Enabled)
            $('[name="Enabled"]').attr('checked', 'checked');
        if (rq_data.Visible)
            $('[name="Visible"]').attr('checked', 'checked');
    }

    if (rq)
        init_data();

    $('.weeklyCheckin-edit-rule').click(function () {
        if (Number($('[name="DayCount"]').val()) == 0) return;
        let html = tableTrInit(Number($('[name="DayCount"]').val()), Number($('[name="RechargeAmount"]').val()), Number($('[name="WashAmount"]').val()), Number($('[name="Reward"]').val()));
        $('.weeklyCheckin-rule-tbody').append(html);
    });

    function tableTrInit(dayCount, rechargeAmount, washAmount, reward) {
        let html = '<tr>';
        html += '<td  d-name="DayCount">' + dayCount + '</td>';
        html += '<td  d-name="RechargeAmount">' + rechargeAmount + '</td>';
        html += '<td  d-name="WashAmount">' + washAmount + '</td>';
        html += '<td  d-name="Reward">' + reward + '</td>';
        html += '<td><a class="layui-btn layui-btn-danger layui-btn-xs weeklyCheckin-edit-rule-delete">删除</a> </td>';
        html += '</tr>';
        return html;
    }

    $(document).on('click', ".weeklyCheckin-edit-rule-delete", function () {
        $(this).parent().parent().remove();
    });

    form.render();

    form.on('submit(pro-createpage-submit)', function (e) {
        let arr = new Array();
        $('.weeklyCheckin-rule-tbody').find('tr').each(function (i, v) {
            let subArr = {};
            $(v).find('td').each(function (ii, vv) {
                if ($(vv).attr('d-name') != undefined)
                    subArr[$(vv).attr('d-name')] = $(vv).text();
            })
            arr.push(subArr);
        });

        var config = {
            RuleConfig: JSON.stringify(arr)
        };


        //弹出loading
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        $.post("/mch/promotions/savepromoconfig", {
            aType: e.field['AType'],
            Title: e.field['Title'],
            config: JSON.stringify(config),
            id: rq_data.Id,
            enabled: e.field['Enabled']
        }, function (res) {
            layer.msg(res.msg);
            top.layer.close(index);
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

});