using IRCCl.Core;
using Qmmands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRCCl.Commands
{
    public sealed class BaseIRCCommands : ModuleBase<CustomCommandContext>
    {
        public CommandService Service { get; set; }

        [Command("help", "commands")]
        [Description("Lists available commands.")]
        public void Help()
        {
            IRC.Messages.AddSystemMessage(string.Join('\n', Service.GetAllCommands().Select(x => $"`{x.Name}` - {x.Description}")));
        }

        [Command("join")]
        [Description("Join to IRC Channel")]
        public void JoinChannel(string channel)
        {
            IRC.client.Channels.Add(new NetIRC.Channel(channel));
        }
    }
}
