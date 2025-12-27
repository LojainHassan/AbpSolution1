using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace AbpSolution1.Documents;

public class Document : AuditedAggregateRoot<Guid>
{
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public string Code { get; set; }
    public string DocumentNo { get; set; }
    public Guid UserId { get; set; }
    public Guid WorkflowStatusId { get; set; }

    public Document()
    {
    }

    public Document(
        Guid id, 
        string nameAr, 
        string nameEn, 
        string code, 
        string documentNo, 
        Guid userId, 
        Guid workflowStatusId)
        : base(id)
    {
        NameAr = nameAr;
        NameEn = nameEn;
        Code = code;
        DocumentNo = documentNo;
        UserId = userId;
        WorkflowStatusId = workflowStatusId;
    }
}
