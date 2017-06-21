using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilieLaissBackend.Model.Validation
{
    public class CheckValueDTO
    {
        public long ID { get; set; }
        public int AdditionalType { get; set; }
        public string Value { get; set; }
    }
}