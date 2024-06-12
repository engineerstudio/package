layui.use(['form', 'layer', 'layedit', 'laydate', 'upload'], function () {
    var form = layui.form
    layer = parent.layer === undefined ? layui.layer : top.layer,
        laypage = layui.laypage,
        upload = layui.upload,
        layedit = layui.layedit,
        laydate = layui.laydate,
        $ = layui.jquery;


    var rq = getQueryVariable('p');
    //var rq_data = JSON.parse(decodeURI(rq));
    console.log(rq);
    $('[name="FundsTransType"]').val(rq);

    var json_data = JSON.parse(localStorage.getItem('sys_enum_data'));
    function getFundsSubTypeData(k) {
        let key = 'FundLogType_' + k;
        let d = json_data[key]
        let html = '';
        for (var i in d) {
            html += '<option value="' + i + '">' + d[i] + '</option>';
        }
        $('[name="FundsSubType"]').html(html);
        form.render();
    }
    // 初始化二级菜单
    getFundsSubTypeData($('[name="FundsType"]').val());


    form.on("submit(funds-manager-submit)", function (data) {

        console.log(data.field);

        //return false;
        //弹出loading
        //  var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        // 实际使用时的提交信息
        $.post("/mch/pay/manualfunds", data.field, function (res) {
            console.log(res);
            //top.layer.close(index);
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

    form.on('select(FundsType)', function (e) {
        getFundsSubTypeData(e.value);
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