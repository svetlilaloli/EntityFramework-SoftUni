namespace MusicHub
{
    using System;
    using System.Linq;
    using System.Text;
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            MusicHubDbContext context = 
                new MusicHubDbContext();

            //DbInitializer.ResetDatabase(context);

            //Test your solutions here
            //Console.WriteLine(ExportAlbumsInfo(context, 9));
            Console.WriteLine(ExportSongsAboveDuration(context, 300));
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            StringBuilder result = new StringBuilder();

            var albums = context.Albums.Where(a => a.ProducerId == producerId).ToList();

            foreach (var album in albums.OrderByDescending(a => a.Price))
            {
                result.AppendLine($"-AlbumName: {album.Name}");
                result.AppendLine($"-ReleaseDate: {album.ReleaseDate:MM/dd/yyyy}");
                result.AppendLine($"-ProducerName: {album.Producer.Name}");
                result.AppendLine($"-Songs:");
                
                int songNum = 1;
                foreach (var song in album.Songs.OrderByDescending(s => s.Name).ThenBy(s => s.Writer.Name))
                {
                    result.AppendLine($"---#{songNum}");
                    result.AppendLine($"---SongName: {song.Name}");
                    result.AppendLine($"---Price: {song.Price:f2}");
                    result.AppendLine($"---Writer: {song.Writer.Name}");
                    songNum++;
                }
                result.AppendLine($"-AlbumPrice: {album.Price:f2}");
            }

            return result.ToString().TrimEnd();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            StringBuilder result = new StringBuilder();

            var songs = context.Songs.Where(s => s.Duration.TotalSeconds > duration).ToList();

            int songNum = 1;
            foreach (var song in songs.OrderBy(s => s.Name).ThenBy(s => s.Writer.Name).ThenBy(s => s.SongPerformers.FirstOrDefault()))
            {
                result.AppendLine($"-Song #{songNum}");
                result.AppendLine($"---SongName: {song.Name}");
                result.AppendLine($"---Writer: {song.Writer.Name}");
                result.AppendLine($"---Performer: {song.SongPerformers.FirstOrDefault().Performer.FirstName} {song.SongPerformers.FirstOrDefault().Performer.LastName}");
                result.AppendLine($"---AlbumProducer: {song.Album.Producer.Name}");
                result.AppendLine($"---Duration: {song.Duration:c}");

                songNum++;
            }

            return result.ToString().TrimEnd();
        }
    }
}
