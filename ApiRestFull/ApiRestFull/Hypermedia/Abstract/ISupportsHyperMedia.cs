using System.Collections.Generic;

namespace ApiRestFull.Hypermedia.Abstract
{
    public interface ISupportsHyperMedia
    {
        List<HyperMediaLink> Links  {  get; set; }
    }
}
