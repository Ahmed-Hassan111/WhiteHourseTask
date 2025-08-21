namespace task.Models
{
    public class SliderItem
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string SubTitle { get; set; } = string.Empty;
        public string Orientation { get; set; } = "horizontal";
        public int Slice1Rotation { get; set; } = 0;
        public int Slice2Rotation { get; set; } = 0;
        public decimal Slice1Scale { get; set; } = 1.0m;
        public decimal Slice2Scale { get; set; } = 1.0m;
    }

}
