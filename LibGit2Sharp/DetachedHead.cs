namespace LibGit2Sharp
{
    /// <summary>
    /// Represent a detached head.
    /// </summary>
    public class DetachedHead : Branch
    {
        internal DetachedHead(Repository repo, Reference reference)
            : base(repo, reference, "(no branch)")
        { }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns>The constant '(no branch)'</returns>
        protected override string Shorten()
        {
            return CanonicalName;
        }

        /// <summary>
        /// Gets the remote branch which is connected to this local one, or null if there is none.
        /// </summary>
        public override Branch TrackedBranch
        {
            get { return null; }
        }
    }
}
