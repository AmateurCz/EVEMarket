using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using YamlDotNet.Serialization;

namespace EVEMarket.StaticData
{
    public class StaticDataManager : IDisposable
    {
        private Dictionary<string, ZipArchive> m_archives;

        public Dictionary<string, ZipArchive> Archives
        {
            get
            {
                if (m_disposed)
                    throw new ObjectDisposedException(nameof(StaticDataManager));
                if (m_archives == null)
                    m_archives = new Dictionary<string, ZipArchive>();
                return m_archives;
            }
        }

        public StaticDataManager()
        {
        }

        #region Icons

        public ZipArchive IconArchive
        {
            get
            {
                if (m_disposed)
                    throw new ObjectDisposedException(nameof(StaticDataManager));
                ZipArchive archive;
                lock (Archives)
                {
                    if (!Archives.TryGetValue(nameof(IconArchive), out archive))
                    {
                        archive = OpenArchive(WPF.Properties.Settings.Default.EVEIcons);
                        Archives.Add(nameof(IconArchive), archive);
                    }
                }
                return archive;
            }
        }

        public Stream GetIcon(string path)
        {
            if (m_disposed)
                throw new ObjectDisposedException(nameof(StaticDataManager));
            var entry = IconArchive.GetEntry(path.Replace('\\', '/'));
            return entry.Open();
        }

        public Stream GetIcon(int id)
        {
            if (m_disposed)
                throw new ObjectDisposedException(nameof(StaticDataManager));
            throw new NotImplementedException();
        }

        #endregion Icons

        #region yaml db

        public ZipArchive DatabaseArchive
        {
            get
            {
                if (m_disposed)
                    throw new ObjectDisposedException(nameof(StaticDataManager));
                ZipArchive archive;
                lock (Archives)
                {
                    if (!Archives.TryGetValue(nameof(DatabaseArchive), out archive))
                    {
                        archive = OpenArchive(WPF.Properties.Settings.Default.EVEDatabase);
                        Archives.Add(nameof(IconArchive), archive);
                    }
                }
                return archive;
            }
        }

        #endregion yaml db

        #region types

        private List<Model.Type> _types;

        public List<Model.Type> Types
        {
            get
            {
                if (_types == null)
                {
                    var deselializer = new DeserializerBuilder().Build();
                    StreamReader reader = new StreamReader(TypeYAMLStream);
                    var obj = deselializer.Deserialize(new StringReader(reader.ReadToEnd())) as Dictionary<object, object>;
                    _types = obj.Select(YamlBuilder.CreateType).ToList();
                }

                return _types;
            }
        }

        private Stream TypeYAMLStream
        {
            get
            {
                if (m_disposed)
                    throw new ObjectDisposedException(nameof(StaticDataManager));
                var entry = DatabaseArchive.GetEntry("sde/fsd/typeIDs.yaml");
                return entry.Open();
            }
        }

        #endregion types

        #region groups

        private List<Model.Group> _groups;

        public List<Model.Group> Groups
        {
            get
            {
                if (_groups == null)
                {
                    var deselializer = new DeserializerBuilder().Build();
                    StreamReader reader = new StreamReader(GroupsYAMLStream);
                    var obj = deselializer.Deserialize(new StringReader(reader.ReadToEnd())) as Dictionary<object, object>;
                    _groups = obj.Select(YamlBuilder.CreateGroup).ToList();
                }

                return _groups;
            }
        }

        private Stream GroupsYAMLStream
        {
            get
            {
                if (m_disposed)
                    throw new ObjectDisposedException(nameof(StaticDataManager));
                var entry = DatabaseArchive.GetEntry("sde/fsd/groupIDs.yaml");
                return entry.Open();
            }
        }

        #endregion groups

        #region category

        private List<Model.Category> _categories;

        public List<Model.Category> Categories
        {
            get
            {
                if (_categories == null)
                {
                    var deselializer = new DeserializerBuilder().Build();
                    StreamReader reader = new StreamReader(GroupsYAMLStream);
                    var obj = deselializer.Deserialize(new StringReader(reader.ReadToEnd())) as Dictionary<object, object>;
                    _categories = obj.Select(YamlBuilder.CreateCategory).ToList();
                }

                return _categories;
            }
        }

        private Stream CategoriesYAMLStream
        {
            get
            {
                if (m_disposed)
                    throw new ObjectDisposedException(nameof(StaticDataManager));
                var entry = DatabaseArchive.GetEntry("sde/fsd/categoryIDs.yaml");
                return entry.Open();
            }
        }

        #endregion category

        #region icons

        private List<Model.Icon> _icons;

        public List<Model.Icon> Icons
        {
            get
            {
                if (_icons == null)
                {
                    var deselializer = new DeserializerBuilder().Build();
                    StreamReader reader = new StreamReader(IconsYAMLStream);
                    var obj = deselializer.Deserialize(new StringReader(reader.ReadToEnd())) as Dictionary<object, object>;
                    _icons = obj.Select(YamlBuilder.CreateIcon).ToList();
                }

                return _icons;
            }
        }

        private Stream IconsYAMLStream
        {
            get
            {
                if (m_disposed)
                    throw new ObjectDisposedException(nameof(StaticDataManager));
                var entry = DatabaseArchive.GetEntry("sde/fsd/iconIDs.yaml");
                return entry.Open();
            }
        }

        #endregion icons

        private ZipArchive OpenArchive(string path)
        {
#if WPF
            return new ZipArchive(File.OpenRead(path), ZipArchiveMode.Read);
#else
                        throw new NotImplementedException();
#endif
        }

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

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free
        //       unmanaged resources. ~StaticData() { // Do not change this code. Put cleanup code in
        // Dispose(bool disposing) above. Dispose(false); }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above. GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}