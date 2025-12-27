using System;
using Volo.Abp.Application.Dtos;

namespace AbpSolution1.Documents;

public class WorkflowStatusDto : EntityDto<Guid>
{
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public string Code { get; set; }
}
