using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using AbpSolution1.Documents;

namespace AbpSolution1;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class AbpSolution1DocumentMapper : MapperBase<Document, DocumentDto>
{
    public override partial DocumentDto Map(Document source);

    public override partial void Map(Document source, DocumentDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class AbpSolution1CreateUpdateDocumentDtoMapper : MapperBase<CreateUpdateDocumentDto, Document>
{
    public override partial Document Map(CreateUpdateDocumentDto source);

    public override partial void Map(CreateUpdateDocumentDto source, Document destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class AbpSolution1WorkflowStatusMapper : MapperBase<WorkflowStatus, WorkflowStatusDto>
{
    public override partial WorkflowStatusDto Map(WorkflowStatus source);

    public override partial void Map(WorkflowStatus source, WorkflowStatusDto destination);
}


