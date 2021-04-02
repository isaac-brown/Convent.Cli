// <copyright file="CommitMessageCommands.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Convent.Cli
{
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using Convent.Commits;

    /// <summary>
    /// Extensions to add application specific commands to an existing command.
    /// </summary>
    internal static class CommitMessageCommands
    {
        /// <summary>
        /// Adds 'feature', 'fix' and 'chore' commands to the given <paramref name="command"/>.
        /// </summary>
        /// <param name="command">The parent command.</param>
        /// <returns>The command for chaining.</returns>
        public static Command AddCommitMessageCommands(this Command command)
        {
            command.AddCommand(CreateChoreCommand());
            command.AddCommand(CreateFixCommand());
            command.AddCommand(CreateFeatureCommand());
            return command;
        }

        private static Command CreateChoreCommand()
        {
            return new Command("chore")
            {
                Description = "Creates a random chore commit message.",
                Handler = CommandHandler.Create<SharedCommandOptions, IConsole>(HandleChoreCommand),
            };
        }

        private static Command CreateFeatureCommand()
        {
            var command = new Command("feature")
            {
                Description = "Creates a random feature commit message.",
                Handler = CommandHandler.Create<SharedCommandOptions, IConsole>(HandleFeatureCommand),
            };

            command.AddAlias("feat");

            return command;
        }

        private static Command CreateFixCommand()
        {
            return new Command("fix")
            {
                Description = "Creates a random fix commit message.",
                Handler = CommandHandler.Create<SharedCommandOptions, IConsole>(HandleFixCommand),
            };
        }

        private static void HandleFixCommand(SharedCommandOptions sharedCommandOptions, IConsole console)
        {
            HandleCommitCommand(CommitType.Fix, sharedCommandOptions, console);
        }

        private static void HandleChoreCommand(SharedCommandOptions sharedCommandOptions, IConsole console)
        {
            HandleCommitCommand(CommitType.Chore, sharedCommandOptions, console);
        }

        private static void HandleFeatureCommand(SharedCommandOptions sharedCommandOptions, IConsole console)
        {
            HandleCommitCommand(CommitType.Feature, sharedCommandOptions, console);
        }

        private static void HandleCommitCommand(CommitType commitType, SharedCommandOptions sharedCommandOptions, IConsole console)
        {
            var messageFactory = new ConventionalCommitMessageFactory();
            var commitMessageOptions = sharedCommandOptions.ToCommitMessageOptions();
            var commitMessage = messageFactory.CreateCommitMessage(commitType, commitMessageOptions);
            console.Out.Write(commitMessage);
        }
    }
}
