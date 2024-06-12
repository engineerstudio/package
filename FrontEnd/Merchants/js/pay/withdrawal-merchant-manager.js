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
    //console.log(rq_data);
    var config = {};
    if (JSON.stringify(rq_data) != '{}') {
        console.log(rq_data.ConfigStr)
        config = JSON.parse(rq_data.ConfigStr);
        //console.log('1111');
        $('[name="Name"]').val(rq_data.Name);
        console.log(rq_data.Id)
        $('[name="Id"]').val(rq_data.Id);
        $('[name="Enabled"]').attr('checked', rq_data.Enabled);

        //if (typeof (rq_data.PayCategoryId) != 'undefined')
        //    loadpaycategoryconfig(rq_data.PayCategoryId);
    }

    $.post('/mch/withdrawals/paycategory', function (d) {
        //console.log(d);
        d = JSON.parse(d);
        //console.log(d);

        var op = '';
        for (var i in d.info) {
            console.log(i);
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

        loadpaycategoryconfig(d.value);

    });

    function loadpaycategoryconfig(dd) {
        // 只获取配置内容
        $.post('/mch/withdrawals/paycategoryconfig', { id: dd }, function (d) {
            d = JSON.parse(d);
            console.log(d);
            //$('[name="Id"]').val(d.info.Id);
            let config = JSON.parse(d.info.ConfigStr);
            console.log(config);
            var op = '';
            for (var i in config) {
                //console.log(config[i])
                op += '<div class="layui-form-item">';
                op += '<label class="layui-form-label">' + config[i] + '</label>';
                op += '<div class="layui-input-block">';
                op += '<input type="text" class="layui-input" name="config.' + i + '" placeholder="请输入' + config[i] + '" value="' + getconfigvalue(i) + '"   >';
                op += '</div></div>';
            }
            $('#withdrawal-channel-config').html('');
            //console.log(op);
            $('#withdrawal-channel-config').append(op);

        });
    }

    function getconfigvalue(d) {
        console.log(d)
        console.log(config)
        return config[d];
    }

    form.on("submit(withdrawal-merchant-manager-submit)", function (data) {
        console.log(data);
        var configStr = '';
        var arr = {};
        for (var i in data.field) {
            if (i.startsWith('config')) {
                var ii = i.split('.');
                arr[ii[1]] = data.field[i];
            }
        }
        console.log(JSON.stringify(arr));
        data.field['ConfigStr'] = JSON.stringify(arr);
        data.field['Enabled'] = data.field['Enabled'] == 'on' ? true : false;
        var d = { Id: data.field['Id'], MerchantId: 0, Name: data.field['Name'], Enabled: data.field['Enabled'], PayCategory: data.field['PayCategory'], ConfigStr: JSON.stringify(arr) };
        console.log(d);
        //return false;

        $.post("/mch/withdrawals/savemerchantconfig", {
            d: d
        }, function (res) {
            console.log(res);
            res = JSON.parse(res);
            top.layer.msg(res.msg);
            if (res.code == 1) {
                layer.closeAll("iframe");
                parent.location.reload();
            }
        })
        return false;
    })


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