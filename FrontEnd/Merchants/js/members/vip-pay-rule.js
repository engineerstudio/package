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
    $('[name="Id"]').val(rq_data.Id);
    loadpaymerchant();






    form.on('submit(member-vip-pay-rule-submit)', function (e) {
        var index = parent.layer.getFrameIndex(window.name); 
        console.log(e.field);
        let payMerchantArr = [];
        for (let i in e.field) {
            console.log(i);
            if (i.startsWith('payMerchant['))
                payMerchantArr.push($('[name="' + i + '"]').attr('d-id'))
        }
        console.log(JSON.stringify(payMerchantArr))
        let d = { merchantPayIds: payMerchantArr.toString(), id: e.field['Id'] };
        console.log(d);
        $.ajax({
            url: "/mch/vips/setpay",
            data: d,
            type: 'POST',
            success: function (res) {
                res = JSON.parse(res);
                console.log(res);
                if (res.code == 1) {
                    layer.msg(res.msg)
                    parent.layer.close(index);
                } else {
                }
            },
            error: function (res) {
            }
        })


        return false;
    })






    function loadpaymerchant() {
        $.ajax({
            url: "/mch/pay/paymerchantdic",
            type: 'POST',
            success: function (res) {
                res = JSON.parse(res);
                if (res.code == 1) {
                    let d = JSON.parse(res.msg);
                    //console.log(d);
                    let html = '';
                    for (var i in d) {
                        //console.log(i);
                        html += '<input type="checkbox" name="payMerchant[' + i + ']" d-id=' + i + ' title="' + d[i] + '">';
                    }
                    //$(d).each(function (i, v) {
                    //    //console.log(i);
                    //    //html += '<input type="checkbox" name="like[' + i + ']" title="' + v + '">';
                    //});
                    console.log(html);
                    $('.multi').html(html);
                    form.render();

                } else {
                    //top.layer.close(index);
                }
            },
            error: function (res) {
                //console.log(res);
                //top.layer.msg(res.msg);
                //top.layer.close(index);

            }
        })
    }


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