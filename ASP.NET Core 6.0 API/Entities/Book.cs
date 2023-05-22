using System;
using System.Collections.Generic;

namespace ASP.NET_Core_6._0_API.Entities
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime PublishDate { get; set; }
        public int PublisherId { get; set; }
        public string Isbn { get; set; } = null!;
        public int LanguageId { get; set; }
        public int NumberOfPages { get; set; }
        public string? Description { get; set; }

        public virtual Language Language { get; set; } = null!;
        public virtual Publisher Publisher { get; set; } = null!;
    }
}
