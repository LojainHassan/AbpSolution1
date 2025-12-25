using System;
using System.Collections.Generic;
using System.Text;

namespace AbpSolution1.WorkFlow;
// API 1: Receive Document Metadata
public class ReceiveDocumentDto
{
    public Guid DocumentId { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid UserId { get; set; }
    public bool IsFromWorkflow { get; set; }
}


// Combined API: Receive Metadata and Update Status
public class ReceiveAndUpdateStatusDto
{
    public Guid DocumentId { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid UserId { get; set; }
    public bool IsFromWorkflow { get; set; }

    // Status Update Fields
    public string Status { get; set; }
    public string WorkflowInstanceId { get; set; }
    public string Timestamp { get; set; }
}

public class ReceiveAndUpdateStatusResponseDto
{
    public bool Success { get; set; }
    public Guid DocumentId { get; set; }
    public string NewStatus { get; set; }
    public string WorkflowInstanceId { get; set; }
    public string Message { get; set; }
}

// API 3: Send Request to Department Manager
public class NotifyManagerDto
{
    public Guid DocumentId { get; set; }
    public Guid DepartmentId { get; set; }
    public string ManagerId { get; set; }
    public string WorkflowInstanceId { get; set; }
    public List<string> NotificationMethod { get; set; }
}

public class NotifyManagerResponseDto
{
    public bool Success { get; set; }
    public bool NotificationSent { get; set; }
    public string ManagerId { get; set; }
    public string ManagerEmail { get; set; }
    public string TaskId { get; set; }
}

// API 4: Manager Review and Decision
public class ManagerDecisionDto
{
    public string TaskId { get; set; }
    public string WorkflowInstanceId { get; set; }
    public Guid DocumentId { get; set; }
    public string Decision { get; set; }
    public string Comments { get; set; }
    public string ReviewerId { get; set; }
    public string ReviewedAt { get; set; }
}

public class ManagerDecisionResponseDto
{
    public bool Success { get; set; }
    public string Decision { get; set; }
    public Guid DocumentId { get; set; }
    public string NextStep { get; set; }
}

// API 5: Update Status to Approved
public class ApproveDocumentDto
{
    public string WorkflowInstanceId { get; set; }
    public string ApprovedBy { get; set; }
    public string ApprovedAt { get; set; }
    public string Comments { get; set; }
}

public class ApproveDocumentResponseDto
{
    public bool Success { get; set; }
    public Guid DocumentId { get; set; }
    public string Status { get; set; }
    public List<string> NextActions { get; set; }
}

// API 6: Update Status to Rejected
public class RejectDocumentDto
{
    public string WorkflowInstanceId { get; set; }
    public string RejectedBy { get; set; }
    public string RejectedAt { get; set; }
    public string RejectionReason { get; set; }
}

public class RejectDocumentResponseDto
{
    public bool Success { get; set; }
    public Guid DocumentId { get; set; }
    public string Status { get; set; }
    public bool WorkflowTerminated { get; set; }
}

// API 7: Handle Update Failure / Log Error
public class LogErrorDto
{
    public string WorkflowInstanceId { get; set; }
    public Guid DocumentId { get; set; }
    public string ErrorType { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime Timestamp { get; set; }
    public string StackTrace { get; set; }
}

public class LogErrorResponseDto
{
    public bool Success { get; set; }
    public bool ErrorLogged { get; set; }
    public string ErrorId { get; set; }
    public bool NotificationSent { get; set; }
}

// Camunda APIs
public class StartCamundaWorkflowDto
{
    public string BpmnProcessId { get; set; }
    public Dictionary<string, object> Variables { get; set; }
}

public class CompleteCamundaTaskDto
{
    public Dictionary<string, object> Variables { get; set; }
}