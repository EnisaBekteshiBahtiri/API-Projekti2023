namespace ASP.NET_Core_6._0_API.DTO
{
    public class FilterBookDTO
    {
        public string? Name { get; set; } = null!;
        public DateTime? PublishDate { get; set; }
        public int? PublisherId { get; set; }
        public string? Isbn { get; set; } = null!;
        public string? LanguageName { get; set; } = null!;
        public int? LanguageId { get; set; }
        public int? NumberOfPages { get; set; }
        public string? Description { get; set; }
    }
}
