using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AbpSolution1.Documents;

public interface IDocumentAppService :
    ICrudAppService< 
        DocumentDto, 
        Guid, 
        PagedAndSortedResultRequestDto, 
        CreateUpdateDocumentDto>
{
}
