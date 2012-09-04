using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8TracksEngine.Model
{
    public class Mix
    {
        public string id;
        public string name;
        public bool published;
        public string description;
        long? plays_count;
        long? likes_count;
        public string slug;
        public string path;
        public CoverUrls cover_urls;
        public string restful_url;
        public string tag_list_cache;
        public string first_published_at;
        public bool liked_by_current_user;
        public bool nsfw;
        public User user;

    }
}
