layui.config({
    base: "https://cdn.wwzznn.com/layuicms/js/extentions/"
}).extend({
    'ext': 'ext',
    "jqueryext": "jqueryext",
    'formExt': 'formExt',
});
layui.use(['form', 'layer', 'layedit', 'table', 'laydate', 'upload', 'jqueryext', 'ext', 'formExt'], function () {
    var form = layui.form
    layer = parent.layer === undefined ? layui.layer : top.layer,
        laypage = layui.laypage,
        upload = layui.upload,
        layedit = layui.layedit,
        laydate = layui.laydate,
        table = layui.table,
        ext = layui.ext,
        formExt = layui.formExt,
        $ = layui.jquery;


    var rq = localStorage.getItem('agent-modify-data');
    var rq_data = JSON.parse(decodeURI(rq));
    localStorage.removeItem('agent-modify-data');

    var agentId = 0, commissionId = 0, contractId = 0, agentRebateId = 0;
    if (decodeURI(rq) == '{}') { console.log('new'); }
    else {
        agentId = rq_data.AgentId;
        var setting = JSON.parse(rq_data.AgentSetting);
        commissionId = setting.CommissionId;
        contractId = setting.ContractId;
        agentRebateId = 0;
        $('[name="Url"]').val(setting.Url);
        $('[name="AccountName"]').attr('disabled', 'disabled');
    }

    formExt.fill('#list-modify-form', rq_data, function () {
        var agentsHtml = ext.getSelectOptionsByUrl("/mch/member/getagents", '站点顶级代理', null, 0, agentId); // 缺少个默认值
        $('[name="ParentAgentId"]').html(agentsHtml);
        var comissionHtml = ext.getSelectOptionsByUrl("/mch/promotions/getagentpromodic", '请选择返佣方案', { typeStr: 'Commission' }, 0, commissionId);
        $('[name="CommissionId"]').html(comissionHtml);
        var contractHtml = ext.getSelectOptionsByUrl("/mch/promotions/getagentpromodic", '请选择契约方案', { typeStr: 'Contract' }, 0, contractId)
        $('[name="ContractId"]').html(contractHtml);
        var contractHtml = ext.getSelectOptionsByUrl("/mch/promotions/getagentpromodic", '请选择返点方案', { typeStr: 'AgentRebate' }, 0, agentRebateId)
        $('[name="AgentRebateId"]').html(contractHtml);
    });
    form.render();

    formExt.submit('list-modify-submit', "/mch/member/setagentrule")

})