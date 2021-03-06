﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Exercise2
{
    public class Node
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
            string[] input = arg.Split(' ');
            return input.Select(x => new Tuple<string, int>(x, 1)).ToList();
        }

        public Tuple<string, int> Reduce(List<Tuple<string, int>> mappedOutput)
        {
            Dictionary<string, int> reducedList = new Dictionary<string, int>();

            Tuple<string, List<int>> intermediaryTuple = new Tuple<string, List<int>>(
                mappedOutput.First().Item1, new List<int>());

            //Concatenate the 1s of intermediaryTuple into a list of ones [<x, 1>, <y, 1>, ... => (1, 1, ...)]
            intermediaryTuple.Item2.AddRange(mappedOutput.Select(_ => 1));

            Tuple<string, int> reduceResult = new Tuple<string, int>(
                intermediaryTuple.Item1, intermediaryTuple.Item2.Sum());

            Console.WriteLine(reduceResult.Item1 + " " + reduceResult.Item2);
            return reduceResult;
        }

        /// <summary>
        /// triggers the reduce procedure only when readyToReduce is set to True
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (readyToReduce)
                Reduce(Data.Cast<Tuple<string, int>>().ToList());
        }

        /// <summary>
        /// Calls OnPropertyChanged every time readyToReduce changes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
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