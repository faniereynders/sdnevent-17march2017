namespace uniform_interface.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Profile[] Friends { get; set; }
    }

}