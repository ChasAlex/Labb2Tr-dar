namespace Labb2Trådar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Startar racet och kör igång threadsen
            Console.WriteLine("Welcome to the Amazing race! 10km race!");
            Console.ReadLine(); 

            Car car1 = new Car("Racecar 1", 1);
            Car car2 = new Car("Racecar 2", 2);
            

            Thread car1Thread = new Thread(car1.Race);
            Thread car2Thread = new Thread(car2.Race);
            


            car1Thread.Start();
            car2Thread.Start();
            

            car1Thread.Join();
            car2Thread.Join();

            Console.Clear();
            // Hämtar winnerId som vann racet och skriver ut det
            Console.WriteLine($"The winner is Racecar {Car.winnerId}");
            Console.ReadLine();
              

        }



    }
}