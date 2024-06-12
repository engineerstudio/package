layui.use(['form', 'layer', 'jquery'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer
    $ = layui.jquery;
    localStorage.setItem("req_key", "/mch");
    $(".loginBody .seraph").click(function () {
        layer.msg("这只是做个样式，至于功能，你见过哪个后台能这样登录的？还是老老实实的找管理员去注册吧", {
            time: 5000
        });
    })


    //登录按钮
    form.on("submit(login)", function (data) {
        var obj = $(this);
        $.ajax({
            type: 'POST',
            url: '/mch/account/signin',
            data: data.field,
            success: function (res) {
                res = JSON.parse(res);
                console.log(res);
                if (res.code == 1) {
                    window.localStorage.setItem('name', res.name);
                    window.location.href = "/views/";
                }
            },
            complete: function () {
                obj.text("登录").removeAttr("disabled").removeClass("layui-disabled");
            }
        });

        return false;
    })


    $("#CaptchaCodeImg").click(function () {
        d = new Date();
        $("#CaptchaCodeImg").attr("src", "/mch/account/GetCaptchaImage?" + d.getTime());
    });


    //表单输入效果
    $(".loginBody .input-item").click(function (e) {
        e.stopPropagation();
        $(this).addClass("layui-input-focus").find(".layui-input").focus();
    })
    $(".loginBody .layui-form-item .layui-input").focus(function () {
        $(this).parent().addClass("layui-input-focus");
    })
    $(".loginBody .layui-form-item .layui-input").blur(function () {
        $(this).parent().removeClass("layui-input-focus");
        if ($(this).val() != '') {
            $(this).parent().addClass("layui-input-active");
        } else {
            $(this).parent().removeClass("layui-input-active");
        }
    })
})
