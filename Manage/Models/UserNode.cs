namespace Manage.Models
{
    public class UserNode
    {
        public Users User { get; set; }
        public List<UserNode> Children { get; set; } = new List<UserNode>();

        public UserNode(Users user)
        {
            User = user;
        }
    }
}
