using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using AbpSolution1.WorkFlow;


namespace AbpSolution1.Controllers;
[RemoteService(Name = "Docsys")]
[Area("Business")]
[Route("api/Docsys/Workflow")]
public class WorkflowController : AbpController
{
    private readonly IDocumentWorkflowAppService _workflowAppService;

    public WorkflowController(IDocumentWorkflowAppService workflowAppService)
    {
        _workflowAppService = workflowAppService;
    }

    [HttpPost]
    [Route("document/receive-update-status")]
    public virtual Task<ReceiveAndUpdateStatusResponseDto> ReceiveAndUpdateStatusAsync([FromBody] ReceiveAndUpdateStatusDto input)
    {
        return _workflowAppService.ReceiveAndUpdateStatusAsync(input);
    }

    [HttpPost]
    [Route("approval/notify-manager")]
    public virtual Task<NotifyManagerResponseDto> NotifyManagerAsync([FromBody] NotifyManagerDto input)
    {
        return _workflowAppService.NotifyManagerAsync(input);
    }

    [HttpPost]
    [Route("approval/decision")]
    public virtual Task<ManagerDecisionResponseDto> ManagerDecisionAsync([FromBody] ManagerDecisionDto input)
    {
        return _workflowAppService.ManagerDecisionAsync(input);
    }

    [HttpPut]
    [Route("document/{documentId}/approve")]
    public virtual Task<ApproveDocumentResponseDto> ApproveDocumentAsync(Guid documentId, [FromBody] ApproveDocumentDto input)
    {
        return _workflowAppService.ApproveDocumentAsync(documentId, input);
    }

    [HttpPut]
    [Route("document/{documentId}/reject")]
    public virtual Task<RejectDocumentResponseDto> RejectDocumentAsync(Guid documentId, [FromBody] RejectDocumentDto input)
    {
        return _workflowAppService.RejectDocumentAsync(documentId, input);
    }

    [HttpPost]
    [Route("error/log")]
    public virtual Task<LogErrorResponseDto> LogErrorAsync([FromBody] LogErrorDto input)
    {
        return _workflowAppService.LogErrorAsync(input);
    }
}