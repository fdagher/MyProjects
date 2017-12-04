using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarsBot.Repository
{
    public enum CarType
    {
        Any = 1,
        Honda = 2,
        Mazda = 3,
        Nissan = 4,
        Audi = 5,
        Toyota
    }

    public enum BodyType
    {
        Any = 1,
        Convertible = 2,
        Coupe = 3,
        Hatch = 4,
        Sedan = 5,
        SUV = 6,
        Wagon = 7
    }

    public class Car
    {
        public CarType Make;

        public string Model;

        public string Transmission;

        public string Engine;

        public BodyType Body;

        public int? Price;

        public int? KilometersDriven;

        public string URL;

        public string RegistrationNumber;

        public override string ToString()
        {
            //return "Make: " + Make.ToString() + ". Model: " + Model + ". Transmission: " + Transmission + ". Engine: " + Engine + ". Body: " + Body.ToString() + ". Price: " + Price + ". Kilometers: " + KilometersDriven;
            return "<tr><td>" + Make.ToString() + "</td><td>" + Model + "</td><td>" + Transmission + "</td><td>" + Engine + "</td><td>" + Body.ToString() + "</td><td>" + string.Format("{0:n0}", Price) + "</td><td>" + string.Format("{0:n0}", KilometersDriven) + "</td><td>" + string.Format("{0:n0}", RegistrationNumber) + "</td><td><a target='_blank' href='" + URL + "'>Details...</a></td>";
        }
    }
}