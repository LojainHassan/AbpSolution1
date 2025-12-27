using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AbpSolution1.Documents;

public class WorkflowStatusAppService : AbpSolution1AppService, IWorkflowStatusAppService
{
    private readonly IRepository<WorkflowStatus, Guid> _repository;

    public WorkflowStatusAppService(IRepository<WorkflowStatus, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<PagedResultDto<WorkflowStatusDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var queryable = await _repository.GetQueryableAsync();
        
        var count = await AsyncExecuter.CountAsync(queryable);
        
        var list = await AsyncExecuter.ToListAsync(queryable
            .OrderBy(input.Sorting ?? "Code")
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount));

        return new PagedResultDto<WorkflowStatusDto>(
            count,
            ObjectMapper.Map<List<WorkflowStatus>, List<WorkflowStatusDto>>(list)
        );
    }

    public async Task<WorkflowStatusDto> GetAsync(Guid id)
    {
        var workflowStatus = await _repository.GetAsync(id);
        return ObjectMapper.Map<WorkflowStatus, WorkflowStatusDto>(workflowStatus);
    }

    public async Task<WorkflowStatusDto> CreateAsync(CreateUpdateWorkflowStatusDto input)
    {
        var workflowStatus = ObjectMapper.Map<CreateUpdateWorkflowStatusDto, WorkflowStatus>(input);
        workflowStatus = await _repository.InsertAsync(workflowStatus);
        return ObjectMapper.Map<WorkflowStatus, WorkflowStatusDto>(workflowStatus);
    }

    public async Task<WorkflowStatusDto> UpdateAsync(Guid id, CreateUpdateWorkflowStatusDto input)
    {
        var workflowStatus = await _repository.GetAsync(id);
        ObjectMapper.Map(input, workflowStatus);
        workflowStatus = await _repository.UpdateAsync(workflowStatus);
        return ObjectMapper.Map<WorkflowStatus, WorkflowStatusDto>(workflowStatus);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
