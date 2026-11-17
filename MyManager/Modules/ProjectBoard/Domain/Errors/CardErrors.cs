namespace MyManager.Modules.ProjectBoard.Domain.Errors;

public sealed record CardNotFound(string Description);
public sealed record CardAlreadyExists(string Description);

