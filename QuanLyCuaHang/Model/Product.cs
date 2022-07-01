using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHang.Model
{
    public class Product
    {
        private string name;
        private double value;
        private string image;

        public string Name { get => name; set => name = value; }
        public double Value { get => value; set => this.value = value; }
        public string Image { get => image; set => image = value; }

        public Product(string name, double value, string image)
        {
            Name = name;
            Value = value;
            Image = image;
        }

    }
}
