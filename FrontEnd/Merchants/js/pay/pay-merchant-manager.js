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
    var config = {};
    var valid = {};
    if (JSON.stringify(rq_data) != '{}') {
        config = JSON.parse(rq_data.ConfigStr);
        valid = JSON.parse(rq_data.ValidationStr);
        //console.log(config);
        //console.log(valid);
        $('[name="Name"]').val(rq_data.Name);
        $('[name="Id"]').val(rq_data.Id);
        $('[name="Price_Min"]').val(valid.Price_Min);
        $('[name="Price_Max"]').val(valid.Price_Max);
        $('[name="FixedRange"]').val(valid.FixedRange);
        //console.log(rq_data.Enabled);
        $('[name="Enabled"]').attr('checked', rq_data.Enabled);
        //form.render();
    }
    // load pay type data  

    $.post('/mch/pay/paytypelist', function (d) {
        d = JSON.parse(d);
        //console.log(d);
        if (d.code == 0) {
            top.layer.msg('请先开启支付类型');
            return;
        }
        console.log('paytypeId:' + rq_data.PayTypeId)
        var op = '';
        for (var i = 0; i < d.info.length; i++) {
            console.log('dddddd:' + d.info[i].Id)
            if (rq_data.PayTypeId == d.info[i].Id)
                op += '<option value="' + d.info[i].Id + '" selected>' + d.info[i].Name + '</option>';
            else
                op += '<option value="' + d.info[i].Id + '">' + d.info[i].Name + '</option>';
        }
        $('[name="PayType"]').append(op);
        form.render();
    });


    $.get('/mch/pay/paycategory', function (d) {
        d = JSON.parse(d);
        //console.log(d);
        var op = '';
        for (var i in d.info) {
            if (rq_data.PayCategoryId == i)
                op += '<option value="' + i + '" selected>' + d.info[i] + '</option>';
            else
                op += '<option value="' + i + '">' + d.info[i] + '</option>';
        }
        $('[name="PayCategory"]').append(op);
        if (typeof (rq_data.PayCategoryId) != 'undefined')
            loadpaycategoryconfig(rq_data.PayCategoryId);
        form.render();
    });

    form.on('select(PayCategory)', function (d) {
        //console.log(d);

        //console.log(d.value);  // PayCategoryId

        loadpaycategoryconfig(d.value);

    });

    function loadpaycategoryconfig(dd) {

        $.get('/mch/pay/paycategoryconfig?id=' + dd, function (d) {
            d = JSON.parse(d);
            //console.log(d);
            var config = JSON.parse(d.info.ConfigStr);
            console.log(config);
            var op = '';
            for (var i in config) {
                op += '<div class="layui-form-item">';
                op += '<label class="layui-form-label">' + config[i] + '</label>';
                op += '<div class="layui-input-block">';
                op += '<input type="text" class="layui-input" name="config.' + i + '" placeholder="请输入' + config[i] + '" value="' + getconfigvalue(i) + '"   >';
                op += '</div></div>';
            }
            $('#pay-channel-config').html('');
            //console.log(op);
            $('#pay-channel-config').append(op);

            //for (var i in d.info) {
            //    op += '<option value="' + i + '">' + d.info[i] + '</option>';
            //}
            //$('[name="PayCategory"]').append(op);
            //form.render();
        });
    }

    function getconfigvalue(d) {
        return config[d];
    }

    form.on("submit(pay-merchant-manager-submit)", function (data) {
        console.log(data);
        var configStr = '';
        var arr = {};
        for (var i in data.field) {
            //console.log(i);
            if (i.startsWith('config')) {
                var ii = i.split('.');
                arr[ii[1]] = data.field[i];
            }
        }
        console.log(JSON.stringify(arr));
        data.field['ConfigStr'] = JSON.stringify(arr);

        var arr_v = {};
        arr_v['FixedRange'] = data.field['FixedRange'];
        arr_v['Price_Max'] = data.field['Price_Max'];
        arr_v['Price_Min'] = data.field['Price_Min'];

        data.field['ValidationStr'] = JSON.stringify(arr_v);
        console.log(data.field);
        data.field['Enabled'] = data.field['Enabled'] == 'on' ? true : false;
        //return false;  
        var d = { Id: data.field['Id'], MerchantId: 0, Name: data.field['Name'], Enabled: data.field['Enabled'], PayCategory: data.field['PayCategory'], PayType: data.field['PayType'], ValidationStr: JSON.stringify(arr_v), ConfigStr: JSON.stringify(arr) };
        console.log(d);

        //  Id PayType
        //return false;
        //弹出loading
        //var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        // 实际使用时的提交信息
        $.post("/mch/pay/savemerchantconfig", {
            d: d
        }, function (res) {
            console.log(res);
            res = JSON.parse(res);
            top.layer.msg(res.msg);
            if (res.code == 1) {
                //top.layer.close(index);
                layer.closeAll("iframe");
                //刷新父页面
                parent.location.reload();
            }
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


    $('[name="PayType"]').val(rq_data.PayType);
    $('[name="PayCategory"]').val(rq_data.PayCategoryId);
    form.render();




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