﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilieLaissIdentity.Models
{
    public class SendMailModel
    {
        public string ReceiverAdress { get; set; }

        public string ReceiverName { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsBodyHtml { get; set; }
    }
}