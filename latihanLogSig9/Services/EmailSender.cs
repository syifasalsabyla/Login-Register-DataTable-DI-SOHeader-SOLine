﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace latihanLogSig9.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}
