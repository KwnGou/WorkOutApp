using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOutDTOs
{
    public class Schedule_GridDTO
    {
        public int Id { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public byte[]? Rowversion { get; set; }
    }
}
