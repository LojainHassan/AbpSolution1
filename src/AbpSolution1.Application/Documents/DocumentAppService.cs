using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AbpSolution1.Documents;

public class DocumentAppService :
    CrudAppService<
        Document,
        DocumentDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateDocumentDto>,
    IDocumentAppService
{
    private readonly IRepository<WorkflowStatus, Guid> _workflowStatusRepository;
    private readonly IRepository<Volo.Abp.Identity.IdentityUser, Guid> _userRepository;

    public DocumentAppService(
        IRepository<Document, Guid> repository,
        IRepository<WorkflowStatus, Guid> workflowStatusRepository,
        IRepository<Volo.Abp.Identity.IdentityUser, Guid> userRepository)
        : base(repository)
    {
        _workflowStatusRepository = workflowStatusRepository;
        _userRepository = userRepository;
    }

    public override async Task<DocumentDto> GetAsync(Guid id)
    {
        var document = await Repository.GetAsync(id);
        var dto = ObjectMapper.Map<Document, DocumentDto>(document);
        
        var status = await _workflowStatusRepository.FindAsync(document.WorkflowStatusId);
        dto.WorkflowStatusName = status?.NameEn ?? string.Empty;

        var user = await _userRepository.FindAsync(document.UserId);
        dto.UserName = user?.UserName ?? string.Empty;

        return dto;
    }

    public override async Task<PagedResultDto<DocumentDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var queryable = await Repository.GetQueryableAsync();
        
        var query = from doc in queryable
                    join status in await _workflowStatusRepository.GetQueryableAsync() on doc.WorkflowStatusId equals status.Id into statuses
                    from status in statuses.DefaultIfEmpty()
                    join user in await _userRepository.GetQueryableAsync() on doc.UserId equals user.Id into users
                    from user in users.DefaultIfEmpty()
                    select new DocumentDto
                    {
                        Id = doc.Id,
                        Code = doc.Code,
                        NameAr = doc.NameAr,
                        NameEn = doc.NameEn,
                        DocumentNo = doc.DocumentNo,
                        WorkflowStatusId = doc.WorkflowStatusId,
                        WorkflowStatusName = status.NameEn,
                        UserId = doc.UserId,
                        UserName = user.UserName,
                        CreationTime = doc.CreationTime
                    };

        var count = await AsyncExecuter.CountAsync(query);
        
        var list = await AsyncExecuter.ToListAsync(query
            .OrderBy(NormalizeSorting(input.Sorting ?? string.Empty))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount));

        return new PagedResultDto<DocumentDto>(count, list);
    }

    private string NormalizeSorting(string sorting)
    {
        if (string.IsNullOrEmpty(sorting))
        {
            return "CreationTime DESC";
        }

        return sorting;
    }
}
