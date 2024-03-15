using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LibGit2Sharp
{
    /// <summary>
    /// Criteria used to filter out and order the commits of the repository when querying its history.
    /// </summary>
    public sealed class CommitFilter
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CommitFilter"/>.
        /// </summary>
        public CommitFilter()
        {
            SortBy = CommitSortStrategies.Time;
            IncludeReachableFrom = "HEAD";
            FirstParentOnly = false;
        }

        /// <summary>
        /// The ordering strategy to use.
        /// <para>
        ///   By default, the commits are shown in reverse chronological order.
        /// </para>
        /// </summary>
        public CommitSortStrategies SortBy { get; set; }

        /// <summary>
        /// A pointer to a commit object or a list of pointers to consider as starting points.
        /// <para>
        ///   Can be either a <see cref="string"/> containing the sha or reference canonical name to use,
        ///   a <see cref="Branch"/>, a <see cref="Reference"/>, a <see cref="Commit"/>, a <see cref="Tag"/>,
        ///   a <see cref="TagAnnotation"/>, an <see cref="ObjectId"/> or even a mixed collection of all of the above.
        ///   By default, the <see cref="Repository.Head"/> will be used as boundary.
        /// </para>
        /// </summary>
        public object IncludeReachableFrom { get; set; }

        internal IList<object> SinceList
        {
            get { return ToList(IncludeReachableFrom); }
        }

        /// <summary>
        /// A pointer to a commit object or a list of pointers which will be excluded (along with ancestors) from the enumeration.
        /// <para>
        ///   Can be either a <see cref="string"/> containing the sha or reference canonical name to use,
        ///   a <see cref="Branch"/>, a <see cref="Reference"/>, a <see cref="Commit"/>, a <see cref="Tag"/>,
        ///   a <see cref="TagAnnotation"/>, an <see cref="ObjectId"/> or even a mixed collection of all of the above.
        /// </para>
        /// </summary>
        public object ExcludeReachableFrom { get; set; }

        internal IList<object> UntilList
        {
            get { return ToList(ExcludeReachableFrom); }
        }

        /// <summary>
        /// Whether to limit the walk to each commit's first parent, instead of all of them
        /// </summary>
        public bool FirstParentOnly { get; set; }

        private static IList<object> ToList(object obj)
        {
            var list = new List<object>();

            if (obj == null)
            {
                return list;
            }

            if (Types.Contains(obj.GetType()))
            {
                list.Add(obj);
                return list;
            }

            list.AddRange(((IEnumerable)obj).Cast<object>());
            return list;
        }

        private static IEnumerable<Type> Types
        {
            get
            {
                yield return typeof(string);
                yield return typeof(ObjectId);
                yield return typeof(Commit);
                yield return typeof(TagAnnotation);
                yield return typeof(Tag);
                yield return typeof(Branch);
                yield return typeof(DetachedHead);
                yield return typeof(Reference);
                yield return typeof(DirectReference);
                yield return typeof(SymbolicReference);
            }
        }
    }
}
