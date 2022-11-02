namespace TripAdvisor.Data
{
    public class FoodDto
    {
        public Food? Food { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return $"{Food!.Name} - {Count}шт.";
        }
    }

}
