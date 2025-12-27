using System;
using Volo.Abp.Application.Dtos;

namespace AbpSolution1.Documents;

public class DocumentDto : AuditedEntityDto<Guid>
{
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public string Code { get; set; }
    public string DocumentNo { get; set; }
    public Guid UserId { get; set; }
    public Guid WorkflowStatusId { get; set; }
    
    public string WorkflowStatusName { get; set; }
    public string UserName { get; set; }
}
