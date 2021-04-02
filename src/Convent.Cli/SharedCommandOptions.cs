// <copyright file="SharedCommandOptions.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Convent.Cli
{
    using Convent.Commits;

    /// <summary>
    /// Represents command line options which are shared across all commands.
    /// </summary>
    internal class SharedCommandOptions
    {
        private readonly bool scope;
        private readonly bool body;
        private readonly bool issue;
        private readonly bool breakingChange;

        /// <summary>
        /// Initializes a new instance of the <see cref="SharedCommandOptions"/> class.
        /// Represents command line options which are shared across all commands.
        /// </summary>
        /// <param name="scope">When true, adds a scope element to the commit message.</param>
        /// <param name="body">When true, adds a body element to the commit message.</param>
        /// <param name="issue">When true, adds an issue element to the commit message.</param>
        /// <param name="breakingChange">When true, adds a breaking change element to the commit message.</param>
        public SharedCommandOptions(bool scope, bool body, bool issue, bool breakingChange)
        {
            this.breakingChange = breakingChange;
            this.issue = issue;
            this.body = body;
            this.scope = scope;
        }

        /// <summary>
        /// Maps the <see cref="SharedCommandOptions"/> to a new <see cref="CommitMessageOptions"/> instance.
        /// </summary>
        /// <returns>A new <see cref="CommitMessageOptions"/> instance.</returns>
        public CommitMessageOptions ToCommitMessageOptions()
        {
            return new CommitMessageOptions
            {
                HasBody = this.body,
                HasBreakingChange = this.breakingChange,
                HasIssue = this.issue,
                HasScope = this.scope,
            };
        }
    }
}