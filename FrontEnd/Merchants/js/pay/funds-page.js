layui.use(['form', 'layer', 'table', 'laytpl'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl,
        table = layui.table;



    $(".panel").on("click", function () {

        console.log($(this).attr('lay-datatype'));
        var type = $(this).attr('lay-datatype');
        var url = '';
        var txt = '';
        switch (type) {
            case 'addition':
                url = 'funds-additionsubtraction.html?p=In';
                txt = '手动加款';
                break;
            case 'subtraction':
                url = 'funds-additionsubtraction.html?p=Out';
                txt = '手动减款';
                break;
            default:
        }


        //return false;

        var index = layui.layer.open({
            title: txt,
            type: 2,
            anim: 1,
            area: ['60%', '80%'],
            content: url,
            success: function (layero, index) {
                //var body = layui.layer.getChildFrame('body', index);
                //body.find("#Id").val(data.Id);
            }
        });
    });



    //$(".pay-type-manager").on("click", function () {
    //    var index = layui.layer.open({
    //        title: '支付类型配置',
    //        type: 2,
    //        anim: 1,
    //        area: ['60%', '80%'],
    //        content: "pay-type-manager.html?p={}",
    //        success: function (layero, index) {
    //            //var body = layui.layer.getChildFrame('body', index);
    //            //body.find("#Id").val(data.Id);
    //        }
    //    });
    //});


});
