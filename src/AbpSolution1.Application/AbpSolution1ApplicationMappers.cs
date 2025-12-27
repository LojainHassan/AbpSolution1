using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using AbpSolution1.Books;
using AbpSolution1.Documents;

namespace AbpSolution1;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class AbpSolution1BookToBookDtoMapper : MapperBase<Book, BookDto>
{
    public override partial BookDto Map(Book source);

    public override partial void Map(Book source, BookDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class AbpSolution1CreateUpdateBookDtoToBookMapper : MapperBase<CreateUpdateBookDto, Book>
{
    public override partial Book Map(CreateUpdateBookDto source);

    public override partial void Map(CreateUpdateBookDto source, Book destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class AbpSolution1WorkflowStatusToWorkflowStatusDtoMapper : MapperBase<WorkflowStatus, WorkflowStatusDto>
{
    public override partial WorkflowStatusDto Map(WorkflowStatus source);

    public override partial void Map(WorkflowStatus source, WorkflowStatusDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class AbpSolution1CreateUpdateWorkflowStatusDtoToWorkflowStatusMapper : MapperBase<CreateUpdateWorkflowStatusDto, WorkflowStatus>
{
    public override partial WorkflowStatus Map(CreateUpdateWorkflowStatusDto source);

    public override partial void Map(CreateUpdateWorkflowStatusDto source, WorkflowStatus destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class AbpSolution1DocumentToDocumentDtoMapper : MapperBase<Document, DocumentDto>
{
    public override partial DocumentDto Map(Document source);

    public override partial void Map(Document source, DocumentDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class AbpSolution1CreateUpdateDocumentDtoToDocumentMapper : MapperBase<CreateUpdateDocumentDto, Document>
{
    public override partial Document Map(CreateUpdateDocumentDto source);

    public override partial void Map(CreateUpdateDocumentDto source, Document destination);
}



