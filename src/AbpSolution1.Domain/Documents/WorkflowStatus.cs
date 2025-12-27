using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace AbpSolution1.Documents;

public class WorkflowStatus : AuditedEntity<Guid>
{
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public string Code { get; set; }

    public WorkflowStatus()
    {
    }

    public WorkflowStatus(Guid id, string nameAr, string nameEn, string code)
        : base(id)
    {
        NameAr = nameAr;
        NameEn = nameEn;
        Code = code;
    }
}
