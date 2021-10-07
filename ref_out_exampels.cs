using System;

namespace ConsoleApp3
{
    public class ExampleFactoryExample
    {
        public void Action()
        {
            var obj = new Example { Name = "A", Value = 1 };
            ShowObj(obj);
            Foo1(obj);

            ShowObj(obj);
            Foo2(ref obj);

            ShowObj(obj);
            Foo3(ref obj);

            ShowObj(obj);
            Foo4(obj);

            ShowObj(obj);
            Modify(obj.Name);

            ShowObj(obj);
            Modify(obj);

            ShowObj(obj);

        }

        public void Foo1(Example example)
        {
            example = new Example { Name = "B", Value = 2 };

            example.Name = "Y";
            example.Value = 444;
        }

        public void Foo2(ref Example example)
        {
            example = new Example { Name = "Z", Value = 3 };
        }

        public void Foo3(ref Example example)
        {
            example.Name = "C";
            example.Value = 4;
        }

        public void Foo4(Example example)
        {
            example.Name = "D";
            example.Value = 5;
        }

        public void Modify(string str)
        {
            str = "E";
        }

        public void Modify(Example obj)
        {
            obj.Name = "F";
        }

        public void ShowObj<T>(T obj) where T : Example
        {
            Console.WriteLine($"Name: {obj.Name}, Value: {obj.Value}");
        }
    }
    public class Example
    {
        public int Value { get; set; }

        public string Name { get; set; }
    }
}
