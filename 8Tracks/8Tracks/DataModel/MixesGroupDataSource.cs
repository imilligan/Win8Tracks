using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _8TracksEngine.Model;

namespace _8Tracks.DataModel
{
    public class MixesGroupDataSource : MixDataSource
    {

        private static ObservableCollection<MixesGroupDataSource> _dataSource;
        public static ObservableCollection<MixesGroupDataSource> DataSource
        {
            get
            {
                if (_dataSource == null)
                {
                    _dataSource = new ObservableCollection<MixesGroupDataSource>();
                }
                return _dataSource;
            }
            set
            {
                _dataSource = value;
            }
        }

        public MixesGroupDataSource(String uniqueId, String title, String subtitle, String imagePath, String description)
            : base( uniqueId, title, subtitle, imagePath, description )
        {

        }

        private ObservableCollection<MixDataSource> _items = new ObservableCollection<MixDataSource>();
        public ObservableCollection<MixDataSource> Items
        {
            get 
            {
                this.OnPropertyChanged("TopItems");
                this.OnPropertyChanged("Items");
                return this._items;
            }
        }
        
        public IEnumerable<MixDataSource> TopItems
        {
            get { return this._items.Take(12); }
        }

        public void AddItems(IEnumerable<Mix> mixes)
        {
            foreach (var mix in mixes)
            {
                MixDataSource ds = new MixDataSource(mix.id, mix.name, mix.slug, mix.cover_urls.max200, mix.description);
                _items.Add(ds);
            }
        }

        public static MixDataSource GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = DataSource.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }
    }
}
