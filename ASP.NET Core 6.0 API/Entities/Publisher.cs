using System;
using System.Collections.Generic;

namespace ASP.NET_Core_6._0_API.Entities
{
    public partial class Publisher
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime Birthday { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
