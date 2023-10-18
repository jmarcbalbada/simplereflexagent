using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleReflexAgents
{
    public partial class Form1 : Form
    {
        private int[] location;
        private int[] status;
        private int[] action;
        private int startLocation;
        private int startStatus;
        private Room currentRoom;
        private Room roomA;
        private Room roomB;
        private Room roomC;
        private Room roomD;
        //Random rand;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeAll()
        {
            //for location = 0 - A, 1 - B, 2 - C, 3 - D
            location = new int[] { 0, 1, 2, 3 };
            //for status = 0 - Clean, 1 - Dirty
            status = new int[] { 0, 1 };
            //for action = 0 - Left, 1 - Right, 2 - Up, 3 - Down, 4 - Clean, 5 - Nop
            action = new int[] { 0, 1, 2, 3, 4, 5 };
            currentRoom = new Room();
            roomA = new Room(0);
            roomB = new Room(1);
            roomC = new Room(2);
            roomD = new Room(3);
            roomA = RandomizeRoomAssignment(roomA);
            roomB = RandomizeRoomAssignment(roomB);
            roomC = RandomizeRoomAssignment(roomC);
            roomD = RandomizeRoomAssignment(roomD);
            //rand = new Random();
        }

        private int RandomizerForLocation()
        {
            int randomLocation, randomIndex;
            Random random = new Random();
            randomIndex = random.Next(0, location.Length);
            randomLocation = location[randomIndex];
            //Console.WriteLine("random location: " + randomLocation);

            return randomLocation;
        }

        private int RandomizerForStatus()
        {
            int randomStatus, randomIndex;
            Thread.Sleep(500);
            Random random = new Random();
            randomIndex = random.Next(0, status.Length);
            randomStatus = status[randomIndex];
            //Console.WriteLine("random status: " + randomStatus);

            return randomStatus;
        }


        private Room RandomizeRoomAssignment(Room room)
        {
            Room resultRoom = room;
            resultRoom.setStatus(RandomizerForStatus());
            return resultRoom;
        }

        private Room InitializeCurrentToActualRoom(Room current)
        {
            Room newCurrent = current;
            switch (current.getLocation())
            {
                case 0:
                    current.setStatus(this.roomA.getStatus());
                    break;
                case 1:
                    current.setStatus(this.roomB.getStatus());
                    break;
                case 2:
                    current.setStatus(this.roomC.getStatus());
                    break;
                case 3:
                    current.setStatus(this.roomD.getStatus());
                    break;
            }

            return newCurrent;
        }

        private void printAndInitalizeRandomLocationAndStatus()
        {
            int randomLocation = RandomizerForLocation();
            startLocation = randomLocation;
            currentRoom.setLocation(startLocation);
            currentRoom = InitializeCurrentToActualRoom(currentRoom);
            Console.WriteLine("Current Room: ");
            currentRoom.printLocation();
            currentRoom.printStatus();

            switch(currentRoom.getLocation())
            {
                case 0: currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = "Room A"));
                    break;
                case 1: currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = "Room B"));
                    break;
                case 2:currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = "Room C"));
                    break;
                case 3: currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = "Room D")); 
                    break;
            }
        }

        async Task vacuumCleanerAsync(Room currentRoom)
        {
            //for action = 0 - Left, 1 - Right, 2 - Up, 3 - Down, 4 - Clean, 5 - Nop
            //for location = 0 - A, 1 - B, 2 - C, 3 - D
            //for status = 0 - Clean, 1 - Dirty
            //action 4 - TO CLEAN, 5 - NO OPERATION

            //int location = currentRoom.getLocation();
            //int status = currentRoom.getStatus();
            int i = 0;

            while(true)
            {
                await Task.Delay(2000);
                switch (currentRoom.getLocation())
                {
                    // on Room A
                    case 0:
                        // if dirty, then clean room
                        if (currentRoom.getStatus() == 1)
                        {
                            currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = "Room A"));
                            currentRoom.setStatus(0);
                            roomA = currentRoom;
                            FillRoom(roomA);
                            Console.WriteLine("Cleaning A");
                            statusPrompt.Invoke((MethodInvoker)(() => statusPrompt.Text = "Cleaning A"));
                            actionPrompt.Invoke((MethodInvoker)(() => actionPrompt.Text = "Cleaning..."));
                            currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = ""));
                            await Task.Delay(4000);
                            roomA.printLocation();
                            roomA.printStatus();
                            TraverseNextRoom();
                            Console.WriteLine("Traversing");
                            //statusPrompt.Text = "Traversing";
                            currentRoom.printLocation();
                            currentRoom.printStatus();
                        }
                        else
                        {
                            currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = "Room A"));
                            Console.WriteLine("Traversing");
                            statusPrompt.Invoke((MethodInvoker)(() => statusPrompt.Text = "Traversing"));
                            //statusPrompt.Text = "Traversing";
                            TraverseNextRoom();
                            currentRoom.printLocation();
                            currentRoom.printStatus();
                        }
                        break;
                    case 1:
                        // if dirty, then clean room
                        if (currentRoom.getStatus() == 1)
                        {
                            currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = "Room B"));
                            currentRoom.setStatus(0);
                            roomB = currentRoom;
                            FillRoom(roomB);
                            Console.WriteLine("Cleaning B");
                            statusPrompt.Invoke((MethodInvoker)(() => statusPrompt.Text = "Cleaning B"));
                            actionPrompt.Invoke((MethodInvoker)(() => actionPrompt.Text = "Cleaning..."));
                            currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = ""));
                            await Task.Delay(4000);
                            //statusPrompt.Text = "Cleaning B";
                            Console.WriteLine("Traversing");
                            //statusPrompt.Text = "Traversing";
                            roomB.printLocation();
                            roomB.printStatus();
                            TraverseNextRoom();
                            currentRoom.printLocation();
                            currentRoom.printStatus();
                        }
                        else
                        {
                            currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = "Room B"));
                            Console.WriteLine("Traversing");
                            statusPrompt.Invoke((MethodInvoker)(() => statusPrompt.Text = "Traversing"));
                            //statusPrompt.Text = "Traversing";
                            TraverseNextRoom();
                            currentRoom.printLocation();
                            currentRoom.printStatus();
                        }
                        break;
                    case 2:
                        // if dirty, then clean room
                        if (currentRoom.getStatus() == 1)
                        {
                            currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = "Room C"));
                            currentRoom.setStatus(0);
                            roomC = currentRoom;
                            FillRoom(roomC);
                            Console.WriteLine("Cleaning C");
                            statusPrompt.Invoke((MethodInvoker)(() => statusPrompt.Text = "Cleaning C"));
                            actionPrompt.Invoke((MethodInvoker)(() => actionPrompt.Text = "Cleaning..."));
                            currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = ""));
                            await Task.Delay(4000);
                            //statusPrompt.Text = "Cleaning C";
                            Console.WriteLine("Traversing");
                            //statusPrompt.Text = "Traversing";
                            roomC.printLocation();
                            roomC.printStatus();
                            TraverseNextRoom();
                            currentRoom.printLocation();
                            currentRoom.printStatus();
                        }
                        else
                        {
                            currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = "Room C"));
                            Console.WriteLine("Traversing");
                            statusPrompt.Invoke((MethodInvoker)(() => statusPrompt.Text = "Traversing"));
                            //statusPrompt.Text = "Traversing";
                            TraverseNextRoom();
                            currentRoom.printLocation();
                            currentRoom.printStatus();
                        }
                        break;
                    case 3:
                        // if dirty, then clean room
                        if (currentRoom.getStatus() == 1)
                        {
                            currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = "Room D"));
                            currentRoom.setStatus(0);
                            roomD = currentRoom;
                            FillRoom(roomD);
                            Console.WriteLine("Cleaning D");
                            statusPrompt.Invoke((MethodInvoker)(() => statusPrompt.Text = "Cleaning D"));
                            actionPrompt.Invoke((MethodInvoker)(() => actionPrompt.Text = "Cleaning..."));
                            currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = ""));
                            await Task.Delay(4000);
                            //statusPrompt.Text = "Cleaning D";
                            Console.WriteLine("Traversing");
                            //statusPrompt.Text = "Traversing";
                            roomD.printLocation();
                            roomD.printStatus();
                            TraverseNextRoom();
                            currentRoom.printLocation();
                            currentRoom.printStatus();
                        }
                        else
                        {
                            currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = "Room D"));
                            Console.WriteLine("Traversing");
                            statusPrompt.Invoke((MethodInvoker)(() => statusPrompt.Text = "Traversing"));
                            TraverseNextRoom();
                            currentRoom.printLocation();
                            currentRoom.printStatus();
                        }
                        break;
                }
                i++;
            }
        }

        private void TraverseNextRoom()
        {
            Random random = new Random();
            // move adjacent
            // A -> B, C | B -> A, D | C -> A , D | D -> B , C
            switch (this.currentRoom.getLocation())
            {
                case 0:
                    {
                        // A | 0 - B, 1 - C
                        int randomizer = random.Next(0, 2);
                        Console.WriteLine("random: " + randomizer);
                        if (randomizer == 0)
                        {
                            currentRoom.setLocation(roomB.getLocation());
                            currentRoom.setStatus(roomB.getStatus());
                            actionPrompt.Invoke((MethodInvoker)(() => actionPrompt.Text = "→"));
                            currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = "Room B"));
                        }
                        else if(randomizer == 1)
                        {
                            currentRoom.setLocation(roomC.getLocation());
                            currentRoom.setStatus(roomC.getStatus());
                            actionPrompt.Invoke((MethodInvoker)(() => actionPrompt.Text = "↓"));
                            currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = "Room C"));
                        }
                    }
                    break;
                case 1:
                    {
                        // B | 0 - A, 1 - D
                        int randomizer = random.Next(0, 2);
                        Console.WriteLine("random: " + randomizer);
                        if (randomizer == 0)
                        {
                            currentRoom.setLocation(roomA.getLocation());
                            currentRoom.setStatus(roomA.getStatus());
                            actionPrompt.Invoke((MethodInvoker)(() => actionPrompt.Text = "←"));
                            currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = "Room A"));
                        }
                        else if (randomizer == 1)
                        {
                            currentRoom.setLocation(roomD.getLocation());
                            currentRoom.setStatus(roomD.getStatus());
                            actionPrompt.Invoke((MethodInvoker)(() => actionPrompt.Text = "↓"));
                            currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = "Room D"));
                        }
                    }
                    break;
                case 2:
                    {
                        // C | 0 - A, 1 - D
                        int randomizer = random.Next(0, 2);
                        Console.WriteLine("random: " + randomizer);
                        if (randomizer == 0)
                        {
                            currentRoom.setLocation(roomA.getLocation());
                            currentRoom.setStatus(roomA.getStatus());
                            actionPrompt.Invoke((MethodInvoker)(() => actionPrompt.Text = "↑"));
                            currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = "Room A"));
                        }
                        else if (randomizer == 1)
                        {
                            currentRoom.setLocation(roomD.getLocation());
                            currentRoom.setStatus(roomD.getStatus());
                            actionPrompt.Invoke((MethodInvoker)(() => actionPrompt.Text = "→"));
                            currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = "Room D"));
                        }
                    }
                    break;
                case 3:
                    {
                        // D | 0 - B, 1 - C
                        int randomizer = random.Next(0, 2);
                        Console.WriteLine("random: " + randomizer);
                        if (randomizer == 0)
                        {
                            currentRoom.setLocation(roomB.getLocation());
                            currentRoom.setStatus(roomB.getStatus());
                            actionPrompt.Invoke((MethodInvoker)(() => actionPrompt.Text = "↑"));
                            currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = "Room B"));
                        }
                        else if (randomizer == 1)
                        {
                            currentRoom.setLocation(roomC.getLocation());
                            currentRoom.setStatus(roomC.getStatus());
                            actionPrompt.Invoke((MethodInvoker)(() => actionPrompt.Text = "←"));
                            currentRoomPrompt.Invoke((MethodInvoker)(() => currentRoomPrompt.Text = "Room C"));
                        }
                    }
                    break;
            }
        }

        void printAllRooms()
        {
            roomA.printLocation();
            roomA.printStatus();

            roomB.printLocation();
            roomB.printStatus();

            roomC.printLocation();
            roomC.printStatus();

            roomD.printLocation();
            roomD.printStatus();
        }

        void FillRoomsWithColors()
        {
            //for status = 0 - Clean, 1 - Dirty
            if (roomA.getStatus() == 1)
            {
                pictureBoxA.BackColor = Color.Brown;
            }
            else
            {
                pictureBoxA.BackColor = Color.Green;
            }

            if (roomB.getStatus() == 1)
            {
                pictureBoxB.BackColor = Color.Brown;
            }
            else
            {
                pictureBoxB.BackColor = Color.Green;
            }

            if (roomC.getStatus() == 1)
            {
                pictureBoxC.BackColor = Color.Brown;
            }
            else
            {
                pictureBoxC.BackColor = Color.Green;
            }

            if (roomD.getStatus() == 1)
            {
                pictureBoxD.BackColor = Color.Brown;
            }
            else
            {
                pictureBoxD.BackColor = Color.Green;
            }
        }

        void FillRoom(Room room)
        {
            switch (room.getLocation())
            {
                case 0: pictureBoxA.BackColor = Color.Green;
                    break;
                case 1: pictureBoxB.BackColor = Color.Green;
                    break;
                case 2: pictureBoxC.BackColor = Color.Green;
                    break;
                case 3: pictureBoxD.BackColor = Color.Green;
                    break;
            }
        }

        private async void StartCleaningProcess()
        {
            await Task.Run(() => {
                vacuumCleanerAsync(currentRoom);
            });
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeAll();
            printAllRooms();
            printAndInitalizeRandomLocationAndStatus();
            FillRoomsWithColors();
            StartCleaningProcess();
        }

        private void pictureBoxA_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxB_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxC_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxD_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxA_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(Color.Black);
            Rectangle rect = new Rectangle(0,0, pictureBoxA.Width - 1, pictureBoxA.Height - 1);
            graphics.DrawRectangle(pen, rect);
            pen.Dispose();
        }

        private void pictureBoxB_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(Color.Black);
            Rectangle rect = new Rectangle(0, 0, pictureBoxB.Width - 1, pictureBoxB.Height - 1);
            graphics.DrawRectangle(pen, rect);
            pen.Dispose();
        }

        private void pictureBoxC_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(Color.Black);
            Rectangle rect = new Rectangle(0, 0, pictureBoxC.Width - 1, pictureBoxC.Height - 1);
            graphics.DrawRectangle(pen, rect);
            pen.Dispose();
        }

        private void pictureBoxD_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(Color.Black);
            Rectangle rect = new Rectangle(0, 0, pictureBoxD.Width - 1, pictureBoxD.Height - 1);
            graphics.DrawRectangle(pen, rect);

            pen.Dispose();
        }

        private void legendClean_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(Color.Black);
            Rectangle rect = new Rectangle(0, 0, legendClean.Width - 1, legendClean.Height - 1);
            graphics.DrawRectangle(pen, rect);
            pen.Dispose();
            legendClean.BackColor = Color.Green;
        }

        private void legendDirty_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(Color.Black);
            Rectangle rect = new Rectangle(0, 0, legendDirty.Width - 1, legendDirty.Height - 1);
            graphics.DrawRectangle(pen, rect);
            pen.Dispose();
            legendDirty.BackColor = Color.Brown;
        }
    }
}
