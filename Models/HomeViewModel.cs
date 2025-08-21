namespace task.Models
{
    public class HomeViewModel
    {
        public List<SliderItem> Sliders { get; set; } = new List<SliderItem>();
        public AboutSection About { get; set; } = new AboutSection();
        public List<CounterItem> Counters { get; set; } = new List<CounterItem>(); // غيرت لـ List<CounterItem>
        public List<ServiceItem> Services { get; set; } = new List<ServiceItem>();
        public List<Client> Clients { get; set; } = new List<Client>();
        public FooterInfo Footer { get; set; } = new FooterInfo();
    }
}
