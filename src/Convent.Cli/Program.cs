// <copyright file="Program.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Convent.Cli
{
    using System.CommandLine;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Entrypoint for the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entrypoint for the application.
        /// </summary>
        /// <param name="args">The arguments for the application.</param>
        /// <returns>A <see cref="Task"/> representing the outcome of the application.</returns>
        public static async Task Main(string[] args)
        {
            var rootCommand = new RootCommand();

            var messageCommand = new Command(name: "message", description: "Create commit messages");
            messageCommand.AddCommitMessageCommands();
            rootCommand.AddCommand(messageCommand);

            var commitCommand = new Command(name: "commit", description: "Create commits in a repository");
            commitCommand.AddCommitSubCommands();

            commitCommand.AddArgument(new Argument<DirectoryInfo>(
                name: "path",
                getDefaultValue: () => new DirectoryInfo(path: Directory.GetCurrentDirectory()),
                description: "The path to the root of the git repository"));

            rootCommand.AddCommand(commitCommand);

            rootCommand.AddGlobalOptions();

            await rootCommand.InvokeAsync(args);
        }
    }
}
