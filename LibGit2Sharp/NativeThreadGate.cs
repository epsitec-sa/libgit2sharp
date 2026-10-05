using System.Threading;

namespace LibGit2Sharp.Core
{
    internal static class NativeThreadGate
    {
        private static readonly object s_gate = new object();

        internal static ReleaseHandle Acquire()
        {
            Monitor.Enter(s_gate);
            return new ReleaseHandle();
        }

        internal struct ReleaseHandle : System.IDisposable
        {
            private int _released;
            public void Dispose()
            {
                if (Interlocked.Exchange(ref _released, 1) == 0)
                    Monitor.Exit(s_gate);
            }
        }
    }
}
