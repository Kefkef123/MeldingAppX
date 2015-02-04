namespace MeldingAppX.Models
{
    public class Photo
    {
        public int id { get; set; }
        public string Name { get; set; }    
        public string EncodedFile { get; set; }
        public string ContentType { get; set; }
        public int ContentLength { get; set; }
        public string Url { get; set; }
        public string FileName { get; set; }
    }
}
