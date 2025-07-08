namespace FreeTube.Dtos
{
    public class NewRentalDto
    {
        public int CustomerId { get; set; }
        public List<int>? MovieIdes { get; set; }
    }
}
