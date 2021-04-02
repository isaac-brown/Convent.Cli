// <copyright file="GlobalOptions.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Convent.Cli
{
    using System.CommandLine;

    /// <summary>
    /// Adds global options to a <see cref="Command"/>.
    /// </summary>
    internal static class GlobalOptions
    {
        /// <summary>
        /// Adds the options `scope` and `body` and `issue` and `breaking-change` to the given <paramref name="command"/>.
        /// </summary>
        /// <param name="command">The <see cref="Command"/> to add options to.</param>
        public static void AddGlobalOptions(this Command command)
        {
            var options = new[]
            {
                new Option<bool>(
                    aliases: new[] { "--scope", "-s" },
                    getDefaultValue: () => false,
                    description: "When true, adds a scope element to the commit message"),
                new Option<bool>(
                    aliases: new[] { "--body", "-b" },
                    getDefaultValue: () => false,
                    description: "When true, adds a body element to the commit message"),
                new Option<bool>(
                    aliases: new[] { "--issue", "-i" },
                    getDefaultValue: () => false,
                    description: "When true, adds an issue element to the commit message"),
                new Option<bool>(
                    aliases: new[] { "--breaking-change" },
                    getDefaultValue: () => false,
                    description: "When true, adds a breaking change element to the commit message"),
            };

            foreach (var option in options)
            {
                command.AddGlobalOption(option);
            }
        }
    }
}
