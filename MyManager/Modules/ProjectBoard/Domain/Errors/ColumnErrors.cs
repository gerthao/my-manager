using System.Diagnostics;
using System.Runtime.CompilerServices;
using MyManager.Attributes;
using MyManager.Common;

namespace MyManager.Modules.ProjectBoard.Domain.Errors;

public sealed record ColumnAlreadyExists(int? ColumnId = null, int? BoardId = null, string? Description = null);
public sealed record ColumnNotFound(int? ColumnId = null, int? BoardId = null, string? Description = null);
public sealed record ColumnToDeleteHasCards(int? ColumnId = null, int? BoardId = null, string? Description = null);
public sealed record ColumnUnchanged(int? ColumnId = null, int? BoardId = null, string? Description = null);