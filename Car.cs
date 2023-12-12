using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Labb2Trådar
{
    internal class Car
    {
        private static readonly object lockObject = new object();
        public string _name { get; set; }
        public int _speed { get; set; }
        public static int winnerId = 0;
        public int _carId { get; set; }
        
       
        // Konstruktör för Bilarna, Sätter grund hastigheten på 120km/h = 33.33 m/s
        public Car(string name, int CarId)
        {
            _carId = CarId;
            _name = name;
            _speed = 33;
            

        }


        // Race method som varje thread och c
        public void Race()
        {
            // Variabler för olika distanser samt distans mellan att "händelser" kommer att ske
            int totalDistance = 10000;
            int eventIntervalInSeconds = 30;
            int eventIntervalInDistance = _speed * eventIntervalInSeconds;
            int distance = 0;

            // Skriver ut distansen och fart samt om distance traveled är jämnt delbart med distansen mellan event, Då har det gått 30 sekunder och ett event ska ske
            while (distance < totalDistance)
            {
                CarOngoing(distance, _speed, _carId);
                distance += _speed;

                if (distance % eventIntervalInDistance == 0)
                {
                    
                   CarProblem();
                    
                }

                Thread.Sleep(1000);
            }

            // Tillhandahåller ett winnerId till bilen som tar sig ut ur while loopen first
            lock (lockObject)
            {
                if (winnerId == 0)
                {
                    
                    winnerId = _carId;
                    
                }
            }
        }

    
        // Skapar en händelse för en bil och skriver ut det på en egen line
        // Efter att händelse har skett och skrivits ut clearas strängen så att det inte står kvar
        public void CarProblem()
        {
            Random random_problem = new Random();
            Console.SetCursorPosition(0, (5+_carId));
            int random_int = random_problem.Next(1, 101);

            if (random_int >= 1 && random_int <= 2)
            {
                Console.Write($"{_name}: Running out of fuel!");
                Thread.Sleep(30000);
                ClearLine(5 + _carId);
                
            }
            else if(random_int > 2 &&  random_int <= 6) 
            {
                Console.Write($"{_name}: Puncure!");
                Thread.Sleep(20000);
                ClearLine(5 + _carId);

            }
            else if(random_int > 6 && random_int <= 16)
            {
                Console.Write($"{_name}: Bird in the window!");
                Thread.Sleep(10000);
                ClearLine(5 + _carId);

            }
            else if(random_int > 16 && random_int <= 36)
            {
                Console.Write($"{_name}: Engine problems!");
                Thread.Sleep(1000);
                _speed -= 1;
                ClearLine(5 + _carId);

            }
            



        }

        // Clearar strängen genom att ersätta den med en sträng med mellanslag lika lång som console width
        private void ClearLine(int lineNumber)
        {
            Console.SetCursorPosition(0, lineNumber);
            Console.Write(new string(' ', Console.WindowWidth));
            
        }



        // Uppdaterar Bilen distans och fart, genom att byta mellan tom sträng och ny information om bilen
        public void CarOngoing( int distance, int speed, int carId)
        {
            lock (lockObject)
            {
                Console.SetCursorPosition(0, carId);

                
                Console.Write(new string(' ', Console.WindowWidth - 1));

                
                Console.SetCursorPosition(0, carId);

                Console.WriteLine($"{_name} Racecar has traveled: {distance}m " +
                    $"Current speed: {speed}m/s");
                Thread.Sleep(10);

                
            }
        }








    }
}
