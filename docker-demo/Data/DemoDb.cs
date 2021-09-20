using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace docker_demo.Data
{
    public class DemoDb : DbContext
    {
        public DemoDb(DbContextOptions<DemoDb> options)
            : base(options)
        {
        }
        public static void Initialize(DemoDb context)
        {
            context.Database.EnsureCreated();
            if (context.Students.Any())
            {
                return;
            }
            var students = new Student[]
            {
                new Student{Name="张三", Sex="男", Birthday=new DateTime(2000,8,15)},
                new Student{Name="李四", Sex="女", Birthday=new DateTime(2001,7,5)},
                new Student{Name="王五", Sex="男", Birthday=new DateTime(2000,4,8)},
                new Student{Name="赵六", Sex="女", Birthday=new DateTime(2001,12,29)}
            };
            context.Students.AddRange(students);
            context.SaveChanges();
        }
        public DbSet<Student> Students { get; set; }
    }
}
