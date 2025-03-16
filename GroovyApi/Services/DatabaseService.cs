using GroovyApi.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace GroovyApi.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        // Read
        public DataTable GetUsers()
        {
            DataTable users = SelectQuery("SELECT * FROM user_info");
            return users;
        }
        public User GetUserByUserName(string userName)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@UserName", userName }
            };
            DataTable userDT = SelectQuery($"SELECT * FROM user_info WHERE username = @UserName", parameters);

            if (userDT.Rows.Count == 0)
            {
                return null;
            }
            DataRow userRow = userDT.Rows[0];

            User user = new User()
            {
                Id = int.Parse(userRow["user_id"].ToString()),
                Username = userRow["username"].ToString(),
                Email = userRow["email"].ToString(),
                Password_Hash = userRow["password_hash"].ToString(),
                AvatarUrl = userRow["avatar_url"].ToString(),
                CreatedAt = DateTime.Parse(userRow["created_at"].ToString()),
            };

            return user;
        }
        public List<Artist> GetArtists()
        {
            DataTable dt = SelectQuery("SELECT * FROM artist ORDER BY name");
            IEnumerable<Artist> enumerable = dt.AsEnumerable()
              .Select(row => new Artist
              {
                  Id = row.Field<int>("artist_id"),
                  Name = row.Field<string>("name"),
                  Color = row.Field<string>("color"),
                  ImageUrl = row.Field<string>("image_url")
              });

            List<Artist> list = enumerable.ToList();
            return list;
        }
        public Artist GetArtistById(int artistId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@ArtistId", artistId }
            };
            DataTable artistDT = SelectQuery("SELECT * FROM artist WHERE artist_id = @ArtistId", parameters);
            DataRow artistRow = artistDT.Rows[0];

            Artist artist = new Artist()
            {
                Id = int.Parse(artistRow["artist_id"].ToString()),
                Name = artistRow["name"].ToString(),
                ImageUrl = artistRow["image_url"].ToString(),
                Color = artistRow["color"].ToString(),
            };

            return artist;
        }
        public Song GetSongById(int songId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@SongId", songId }
            };
            DataTable songDT = SelectQuery("SELECT * FROM song WHERE song_id = @SongId", parameters);
            DataRow songRow = songDT.Rows[0];

            Song song = new Song()
            {
                Id = int.Parse(songRow["song_id"].ToString()),
                Title = songRow["title"].ToString(),
                CoverUrl = songRow["cover_url"].ToString(),
                SongUrl = songRow["song_url"].ToString(),
                Color = songRow["color"].ToString(),
                Clicks = int.Parse(songRow["clicks"].ToString())
            };

            return song;
        }
        public List<Genre> GetGenres()
        {
            DataTable dt = SelectQuery("SELECT * FROM genre");
            IEnumerable<Genre> enumerable = dt.AsEnumerable()
              .Select(row => new Genre
              {
                  Id = row.Field<int>("genre_id"),
                  Name = row.Field<string>("name"),
                  Color = row.Field<string>("color"),
              });

            List<Genre> list = enumerable.ToList();
            return list;
        }
        public List<Song> GetSearchedSongs(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return GetAllSongs();
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@SearchTerm", searchTerm },
                { "@SearchPattern", "%" + searchTerm + "%" }
            };

            DataTable searchResult = SelectQuery("SELECT DISTINCT s.song_id, s.title, s.song_url, s.cover_url, s.color, s.clicks FROM song s LEFT JOIN song_artist sa ON s.song_id = sa.song_id LEFT JOIN artist a ON sa.artist_id = a.artist_id LEFT JOIN song_genre sg ON s.song_id = sg.song_id LEFT JOIN genre g ON sg.genre_id = g.genre_id WHERE @SearchTerm IS NULL OR s.title LIKE @SearchPattern OR a.name LIKE @SearchPattern OR g.name LIKE @SearchPattern", parameters);
            IEnumerable<Song> enumerable = searchResult.AsEnumerable()
              .Select(row => new Song
              {
                  Id = row.Field<int>("song_id"),
                  Title = row.Field<string>("title"),
                  Color = row.Field<string>("color"),
                  CoverUrl = row.Field<string>("cover_url"),
                  SongUrl = row.Field<string>("song_url"),
                  Clicks = row.Field<int>("clicks")
              });

            List<Song> list = enumerable.ToList();
            return list;
        }
        public DataTable GetRecommendedSongs()
        {
            DataTable artists = SelectQuery("SELECT * FROM song ORDER BY clicks DESC LIMIT 20");
            return artists;
        }
        public List<Song> GetAllSongs()
        {
            DataTable dt = SelectQuery("SELECT * FROM song");
            IEnumerable<Song> enumerable = dt.AsEnumerable()
              .Select(row => new Song
              {
                  Id = row.Field<int>("song_id"),
                  Title = row.Field<string>("title"),
                  Color = row.Field<string>("color"),
                  CoverUrl = row.Field<string>("cover_url"),
                  SongUrl = row.Field<string>("song_url"),
                  Clicks = row.Field<int>("clicks")
              });

            List<Song> list = enumerable.ToList();
            return list;
        }
        public DataTable GetArtistsOfSong(int songId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@SongId", songId }
            };
            DataTable songs = SelectQuery("SELECT * FROM artist JOIN song_artist ON artist.artist_id = song_artist.artist_id WHERE song_artist.song_id = @SongId", parameters);
            return songs;
        }
        public List<Artist> GetArtistsOfGenre(int genreId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@GenreId", genreId }
            };
            DataTable dt = SelectQuery("SELECT * FROM artist JOIN artist_genre ON artist.artist_id = artist_genre.artist_id WHERE artist_genre.genre_id = @GenreId", parameters);
            IEnumerable<Artist> enumerable = dt.AsEnumerable()
              .Select(row => new Artist
              {
                  Id = row.Field<int>("artist_id"),
                  Name = row.Field<string>("name"),
                  Color = row.Field<string>("color"),
                  ImageUrl = row.Field<string>("image_url")
              });

            List<Artist> list = enumerable.ToList();
            return list;
        }
        public List<Song> GetSongsOfArtist(int artistId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@ArtistId", artistId }
            };
            DataTable dt = SelectQuery("SELECT * FROM song JOIN song_artist ON song.song_id = song_artist.song_id WHERE song_artist.artist_id = @ArtistId", parameters);
            IEnumerable<Song> enumerable = dt.AsEnumerable()
             .Select(row => new Song
             {
                 Id = row.Field<int>("song_id"),
                 Title = row.Field<string>("title"),
                 CoverUrl = row.Field<string>("cover_url"),
                 SongUrl = row.Field<string>("song_url"),
                 Color = row.Field<string>("color"),
                 Clicks = row.Field<int>("clicks"),
             });

            List<Song> list = enumerable.ToList();
            return list;
        }
        public List<Song> GetSongsOfArtists(List<int> artistIds)
        {
            string ids = string.Join(", ", artistIds.Select(id => id.ToString()));
            DataTable dt = SelectQuery($"SELECT * FROM song JOIN song_artist ON song.song_id = song_artist.song_id WHERE song_artist.artist_id IN ({ids})");
            IEnumerable<Song> enumerable = dt.AsEnumerable()
              .Select(row => new Song
              {
                  Id = row.Field<int>("song_id"),
                  Title = row.Field<string>("title"),
                  Color = row.Field<string>("color"),
                  CoverUrl = row.Field<string>("cover_url"),
                  SongUrl = row.Field<string>("song_url"),
                  Clicks = row.Field<int>("clicks")
              });

            List<Song> list = enumerable.ToList();
            return list;
        }


        // Create
        public int AddUser(User user)
        {
            string query = "INSERT INTO user_info (username, email, password_hash, avatar_url) VALUES (@Username, @Email, @PasswordHash, @AvatarUrl)";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@Username", user.Username },
                { "@Email", user.Email },
                { "@PasswordHash", user.Password_Hash },
                { "@AvatarUrl", user.AvatarUrl },
            };

            int resultId = ExecuteNonQueryInsert(query, parameters);
            return resultId;

        }
        public int AddArtist(Artist artist)
        {
            string query = "INSERT INTO artist (name, image_url, color) VALUES (@Name, @ImageUrl, @Color)";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@Name", artist.Name },
                { "@ImageUrl", artist.ImageUrl },
                { "@Color", artist.Color }
            };

            int resultId = ExecuteNonQueryInsert(query, parameters);
            return resultId;
        }
        public int AddGenre(Genre genre)
        {
            string query = "INSERT INTO genre (name, color) VALUES (@Name, @Color)";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@Name", genre.Name },
                { "@Color", genre.Color }
            };

            int resultId = ExecuteNonQueryInsert(query, parameters);
            return resultId;
        }
        public int AddSong(Song song)
        {
            string query = "INSERT INTO song (title, song_url, cover_url, color, clicks) VALUES (@Title, @SongUrl, @CoverUrl, @Color, @Clicks)";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@Title", song.Title },
                { "@SongUrl", song.SongUrl },
                { "@CoverUrl", song.CoverUrl },
                { "@Color", song.Color },
                { "@Clicks", 0 },
            };

            int resultId = ExecuteNonQueryInsert(query, parameters);
            return resultId;
        }
        public List<int> AddArtistGenres(int artistId, List<int> genreIds)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            List<string> values = new List<string>();

            for (int i = 0; i < genreIds.Count; i++)
            {
                values.Add($"(@ArtistId, @GenreId{i})");
                parameters[$"@GenreId{i}"] = genreIds[i];
            }
            parameters["@ArtistId"] = artistId;

            string query = $"INSERT INTO artist_genre (artist_id, genre_id) VALUES {string.Join(", ", values)}; SELECT LAST_INSERT_ID();";

            List<int> resultIds = ExecuteBatchInsert(query, parameters);
            return resultIds;
        }
        public List<int> AddSongGenres(int songId, List<int> genreIds)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            List<string> values = new List<string>();

            for (int i = 0; i < genreIds.Count; i++)
            {
                values.Add($"(@SongId, @GenreId{i})");
                parameters[$"@GenreId{i}"] = genreIds[i];
            }
            parameters["@SongId"] = songId;

            string query = $"INSERT INTO song_genre (song_id, genre_id) VALUES {string.Join(", ", values)}; SELECT LAST_INSERT_ID();";

            List<int> resultIds = ExecuteBatchInsert(query, parameters);
            return resultIds;
        }
        public List<int> AddSongArtists(int songId, List<int> artistIds)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            List<string> values = new List<string>();

            for (int i = 0; i < artistIds.Count; i++)
            {
                values.Add($"(@SongId, @ArtistId{i})");
                parameters[$"@ArtistId{i}"] = artistIds[i];
            }
            parameters["@SongId"] = songId;

            string query = $"INSERT INTO song_artist (song_id, artist_id) VALUES {string.Join(", ", values)}; SELECT LAST_INSERT_ID();";

            List<int> resultIds = ExecuteBatchInsert(query, parameters);
            return resultIds;
        }


        // Delete
        public bool DeleteArtist(int artistId)
        {
            string query = "DELETE FROM artist WHERE artist_id = @ArtistId";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@ArtistId", artistId },
            };

            int affectedRows = ExecuteNonQueryUpdateDelete(query, parameters);
            return affectedRows > 0;
        }
        public bool DeleteSong(int songId)
        {
            string query = "DELETE FROM song WHERE song_id = @SongId";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@SongId", songId },
            };

            int affectedRows = ExecuteNonQueryUpdateDelete(query, parameters);
            return affectedRows > 0;
        }
        public bool DeleteGenre(int genreId)
        {
            string query = "DELETE FROM genre WHERE genre_id = @GenreId";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@GenreId", genreId },
            };

            int affectedRows = ExecuteNonQueryUpdateDelete(query, parameters);
            return affectedRows > 0;
        }
        public List<string> DeleteOrphanedArtistsAndSongsAndReturnFileUrls()
        {
            List<string> fileUrisToDelete = new List<string>();

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            using (MySqlCommand cmdGetArtists = new MySqlCommand("SELECT image_url FROM artist WHERE artist_id NOT IN (SELECT artist_id FROM artist_genre)", conn))
            using (MySqlCommand cmdGetSongs = new MySqlCommand("SELECT cover_url, song_url FROM song WHERE song_id NOT IN (SELECT song_id FROM song_artist)", conn))
            using (MySqlCommand cmdDeleteArtists = new MySqlCommand("DELETE FROM artist WHERE artist_id NOT IN (SELECT artist_id FROM artist_genre)", conn))
            using (MySqlCommand cmdDeleteSongs = new MySqlCommand("DELETE FROM song WHERE song_id NOT IN (SELECT song_id FROM song_artist)", conn))
            {
                conn.Open();
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Assign transaction
                        cmdGetArtists.Transaction = transaction;
                        cmdGetSongs.Transaction = transaction;
                        cmdDeleteArtists.Transaction = transaction;
                        cmdDeleteSongs.Transaction = transaction;

                        // Get artist uris to delete
                        using (MySqlDataReader reader = cmdGetArtists.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string imageUrl = reader["image_url"].ToString();
                                if (!string.IsNullOrEmpty(imageUrl))
                                {
                                    fileUrisToDelete.Add(imageUrl);
                                }
                            }
                        }

                        // Delete artists
                        cmdDeleteArtists.ExecuteNonQuery();

                        // Get song uris to delete
                        using (MySqlDataReader reader = cmdGetSongs.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string coverUrl = reader["cover_url"].ToString();
                                string songUrl = reader["song_url"].ToString();
                                if (!string.IsNullOrEmpty(coverUrl))
                                {
                                    fileUrisToDelete.Add(coverUrl);
                                }
                                if (!string.IsNullOrEmpty(songUrl))
                                {
                                    fileUrisToDelete.Add(songUrl);
                                }
                            }
                        }

                        // Delete songs
                        cmdDeleteSongs.ExecuteNonQuery();

                        // Commit transaction
                        transaction.Commit();
                    }
                    catch (MySqlException ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Database error: {ex.Message}");
                    }
                }
                conn.Close();
            }

            return fileUrisToDelete;
        }


        // Helper
        private DataTable SelectQuery(string query, Dictionary<string, object> parameters = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();

                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }

                    conn.Close();
                }

                return dt;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");

                return new DataTable();
            }
        }
        private int ExecuteNonQueryInsert(string query, Dictionary<string, object> parameters = null)
        {
            int result = 0;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    conn.Open();

                    cmd.ExecuteNonQuery();
                    result = Convert.ToInt32(new MySqlCommand("SELECT LAST_INSERT_ID();", conn).ExecuteScalar());

                    conn.Close();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }

            return result;
        }
        private int ExecuteNonQueryUpdateDelete(string query, Dictionary<string, object> parameters = null)
        {
            int affectedRows = 0;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    conn.Open();

                    affectedRows = cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }

            return affectedRows;
        }
        private List<int> ExecuteBatchInsert(string query, Dictionary<string, object> parameters)
        {
            List<int> insertedIds = new List<int>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();
                    if (parameters != null && parameters.Count > 0)
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        cmd.ExecuteNonQuery();


                        int firstId = Convert.ToInt32(new MySqlCommand("SELECT LAST_INSERT_ID();", conn).ExecuteScalar());

                        for (int i = 0; i < parameters.Count; i++)
                        {
                            insertedIds.Add(firstId + i);
                        }
                    }
                    conn.Close();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }

            return insertedIds;
        }
        private int ExecuteBatchUpdateDelete(string query, Dictionary<string, object> parameters)
        {
            int affectedRows = 0;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();

                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }

                    affectedRows = cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }

            return affectedRows;
        }
    }
}
