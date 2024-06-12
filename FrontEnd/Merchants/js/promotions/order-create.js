layui.config({
    base: "https://cdn.wwzznn.com/layuicms/js/extentions/"
}).extend({
    'ext': 'ext',
    "jqueryext": "jqueryext"
});
layui.use(['form', 'layer', 'layedit', 'laydate', 'upload', 'jqueryext', 'ext'], function () {
    var form = layui.form
    layer = parent.layer === undefined ? layui.layer : top.layer,
        laypage = layui.laypage,
        upload = layui.upload,
        layedit = layui.layedit,
        laydate = layui.laydate,
        ext = layui.ext,
        $ = layui.jquery;



    // groupdic
    $('[name="AType"]').html(ext.getSelectOptions('FundLogType_Promotions', '请选择搜索的活动类型'));

    form.render();
    form.on("submit(order-create-submit)", function (data) {
        //弹出loading
        //var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });

        console.log(data.field);
        var postObj = { AType: $('[name="AType"]').val(), ProId: $('[name="ProId"]').val(), MemberId: $('[name="MemberId"]').val(), Reward: $('[name="Reward"]').val(), IP: $('[name="IP"]').val(), Description: $('[name="Description"]').val() };

        console.log(postObj);
        //return false;
        // 实际使用时的提交信息
        $.post("/mch/promotions/saveorder", postObj, function (res) {
            console.log(res);
            //setTimeout(function () {
            //    //top.layer.close(index);
            //    top.layer.msg("用户添加成功！");
            //    layer.closeAll("iframe");
            //    //刷新父页面
            //    parent.location.reload();
            //}, 500);

        })
        return false;
    })

}) 
