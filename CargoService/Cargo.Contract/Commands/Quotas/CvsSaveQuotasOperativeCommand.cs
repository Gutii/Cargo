using Cargo.Contract.DTOs.Quotas;
using IdealResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Contract.Commands.Quotas
{
    public class CvsSaveQuotasOperativeCommand : ICommand<Result>
    {
        public byte[] Bytes { get; set; }
    }
}
