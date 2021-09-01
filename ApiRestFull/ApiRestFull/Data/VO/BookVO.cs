using ApiRestFull.Model.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRestFull.Data.VO
{
    public class BookVO
    {
        public long Id { get; set; }
        public string Author { get; set; }

        public DateTime LanchDate { get; set; }

        public decimal Price { get; set; }

        public string Title { get; set; }
    }
}
