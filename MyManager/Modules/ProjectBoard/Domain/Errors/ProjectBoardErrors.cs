using MyManager.Common;

namespace MyManager.Modules.ProjectBoard.Domain.Errors;

public static class ProjectBoardErrors
{
    public static Error AlreadyExists(string name) =>
        new("ProjectBoard.Project.AlreadyExists", $"Project with name {name} already exists");

    public static Error CouldNotCreate(string name) =>
        new("ProjectBoard.Project.CouldNotCreate", $"Could not create project with name {name}");

    public static Error CouldNotDelete(string name) =>
        new("ProjectBoard.Project.CouldNotDelete", $"Could not delete project with name {name}");

    public static Error CouldNotGet(string name) =>
        new("ProjectBoard.Project.CouldNotGet", $"Could not get project with name {name}");

    public static Error CouldNotGetBuckets(string name) => new("ProjectBoard.Project.CouldNotGetBuckets",
        $"Could not get buckets for project with name {name}");

    public static Error CouldNotGetTasks(string name) =>
        new("ProjectBoard.Project.CouldNotGetTasks", $"Could not get tasks for project with name {name}");

    public static Error CouldNotUpdate(string name) =>
        new("ProjectBoard.Project.CouldNotUpdate", $"Could not update project with name {name}");

    public static Error InvalidSlug(string slug) => new("Project.InvalidSlug", $"Slug {slug} is invalid");
    public static Error NotFound(int id) => new("ProjectBoard.Project.NotFound", $"Project with id {id} not found");

    public static Error NotFound(string slug) =>
        new("ProjectBoard.Project.NotFound", $"Project with slug {slug} not found");
}
