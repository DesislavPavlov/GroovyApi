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
        public List<User> GetUsers()
        {
            DataTable dt = SelectQuery("SELECT * FROM user_info");
            IEnumerable<User> enumerable = dt.AsEnumerable()
              .Select(userRow => new User
              {
                  Id = int.Parse(userRow["user_id"].ToString()),
                  Username = userRow["username"].ToString(),
                  Email = userRow["email"].ToString(),
                  Password_Hash = userRow["password_hash"].ToString(),
                  AvatarUrl = userRow["avatar_url"].ToString(),
                  CreatedAt = DateTime.Parse(userRow["created_at"].ToString()),
              });

            List<User> list = enumerable.ToList();
            return list;
        }
        public User GetUserById(int id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@Id", id }
            };
            DataTable userDT = SelectQuery($"SELECT * FROM user_info WHERE user_id = @Id", parameters);

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
        public List<Artist> GetArtistsOfSong(int songId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@SongId", songId }
            };

            DataTable dt = SelectQuery("SELECT * FROM artist JOIN song_artist ON artist.artist_id = song_artist.artist_id WHERE song_artist.song_id = @SongId", parameters);
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
        public Dictionary<int, List<int>> GetArtistIdsOfSongs(List<int> songIds)
        {
            string ids = string.Join(", ", songIds.Select(id => id.ToString()));
            DataTable dt = SelectQuery($"SELECT song_id, artist_id FROM song_artist WHERE song_id IN ({ids})");

            Dictionary<int, List<int>> dict = dt.AsEnumerable()
                .GroupBy(row => row.Field<int>("song_id"))
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(row => row.Field<int>("artist_id")).ToList()
                );

            return dict;
        }
        public Dictionary<int, List<int>> GetGenreIdsOfSongs(List<int> songIds)
        {
            string ids = string.Join(", ", songIds.Select(id => id.ToString()));
            DataTable dt = SelectQuery($"SELECT song_id, genre_id FROM song_genre WHERE song_id IN ({ids})");

            Dictionary<int, List<int>> dict = dt.AsEnumerable()
                .GroupBy(row => row.Field<int>("song_id"))
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(row => row.Field<int>("genre_id")).ToList()
                );

            return dict;
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
            DataTable dt = SelectQuery($"SELECT DISTINCT * FROM song JOIN song_artist ON song.song_id = song_artist.song_id WHERE song_artist.artist_id IN ({ids})");
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
        public List<Genre> GetGenresOfArtist(int artistId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@ArtistId", artistId }
            };
            DataTable dt = SelectQuery("SELECT * FROM genre JOIN artist_genre ON genre.genre_id = artist_genre.genre_id WHERE artist_genre.artist_id = @ArtistId", parameters);
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
        public List<Genre> GetGenresOfArtists(List<int> artistIds)
        {
            string ids = string.Join(", ", artistIds.Select(id => id.ToString()));
            DataTable dt = SelectQuery($"SELECT * FROM genre JOIN artist_genre ON genre.genre_id = artist_genre.genre_id WHERE artist_genre.artist_id IN ({ids}) GROUP BY genre.genre_id");
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
        public Dictionary<int, List<int>> GetGenreIdsOfArtists(List<int> artistIds)
        {
            string ids = string.Join(", ", artistIds.Select(id => id.ToString()));
            DataTable dt = SelectQuery($"SELECT artist_id, genre_id FROM artist_genre WHERE artist_id IN ({ids})");

            Dictionary<int, List<int>> dict = dt.AsEnumerable()
                .GroupBy(row => row.Field<int>("artist_id"))
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(row => row.Field<int>("genre_id")).ToList()
                );

            return dict;
        }
        public List<Song> GetUserFavouriteSongs(int userId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@UserId", userId }
            };
            DataTable dt = SelectQuery("SELECT s.* FROM song s JOIN favourite f ON s.song_id = f.song_id WHERE f.user_id = @UserId", parameters);
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
        public List<Artist> GetUserFavouriteArtists(int userId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@UserId", userId }
            };
            DataTable dt = SelectQuery("SELECT a.* FROM artist a LEFT JOIN user_artist_activity uaa ON a.artist_id = uaa.artist_id AND uaa.user_id = @UserId ORDER BY uaa.clicks DESC LIMIT 5", parameters);
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
        public List<Genre> GetUserFavouriteGenres(int userId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@UserId", userId }
            };
            DataTable dt = SelectQuery("SELECT g.* FROM genre g LEFT JOIN user_genre_activity uga ON g.genre_id = uga.genre_id AND uga.user_id = @UserId ORDER BY uga.clicks DESC LIMIT 5", parameters);
            IEnumerable<Genre> enumerable = dt.AsEnumerable()
              .Select(row => new Genre
              {
                  Id = row.Field<int>("genre_id"),
                  Name = row.Field<string>("name"),
                  Color = row.Field<string>("color")
              });

            List<Genre> list = enumerable.ToList();
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
        public int AddSongClick(int songId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@SongId", songId },
            };

            int affectedRows = ExecuteNonQueryUpdateDelete("UPDATE song SET clicks = clicks + 1 WHERE song_id = @SongId LIMIT 1", parameters);
            return affectedRows;
        }
        public int AddUserArtistClick(int userId, int artistId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@UserId", userId },
                { "@ArtistId", artistId },
            };

            int affectedRows = ExecuteNonQueryUpdateDelete("UPDATE user_artist_activity SET clicks = clicks + 1 WHERE user_id = @UserId AND artist_id = @ArtistId LIMIT 1", parameters);
            if (affectedRows <= 0)
            {
                return ExecuteNonQueryInsert("INSERT INTO user_artist_activity (user_id, artist_id, clicks) VALUES (@UserId, @ArtistId, 1)", parameters);
            }

            return affectedRows;
        }
        public int AddBatchUserArtistClick(int userId, List<int> artistIds)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@UserId", userId }
            };

            int affectedRows = ExecuteNonQueryUpdateDelete($"UPDATE user_artist_activity SET clicks = clicks + 1 WHERE user_id = @UserId AND artist_id IN ({string.Join(",", artistIds)})", parameters);

            if (affectedRows != artistIds.Count)
            {
                DataTable existingArtistIdsDt = SelectQuery($"SELECT artist_id FROM user_artist_activity WHERE user_id = @UserId AND artist_id IN ({string.Join(",", artistIds)})");

                List<int> existingArtistIds = new List<int>();
                foreach (DataRow row in existingArtistIdsDt.Rows)
                {
                    existingArtistIds.Add(row.Field<int>("artist_id"));
                }

                List<int> newArtistIds = artistIds.Except(existingArtistIds).ToList();

                List<string> values = new List<string>();

                for (int i = 0; i < newArtistIds.Count; i++)
                {
                    values.Add($"(@UserId, @ArtistId{i}, 1)");
                    parameters[$"@ArtistId{i}"] = newArtistIds[i];
                }

                List<int> ids = ExecuteBatchInsert($"INSERT INTO user_artist_activity (user_id, artist_id, clicks) VALUES {string.Join(",", values)}; SELECT LAST_INSERT_ID();", parameters);
                return ids.Count;
            }
            else
            {
                return affectedRows;
            }
        }
        public int AddUserGenreClick(int userId, int genreId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@UserId", userId },
                { "@GenreId", genreId },
            };

            int affectedRows = ExecuteNonQueryUpdateDelete("UPDATE user_genre_activity SET clicks = clicks + 1 WHERE user_id = @UserId AND genre_id = @GenreId LIMIT 1", parameters);
            if (affectedRows <= 0)
            {
                return ExecuteNonQueryInsert("INSERT INTO user_genre_activity (user_id, genre_id, clicks) VALUES (@UserId, @GenreId, 1)", parameters);
            }

            return affectedRows;
        }
        public int AddBatchUserGenreClick(int userId, List<int> genreIds)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@UserId", userId }
            };

            int affectedRows = ExecuteNonQueryUpdateDelete($"UPDATE user_genre_activity SET clicks = clicks + 1 WHERE user_id = @UserId AND genre_id IN ({string.Join(",", genreIds)})", parameters);

            if (affectedRows != genreIds.Count)
            {
                DataTable existingGenreIdsDt = SelectQuery($"SELECT genre_id FROM user_genre_activity WHERE user_id = @UserId AND genre_id IN ({string.Join(",", genreIds)})");

                List<int> existingGenreIds = new List<int>();
                foreach (DataRow row in existingGenreIdsDt.Rows)
                {
                    existingGenreIds.Add(row.Field<int>("genre_id"));
                }

                List<int> newGenreIds = genreIds.Except(existingGenreIds).ToList();

                List<string> values = new List<string>();

                for (int i = 0; i < newGenreIds.Count; i++)
                {
                    values.Add($"(@UserId, @GenreId{i}, 1)");
                    parameters[$"@GenreId{i}"] = newGenreIds[i];
                }

                List<int> ids = ExecuteBatchInsert($"INSERT INTO user_genre_activity (user_id, genre_id, clicks) VALUES {string.Join(",", values)}; SELECT LAST_INSERT_ID();", parameters);
                return ids.Count;
            }
            else
            {
                return affectedRows;
            }
        }
        public int AddSongToUserFavourite(int songId, int userId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@SongId", songId },
                { "@UserId", userId }
            };

            return ExecuteNonQueryInsert("INSERT INTO favourite (song_id, user_id) VALUES (@SongId, @UserId)", parameters);
        }


        // Update
        public bool UpdateGenre(int id, Genre newGenre)
        {
            string query = "UPDATE genre SET name = @Name, color = @Color WHERE genre_id = @Id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@Id", id },
                { "@Name", newGenre.Name },
                { "@Color", newGenre.Color }
            };

            int affectedRows = ExecuteNonQueryUpdateDelete(query, parameters);
            return affectedRows > 0;
        }
        public bool UpdateArtist(int id, Artist newArtist, List<int> genreIds)
        {
            string query = "UPDATE artist SET name = @Name, image_url = @ImageUrl, color = @Color WHERE artist_id = @Id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@Id", id },
                { "@Name", newArtist.Name },
                { "@ImageUrl", newArtist.ImageUrl },
                { "@Color", newArtist.Color }
            };

            int affectedRows = ExecuteNonQueryUpdateDelete(query, parameters);
            if (affectedRows <= 0)
            {
                return false;
            }

            bool success = DeleteArtistGenreRelations(id);
            if (!success)
            {
                return false;
            }

            List<int> ids = AddArtistGenres(id, genreIds);
            if (ids == null || ids.Count == 0)
            {
                return false;
            }

            return true;
        }
        public bool UpdateSong(int id, Song newSong, List<int> artistIds, List<int> genreIds)
        {
            string query = "UPDATE song SET title = @Title, cover_url = @CoverUrl, song_url = @SongUrl, color = @Color WHERE song_id = @Id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@Id", id },
                { "@Title", newSong.Title },
                { "@CoverUrl", newSong.CoverUrl },
                { "@SongUrl", newSong.SongUrl },
                { "@Color", newSong.Color }
            };

            int affectedRows = ExecuteNonQueryUpdateDelete(query, parameters);
            if (affectedRows <= 0)
            {
                return false;
            }

            // Delete old relations
            bool success = DeleteSongArtistRelations(id);
            if (!success)
            {
                return false;
            }

            success = DeleteSongGenreRelations(id);
            if (!success)
            {
                return false;
            }

            // Add new relations
            List<int> ids = AddSongArtists(id, artistIds);
            if (ids == null || ids.Count == 0)
            {
                return false;
            }

            ids = AddSongGenres(id, genreIds);
            if (ids == null || ids.Count == 0)
            {
                return false;
            }

            return true;
        }


        // Delete
        public bool DeleteUser(int userId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@UserId", userId }
            };

            int affectedRows = ExecuteNonQueryUpdateDelete("DELETE FROM user_info WHERE user_id = @UserId", parameters);
            return affectedRows > 0;
        }
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
        public bool DeleteArtistGenreRelations(int artistId)
        {
            string query = "DELETE FROM artist_genre WHERE artist_id = @Id";
            var parameters = new Dictionary<string, object>
            {
                { "@Id", artistId }
            };

            int affectedRows = ExecuteNonQueryUpdateDelete(query, parameters);
            return affectedRows > 0;
        }
        public bool DeleteSongArtistRelations(int songId)
        {
            string query = "DELETE FROM song_artist WHERE song_id = @Id";
            var parameters = new Dictionary<string, object>
            {
                { "@Id", songId }
            };

            int affectedRows = ExecuteNonQueryUpdateDelete(query, parameters);
            return affectedRows > 0;
        }
        public bool DeleteSongGenreRelations(int songId)
        {
            string query = "DELETE FROM song_genre WHERE song_id = @Id";
            var parameters = new Dictionary<string, object>
            {
                { "@Id", songId }
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
        public int DeleteSongFromUserFavourite(int songId, int userId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "SongId", songId },
                { "UserId", userId }
            };

            int affectedRows = ExecuteNonQueryUpdateDelete("DELETE FROM favourite WHERE song_id = @SongId AND user_id = @UserId", parameters);
            return affectedRows;
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
