using DataLogicLayer.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using DataLogicLayer.DAL;

namespace BusinessLogicLayer.Classes
{
	public class SongBusinessLogic
	{
		List<Song> songs = new List<Song>();

		List<Song> searchedSongs = new List<Song>();

        private SongRepository repository = new SongRepository();


		public List<Song> GetAllSongs()
		{
            songs = repository.GetAllSongs();
            return songs;
		}

		public Song CreateNewSong(Song song)
		{
			repository.CreateNewSong(song);
			return song;
		}

		public List<Song> GetSongsWithoutArtist() 
		{ 
			songs = repository.GetSongsWithoutArtist();
			return songs;
		}

		public List<Song> GetSearchedSongs(string input) 
		{
			searchedSongs = repository.GetSearchedSongs(input);
			return searchedSongs;
		}

		public void InsertSongInPlaylist(int pId, int sId)
		{
			repository.InsertSongInPlaylist(pId, sId);
		}

		public void LinkSongToArtist(int ArtistId, int SongId)
		{
			repository.LinkSongToArtist(ArtistId, SongId);
		}
    }
}