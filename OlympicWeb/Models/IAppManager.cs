using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlympicWeb.Models
{
    public interface IAppManager
    {
        List<Post> getPosts();
        string GetBestAthlete();
    }
}
