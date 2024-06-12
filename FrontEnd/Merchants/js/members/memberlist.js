layui.config({
    base: "https://cdn.wwzznn.com/layuicms/js/extentions/"
}).extend({
    'ext': 'ext',
    //"jqueryext": "jqueryext"
});
layui.use(['form', 'layer', 'laydate', 'table', 'laytpl', 'jqueryext', 'ext'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laydate = layui.laydate,
        laytpl = layui.laytpl,
        ext = layui.ext,
        table = layui.table;


   var x=  $.getQueryVariable('p');
    console.log(x);
    $('[name="MemberType"]').html(ext.getSelectOptions('UserType', '请选择搜索的会员类别'));
    $('[name="GroupId"]').html(ext.getSelectOptionsByUrl('/mch/vips/groupdic', '请选择搜索的会员分组'));


    form.render();
    //新闻列表
    var tableIns = table.render({
        elem: '#member-list',
        url: '/mch/member/load',
        method: 'post',
        contentType: 'application/json',
        //where: { 'b': 'a' },
        cellMinWidth: 95,
        page: true,
        height: "full-125",
        limit: 20,
        limits: [10, 15, 20, 25],
        id: "member-listTable",
        cols: [[
            { type: "checkbox", width: 50 },// fixed: "left",
            { field: 'Id', title: 'ID', width: 90, align: "center" },
            { field: 'AccountName', title: '账户名', width: 180, align: 'center' },
            { templet: '#member-type', title: '账户类型', align: 'center' },
            { field: 'GroupName', title: '会员分组', align: 'center' },
            { title: '操作', templet: '#member-listBar', align: "center" } // fixed: "right",
        ]]
    });

    //是否置顶
    form.on('switch(newsTop)', function (data) {
        var index = layer.msg('修改中，请稍候', { icon: 16, time: false, shade: 0.8 });
        setTimeout(function () {
            layer.close(index);
            if (data.elem.checked) {
                layer.msg("置顶成功！");
            } else {
                layer.msg("取消置顶成功！");
            }
        }, 500);
    })


    //搜索
    form.on("submit(memberlist-search-submit)", function (e) {
        console.log(e.field);
        if (e.field['Id'] == '') e.field['Id'] = 0;
        if (e.field['GroupId'] == '') e.field['GroupId'] = 0;

        table.reload("member-listTable", {
            contentType: 'application/x-www-form-urlencoded',
            page: {
                curr: 1 //重新从第 1 页开始
            },
            where: e.field
        });
    });


    // 添加站点
    $('.member-addnew').click(function () {
        var index = layui.layer.open({
            title: "添加站点",
            type: 2,
            content: "member-add.html",
            success: function (layero, index) {

            }
        })
    });


    //添加文章
    function addNews(edit) {
        var index = layui.layer.open({
            title: "添加文章",
            type: 2,
            content: "newsAdd.html",
            success: function (layero, index) {
                var body = layui.layer.getChildFrame('body', index);
                if (edit) {
                    body.find(".newsName").val(edit.newsName);
                    body.find(".abstract").val(edit.abstract);
                    body.find(".thumbImg").attr("src", edit.newsImg);
                    body.find("#news_content").val(edit.content);
                    body.find(".newsStatus select").val(edit.newsStatus);
                    body.find(".openness input[name='openness'][title='" + edit.newsLook + "']").prop("checked", "checked");
                    body.find(".newsTop input[name='newsTop']").prop("checked", edit.newsTop);
                    form.render();
                }
                setTimeout(function () {
                    layui.layer.tips('点击此处返回文章列表', '.layui-layer-setwin .layui-layer-close', {
                        tips: 3
                    });
                }, 500)
            }
        })
        layui.layer.full(index);
        //改变窗口大小时，重置弹窗的宽高，防止超出可视区域（如F12调出debug的操作）
        $(window).on("resize", function () {
            layui.layer.full(index);
        })
    }
    $(".addNews_btn").click(function () {
        addNews();
    })

    //批量删除
    $(".delAll_btn").click(function () {
        var checkStatus = table.checkStatus('member-listTable'),
            data = checkStatus.data,
            newsId = [];
        if (data.length > 0) {
            for (var i in data) {
                newsId.push(data[i].newsId);
            }
            layer.confirm('确定删除选中的文章？', { icon: 3, title: '提示信息' }, function (index) {
                // $.get("删除文章接口",{
                //     newsId : newsId  //将需要删除的newsId作为参数传入
                // },function(data){
                tableIns.reload();
                layer.close(index);
                // })
            })
        } else {
            layer.msg("请选择需要删除的文章");
        }
    })

    //列表操作
    table.on('tool(member-list)', function (obj) {
        var layEvent = obj.event,
            data = obj.data;

        console.log(data);
        data.Pasw = '';
        if (layEvent === 'edit') { //编辑
            layui.layer.open({
                title: "编辑",
                type: 2,
                maxmin: true,
                area: ['60%', '80%'],
                content: "member-edit.html?p=" + encodeURI(JSON.stringify(data)),
                success: function (layero, index) {

                }
            })
        } else if (layEvent === 'agent') { //代理配置
            console.log('group-rule');
            layui.layer.open({
                title: "代理配置",
                type: 2,
                maxmin: true,
                area: ['60%', '80%'],
                content: "member-agent.html?p=" + encodeURI(JSON.stringify(data)),
                success: function (layero, index) {

                }
            })

        } else if (layEvent === 'credit') { //账变日志
            layui.layer.open({
                title: "用户账变",
                type: 2,
                maxmin: true,
                area: ['60%', '80%'],
                content: "member-credit.html?p=" + encodeURI(JSON.stringify(data)),
                success: function (layero, index) {

                }
            })
        } else if (layEvent === 'loginlog') { //账变日志


        }


    });

})