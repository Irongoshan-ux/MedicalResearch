namespace UserManaging.Domain.Entities.Images
{
    public class Image
    {
        public Guid? Id { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
    }
}
