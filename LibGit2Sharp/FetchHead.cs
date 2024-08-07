﻿using System;
using System.Globalization;

namespace LibGit2Sharp
{
    /// <summary>
    /// Represents a local reference data from a remote repository which
    /// has been retreived through a Fetch process.
    /// </summary>
    public class FetchHead : ReferenceWrapper<GitObject>
    {
        /// <summary>
        /// Needed for mocking purposes.
        /// </summary>
        protected FetchHead()
        { }

        internal FetchHead(
            Repository repo,
            string remoteCanonicalName,
            string url,
            ObjectId targetId,
            bool forMerge,
            int index)
            : base(repo,
                   new DirectReference(string.Format(CultureInfo.InvariantCulture,
                                                     "FETCH_HEAD[{0}]",
                                                     index),
                                       repo,
                                       targetId),
                   r => r.CanonicalName)
        {
            Url = url;
            ForMerge = forMerge;
            RemoteCanonicalName = remoteCanonicalName;
        }

        /// <summary>
        /// Returns "FETCH_HEAD[i]", where i is the index of this fetch head.
        /// </summary>
        protected override string Shorten()
        {
            return CanonicalName;
        }

        /// <summary>
        /// Gets the canonical name of the reference this <see cref="FetchHead"/>
        /// points to in the remote repository it's been fetched from.
        /// </summary>
        public virtual string RemoteCanonicalName { get; private set; }

        /// <summary>
        /// Gets the <see cref="GitObject"/> that this fetch head points to.
        /// </summary>
        public virtual GitObject Target
        {
            get { return TargetObject; }
        }

        /// <summary>
        /// The URL of the remote repository this <see cref="FetchHead"/>
        /// has been built from.
        /// </summary>
        public virtual string Url { get; private set; }

        /// <summary>
        /// Determines if this fetch head entry has been explicitly fetched.
        /// </summary>
        public virtual bool ForMerge { get; private set; }

        /// <summary>
        /// Determine if the current <see cref="FetchHead"/> matches a local branch.
        /// </summary>
        /// <returns>true if the current <see cref="FetchHead"/> matches local branch, false otherwise.</returns>
        public virtual bool IsLocalBranch
        {
            get { return RemoteCanonicalName.LooksLikeLocalBranch(); }
        }

        /// <summary>
        /// Determine if the current <see cref="FetchHead"/> matches remote tracking branch.
        /// </summary>
        /// <returns>true if the current <see cref="FetchHead"/> matches remote tracking branch, false otherwise.</returns>
        public virtual bool IsRemoteTrackingBranch
        {
            get { return RemoteCanonicalName.LooksLikeRemoteTrackingBranch(); }
        }

        /// <summary>
        /// Determine if the current <see cref="FetchHead"/> matches tag.
        /// </summary>
        /// <returns>true if the current <see cref="FetchHead"/> matches tag, false otherwise.</returns>
        public virtual bool IsTag
        {
            get { return RemoteCanonicalName.LooksLikeTag(); }
        }

        /// <summary>
        /// Determine if the current <see cref="FetchHead"/> matches note.
        /// </summary>
        /// <returns>true if the current <see cref="FetchHead"/> matches note, false otherwise.</returns>
        public virtual bool IsNote
        {
            get { return RemoteCanonicalName.LooksLikeNote(); }
        }
    }
}
