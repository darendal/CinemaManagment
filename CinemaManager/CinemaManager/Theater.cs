namespace CinemaManager
{
    public class Theater
    {
        public int TheaterId { get; set; }

        public int CinemaId { get; set; }

        public virtual Cinema Cinema { get; set; }
    }
}