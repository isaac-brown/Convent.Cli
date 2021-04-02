// <copyright file="CommitSubCommands.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Convent.Cli
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.IO;
    using Convent.Commits;
    using LibGit2Sharp;

    /// <summary>
    /// Adds sub commands for commiting changes.
    /// </summary>
    internal static class CommitSubCommands
    {
        /// <summary>
        /// Adds sub-commands 'feature', 'fix' and 'chore' to the given <paramref name="command"/>.
        /// </summary>
        /// <param name="command">The command to add sub-commands to.</param>
        /// <returns>The command for chaining.</returns>
        public static Command AddCommitSubCommands(this Command command)
        {
            command.AddCommand(CreateFeatureCommand());
            command.AddCommand(CreateFixCommand());
            command.AddCommand(CreateChoreCommand());
            return command;
        }

        private static Command CreateFeatureCommand()
        {
            var command = new Command("feature")
            {
                Description = "Creates a commit representing a feature with a random commit message",
                Handler = CommandHandler.Create<SharedCommandOptions, DirectoryInfo>(HandleFeatureCommand),
            };

            command.AddAlias("feat");

            return command;
        }

        private static void HandleFeatureCommand(SharedCommandOptions options, DirectoryInfo path)
        {
            HandleCommitCommand(CommitType.Feature, options, path);
        }

        private static Command CreateFixCommand()
        {
            var command = new Command("fix")
            {
                Description = "Creates a commit representing a fix with a random commit message",
                Handler = CommandHandler.Create<SharedCommandOptions, DirectoryInfo>(HandleFixCommand),
            };

            return command;
        }

        private static void HandleFixCommand(SharedCommandOptions options, DirectoryInfo path)
        {
            HandleCommitCommand(CommitType.Fix, options, path);
        }

        private static Command CreateChoreCommand()
        {
            var command = new Command("chore")
            {
                Description = "Creates a commit representing a chore with a random commit message",
                Handler = CommandHandler.Create<SharedCommandOptions, DirectoryInfo>(HandleChoreCommand),
            };

            return command;
        }

        private static void HandleChoreCommand(SharedCommandOptions options, DirectoryInfo path)
        {
            HandleCommitCommand(CommitType.Chore, options, path);
        }

        private static void HandleCommitCommand(CommitType commitType, SharedCommandOptions options, DirectoryInfo path)
        {
            var commitMessageOptions = options.ToCommitMessageOptions();
            ConventionalCommitMessageFactory factory = new();
            string message = factory.CreateCommitMessage(commitType, commitMessageOptions);
            using var repo = new Repository(path.FullName);

            File.AppendAllText(Path.Join(path.FullName, Guid.NewGuid().ToString()), DateTime.Now.ToString());

            Commands.Stage(repo, "*");

            var author = repo.Config.BuildSignature(DateTimeOffset.Now);
            repo.Commit(message, author, author);
        }
    }
}
