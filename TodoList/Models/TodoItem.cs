using System.ComponentModel.DataAnnotations;
namespace TodoList.Models
{
    public class TodoItem
    {
        //[Key]
        //public int Id { get; set; }
        [Required]
        public string Item { get; set; }
        public bool IsDone { get; set; } = false;
    }
}
