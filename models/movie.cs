namespace api_movia.models
{
    public class movie
    {
        public int id { get; set; }
        [MaxLength(length:250)]
        public string title { get; set; }

        public double rate { get; set; }

        public int year { get; set; }

        [MaxLength(length:2500)]
        public string Storyline { get; set; }

        public Byte[] poster { get; set; }

        public Byte generaid { get; set; }

        public genera genera { get; set; }


    }
}
