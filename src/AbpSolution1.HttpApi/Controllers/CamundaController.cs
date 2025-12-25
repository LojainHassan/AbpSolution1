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
[Route("api/Docsys/Camunda")]
public class CamundaController : AbpController
{
    private readonly IDocumentWorkflowAppService _workflowAppService;

    public CamundaController(IDocumentWorkflowAppService workflowAppService)
    {
        _workflowAppService = workflowAppService;
    }

    [HttpPost]
    [Route("workflow/start")]
    public virtual Task<ReceiveAndUpdateStatusResponseDto> StartWorkflowAsync(StartCamundaWorkflowDto input)
    {
        return _workflowAppService.StartCamundaWorkflowAsync(input);
    }

    [HttpPost]
    [Route("task/{taskId}/complete")]
    public virtual async Task<IActionResult> CompleteTaskAsync(string taskId, CompleteCamundaTaskDto input)
    {
        var success = await _workflowAppService.CompleteCamundaTaskAsync(taskId, input);
        if (success)
        {
            return Ok(new { success = true, taskId = taskId, message = "Task completed successfully" });
        }
        return BadRequest(new { success = false, message = "Failed to complete task" });
    }
}