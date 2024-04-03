using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Classes
{
	public class SongBusinessLogic
	{
		List<string> songs = new List<string>();

		public List<string> setWordsList()
		{
			songs.Add("joo");
			songs.Add("popp");

			return songs;
		}
	}
}

