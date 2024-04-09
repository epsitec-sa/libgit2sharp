using LibGit2Sharp.Core;

namespace LibGit2Sharp
{
    /// <summary>
    /// The exception that is thrown when a native operation failed with an uncategorized error code.
    /// </summary>
    public class LibGit2NativeException : NativeException
    {
        internal LibGit2NativeException(string message, GitErrorCode errorCode)
            : base($"{message} (libgit2 native error code = {errorCode})", GitErrorCategory.Unknown)
        {
            this.errorCode = errorCode;
        }

        internal override GitErrorCode ErrorCode => this.errorCode;

        private readonly GitErrorCode errorCode;
    }
}
