using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace AbpSolution1.WorkFlow;

public interface IDocumentWorkflowAppService : IApplicationService
{
    Task<ReceiveAndUpdateStatusResponseDto> ReceiveAndUpdateStatusAsync(ReceiveAndUpdateStatusDto input);
    Task<NotifyManagerResponseDto> NotifyManagerAsync(NotifyManagerDto input);
    Task<ManagerDecisionResponseDto> ManagerDecisionAsync(ManagerDecisionDto input);
    Task<ApproveDocumentResponseDto> ApproveDocumentAsync(Guid documentId, ApproveDocumentDto input);
    Task<RejectDocumentResponseDto> RejectDocumentAsync(Guid documentId, RejectDocumentDto input);
    Task<LogErrorResponseDto> LogErrorAsync(LogErrorDto input);

    // Camunda Specific
    Task<ReceiveAndUpdateStatusResponseDto> StartCamundaWorkflowAsync(StartCamundaWorkflowDto input);
    Task<bool> CompleteCamundaTaskAsync(string taskId, CompleteCamundaTaskDto input);
}