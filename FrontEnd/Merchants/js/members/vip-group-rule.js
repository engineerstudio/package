layui.use(['form', 'layer', 'layedit', 'laydate', 'upload'], function () {
    var form = layui.form
    layer = parent.layer === undefined ? layui.layer : top.layer,
        laypage = layui.laypage,
        upload = layui.upload,
        layedit = layui.layedit,
        laydate = layui.laydate,
        $ = layui.jquery;



    var rq = getQueryVariable('p');
    var rq_data = JSON.parse(decodeURI(rq));
    console.log(rq_data);

    // -- 根据请求数据填充form -- 

    console.log(rq_data.GroupSetting);
    if (rq_data.GroupSetting.length != 0) {
        console.log('未设定');
        var data = JSON.parse(rq_data.GroupSetting);
        $('[name="AccumulatedRechargeAmount"]').val(data.AccumulatedRechargeAmount);
        $('[name="AccumulatedEffectiveAmount"]').val(data.AccumulatedEffectiveAmount);
        $('[name="StayGroupEffectiveAmount"]').val(data.AccumulatedEffectiveAmount);
        $('[name="WithdrawalsCount"]').val(data.AccumulatedEffectiveAmount);
        $('[name="WithdrawalTotalAccount"]').val(data.AccumulatedEffectiveAmount);
        $('[name="ProAmount"]').val(data.AccumulatedEffectiveAmount);
        $('[name="BirthAmount"]').val(data.AccumulatedEffectiveAmount);
        $('[name="MonthSalary"]').val(data.AccumulatedEffectiveAmount);
        $('[name="MaxRebate"]').val(data.AccumulatedEffectiveAmount);
        $('[name="EnabledAuto"]').attr("checked", data.EnabledAuto);
        $('[name="EnabledAutoGift"]').attr("checked", data.EnabledAutoGift);
        $('[name="Description"]').val(data.Description);


        form.render('checkbox');
    }


    form.on("submit(member-vip-group-rule-submit)", function (data) {
        var field = data.field;
        field.EnabledAuto = field.EnabledAuto == 'on' ? true : false;
        field.EnabledAutoGift = field.EnabledAutoGift == 'on' ? true : false;
        field.GroupId = rq_data.Id;


        console.log(field);

        //return false;
        //var s = JSON.parse(field)
        //return false;
        //弹出loading
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        // 实际使用时的提交信息
        $.ajax({
            url: "/mch/vips/setgroup",
            data: field,
            type: 'POST',
            success: function (res) {
                console.log(res);
                res = JSON.parse(res);
                top.layer.msg(res.msg);
                if (res.code == 1) {
                    setTimeout(function () {
                        top.layer.close(index);
                        layer.closeAll("iframe");
                        //刷新父页面
                        parent.location.reload();
                    }, 500);
                } else {
                    top.layer.close(index);
                }
            },
            error: function (res) {
                console.log(res);
                top.layer.msg(res.msg);
                top.layer.close(index);

            }
        })

        return false;
    })


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