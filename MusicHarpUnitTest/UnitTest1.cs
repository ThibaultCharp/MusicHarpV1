using BusinessLogicLayer;
using BusinessLogicLayer.Classes;
using BusinessLogicLayer.EntityDTO_s;
using BusinessLogicLayer.Entitys;

namespace MusicHarpUnitTest
{
    [TestClass]
    public class UnitTest1
    {




            [TestMethod]
            public void TestMethod1()
            {
                PlaylistBusinessLogic playlistBusinessLogic = new PlaylistBusinessLogic(new FakePlaylistDAL());

                int user_id = 1;

                List<Playlist> playlists = playlistBusinessLogic.GetSelectedPlaylists(user_id);

                Assert.AreEqual(3, playlists.Count);
            }

            [TestMethod]
            public void TestCreateNewPlaylist()
            {
                // Arrange
                var fakePlaylistDAL = new FakePlaylistDAL();
                var playlistBusinessLogic = new PlaylistBusinessLogic(fakePlaylistDAL);

                var user1 = new User { Id = 1, Name = "Test", Password = "password", ProfilePicture = "swedrfgyhuj" };
                var playlist = new Playlist { Id = 1, Name = "P1", User = user1 };

                // Act
                var response = playlistBusinessLogic.CreateNewPlaylist(playlist, user1.Id);

                // Assert
                Assert.IsTrue(response.Success, "Playlist creation failed.");
                var createdPlaylist = fakePlaylistDAL.GetSelectedPlaylists(user1.Id).FirstOrDefault(p => p.Id == playlist.Id);
                Assert.IsNotNull(createdPlaylist, "Playlist was not created successfully.");
                Assert.AreEqual(playlist.Name, createdPlaylist.Name, "Playlist name does not match.");
                Assert.AreEqual(user1.Id, createdPlaylist.User.Id, "Playlist user ID does not match.");
            }

            [TestMethod]
            public void TestDeletePlaylist()
            {
                // Arrange
                var fakePlaylistDAL = new FakePlaylistDAL();
                var playlistBusinessLogic = new PlaylistBusinessLogic(fakePlaylistDAL);
                int playlistIdToDelete = 1;

                // Act
                playlistBusinessLogic.DeletePlaylist(playlistIdToDelete);

                // Assert
                var deletedPlaylist = fakePlaylistDAL.GetSelectedPlaylists(null).FirstOrDefault(p => p.Id == playlistIdToDelete);
                Assert.IsNull(deletedPlaylist, "Playlist was not deleted successfully.");
            }

            [TestMethod]
            public void TestEditPlaylist()
            {
                // Arrange
                var fakePlaylistDAL = new FakePlaylistDAL();
                var playlistBusinessLogic = new PlaylistBusinessLogic(fakePlaylistDAL);
                int playlistId = 2;
                int userId = 2;

                // Act
                var editedPlaylist = playlistBusinessLogic.EditPlaylist(playlistId, userId);

                // Assert
                Assert.AreEqual(playlistId, editedPlaylist.Id, "Edited playlist ID does not match.");
                Assert.AreEqual("P2", editedPlaylist.Name, "Edited playlist name does not match.");
                Assert.AreEqual(userId, editedPlaylist.User.Id, "Edited playlist user ID does not match.");
            }

            [TestMethod]
            public void TestSaveEditedPlaylist()
            {
                // Arrange
                var fakePlaylistDAL = new FakePlaylistDAL();
                var playlistBusinessLogic = new PlaylistBusinessLogic(fakePlaylistDAL);
                var editedPlaylist = new Playlist { Id = 3, Name = "Edited Playlist", User = new User { Id = 2 } };

                // Act
                playlistBusinessLogic.SaveEditedPlaylist(editedPlaylist);

                // Assert
                var savedPlaylist = fakePlaylistDAL.GetSelectedPlaylists(editedPlaylist.User.Id).FirstOrDefault(p => p.Id == editedPlaylist.Id);
                Assert.IsNotNull(savedPlaylist, "Playlist was not saved successfully.");
                Assert.AreEqual(editedPlaylist.Name, savedPlaylist.Name, "Saved playlist name does not match.");
            }

            [TestMethod]
            public void TestGetSongsFromPlaylist()
            {
                // Arrange
                var fakePlaylistDAL = new FakePlaylistDAL();
                var playlistBusinessLogic = new PlaylistBusinessLogic(fakePlaylistDAL);
                int playlistId = 3;

                // Act
                var songs = playlistBusinessLogic.GetSongsFromPlaylist(playlistId);

                // Assert
                Assert.IsNotNull(songs, "Songs in playlist not retrieved successfully.");
                Assert.AreEqual(0, songs.Count, "Number of songs in playlist is incorrect.");
            }
        }
    }
