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
           InitializePhilosophers();

            Console.WriteLine("Dinner is starting!");


            var philosopherThreads = new List<Thread>();
            foreach (var philosopher in _allPhilosophers)
            {
                var philosopherThread = new Thread(new ThreadStart(philosopher.EatAll));
                philosopherThreads.Add(philosopherThread);
                philosopherThread.Start();
            }

            foreach (var thread in philosopherThreads)
            {
                thread.Join();
            }

            Console.WriteLine("Dinner is over!");
        }

        private static List<Philosopher> InitializePhilosophers()
        {
            var philosophers = new List<Philosopher>(PHILOSOPHER_COUNT);
            for (int i = 0; i < PHILOSOPHER_COUNT; i++)
            {
                philosophers.Add(new Philosopher(i));
            }
            _allPhilosophers = philosophers;
            foreach (var philosopher in philosophers)
            {
                philosopher.LeftChopstick = philosopher.LeftPhilosopher.RightChopstick ?? new Chopstick();

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
            while (_timesEaten < TIMES_TO_EAT)
            {
                this.Think();
                if (this.PickUp())
                {
                    this.Eat();

                    this.PutDownLeft();
                    this.PutDownRight();
                }
            }
        }

        private bool PickUp()
        {
            if (Monitor.TryEnter(this.LeftChopstick, TIMEOUT))
            {
                Console.WriteLine($"{this.Name} picks up left chopstick.");

                if (Monitor.TryEnter(this.RightChopstick, TIMEOUT))
                {
                    Console.WriteLine($"{this.Name} picks up right chopstick.");

                    return true;
                }
                else
                {
                    this.PutDownLeft();
                }
            }

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