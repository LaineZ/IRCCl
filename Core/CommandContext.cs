using Qmmands;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRCCl.Core
{
    public sealed class CustomCommandContext : CommandContext
    {
        public string Message { get; }
        // Pass your service provider to the base command context.
        public CustomCommandContext(string message, IServiceProvider provider = null) : base(provider)
        {
            Message = message;
        }
    }
}
