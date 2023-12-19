using Domain.Models;
using Domain.Models.AnimalUser;
using Domain.Models.User;

namespace Infrastructure.Database
{
    public class MockDatabase
    {
        public List<Dog> Dogs
        {
            get { return allDogs; }
            set { allDogs = value; }
        }

        public List<Cat> Cats
        {
            get { return allCats; }
            set { allCats = value; }
        }

        public List<Bird> Birds
        {
            get { return allBirds; }
            set { allBirds = value; }
        }


        private static List<Dog> allDogs = new()
        {
            new Dog { Id = Guid.NewGuid(), Name = "Björn"},
            new Dog { Id = Guid.NewGuid(), Name = "Patrik"},
            new Dog { Id = Guid.NewGuid(), Name = "Alfred"},
            new Dog { Id = Guid.NewGuid(), Name = "Greta"},
            new Dog { Id = new Guid("12345678-1234-5678-1234-567812345678"), Name = "TestDogForUnitTests"}
        };

        private static List<Cat> allCats = new()
        {
            new Cat { Id = Guid.NewGuid(), Name = "Emma", LikesToPlay = false},
            new Cat { Id = Guid.NewGuid(), Name = "Ella", LikesToPlay = false},
            new Cat { Id = Guid.NewGuid(), Name = "Emilia", LikesToPlay = true},
            new Cat { Id = new Guid("12345678-1234-5678-1234-567812345678"), Name = "TestCatForUnitTests"}
        };

        private static List<Bird> allBirds = new()
        {
            new Bird { Id = Guid.NewGuid(), Name = "Roger", CanFly = true},
            new Bird { Id = Guid.NewGuid(), Name = "Alex", CanFly = false},
            new Bird { Id = Guid.NewGuid(), Name = "Carola", CanFly= false},
            new Bird { Id = new Guid("59d8fc74-3c94-4ed8-9a38-36b0b6b1074a"), Name = "TestBirdForUnitTests"}
        };
        public List<UserModel> Users
        {
            get { return allUsers; }
            set { allUsers = value; }
        }

        private static List<UserModel> allUsers = new()
        {
            new UserModel { Id = Guid.NewGuid(), UserName = "Erik" },
            new UserModel { Id = Guid.NewGuid(), UserName = "Sven" },
            new UserModel { Id = Guid.NewGuid(), UserName = "Emma" },
        };

    }
}
