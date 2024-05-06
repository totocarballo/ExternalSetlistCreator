using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ExternalSetlistCreator
{
    internal static class Export
    {
        public static int SongSpeed { get; set; } = 100; //Song at 100% speed

        public static void Playlist(string pathSave, List<string> songs)
        {
            var playlist = new Playlist
            {
                Name = "Custom Setlist",
                Author = "ExternalSetlistCreator",
                Id = Guid.NewGuid(),
                SongHashes = songs.ConvertAll(song =>
                {
                    string cleanedSong = Regex.Replace(song, @"\s*\[\d+%]\s*", "");
                    return HashFileAlt(cleanedSong);
                })
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            };

            var outputJson = JsonSerializer.Serialize(playlist, options);

            //The only file YARG reads for now
            string filePath = $"{pathSave}\\favorites.json";

            File.WriteAllText(filePath, outputJson);
        }

        public static void Setlist(string pathSave, int totalSongs, List<string> songs)
        {
            byte[] header = new byte[] { 0xEA, 0xEC, 0x33, 0x01 };

            using FileStream fs = new(pathSave, FileMode.Create);
            fs.Write(header, 0, header.Length);

            byte[] totalSongsBytes = BitConverter.GetBytes((ushort)totalSongs);
            fs.Write(totalSongsBytes, 0, totalSongsBytes.Length);

            fs.WriteByte(0x00);
            fs.WriteByte(0x00);

            foreach (var song in songs)
            {
                fs.WriteByte(0x20);

                Match match = Regex.Match(song, @"^(.+\.(?:chart|mid)) \[(\d+)%\]$");

                string filePath = match.Groups[1].Value;
                int percentage = int.Parse(match.Groups[2].Value);

                string hash = HashFile(filePath);
                byte[] hashBytes = Encoding.UTF8.GetBytes(hash);
                fs.Write(hashBytes, 0, hashBytes.Length);

                fs.WriteByte((byte)percentage);
                fs.WriteByte(0x00);
            }
        }

        public static string HashFile(string path)
        {
            using var md5 = MD5.Create();
            using var stream = File.OpenRead(path);
            byte[] hash = md5.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "");
        }

        public static string HashFileAlt(string path)
        {
            using var sha1 = SHA1.Create();
            using var stream = File.OpenRead(path);
            var hash = sha1.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "");
        }
    }

    public class Playlist
    {
        public string Name { get; set; } = "";
        public string Author { get; set; } = "";
        public Guid Id { get; set; }
        public List<string> SongHashes { get; set; }

        public Playlist()
        {
            SongHashes = new List<string>();
        }
    }
}