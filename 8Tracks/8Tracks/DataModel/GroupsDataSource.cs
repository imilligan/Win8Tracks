using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Tracks.DataModel
{
    public class GroupsDataSource
    {
        private ObservableCollection<MixesGroupDataSource> _allGroups = new ObservableCollection<MixesGroupDataSource>();
        public ObservableCollection<MixesGroupDataSource> AllGroups
        {
            get { return this._allGroups; }
        }


    }
}
