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

        private SongRepository repository = new SongRepository();


		public List<Song> GetAllSongs()
		{
			songs = repository.GetAllSongs();
			return songs;
		}
	}
}

