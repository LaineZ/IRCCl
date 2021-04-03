using Qmmands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IRCCl.Core
{
    class CommandExecutor
    {
        private readonly CommandService _service = new CommandService();

        public CommandExecutor()
        {
            _service.AddModule<Commands.BaseIRCCommands>();
        }


        public async Task<bool> ExecuteCommand(string input)
        {
            if (!CommandUtilities.HasPrefix(input, '/', out string output))
                return false;

            IResult result = await _service.ExecuteAsync(output, new CustomCommandContext(input));
            if (result is FailedResult failedResult)
            {
                switch (result)
                {
                    case CommandNotFoundResult err:
                        return false;
                    case TypeParseFailedResult err:
                        IRC.Messages.AddSystemMessage($"Type error in `{err.Parameter}` excepted type: `{err.Parameter.Type}`  got: `{err.Value.GetType()}`");
                        break;
                    case ArgumentParseFailedResult err:
                        IRC.Messages.AddSystemMessage(err.FailureReason);
                        break;
                }
            }

            return true;
        }
    }
}
