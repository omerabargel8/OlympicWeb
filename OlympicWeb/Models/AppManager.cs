using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OlympicWeb.DB;

namespace OlympicWeb.Models
{
    public class AppManager : IAppManager
    {
        private DBConnect DbConnect;
        public AppManager()
        {
            this.DbConnect = new DBConnect();
            DbConnect.OpenConnection();
        }

        public List<Post>[] getPosts()
        { 
            Post p = new Post { PostId = 12, Content = "hello", Likes = 3 };
            List<Post>[] posts = new List<Post>[10];
            posts[0].Add(p);
            return posts;
        }
        public string GetBestAthlete()
        {
            string table = "olympic_app.athletes";
            List<string> atributes = new List<string>();
            atributes.Add("Athlete_id");
            atributes.Add("Name");
            atributes.Add("Team");
            List<string>[] filter_list = DbConnect.BasicFilter(table, atributes);
            string name = DbConnect.TheBestAthlete("Basketball");
            Console.WriteLine(name);
            return name;
        }
    }
}
