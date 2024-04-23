using BusinessLogicLayer.Entitys;
using BusinessLogicLayer.EntityDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Repo_Interfaces;


namespace BusinessLogicLayer.Classes
{
	public class SongBusinessLogic
	{
		List<SongDTO> songDTOs = new List<SongDTO>();

		List<SongDTO> searchedSongDTOs = new List<SongDTO>();

        private readonly ISongRepository repository;

        public SongBusinessLogic(ISongRepository songRepository)
        {
            repository = songRepository;
        }

        public List<Song> GetAllSongs()
		{
			songDTOs = repository.GetAllSongs();
			List<Song> songs = songDTOs.Select(dto => new Song(dto)).ToList();
            return songs;
		}

		public SongDTO CreateNewSong(Song song)
		{
            SongDTO songDTO = new SongDTO(song);
            repository.CreateNewSong(songDTO);
			return songDTO;
		}

		public List<Song> GetSongsWithoutArtist() 
		{
			songDTOs = repository.GetSongsWithoutArtist();
			List<Song> songs = songDTOs.Select(dto => new Song(dto)).ToList();
			return songs;
		}

		public List<Song> GetSearchedSongs(string input) 
		{
            songDTOs = repository.GetSearchedSongs(input);
			List<Song> searchedSongs = songDTOs.Select(dto => new Song(dto)).ToList();
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