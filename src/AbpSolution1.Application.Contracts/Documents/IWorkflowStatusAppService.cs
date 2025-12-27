using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AbpSolution1.Documents;

public interface IWorkflowStatusAppService : IApplicationService
{
    Task<PagedResultDto<WorkflowStatusDto>> GetListAsync(PagedAndSortedResultRequestDto input);

    Task<WorkflowStatusDto> GetAsync(Guid id);

    Task<WorkflowStatusDto> CreateAsync(CreateUpdateWorkflowStatusDto input);

    Task<WorkflowStatusDto> UpdateAsync(Guid id, CreateUpdateWorkflowStatusDto input);

    Task DeleteAsync(Guid id);
}
