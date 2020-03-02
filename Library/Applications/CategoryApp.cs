using System;
using System.Linq;
using Library.Core;
using Library.Services;
using northwind_master.Library.Models;



namespace Library.Applications
{
    public class CategoryApp
    {
        private CategoryService service = new CategoryService();

        public void ShowList(){
            foreach(var category in service.context.Categories)
            Console.WriteLine(category.CategoryId+ " " + category.CategoryName);
            return;
        }

        public void Show(){
            Console.WriteLine("Id: ");
            String id = Console.ReadLine();
            foreach(var category in service.context.Categories)
            if(category.CategoryId.ToString() == id){
            Console.WriteLine(category.CategoryId+ " " +category.CategoryName+ " " +category.Description);}
            return;
        }

        public void Add(){
            Console.WriteLine("Enter a name: ");
            String name = Console.ReadLine();
            Console.WriteLine("Enter a description: ");
            String description = Console.ReadLine();
            // add to db
            Categories newCategory = new Categories(){
                CategoryName = name,
                Description = description
            };
            service.context.Categories.Add(newCategory);
            service.context.SaveChanges();
            Console.WriteLine("added");
            return;
        }

        public void Update(){
            Console.WriteLine("Give id you want to update: ");
            String id = Console.ReadLine();
            int count = 0;
            foreach(var category in service.context.Categories)
            if(category.CategoryId.ToString() == id){
            Console.WriteLine(category.CategoryId+ " " + category.CategoryName);}
            else{count++;}
            if(count.Equals(service.context.Categories.Count())){Console.WriteLine("You can't write to that id as it doesn't exist!");return;}
            Console.WriteLine("Enter a name: ");
            String name = Console.ReadLine();
            Console.WriteLine("Enter a description: ");
            String description = Console.ReadLine();
            //update on db
            Categories updCategory = service.context.Categories
            .First(c => c.CategoryId.ToString() == id);
            updCategory.CategoryName = name;
            updCategory.Description = description;
            service.context.Update(updCategory);
            service.context.SaveChanges();
            Console.WriteLine("updated");
            return;
        }

        public void Delete(){
            Console.WriteLine("Give id you want to delete: ");
            String id = Console.ReadLine();
            int count = 0;
            foreach(var category in service.context.Categories)
            if(category.CategoryId.ToString() == id){
            Console.WriteLine(category.CategoryId+ " " + category.CategoryName);}
            else{count++;}
            if(count.Equals(service.context.Categories.Count())){Console.WriteLine("You can't delete that id as it doesn't exist!");return;}
            Console.WriteLine("Are you sure? (y/n)");
            String answer = Console.ReadLine();
            if(answer.Equals("y")){
                //delete from db
                Categories delCategory = service.context.Categories
                .FirstOrDefault(c => c.CategoryId.ToString() == id);
                if(delCategory != null){
                    service.context.Categories.Remove(delCategory);
                    service.context.SaveChanges();
                }
                Console.WriteLine("Deleted");
            }
            else{Console.WriteLine("Aborted");}
            return;
        }

        public static void ShowCommandList(){
            Console.WriteLine("category:list         show all categories");
            Console.WriteLine("category:add          add a new category");
            Console.WriteLine("category:show         show a new category");
            Console.WriteLine("category:update       update a category");
            Console.WriteLine("category:delete       delete acategory");
        }
    }
}