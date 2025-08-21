namespace task.Models
{
    public class CounterItem
    {
        public int Id { get; set; }
        public string IconUrl { get; set; } = string.Empty;
        public int Number { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool HasPlus { get; set; } = false;
        public bool HasPercentage { get; set; } = false;
    }

}
