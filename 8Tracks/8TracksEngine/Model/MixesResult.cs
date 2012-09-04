using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8TracksEngine.Model
{
    public class MixesResult
    {
        public string id;
        public string path;
        public string restful_url;
        public long? total_entries;
        public long? page;
        public long? per_page;
        public long? next_page;
        public long? previous_page;
        public long? total_pages;
        public string canonical_path;
        public string status;
        public string errors;
        public string notices;
        public string logged;
        public Mix [] mixes;
    }
}
