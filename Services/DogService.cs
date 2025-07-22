using dog.Models;

namespace dog.Services
{
    public class DogService
    {
        private List<Dog> context = new List<Dog>
        {
            new Dog { Id = 1, Name = "Rex", Breed = "Dachshund", Age = 3, Weight = 7.5f, IsVaccinated = true },
            new Dog { Id = 2, Name = "Max", Breed = "German Spitz", Age = 2, Weight = 6.2f, IsVaccinated = true },
            new Dog { Id = 3, Name = "Luna", Breed = "Labrador", Age = 5, Weight = 25.0f, IsVaccinated = true },
            new Dog { Id = 4, Name = "Bruno", Breed = "Pitbull", Age = 4, Weight = 22.3f, IsVaccinated = false },
            new Dog { Id = 5, Name = "Bella", Breed = "Corgi", Age = 1, Weight = 10.8f, IsVaccinated = true },
            new Dog { Id = 6, Name = "Milo", Breed = "Shiba Inu", Age = 3, Weight = 11.4f, IsVaccinated = true },
            new Dog { Id = 7, Name = "Rocky", Breed = "Bulldog", Age = 6, Weight = 20.0f, IsVaccinated = false }
        };

        public DogService() { }

        public List<Dog> GetAll()
        {
            return context;
        }

        public Dog GetById(int id)
        {
            return context.FirstOrDefault(d => d.Id == id);
        }

        public void Add(Dog dog)
        {
            int nextId = context.Any() ? context.Max(d => d.Id) + 1 : 1;
            dog.Id = nextId;
            context.Add(dog);
        }

        public bool Delete(int id)
        {
            var dog = GetById(id);
            if (dog != null)
            {
                context.Remove(dog);
                return true;
            }
            return false;
        }


        public List<Dog> FindByName(string name)
        {
            return context
                .Where(d => d.Name != null && d.Name.ToLower().Contains(name.ToLower()))
                .ToList();
        }


        public bool Update(int id, Dog updatedDog)
        {
            var dog = GetById(id);
            if (dog == null)
                return false;


            dog.Name = updatedDog.Name;
            dog.Breed = updatedDog.Breed;
            dog.Age = updatedDog.Age;
            dog.Weight = updatedDog.Weight;
            dog.IsVaccinated = updatedDog.IsVaccinated;

            return true;
        }

        public Dog? GetRandom()
        {
            if (!context.Any())
                return null;

            var random = new Random();
            int index = random.Next(context.Count);//de la minim la maxim
            return context[index];
        }

    }
}
