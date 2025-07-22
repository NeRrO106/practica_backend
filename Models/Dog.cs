namespace dog.Models
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; } // in years
        public float Weight { get; set; } // in kilograms
        public bool IsVaccinated { get; set; }
    }
}
