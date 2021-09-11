using ApiRestFull.Hypermedia.Abstract;
using System.Collections.Generic;

namespace ApiRestFull.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
