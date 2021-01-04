using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise3
{
    public class Square
    {
        public string Name { get; set; }
        public Square Next { get; set; }
        public Square() { }
        public Square(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
