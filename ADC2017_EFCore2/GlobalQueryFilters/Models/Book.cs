namespace GlobalQueryFilters.Models
{
    internal class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}