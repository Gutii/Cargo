#nullable enable
using System;

namespace Cargo.Application.Services.PoolAwbNum;

public class ReserveException : Exception
{
    public ReserveException(string? message) : base(message) { }
}