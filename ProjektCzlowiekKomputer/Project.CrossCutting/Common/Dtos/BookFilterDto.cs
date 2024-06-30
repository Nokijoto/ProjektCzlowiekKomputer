namespace Project.CrossCutting.Dtos
{
    public class BookFilterDto
    {
        public int? Id { get; set; }
        public Guid? Guid { get; set; }
        public string? Isbn { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public string? Publisher { get; set; }
        public string? Language { get; set; }
        public double? Rating { get; set; }
    }
}