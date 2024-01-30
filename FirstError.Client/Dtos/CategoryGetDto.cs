namespace FirstError.Client.Dtos
{
    public partial class CategoryGetDto
    {
        public string? Name { get; set; }    
        public string? Description { get; set; }

    }

    public class GetItems<T>
    {
        public List<T> Items  { get; set; }
       
    }
}
