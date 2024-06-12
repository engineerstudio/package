layui.config({
    base: "https://cdn.wwzznn.com/layuicms/js/extentions/"
}).extend({
    'ext': 'ext',
    "jqueryext": "jqueryext",
    "formExt": "formext"
});
layui.use(['form', 'layer', 'layedit', 'table', 'laydate', 'upload', 'laytpl', 'formExt'], function () {
    var form = layui.form
    layer = parent.layer === undefined ? layui.layer : top.layer,
        laypage = layui.laypage,
        upload = layui.upload,
        layedit = layui.layedit,
        laydate = layui.laydate,
        table = layui.table,
        laytpl = layui.laytpl,
        formExt = layui.formExt,
        $ = layui.jquery;


    var rq = getQueryVariable('p');
    var rq_data = JSON.parse(decodeURI(rq));
    console.log(rq_data);


    var temp_banner_data;
    $.post('/mch/areas/sectiondetails', { sectionId: rq_data.Id }, function (d) {
        console.log(d);
        var d = JSON.parse(d);
        temp_banner_data = d;
        if (rq_data.DetailType == 'Multi') {
            let html = '';
            $(d.info).each(function (i, v) {
                let strJson = JSON.stringify(v);
                console.log(strJson);
                html += '<a class="layui-btn layui-btn-xs multi-click"  lay-data="' + encodeURI(strJson) + '">' + v.Name + '</a>';
            });
            $('.multi').html(html);
            $('#Multi').removeClass('util-hidden');
        }
        else if (rq_data.DetailType == 'Content') {
            console.log(d.info.length);
            if (d.info.length != 0) {
                let l = d.info[0];
                console.log(l)
                $('[name="D-Id"]').val(l.Id);
                $('[name="Tcontent"]').val(l.Tcontent);
            } else
                $('[name="D-Id"]').val(0);

            $('[name="Sec-Id"]').val(rq_data.Id);
            $('[name="DetailType"]').val(rq_data.DetailType);

            $('#Content').removeClass('util-hidden');
        }
        else if (rq_data.DetailType == 'Banner') {
            process_banner(d);
            //banner_click();
            fn_upload('.upload-btn');
            //banner_delete_click();
        }


    }).always(function (e) {
        console.log(1);
        $('.multi-click').on('click', multi_click); // 多种类型添加事件 
    });

    function process_banner(d) {
        console.log('init ...');
        $('[name="Sec-Id"]').val(rq_data.Id);
        $('[name="TypeDesc"]').val(rq_data.TypeDesc);
        $('[name="Description"]').val(rq_data.Description);
        let data = d.info;
        console.log(data);
        var getTpl = document.getElementById('view-banner-script').innerHTML
            , view = document.getElementById('view-banner');
        laytpl(getTpl).render(data, function (html) {
            //console.log(html);
            view.innerHTML = html;
        });

        $('#Banner').removeClass('util-hidden');
    }
    // 添加新的 banner
    function process_new_banner(d) {
        let data = [];
        data.push(d);
        //console.log(d)
        var getTpl = document.getElementById('view-banner-script').innerHTML;
        laytpl(getTpl).render(data, function (html) {
            $('#view-banner').append(html);
            //form.render(null, "section-banner-form");
        });
    }

    function multi_click(e) {
        console.log(e);
        console.log($(e.target).attr('lay-data'));

        layui.layer.open({
            title: "编辑",
            type: 2,
            maxmin: true,
            area: ['60%', '80%'],
            content: "section-edit-detail.html?p=" + ($(e.target).attr('lay-data')),
            success: function (layero, index) {

            }
        })


    }


    function fn_upload(id) {
        formExt.upload(id, { url: '/mch/upload/image/' }, function (seclector, data) {
            console.log(seclector);
            console.log(data);
            seclector.prev().attr('src', data.info.src);

        });
    }




    var delete_arr = [];
    function banner_delete_click(e) {
        $('.banner-delete').click(function (ee) {
            var lay_delte_temp_id = $(this).attr('lay-delete-id');
            if (lay_delte_temp_id.indexOf('n') == 0) { // 删除新增的数据
                $(this).parent().parent().remove();
                //var deleteId = $(this).attr('lay-delete-number');
                //if (delete_arr.indexOf(parseInt(deleteId)) >= 0) { // && parseInt(lay_delte_temp_id) == 0
                //    $(temp_banner_data.info).each(function (i, v) {
                //        console.log(v.Alias + '  ' + deleteId)
                //        if (v.Alias == parseInt(deleteId)) {
                //            console.log(v);
                //            temp_banner_data.info.splice(i, 1);
                //        }
                //    });
                //}
            } else {  // 删除数据
                $(this).parent().next().find('[name="Deleted"]').each(function (i, v) { // 设定当前的状态为 [删除]
                    $(v).val(1);
                });
                //let id = $($(this).parent().next().find('[name="Id"]')[0]).val();
                //$(temp_banner_data.info).each(function (i, v) {
                //    if (v.Id == parseInt(id)) {
                //        temp_banner_data.info.splice(i, 1);
                //    }
                //});
                $(this).parent().parent().hide();
            }
            return false;
        });
    }

    // 点击删除当前行操作
    $("#view-banner").on("click", ".banner-delete", function () {
        console.log('--------点击删除当前行操作--------');
        var lay_delte_temp_id = $(this).attr('lay-delete-id');
        if (lay_delte_temp_id.indexOf('n') == 0) { // 删除新增的数据
            $(this).parent().parent().remove();
        } else {  // 删除数据
            $(this).parent().next().find('[name="Deleted"]').each(function (i, v) { // 设定当前的状态为 [删除]
                $(v).val(1);
            });
            $(this).parent().parent().hide();
        }
        return false;
    });


    // 进行图片上传  然后重新遍历
    $('[lay-filter="area-section-edit-Content-new-img"]').click(function (e) {
        var delete_id = Math.round(Math.random() * 1000);
        delete_arr.push(delete_id);
        let id = 'n' + new Date().getTime();
        var newImgInfo = { Id: id, SectionId: 9, MerchantId: 1000, Name: "BANNER", Alias: delete_id, Enabled: true, HasSubMenu: false, PageUrl: "", Tcontent: "", ImgUrl: '' };
        process_new_banner(newImgInfo);
        var up_id = '#btn-upload-' + id;
        console.log(up_id)
        fn_upload(up_id);
        return false;
    });





    form.on('submit(area-section-edit-Banner-submit)', function (e) {
        console.log('area-section-edit-Banner-submit');

        console.log(e.field);
        if ($('.banner-contents').length == 0) return false;

        console.log($('.banner-contents').length);
        //var subJson = {};
        var sub_json_arr = [];
        $('.banner-contents').each(function (i, v) {
            //console.log(v);
            let sub_json = {};
            sub_json.ImgUrl = $(v).find('[name="ImgUrl"]').attr('src');
            sub_json.PageUrl = $(v).find('[name="PageUrl"]').val();
            sub_json.Tcontent = $(v).find('[name="Tcontent"]').val();

            let id = $(v).find('[name="Id"]').val();

            if (id.indexOf('n') == 0)
                id = 0;
            sub_json.Id = id;
            sub_json.Deleted = $(v).find('[name="Deleted"]').val() == 0 ? false : true;
            //console.log(sub_json);
            sub_json_arr.push(sub_json);
        });
        //subJson.SecId = $('[name="Sec-Id"]').val();
        //subJson.SectionBanners = sub_json_arr;
        //console.log(subJson);
        console.log(sub_json_arr);
        console.log($('[name="Sec-Id"]').val());
        //return false;
        $.post("/mch/areas/savebanner", { secId: $('[name="Sec-Id"]').val(), banner: sub_json_arr }, function (res) {

            console.log(res);
            let d = JSON.parse(res);
            console.log(d);

            if (d.code == 1) {
                //top.layer.close(index);
                top.layer.msg(d.msg);
                layer.closeAll("iframe");
                //刷新父页面
                parent.location.reload();
            }
        })

        return false;
    });



    form.on('submit(area-section-edit-Content-submit)', function (e) {
        e.field['secId'] = e.field['Sec-Id'];
        e.field['dId'] = e.field['D-Id'];
        console.log(e.field);

        $.post("/mch/areas/updatecontent", e.field, function (res) {
            console.log(res);
            //top.layer.close(index);
            //top.layer.msg("文章添加成功！");
            layer.closeAll("iframe");
            //刷新父页面
            parent.location.reload();
        })


        return false;
    })





    form.on("submit(area-section-edit-submit)", function (data) {
        console.log(data);
        console.log(data.field['Enabled']);
        $.post("/mch/game/UpdateMerchantGameStatus", { gameTypeStr: rq_data.TypeStr, enabled: data.field['Enabled'] == 'on' ? true : false }, function (res) {
            console.log(res);
            //top.layer.close(index);
            //top.layer.msg("文章添加成功！");
            layer.closeAll("iframe");
            //刷新父页面
            parent.location.reload();
        })


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


})