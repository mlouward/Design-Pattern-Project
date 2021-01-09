namespace Exercise1
{
    public class Node<T>
    {
        public T Content { get; set; }
        public Node<T> Next { get; set; }

        public Node(T content)
        {
            Content = content;
            Next = null;
        }

        public Node(T content, Node<T> next)
        {
            Content = content;
            Next = next;
        }

        public override string ToString()
        {
            return Content.ToString() + " -> ";
        }
    }
}