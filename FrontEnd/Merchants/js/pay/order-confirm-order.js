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
    $('.member').text(rq_data.MemberId);
    $('.paymerchant').text(rq_data.PayMerchantId);
    $('.reqdepositamount').text(rq_data.ReqDepositAmount);
    $('.depositamount').text(rq_data.DepositAmount);
    $('.createTime').text(rq_data.CreateTime);
    $('.confirmTime').text(rq_data.ConfirmTime);
    $('.id').text(rq_data.Id);
    $('.payMerchantOrderId').text(rq_data.PayMerchantOrderId);
    
    $('[name="Id"]').val(rq_data.Id);
    $('[name="memberId"]').val(rq_data.MemberId);

    form.on("submit(pay-confirm-order-submit)", function (data) {

        //console.log(data.field);

        //return false;
        //弹出loading
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        // 实际使用时的提交信息
        $.post("/mch/pay/confirmpayorder", {
            id: data.field['Id'],
            memberId: data.field['memberId']
        }, function (res) {
            console.log(res);
            top.layer.close(index);
            //top.layer.msg("文章添加成功！");
            layer.closeAll("iframe");
            //刷新父页面
            parent.location.reload();
        })
        //setTimeout(function () {
        //    top.layer.close(index);
        //    top.layer.msg("文章添加成功！");
        //    layer.closeAll("iframe");
        //    //刷新父页面
        //    parent.location.reload();
        //}, 500);
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