#nullable enable
using Cargo.Infrastructure.Data.Model.PoolAwbService;
using System.Text.Json.Nodes;

namespace Cargo.Application.Services.PoolAwbNum;

public static class JsonArrayExtension
{
    public static PoolAwb? AsPoolAwb(this JsonArray? val)
    {
        if (val == null)
            return null;
        return new PoolAwb
        {
            Id = val[0]!.GetValue<int>(),
            AgentId = val[1]!.GetValue<int>(),
            Prefix = val[2]!.GetValue<int>(),
            BeginNum = val[3]!.GetValue<int>(),
            FullBeginNum = val[4]!.GetValue<int>(),
            EndNum = val[5]!.GetValue<int>(),
            FullEndNum = val[6]!.GetValue<int>(),
            PoolLen = val[7]!.GetValue<int>(),
            Occupied = val[8]!.GetValue<int>()
        };
    }
}