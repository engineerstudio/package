layui.config({
    base: "https://cdn.wwzznn.com/layuicms/js/extentions/"
}).extend({
    'ext': 'ext',
    "jqueryext": "jqueryext",
    "formExt": "formext"
});
layui.use(['form', 'element', 'layer', 'table', 'laytpl', 'jqueryext', 'ext', 'laydate', "formExt"], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl,
        element = layui.element,
        ext = layui.ext,
        laydate = layui.laydate,
        formExt = layui.formExt,
        table = layui.table;


    var rq = localStorage.getItem('prolist-edit');
    var rq_data = JSON.parse(decodeURI(rq));
    localStorage.removeItem('prolist-edit');

    $('[name="Tag"]').html(ext.getSelectOptionsByUrl('/mch/promotions/tagdic', '请选择活动标签'));
    console.log('tag')

    $.fillSelect('[name="AType"]', ext.getEnumType('ActivityType'));
    $.fillSelect('[name="BonusType"]', ext.getEnumType('BonusType'));
    $.fillSelect('[name="BonusCalType"]', ext.getEnumType('BonusCalType'));
    $.fillSelect('[name="IPCheckType"]', ext.getEnumType('IPCheckType'));
    $.fillSelect('[name="WashType"]', ext.getEnumType('WashType'));
    function init_data() {
        let conf = JSON.parse(rq_data.Config);
        $('[name="Id"]').val(rq_data.Id);
        $('[name="AType"]').val(rq_data.TypeStr);
        $('[name="Title"]').val(rq_data.Title);
        $('[name="Tag"]').val(rq_data.TagId);

        if (rq_data.Enabled)
            $('[name="Enabled"]').attr('checked', 'checked');
        if (rq_data.Visible)
            $('[name="Visible"]').attr('checked', 'checked');

        $('[name="StartTime"]').val(rq_data.StartTime);
        $('[name="EndTime"]').val(rq_data.EndTime);

        $('[name="BonusType"]').val(rq_data.Conf.BonusType);
        $('[name="BonusCalType"]').val(rq_data.Conf.BonusCalType);
        $('[name="BonusCalTypeValue"]').val(conf.BonusCalTypeValue);
        $('[name="IPCheckType"]').val(rq_data.Conf.IPCheckType);
        $('[name="WashType"]').val(rq_data.Conf.Wash);
        $('[name="WashValue"]').val(conf.WashValue);
        $('[name="ImgUrl"]').attr('src', rq_data.Cover);
        $('[name="H5ImgUrl"]').attr('src', rq_data.H5ImgUrl);
        $('#editor').val(rq_data.Description);

    }


    if (rq)
        init_data();

    // 初始化图片
    formExt.upload('.ImgUrl-btn', { url: '/mch/upload/image' }, function (seclector, data) {
        console.log(data);
        seclector.prev().attr('src', data.info.src);
        return false;
    });
    form.render();

    var keditor = KindEditor.create('#editor', {
        uploadJson: '/mch/upload/kindeditor',
        afterUpload: function (url) {
            console.log(url);
        }
    });

    form.on('submit(pro-createpage-submit)', function (e) {

        keditor.sync(); // 获取kindeditor的必须选项
        console.log(e.field)
        //let imgUrl = $('[name="ImgUrl"]').attr('src');
        //console.log(imgUrl);
        //return false;
        //弹出loading
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        e.field['Enabled'] = e.field['Enabled'] == 'on' ? true : false;
        e.field['Visible'] = e.field['Visible'] == 'on' ? true : false;


        $.post("/mch/promotions/savepro", {
            Id: e.field["Id"],
            CategoryId: e.field['CategoryId'],
            AType: e.field['AType'],
            Title: e.field['Title'],
            Config: e.field['Config'],
            Enabled: e.field['Enabled'],
            Visible: e.field['Visible'],
            StartTime: e.field['StartTime'],
            EndTime: e.field['EndTime'],
            BonusType: e.field['BonusType'],
            BonusCalType: e.field['BonusCalType'],
            IPCheckType: e.field['IPCheckType'],
            Wash: e.field['WashType'],
            WashValue: e.field['WashValue'] == '' ? 0 : e.field['WashValue'],
            BonusCalTypeValue: e.field['BonusCalTypeValue'] == '' ? 0 : e.field['BonusCalTypeValue'],
            Content: $('[name="Content"]').val(),
            H5Cover: $('[name="H5ImgUrl"]').attr('src'),
            Cover: $('[name="ImgUrl"]').attr('src'),
            TagId: $('[name="Tag"]').val() == null ? 0 : $('[name="Tag"]').val()
        }, function (res) {
            console.log(res);
            res = JSON.parse(res);
            layer.msg(res.msg);
            if (res.code == 1) {
                top.layer.close(index);
                layer.closeAll("iframe");
                //刷新父页面
                parent.location.reload();
            }
        })

        //$.ajax({
        //    type: 'POST',
        //    url: '/mch/promotions/savepro',
        //    data: obj,
        //    contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        //    success: function (data) {
        //        //var data = JSON.parse(data);
        //        console.log(data);
        //        //layer.msg(data.msg);
        //    },
        //    error: function (xml, errstr, err) {
        //        layer.alert(errstr + '，系统异常！');
        //    }
        //});
        return false;
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

});