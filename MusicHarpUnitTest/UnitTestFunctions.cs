using BusinessLogicLayer;
using BusinessLogicLayer.Classes;
using BusinessLogicLayer.EntityDTO_s;
using BusinessLogicLayer.Entitys;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MusicHarpUnitTest
{
    [TestClass]
    public class UnitTestFunctions
    {
        [TestMethod]
        public void TestGetPlaylistFromSpecificUser_Return3()
        {
            // Arrange 
            PlaylistBusinessLogic playlistBusinessLogic = new PlaylistBusinessLogic(new FakePlaylistDAL());
            int user_id = 1;
            // Act
            List<Playlist> playlists = playlistBusinessLogic.GetSelectedPlaylists(user_id);
                
            // Assert
            Assert.AreEqual(3, playlists.Count);
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
            var deletedPlaylist = fakePlaylistDAL.GetSelectedPlaylists(null).FirstOrDefault(p => p.Id == playlistIdToDelete);

            // Assert
            Assert.IsNull(deletedPlaylist);
        }


        [TestMethod]
        public void TestGetSongsFromPlaylist_Return0()
        {
            // Arrange
            var fakePlaylistDAL = new FakePlaylistDAL();
            var playlistBusinessLogic = new PlaylistBusinessLogic(fakePlaylistDAL);
            int playlistId = 3;

            // Act
            var songs = playlistBusinessLogic.GetSongsFromPlaylist(playlistId);
    
            // Assert
            Assert.AreEqual(0, songs.Count);
        }


        [TestMethod]
        public void TestBeperking_TitleCannotBeBiggerThan30_ShouldTrowExeption() 
        {

            var playlistBusinessLogic = new PlaylistBusinessLogic(new FakePlaylistDAL());

            Playlist playlist = new Playlist { Id = 6, Name = "1234567890!@#$%^&*()1234567890!@#$%^&*()!@#$%^&*"};

            int userID = 6;

            Assert.ThrowsException<Exception>(() => playlistBusinessLogic.CreateNewPlaylist(playlist, userID));
        }

        [TestMethod]
        public void TestBeperking_TitleCannotBeNull_ShouldTrowExeption()
        {
            var playlistBusinessLogic = new PlaylistBusinessLogic(new FakePlaylistDAL());
            Playlist playlist = new Playlist { Id = 6};

            int userID = 6;

            Assert.ThrowsException<Exception>(() => playlistBusinessLogic.CreateNewPlaylist(playlist, userID));
        }

    }
}
