namespace MyManager.Modules.ProjectBoard.Domain.Errors;

public sealed record BoardNotFound;
public sealed record BoardAlreadyExists(string? Description = null);
public sealed record BoardUnchanged;
