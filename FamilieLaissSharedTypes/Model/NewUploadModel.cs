using FamilieLaissSharedTypes.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilieLaissSharedTypes.Model
{
    public class NewUploadModel
    {
        public enUploadType UploadType { get; set; }
        public long ID { get; set; }
        public string BlobName { get; set; }
        public string OriginalName { get; set; }
    }
}
