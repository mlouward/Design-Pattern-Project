using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Exercise2
{
    public class Node<T, TRes>
    {
        public Func<T, TRes> Function { get; set; }
        public float[,] Dataframe { get; set; }
        public Node() { }
        public double ExecuteFunc()
        {
            int nbLign = Dataframe.GetLength(0);
            int nbCols = Dataframe.GetLength(1);
            double[] res = new double[nbCols];
            Parallel.For(0, nbCols, i =>
            {
                Function(Dataframe);
            });
            return res;
        }
    }
}
