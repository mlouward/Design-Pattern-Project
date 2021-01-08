using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Exercise2
{
    public class Node : INotifyPropertyChanged
    {
        // True when NodeController is finished sending information
        private bool readyToReduce;

        public bool ReadyToReduce
        {
            get => readyToReduce;
            set => SetField(ref readyToReduce, value);
        }

        public List<object> Data { get; set; } = new List<object>();

        public Node()
        { }

        public List<Tuple<string, int>> Map(string arg)
        {
            string[] input = arg.Split(" ");
            return input.Select(x => new Tuple<string, int>(x, 1)).ToList();
        }

        public Tuple<string, int> Reduce(List<Tuple<string, int>> mappedInput)
        {
            Dictionary<string, int> reducedList = new Dictionary<string, int>();
            Tuple<string, List<int>> intermediaryTuple = new Tuple<string, List<int>>(
                mappedInput.First().Item1, new List<int>());

            intermediaryTuple.Item2.AddRange(mappedInput.Select(_ => 1));

            Tuple<string, int> reduceResult = new Tuple<string, int>(
                intermediaryTuple.Item1, intermediaryTuple.Item2.Sum());

            Console.WriteLine(reduceResult.Item1 + " " + reduceResult.Item2);
            return reduceResult;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (readyToReduce)
                Reduce(Data.Cast<Tuple<string, int>>().ToList());
            else
                Console.WriteLine("not ready");
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}