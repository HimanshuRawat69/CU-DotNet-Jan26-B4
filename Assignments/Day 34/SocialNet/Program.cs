namespace SocialNet
{
    class User
    {
        public string Name { get; set; }
        public List<User> Friends = new List<User>();
        public User(string name) => Name = name;
        //public void AddFriend(User friend)
        //{
        //    if (!Friends.Contains(friend))
        //    {
        //        Friends.Add(friend); 
        //        friend.Friends.Add(this);
        //    }
        //}
    }

    class SocialNetwork
    {
        private List<User> _members = new List<User>();
        public void AddMember(User member)
        {
            _members.Add(member);
        }

        public void AddFriend(User friend1, User friend2)
        {
            if(!(_members.Contains(friend1) && _members.Contains(friend2))){
                Console.WriteLine($"One of the users {friend1.Name} {friend2.Name} are not on Social PLatform");
            }
            else
            {
                if (!friend1.Friends.Contains(friend2))
                {
                    friend1.Friends.Add(friend2);
                    friend2.Friends.Add(friend1);
                } 
                
            }
        }

        public void ShowNetwork()
        {
            foreach (var member in _members)
            {
                Console.Write(member.Name + " -> ");
                List<string> friends = new List<string>();
                foreach(var friend in member.Friends)
                {
                   friends.Add(friend.Name);
                }
                Console.WriteLine($"{string.Join(", ", friends)}");
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            SocialNetwork network = new SocialNetwork();

            User amit = new User("Amit");
            User bhavya = new User("Bhavya");
            User chirag = new User("Chirag");
            User drishti = new User("Drishti");
            User eena = new User("Eena");

            network.AddMember(amit);
            network.AddMember(bhavya);
            network.AddMember(chirag);
            network.AddMember(drishti);

            //amit.AddFriend(bhavya);
            //amit.AddFriend(chirag);
            //bhavya.AddFriend(chirag);
            //drishti.AddFriend(chirag); 
            network.AddFriend(amit, bhavya);
            network.AddFriend(amit, chirag);
            network.AddFriend(amit, bhavya);
            network.AddFriend(bhavya, chirag);
            network.AddFriend(drishti, chirag);
            network.AddFriend(amit, eena);

            network.ShowNetwork();
        }
    }
}
