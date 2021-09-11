using ApiRestFull.Hypermedia;
using ApiRestFull.Hypermedia.Abstract;
using ApiRestFull.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRestFull.Data.VO
{
    public class BookVO: ISupportsHyperMedia
    {
        public long Id { get; set; }
        public string Author { get; set; }

        public DateTime LanchDate { get; set; }

        public decimal Price { get; set; }

        public string Title { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
