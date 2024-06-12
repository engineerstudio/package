layui.use(['form', 'layer', 'layedit', 'table', 'laydate', 'upload'], function () {
    var form = layui.form
    layer = parent.layer === undefined ? layui.layer : top.layer,
        laypage = layui.laypage,
        upload = layui.upload,
        layedit = layui.layedit,
        laydate = layui.laydate,
        table = layui.table,
        $ = layui.jquery;


    var order_id = getQueryVariable('orderId');
    var cat = getQueryVariable('cat');

    $.post('/mch/game/gameordersdetails', { orderId: order_id, cate: cat }, function (d) {
        d = JSON.parse(d);
        for (let i in d.info) {
            $('[name="' + i + '"]').html(d.info[i]);
        }
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


})