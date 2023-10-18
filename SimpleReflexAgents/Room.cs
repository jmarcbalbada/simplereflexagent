using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleReflexAgents
{
    class Room
    {
        private int location;
        private int status;

        public Room()
        {
            location = 0;
            status = 0;
        }

        public Room(int location)
        {
            this.location = location;
            status = 0;
        }

        public int getLocation()
        {
            return this.location;
        }

        public int getStatus()
        {
            return this.status;
        }

        public void setLocation(int nameSet)
        {
            this.location = nameSet;
        }

        public void setStatus(int statusSet)
        {
            this.status = statusSet;
        }

        public void printLocation()
        {
            switch (this.location)
            {
                case 0:
                    Console.WriteLine("Current Location: A");
                    break;
                case 1:
                    Console.WriteLine("Current Location: B");
                    break;
                case 2:
                    Console.WriteLine("Current Location: C");
                    break;
                case 3:
                    Console.WriteLine("Current Location: D");
                    break;
            }
        }

        public void printStatus()
        {
            switch (this.status)
            {
                case 0:
                    Console.WriteLine("Current Status: Clean");
                    break;
                case 1:
                    Console.WriteLine("Current Status: Dirty");
                    break;
            }
        }
    }
}
