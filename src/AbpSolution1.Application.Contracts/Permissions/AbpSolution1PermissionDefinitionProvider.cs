using AbpSolution1.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace AbpSolution1.Permissions;

public class AbpSolution1PermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(AbpSolution1Permissions.GroupName);

        var booksPermission = myGroup.AddPermission(AbpSolution1Permissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(AbpSolution1Permissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(AbpSolution1Permissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(AbpSolution1Permissions.Books.Delete, L("Permission:Books.Delete"));

        var workflowStatusPermission = myGroup.AddPermission(AbpSolution1Permissions.WorkflowStatuses.Default, L("Permission:WorkflowStatuses"));
        workflowStatusPermission.AddChild(AbpSolution1Permissions.WorkflowStatuses.Create, L("Permission:WorkflowStatuses.Create"));
        workflowStatusPermission.AddChild(AbpSolution1Permissions.WorkflowStatuses.Edit, L("Permission:WorkflowStatuses.Edit"));
        workflowStatusPermission.AddChild(AbpSolution1Permissions.WorkflowStatuses.Delete, L("Permission:WorkflowStatuses.Delete"));

        var documentsPermission = myGroup.AddPermission(AbpSolution1Permissions.Documents.Default, L("Permission:Documents"));
        documentsPermission.AddChild(AbpSolution1Permissions.Documents.Create, L("Permission:Documents.Create"));
        documentsPermission.AddChild(AbpSolution1Permissions.Documents.Edit, L("Permission:Documents.Edit"));
        documentsPermission.AddChild(AbpSolution1Permissions.Documents.Delete, L("Permission:Documents.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AbpSolution1Resource>(name);
    }
}
