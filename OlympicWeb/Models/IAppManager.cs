using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlympicWeb.Models
{
    public interface IAppManager
    {
        List<Post> getPosts();
        bool UserLogin(string username, string password);
        bool UserSignup(string username, string password);
        bool LikePost(string username, int post_id);
        bool DeleteUser(string username, string password, bool isAdmin);
        bool ChangePassword(string username, string password);
    }
}
