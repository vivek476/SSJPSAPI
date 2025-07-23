namespace SSJPSAPI.Model
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; } // 1 to 5
    }
}
