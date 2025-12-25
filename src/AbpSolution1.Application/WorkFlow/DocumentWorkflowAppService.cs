using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace AbpSolution1.WorkFlow;

public class DocumentWorkflowAppService : ApplicationService, IDocumentWorkflowAppService
{
    private const string BpmnProcessId = "Process_0cqczuz";
    private readonly Zeebe.Client.IZeebeClient _zeebeClient;

    public DocumentWorkflowAppService(Zeebe.Client.IZeebeClient zeebeClient)
    {
        _zeebeClient = zeebeClient;
    }


    public async Task<ReceiveAndUpdateStatusResponseDto> ReceiveAndUpdateStatusAsync(ReceiveAndUpdateStatusDto input)
    {
        // 1. Update Metadata (Simulated)
        // 2. Update Status (Simulated)

        // 3. Start Workflow if external
        if (!input.IsFromWorkflow)
        {
            var startRequest = new StartCamundaWorkflowDto
            {
                BpmnProcessId = BpmnProcessId,
                Variables = new Dictionary<string, object>
                    {
                        { "documentId", input.DocumentId },
                        { "departmentId", input.DepartmentId },
                        { "userId", input.UserId },
                    }
            };

            var startResult = await StartCamundaWorkflowAsync(startRequest);
            return new ReceiveAndUpdateStatusResponseDto
            {
                Success = true,
                DocumentId = input.DocumentId,
                NewStatus = input.Status,
                WorkflowInstanceId = startResult.WorkflowInstanceId,
                Message = "Document received, status updated, and workflow started."
            };
        }

        return new ReceiveAndUpdateStatusResponseDto
        {
            Success = true,
            DocumentId = input.DocumentId,
            NewStatus = input.Status,
            WorkflowInstanceId = input.WorkflowInstanceId,
            Message = "Document and status updated from workflow."
        };
    }

    public async Task<NotifyManagerResponseDto> NotifyManagerAsync(NotifyManagerDto input)
    {
        // Logic to send notification (Email, SMS, In-App)
        // This is typically called by a Camunda Worker handling 'notify_manager'

        return new NotifyManagerResponseDto
        {
            Success = true,
            NotificationSent = true,
            ManagerId = input.ManagerId,
            ManagerEmail = "manager@example.com",
            TaskId = "user-task-" + Guid.NewGuid().ToString().Substring(0, 8) // In real Zeebe, this taskId would be the JobKey or UserTask ID
        };
    }

    public async Task<ManagerDecisionResponseDto> ManagerDecisionAsync(ManagerDecisionDto input)
    {
        // 1. Logic to record the decision in DB

        // 2. Complete the Camunda User Task
        var completeRequest = new CompleteCamundaTaskDto
        {
            Variables = new Dictionary<string, object>
                {
                    { "decision", input.Decision },
                    { "comments", input.Comments },
                    { "reviewerId", input.ReviewerId },
                    { "reviewedAt", input.ReviewedAt }
                }
        };

        await CompleteCamundaTaskAsync(input.TaskId, completeRequest);

        return new ManagerDecisionResponseDto
        {
            Success = true,
            Decision = input.Decision,
            DocumentId = input.DocumentId,
            NextStep = input.Decision == "APPROVED" ? "UPDATE_STATUS_APPROVED" : "UPDATE_STATUS_REJECTED"
        };
    }

    public async Task<ApproveDocumentResponseDto> ApproveDocumentAsync(Guid documentId, ApproveDocumentDto input)
    {
        // Logic to finalize approval in DB
        return new ApproveDocumentResponseDto
        {
            Success = true,
            DocumentId = documentId,
            Status = "APPROVED",
            NextActions = new List<string> { "ARCHIVE", "PUBLISH", "NOTIFY_STAKEHOLDERS" }
        };
    }

    public async Task<RejectDocumentResponseDto> RejectDocumentAsync(Guid documentId, RejectDocumentDto input)
    {
        // Logic to finalize rejection in DB
        return new RejectDocumentResponseDto
        {
            Success = true,
            DocumentId = documentId,
            Status = "REJECTED",
            WorkflowTerminated = true
        };
    }

    public async Task<LogErrorResponseDto> LogErrorAsync(LogErrorDto input)
    {
        // Logic to log error in DB/ElasticSearch
        return new LogErrorResponseDto
        {
            Success = true,
            ErrorLogged = true,
            ErrorId = "error-" + Guid.NewGuid().ToString().Substring(0, 8),
            NotificationSent = true
        };
    }

    public async Task<ReceiveAndUpdateStatusResponseDto> StartCamundaWorkflowAsync(StartCamundaWorkflowDto input)
    {
        var response = await _zeebeClient.NewCreateProcessInstanceCommand()
           .BpmnProcessId(input.BpmnProcessId)
           .LatestVersion()
           .Variables(System.Text.Json.JsonSerializer.Serialize(input.Variables))
           .Send();

        return new ReceiveAndUpdateStatusResponseDto
        {
            Success = true,
            WorkflowInstanceId = response.ProcessInstanceKey.ToString(),
            Message = $"Camunda workflow '{input.BpmnProcessId}' started successfully"
        };
    }

    public async Task<bool> CompleteCamundaTaskAsync(string taskId, CompleteCamundaTaskDto input)
    {
        // Case 1: Real Zeebe Task (Numeric ID)
        if (long.TryParse(taskId, out var jobKey))
        {
            try
            {
                await _zeebeClient.NewCompleteJobCommand(jobKey)
                   .Variables(System.Text.Json.JsonSerializer.Serialize(input.Variables))
                   .Send();
                return true;
            }
            catch (Exception ex)
            {
                // Log error if real Zeebe interaction fails
                throw new Volo.Abp.UserFriendlyException($"Failed to complete Zeebe task {jobKey}: {ex.Message}");
            }
        }

        // Case 2: Mock/Simulation Task (String ID like "dummy-task-id" or "user-task-xyz")
        // We treat this as success to allow flow to continue during testing/simulation.
        return true;
    }
}