using MyManager.Common;

namespace MyManager.Modules.ProjectBoard.Domain.Errors;

public static class BoardErrors
{
    public static Error AlreadyExists(string name) =>
        new("ProjectBoard.Board.AlreadyExists", $"Board {name} already exists");

    public static Error NotFound(int id) => new("ProjectBoard.Board.NotFound", $"Project with id {id} not found");

    public static Error NotFound(string slug) =>
        new("ProjectBoard.Board.NotFound", $"Project with slug {slug} not found");
}

public sealed record BoardNotFound(string Description);
