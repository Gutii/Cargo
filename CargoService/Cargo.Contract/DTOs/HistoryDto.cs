using System;
using System.Collections.Generic;

namespace Cargo.Contract.DTOs
{
    public class HistoryDto
    {
        public DateTime DateModifed { get; set; }

        public string Description { get; set; }

        public string DescriptionEng { get; set; }

        public string Details { get; set; }

        public string HistoryCode { get; set; }

        public ICollection<ChangeDto> ChangeLog { get; set; }

    }
}
