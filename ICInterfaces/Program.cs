using System;
using System.Collections.Generic;

public class Car : IComparable<Car>, ICloneable, IEquatable<Car>
{
    public string Name { get; set; }
    public int MaxMph { get; set; }
    public int Horsepower { get; set; }
    public decimal Price { get; set; }

    public int CompareTo(Car other)
    {
        if (other == null)
            return 1;

        return this.MaxMph.CompareTo(other.MaxMph);
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }

    public bool Equals(Car other)
    {
        if (other == null)
            return false;

        return this.Name == other.Name &&
               this.MaxMph == other.MaxMph &&
               this.Horsepower == other.Horsepower &&
               this.Price == other.Price;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        if (obj is Car car)
            return Equals(car);

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, MaxMph, Horsepower, Price);
    }

    public override string ToString()
    {
        return $"Name: {Name}, MaxMph: {MaxMph}, Horsepower: {Horsepower}, Price: {Price:C}";
    }
}

public class CarComparer : IComparer<Car>
{
    public int Compare(Car x, Car y)
    {
        if (x == null && y == null)
            return 0;
        if (x == null)
            return -1;
        if (y == null)
            return 1;

        int priceComparison = x.Price.CompareTo(y.Price);
        if (priceComparison != 0)
            return priceComparison;

        return x.Horsepower.CompareTo(y.Horsepower);
    }
}

class Program
{
    static void Main()
    {
        Car car1 = new Car { Name = "Car1", MaxMph = 200, Horsepower = 400, Price = 50000 };
        Car car2 = new Car { Name = "Car2", MaxMph = 150, Horsepower = 300, Price = 30000 };
        Car car3 = new Car { Name = "Car3", MaxMph = 200, Horsepower = 400, Price = 50000 };

        Console.WriteLine(car1.CompareTo(car2));
        Console.WriteLine(car1.CompareTo(car3));

        Car carClone = (Car)car1.Clone();
        Console.WriteLine(carClone.ToString());

        Console.WriteLine(car1.Equals(car2));
        Console.WriteLine(car1.Equals(car3));

        List<Car> cars = new List<Car> { car1, car2, car3 };
        cars.Sort(new CarComparer());

        foreach (var car in cars)
        {
            Console.WriteLine(car);
        }
    }
}
