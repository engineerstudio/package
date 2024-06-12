layui.config({
    base: "https://cdn.wwzznn.com/layuicms/js/extentions/"
}).extend({
    'ext': 'ext',
    "jqueryext": "jqueryext"
});
layui.use(['form', 'layer', 'layedit', 'table', 'laydate', 'upload', 'jqueryext', 'ext'], function () {
    var form = layui.form
    layer = parent.layer === undefined ? layui.layer : top.layer,
        laypage = layui.laypage,
        upload = layui.upload,
        layedit = layui.layedit,
        laydate = layui.laydate,
        table = layui.table,
        ext = layui.ext,
        $ = layui.jquery;


    var rq = localStorage.getItem('area-notice-entity');
    localStorage.removeItem('area-notice-entity');
    console.log(decodeURI(rq));
    var rq_data = JSON.parse(decodeURI(rq));

    console.log(rq);
    console.log(rq_data == null);

    function init_form(d) {
        console.log('init_form');
        $('[name="Type"]').val(d.TypeStr);
        if (d.TypeStr == 'Announcement') $('.group-div').hide();
        $('[name="Id"]').val(d.Id);
        $('[name="Title"]').val(d.Title);
        $('[name="Description"]').val(d.Description);
        if (d.IsDisplay)
            $('[name="IsDisplay"]').attr('checked', 'checked');
    }

    if (rq != null)
        init_form(rq_data);

    // 初始化select "NoticeType"
    var notice_type_html = ext.getSelectOptions("NoticeType");
    $('[name="Type"]').append(notice_type_html);

    ext.getGroupCheckbox('/mch/vips/groupdic', '.groups', rq_data == null ? null : rq_data.GroupId);

    var keditor = KindEditor.create('#Description', {
        uploadJson: '/mch/upload/kindeditor',
        afterUpload: function (url) {
            console.log(url);
        }
    });


    form.render();


    form.on('select(Type)', function (data) {
        if (data.value == 'Announcement') {
            $('.group-div').hide();
        } else {
            $('.group-div').show();
        }
    });

    form.on("submit(area-notice-edit-submit)", function (data) {
        console.log(data.field);
        // 获取Group内容
        let groupArr = [];
        for (let i in data.field) {
            if (i.startsWith('group['))
                groupArr.push(Number($('[name="' + i + '"]').attr('d-id')))
        }
        console.log(groupArr.join(','))
        keditor.sync();
        var post_data = {
            Description: $('#Description').val(),
            Type: $('[name="Type"]').val(),
            IsDisplay: typeof (data.field["IsDisplay"]) == 'undefined' ? false : true,
            Title: data.field['Title'],
            Id: data.field['Id'],
            GroupId: groupArr.join(',')
        };
        console.log(post_data);
        $.post("/mch/areas/noticesave", post_data, function (res) {
            console.log(res);
            res = JSON.parse(res);
            top.layer.msg(res.msg);
            if (res.code == 1) {
                layer.closeAll("iframe");
                parent.location.reload();
            }
        })
        return false;
    });

})