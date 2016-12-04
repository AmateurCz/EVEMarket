using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace EVEMarket.Shared
{
    public abstract class StaticData : IDisposable
    {
        private Dictionary<string, ZipArchive> m_archives;

        public Dictionary<string, ZipArchive> Archives
        {
            get
            {
                if (m_disposed)
                    throw new ObjectDisposedException(nameof(StaticData));
                if (m_archives == null)
                    m_archives = new Dictionary<string, ZipArchive>();
                return m_archives;
            }
        }

        public StaticData()
        {
        }

        protected abstract Stream OpenFile(string path);

        #region Icons

        public ZipArchive IconArchive
        {
            get
            {
                if (m_disposed)
                    throw new ObjectDisposedException(nameof(StaticData));
                ZipArchive archive;
                lock (Archives)
                {
                    if (!Archives.TryGetValue(nameof(IconArchive), out archive))
                    {
                        archive = new ZipArchive(OpenFile(@"Y:\YC-118-10_1.0_Icons.zip"), ZipArchiveMode.Read);
                        Archives.Add(nameof(IconArchive), archive);
                    }
                }
                return archive;
            }
        }

        public Stream GetIcon(string path)
        {
            if (m_disposed)
                throw new ObjectDisposedException(nameof(StaticData));
            var entry = IconArchive.GetEntry(path.Replace('\\', '/'));
            return entry.Open();
        }

        public Stream GetIcon(int id)
        {
            if (m_disposed)
                throw new ObjectDisposedException(nameof(StaticData));
            throw new NotImplementedException();
        }

        #endregion Icons

        #region IDisposable Support

        private bool m_disposed = false; // To detect redundant calls

        private void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if (m_archives != null)
                    {
                        foreach (var val in m_archives)
                            val.Value.Dispose();
                        m_archives.Clear();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                m_disposed = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~StaticData() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}