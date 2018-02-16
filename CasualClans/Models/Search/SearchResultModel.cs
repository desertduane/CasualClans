using CasualClans.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualClans.Models.Search
{
    public class SearchResultModel
    {
        public IEnumerable<PostListingModel> Posts { get; set; }
        public string SearchQuery { get; set; }
        public bool EmptySearchResult { get; set; }
    }
}
