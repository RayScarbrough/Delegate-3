using System;

namespace delegatesAndEvents
{
 
    public delegate void RaceDelegate(int winner);

    public class Race
    {
       
        public event RaceDelegate RaceFinished;

        public void Racing(int contestants, int laps)
        {
            Console.WriteLine("Ready\nSet\nGo!");
            Random r = new Random();
            int[] participants = new int[contestants];
            bool done = false;
            int champ = -1;

           
            while (!done)
            {
                for (int i = 0; i < contestants; i++)
                {
                    if (participants[i] < laps)
                    {
                        participants[i] += r.Next(1, 5);
                    }
                    else if (champ == -1)
                    {
                        champ = i + 1; 
                        done = true;
                    }
                }
            }

            TheWinner(champ);
        }

        private void TheWinner(int champ)
        {
            Console.WriteLine("We have a winner!");
           
            RaceFinished?.Invoke(champ);
        }
    }

    class Program
    {
        public static void Main()
        {
         
            Race round1 = new Race();

          
            round1.RaceFinished += footRace;

           
            Console.WriteLine("Foot Race:");
            round1.Racing(5, 10);

           
            round1.RaceFinished -= footRace; 
            round1.RaceFinished += carRace;

      
            Console.WriteLine("\nCar Race:");
            round1.Racing(5, 10);

          
            round1.RaceFinished -= carRace; 
            round1.RaceFinished += winner => Console.WriteLine($"Bike number {winner} is the winner.");

       
            Console.WriteLine("\nBike Race:");
            round1.Racing(5, 10);
        }

       
        public static void carRace(int winner)
        {
            Console.WriteLine($"Car number {winner} is the winner.");
        }

        public static void footRace(int winner)
        {
            Console.WriteLine($"Racer number {winner} is the winner.");
        }
    }
}
