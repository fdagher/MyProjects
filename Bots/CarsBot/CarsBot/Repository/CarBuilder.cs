using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarsBot.Repository
{
    public class CarBuilder
    {
        public static IList<Car> Build()
        {
            return new List<Car>
            {
                new Car { Make = CarType.Toyota, Body = BodyType.Wagon, Model = "Tarago", Engine = "4 cyl 2.4 Ltr", KilometersDriven = 63110, Price = 27900, Transmission = "Automatic", RegistrationNumber="ABC123", URL = "https://www.pickles.com.au/cars/item/-/details/CP-02-14--Built-01-14--Toyota--Tarago--ACR50R-MY13-GLX--Wagon--8-Seats--5-Doors/103453043" },
                new Car { Make = CarType.Toyota, Body = BodyType.Wagon, Model = "Tarago", Engine = "4 cyl 2.4 Ltr", KilometersDriven = 73036, Price = 31990, Transmission = "Automatic", RegistrationNumber="ABC123", URL = "https://www.pickles.com.au/cars/item/-/details/CP-10-14--Built-09-14--Toyota--Tarago--ACR50R-MY13-GLi--Wagon--8-Seats--5-Doors/103448936" },
                new Car { Make = CarType.Toyota, Body = BodyType.Wagon, Model = "Prius V", Engine = "4cyl 1.8L Petrol", KilometersDriven = 5768, Price = 34990, Transmission = "Automatic", RegistrationNumber="ABC123", URL = "https://www.pickles.com.au/cars/item/-/details/CP-01-11--Built-12-10--Toyota--Tarago--GSR50R-MY09-GLX--Wagon--8-Seats--5-Doors/103452628"},
                new Car { Make = CarType.Toyota, Body = BodyType.Hatch, Model = "Corolla", Engine = "4 cyl 1.8 Ltr", KilometersDriven = 31889, Price = 17493, Transmission = "Automatic", RegistrationNumber="ABC123", URL = "https://www.pickles.com.au/cars/item/-/details/CP-01-16--Built-01-16--Toyota--Corolla--ZRE182R-Ascent-S-CVT--Hatchback--5-Seats--5-Doors/402287710"},
                new Car { Make = CarType.Nissan, Body = BodyType.Sedan, Model = "Altima", Engine = "4 cyl 2.5 Ltr", KilometersDriven = 49619, Price = 14500, Transmission = "Automatic", RegistrationNumber="ABC123", URL = "https://www.pickles.com.au/cars/item/-/details/CP-05-15--Built-04-15--Nissan--Altima--L33-ST-X-tronic--Sedan--5-Seats--4-Doors/452240982"},
                new Car { Make = CarType.Honda, Body = BodyType.Wagon, Model = "Odyssey", Engine = "4 cyl 2.4 Ltr", KilometersDriven = 156719, Price = 30000, Transmission = "Automatic", RegistrationNumber="ABC123", URL="https://www.pickles.com.au/cars/item/-/details/CP-05-13--Built-03-13--Honda--Odyssey--4th-Gen-MY13-Luxury--Wagon--8-Seats--5-Doors/2402187727"},
                new Car { Make = CarType.Honda, Body = BodyType.Wagon, Model = "Odyssey", Engine = "6cyl 2.4L Turbo Petrol", KilometersDriven = 107770, Price = 19000, Transmission = "Manual", RegistrationNumber="ABC123", URL="https://www.pickles.com.au/cars/item/-/details/CP-05-13--Built-03-13--Honda--Odyssey--4th-Gen-MY13-Luxury--Wagon--8-Seats--5-Doors/2402187727"},
                new Car { Make = CarType.Honda, Body = BodyType.SUV, Model = "CR-V", Engine = "4 cyl 2 Ltr", KilometersDriven = 49750, Price = 25999, Transmission = "Automatic", RegistrationNumber="ABC123", URL="https://www.pickles.com.au/cars/item/-/details/CP-03-14--Honda--CR-V--RM-MY14-VTi-Navi--Wagon--5-Seats--5-Doors/1040097713"},
                new Car { Make = CarType.Mazda, Body = BodyType.Coupe, Model = "MX-5", Engine = "4 cyl 2 Ltr", KilometersDriven = 126602, Price = 14990, Transmission = "Automatic", RegistrationNumber="ABC123", URL="https://www.pickles.com.au/cars/item/-/details/CP-03-08--Built-03-08--Mazda--MX-5--NC30F1-MY07-Roadster-Coupe--Hardtop--2-Seats--2-Doors/452241871"},
                new Car { Make = CarType.Audi, Body = BodyType.Convertible, Model = "A3", Engine = "4 cyl 1.8 Ltr", KilometersDriven = 39434, Price = 30990, Transmission = "Automatic", RegistrationNumber="ABC123", URL = "https://www.pickles.com.au/cars/item/-/details/CP-04-14--Built-01-14--Audi--A3--8V-MY14-Ambition-S-tronic--Sedan--5-Seats--4-Doors/103450551"},
            };
        }
    }
}