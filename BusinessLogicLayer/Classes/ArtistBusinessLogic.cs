using BusinessLogicLayer.Entitys;
using BusinessLogicLayer.EntityDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Repo_Interfaces;

namespace BusinessLogicLayer.Classes
{
    public class ArtistBusinessLogic
    {
        List<ArtistDTO> artistDTOs = new List<ArtistDTO>();
        List<SongDTO> songDTOs = new List<SongDTO>();
        
        private readonly IArtistRepository repository;

        public ArtistBusinessLogic(IArtistRepository artistRepository)
        {
            repository = artistRepository;
        }

        public List<Artist> GetAllArtists()
        {
            artistDTOs = repository.GetAllArtists();
            List<Artist> artists = artistDTOs.Select(dto => new Artist(dto)).ToList();
            return artists;
        }

        public ArtistDTO CreateNewArtist(Artist artist)
        {
            ArtistDTO artistDTO = new ArtistDTO(artist);
            repository.CreateNewArtist(artistDTO);
            return artistDTO;
        }

        public List<Song> GetSongsFromArtist(int id)
        {
            songDTOs = repository.SongsFromArtist(id);
            List<Song> songs = songDTOs.Select(dto => new Song(dto)).ToList();
            return songs;
        }
    }
}
