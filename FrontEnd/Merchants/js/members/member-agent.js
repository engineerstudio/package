
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
    var agentId = rq_data.AgentId;
    // -- 设置返点Id
    var rebateId = 0;
    if (rq_data.AgentSetting.length != 0) {
        var rq_data_agentSetting = JSON.parse(rq_data.AgentSetting);
        rebateId = rq_data_agentSetting.RebateId;
    }
    // -- 根据请求数据填充form -- 
    $('#member-agent-name').text(rq_data.AccountName);
    $('[name="Type"]').attr("checked", rq_data.Type == 1 ? true : false);
    form.render('checkbox');

    // -- 获取代理返点活动数据 --
    $.ajax({
        url: "/mch/promotions/getagentpromodic",
        type: 'POST',
        data: { merchantId: rq_data.MerchantId, typeStr:'Commission' },
        success: function (data) {
            console.log(data);
            data = JSON.parse(data);
            if (data.code == 0) {
                layer.msg(data.msg);
                return;
            }
            else {
                var op = '';
                var rebateDicJson = JSON.parse(data.msg)
                for (var i in rebateDicJson) {
                    if (rebateId != 0 && rebateId == i)
                        op += '<option value="' + i + '" selected>' + rebateDicJson[i] + '</option>';
                    else
                        op += '<option value="' + i + '">' + rebateDicJson[i] + '</option>';
                }
                $('[name="RebateId"]').append(op);

                form.render('select');
            }
        }
    });


    //  -- 获取上级代理(所有为代理的用户)
    $.ajax({
        url: "/mch/member/getagents",
        type: 'POST',
        data: { merchantId: rq_data.MerchantId },
        success: function (data) {
            console.log(data);
            data = JSON.parse(data);
            if (data.code == 0) {
                layer.msg(data.msg);
                return;
            }
            else {
                var op = '';
                var agentDicJson = JSON.parse(data.msg);
                for (var i in agentDicJson) {
                    if (agentId != 0 && agentId == i)
                        op += '<option value="' + i + '" selected>' + agentDicJson[i] + '</option>';
                    else
                        op += '<option value="' + i + '">' + agentDicJson[i] + '</option>';
                }
                $('[name="AgentId"]').append(op);

                form.render('select');
            }
        }
    });





    form.on("submit(member-agent-submit)", function (data) {
        console.log(field);
        var field = data.field;
        field.Type = field.Type == 'on' ? 2 : 1;
        field.UserId = rq_data.Id;


        console.log(field);

        //return false;
        //var s = JSON.parse(field)
        //return false;
        //弹出loading
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        // 实际使用时的提交信息
        $.ajax({
            url: "/mch/member/setagent",
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