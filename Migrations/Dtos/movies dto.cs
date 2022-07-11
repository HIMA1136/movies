namespace api_movia.Migrations.Dtos
{
    public class movies_dto
    {
        [MaxLength(length: 250)]
        public string title { get; set; }

        public double rate { get; set; }

        public int year { get; set; }

        [MaxLength(length: 2500)]
        public string Storyline { get; set; }

        public IFormFile poster { get; set; }

        public Byte generaid { get; set; }

        public genera genera { get; set; }

    }
}
