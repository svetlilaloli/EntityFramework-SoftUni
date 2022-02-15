namespace VaporStore.DataProcessor.Dto.Export
{
    public class GenreOutputDto
    {
        public int Id { get; set; }
        public string Genre { get; set; }
        public GameOutputDto[] Games { get; set; }
        public int TotalPlayers { get; set; }
    }
}
