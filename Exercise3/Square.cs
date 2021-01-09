namespace Exercise3
{
    public class Square
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public Square Next { get; set; }

        public Square()
        {
        }

        public Square(int index)
        {
            Index = index;
        }

        public override string ToString()
        {
            return Index.ToString();
        }
    }
}