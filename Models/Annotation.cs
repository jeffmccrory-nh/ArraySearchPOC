namespace ArraySearchPOC.Models
{
    public class Annotation
    {
        public int PageId { get; set; } = 0;
        public Box Box { get; set; } = new();
    }

    public class Box
    {
        public int Left { get; set; } = 0;
        public int Top { get; set; } = 0;
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;

    }
}
