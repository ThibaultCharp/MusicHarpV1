using BusinessLogicLayer.EntityDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repo_Interfaces
{
    public interface IArtistRepository
    {
        List<ArtistDTO> GetAllArtists();
        void CreateNewArtist(ArtistDTO artist);

    }
}
