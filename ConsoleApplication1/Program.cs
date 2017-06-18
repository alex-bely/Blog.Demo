using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            using (EntityModel db = new EntityModel())
            {
                // создаем два объекта User
                Role r = new Role { Name = "gh" };

                // добавляем их в бд
                db.Roles.Add(r);
                
                db.SaveChanges();
                Console.WriteLine("Объекты успешно сохранены");

                // получаем объекты из бд и выводим на консоль
                var users = db.Roles;
                Console.WriteLine("Список объектов:");
                foreach (var u in users)
                {
                    Console.WriteLine(u.Id+" "+u.Name);
                }
            }


           
        }
    }
}
