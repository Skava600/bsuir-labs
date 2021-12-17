using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace DiningPhilosophers
{
    class Program
    {
        private const int PHILOSOPHER_COUNT = 5;
        public static List<Philosopher> _allPhilosophers = new List<Philosopher>();
        static void Main(string[] args)
        {
            // Construct philosophers and chopsticks
           InitializePhilosophers();

            // Start dinner
            Console.WriteLine("Dinner is starting!");

            // Spawn threads for each philosopher's eating cycle
            var philosopherThreads = new List<Thread>();
            foreach (var philosopher in _allPhilosophers)
            {
                var philosopherThread = new Thread(new ThreadStart(philosopher.EatAll));
                philosopherThreads.Add(philosopherThread);
                philosopherThread.Start();
            }

            // Wait for all philosopher's to finish eating
            foreach (var thread in philosopherThreads)
            {
                thread.Join();
            }

            // Done
            Console.WriteLine("Dinner is over!");
        }

        private static List<Philosopher> InitializePhilosophers()
        {
            // Construct philosophers
            var philosophers = new List<Philosopher>(PHILOSOPHER_COUNT);
            for (int i = 0; i < PHILOSOPHER_COUNT; i++)
            {
                philosophers.Add(new Philosopher(i));
            }
            _allPhilosophers = philosophers;
            // Assign chopsticks to each philosopher
            foreach (var philosopher in philosophers)
            {
                // Assign left chopstick
                philosopher.LeftChopstick = philosopher.LeftPhilosopher.RightChopstick ?? new Chopstick();

                // Assign right chopstick
                philosopher.RightChopstick = philosopher.RightPhilosopher.LeftChopstick ?? new Chopstick();
            }

            return philosophers;
        }
    }

    [DebuggerDisplay("Name = {Name}")]
    public class Philosopher
    {
        private const int TIMES_TO_EAT = 5;
        private const int TIMEOUT = 500;
        private int _timesEaten = 0;
        private readonly int _index;

        public Philosopher(int index)
        {
            _index = index;
            this.Name = string.Format("Philosopher {0}", _index);
            this.State = State.Thinking;
        }

        public string Name { get; private set; }
        public State State { get; private set; }
        public Chopstick LeftChopstick { get; set; }
        public Chopstick RightChopstick { get; set; }

        public Philosopher LeftPhilosopher
        {
            get
            {
                if (_index == 0)
                    return Program._allPhilosophers[^1];
                else
                    return Program._allPhilosophers[_index - 1];
            }
        }

        public Philosopher RightPhilosopher
        {
            get
            {
                if (_index == Program._allPhilosophers.Count - 1)
                    return Program._allPhilosophers[0];
                else
                    return Program._allPhilosophers[_index + 1];
            }
        }

        public void EatAll()
        {
            // Cycle through thinking and eating until done eating.
            while (_timesEaten < TIMES_TO_EAT)
            {
                this.Think();
                if (this.PickUp())
                {
                    // Chopsticks acquired, eat up
                    this.Eat();

                    // Release chopsticks
                    this.PutDownLeft();
                    this.PutDownRight();
                }
            }
        }

        private bool PickUp()
        {
            // Try to pick up the left chopstick
            if (Monitor.TryEnter(this.LeftChopstick, TIMEOUT))
            {
                Console.WriteLine($"{this.Name} picks up left chopstick.");

                // Now try to pick up the right
                if (Monitor.TryEnter(this.RightChopstick, TIMEOUT))
                {
                    Console.WriteLine($"{this.Name} picks up right chopstick.");

                    // Both chopsticks acquired, its now time to eat
                    return true;
                }
                else
                {
                    // Could not get the right chopstick, so put down the left
                    this.PutDownLeft();
                }
            }

            // Could not acquire chopsticks, try again
            return false;
        }

        private void Eat()
        {
            this.State = State.Eating;
            _timesEaten++;
            Console.WriteLine($"{this.Name} eats for the {_timesEaten} time" );
        }

        private void PutDownLeft()
        {
            Monitor.Exit(this.LeftChopstick);
            Console.WriteLine($"{this.Name} puts down left chopstick.");
        }

        private void PutDownRight()
        {
            Monitor.Exit(this.RightChopstick);
            Console.WriteLine($"{this.Name} puts down right chopstick.");
        }


        private void Think()
        {
            this.State = State.Thinking;
            Console.WriteLine($"{this.Name} thinking");
            Thread.Sleep(2000);
        }
    }

    public enum State
    {
        Thinking = 0,
        Eating = 1
    }

    [DebuggerDisplay("Name = {Name}")]
    public class Chopstick
    {

    }
}