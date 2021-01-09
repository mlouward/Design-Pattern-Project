namespace Exercise2
{
    public class NodeController
    {
        public static void Send(object data, Node to)
        {
            if (data as string == "start")
            {
                to.ReadyToReduce = false;
                return;
            }
            else if (data as string == "end")
            {
                to.ReadyToReduce = true;
                return;
            }
            to.Data.Add(data);
        }
    }
}