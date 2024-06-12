layui.use(['form', 'layer', 'layedit', 'laydate', 'upload'], function () {
    var form = layui.form
    layer = parent.layer === undefined ? layui.layer : top.layer,
        laypage = layui.laypage,
        upload = layui.upload,
        layedit = layui.layedit,
        laydate = layui.laydate,
        $ = layui.jquery;

    var index = parent.layer.getFrameIndex(window.name);
    //console.log(parent.layer);
    //console.log(top.layer);
    //console.log(layer);
    var rq = getQueryVariable('p');
    var rq_data = JSON.parse(decodeURI(rq));
    console.log(rq_data);
    $('[name="Id"]').val(rq_data.Id);
    $('[name="MemberId"]').val(rq_data.MemberId);

    $.post('/mch/withdrawals/withdrawaldic', function (d) {
        d = JSON.parse(d);
        console.log(d);
        if (d.code == 0) {
            top.layer.msg('请先开启支付类型');
            return;
        }

        var op = '';
        for (var i in d.info) {
            op += '<option value="' + i + '">' + d.info[i] + '</option>';
        }

        console.log(op);
        $('[name="withdrawType"]').append(op);
        form.render();
    });


    form.on('select(PayCategory)', function (d) {
        console.log(d);

    });



    form.on("submit(withdraw-orderlist-review-confirm-submit)", function (data) {
        console.log(data);

        var d = { MerchantId: 0, OrderId: data.field['Id'], Marks: data.field['remarks'], WithdrawMerchantId: data.field['withdrawType'], MemberId: data.field['MemberId'] };
        console.log(d);

        $.post("/mch/withdrawals/submitorder", {
            q: d
        }, function (res) {
            console.log(res);
            if (typeof (res) != 'object')
                res = JSON.parse(res);
            console.log(res);
            if (res.code == 1) {
                top.layer.close(index);
                layer.closeAll("iframe");
                layer.close(index);
                parent.layer.close(index);
                //刷新父页面
                parent.location.reload();
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