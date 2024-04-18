using DataLogicLayer.DAL;
using DataLogicLayer.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Classes
{
    public class ArtistBusinessLogic
    {
        List<Artist> artists = new List<Artist>();
        ArtistRepostitory repository = new ArtistRepostitory();

        public List<Artist> GetAllArtists()
        {
            artists = repository.GetAllArtists();
            return artists;
        }

        public Artist CreateNewArtist(Artist artist)
        {
            repository.CreateNewArtist(artist);
            return artist;
        }

    }
}
