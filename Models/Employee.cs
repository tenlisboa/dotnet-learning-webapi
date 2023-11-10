using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnApi.Models;

[Table("employees")]
public class Employee
{
    [Key]
    [Column("id")]
    public int Id { get; private set; }

    [Column("name")]
    public string Name { get; private set; }

    [Column("age")]
    public int Age { get; private set; }

    [Column("photo")]
    public string? Photo { get; private set; }

    public Employee(string name, int age, string? photo)
    {
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.Age = age;
        this.Photo = photo;
    }
}