using BusinessLogicLayer;
using BusinessLogicLayer.Classes;
using BusinessLogicLayer.Entitys;
using DataLogicLayer.DAL;

namespace MusicHarpUnitTest
{
    [TestClass]
    public class UnitTest1
    {

        SongBusinessLogic songBusinessLogic = new SongBusinessLogic(new SongRepository());
        [TestMethod]
        public void TestMethod1()
        {
            List<Song> songs = new List<Song>();
            Song song = new Song
            {
                Id = 100,
                SongName = "TestSong",
                SongUrl = "https://www.youtube.com/embed/YMAAqmNt6mo?si=O45DcS2NToETQQH_",
                ArtistName = "testArtist"
            };

            songBusinessLogic.CreateNewSong(song);
            songs = songBusinessLogic.GetAllSongs();

            Assert.IsTrue(songs.Any(s => s.Id == 100 && s.SongName == "TestSong" && s.SongUrl == "https://www.youtube.com/embed/YMAAqmNt6mo?si=O45DcS2NToETQQH_" && s.ArtistName == "testArtist"), "The song was not added correctly.");
        }
    }
}