using System;
using System.ComponentModel.DataAnnotations;

namespace AbpSolution1.Documents;

public class CreateUpdateDocumentDto
{
    [Required]
    [StringLength(128)]
    public string NameAr { get; set; }

    [Required]
    [StringLength(128)]
    public string NameEn { get; set; }

    [StringLength(32)]
    public string Code { get; set; }

    [Required]
    [StringLength(64)]
    public string DocumentNo { get; set; }

    [Required]
    public Guid WorkflowStatusId { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
}
