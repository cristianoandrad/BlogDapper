using System;
using BlogDapper.Models;
using BlogDapper.Repositories;
using BlogDapper.Screens.TagScreens;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace BlogDapper
{
    class Program
    {
        private const string connectionString = "Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            Database.Connection = new SqlConnection(connectionString);

            Database.Connection.Open();

            // ReadUser();
            // CreateUser();
            // UpdateUser();
            // DeleteUser();
            // ReadUsers(connection);
            // ReadRoles(connection);
            // ReadTags(connection);
            // CreateUsers(connection);
            // ReadUsersWithRoles(connection);
            Load();

            Console.ReadKey();

            Database.Connection.Close();

        }

        private static void Load()
        {
            Console.Clear();
            Console.WriteLine("Meu Blog");
            Console.WriteLine("-----------------");
            Console.WriteLine("O que deseja fazer?");
            Console.WriteLine();
            Console.WriteLine("1 - Gestão de usuário");
            Console.WriteLine("2 - Gestão de perfil");
            Console.WriteLine("3 - Gestão de categoria");
            Console.WriteLine("4 - Gestão de tag");
            Console.WriteLine("5 - Vincular perfil/usuário");
            Console.WriteLine("6 - Vincular post/tag");
            Console.WriteLine("7 - Relatórios");
            Console.WriteLine();
            Console.WriteLine();
            var option = short.Parse(Console.ReadLine()!);

            switch (option)
            {
                case 4:
                    MenuTagScreen.Load();
                    break;
                default: Load(); break;
            }
        }

        public static void ReadUsers(SqlConnection connection)
        {
            var repository = new Repository<User>(connection);
            var items = repository.Get();

            foreach (var item in items)
            {
                Console.WriteLine(item.Name);

                foreach (var role in item.Roles)
                {
                    System.Console.WriteLine($" - {role.Name}");
                }
            }
        }

        public static void ReadUsersWithRoles(SqlConnection connection)
        {
            var repository = new UserRepository(connection);
            var items = repository.GetWithRoles();

            foreach (var item in items)
            {
                Console.WriteLine(item.Name);

                foreach (var role in item.Roles)
                {
                    Console.WriteLine($" - {role.Name}");
                }
            }
        }

        public static void CreateUsers(SqlConnection connection)
        {
            var repository = new Repository<User>(connection);

            var user = new User()
            {
                Bio = "Bio",
                Email = "equipe@balta.com",
                Image = "https://...",
                Name = "name",
                PasswordHash = "HASH",
                Slug = "slug",
            };

            repository.Create(user);
        }

        public static void ReadRoles(SqlConnection connection)
        {
            var repository = new Repository<Role>(connection);
            var items = repository.Get();

            foreach (var item in items)
                Console.WriteLine(item.Name);
        }

        public static void ReadTags(SqlConnection connection)
        {
            var repository = new Repository<Tag>(connection);
            var items = repository.Get();

            foreach (var item in items)
                Console.WriteLine(item.Name);
        }

        public static void ReadUser()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var user = connection.Get<User>(1);

                Console.WriteLine(user.Name);
            }
        }

        // public static void CreateUser()
        // {
        //     var user = new User()
        //     {
        //         Bio = "Equipe Balta",
        //         Email = "equipe@balta.com",
        //         Image = "https://...",
        //         Name = "Equipe Balta",
        //         PasswordHash = "HASH",
        //         Slug = "equipe-balta",
        //     };

        //     using (var connection = new SqlConnection(connectionString))
        //     {
        //         connection.Insert<User>(user);

        //         Console.WriteLine("Cadastro realizado com sucesso");
        //     }
        // }

        // public static void UpdateUser()
        // {
        //     var user = new User()
        //     {
        //         Id = 2,
        //         Bio = "Equipe | Balta",
        //         Email = "equipe@balta.com",
        //         Image = "https://...",
        //         Name = "Equipe de suporte Balta",
        //         PasswordHash = "HASH",
        //         Slug = "equipe-balta",
        //     };

        //     using (var connection = new SqlConnection(connectionString))
        //     {
        //         connection.Update<User>(user);

        //         Console.WriteLine("Atualização realizada com sucesso");
        //     }
        // }

        // public static void DeleteUser()
        // {
        //     using (var connection = new SqlConnection(connectionString))
        //     {
        //         var user = connection.Get<User>(2);

        //         connection.Delete<User>(user);

        //         Console.WriteLine("Exclusão realizada com sucesso");
        //     }
        // }

    }
}
