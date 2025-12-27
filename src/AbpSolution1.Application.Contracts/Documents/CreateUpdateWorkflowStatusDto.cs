using System;
using System.ComponentModel.DataAnnotations;

namespace AbpSolution1.Documents;

public class CreateUpdateWorkflowStatusDto
{
    [Required]
    [StringLength(128)]
    public string NameAr { get; set; }

    [Required]
    [StringLength(128)]
    public string NameEn { get; set; }

    [Required]
    [StringLength(32)]
    public string Code { get; set; }
}
