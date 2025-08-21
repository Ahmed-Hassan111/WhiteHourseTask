namespace task.Models
{
    public class FooterInfo
    {
        public int Id { get; set; }
        public string LogoUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        // Social Links
        public string Facebook { get; set; } = string.Empty;
        public string Twitter { get; set; } = string.Empty;
        public string Instagram { get; set; } = string.Empty;
        public string Linkedin { get; set; } = string.Empty;

        public string GoogleMapEmbedUrl { get; set; } = string.Empty;
    }

}
