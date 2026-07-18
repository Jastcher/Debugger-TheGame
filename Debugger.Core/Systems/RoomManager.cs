
using System.Drawing;
using System.Dynamic;
using Debugger.Core.Entities;

namespace Debugger.Core.Systems
{
    public class RoomManager
    {
        public event Action<int>? OnRoomChanged;

        // size has to be odd
        public int Size { get; private set; } = 9;

        public int CurrentPosX { get; private set; }
        public int CurrentPosY { get; private set; }

        public int CenterX { get; private set; }
        public int CenterY { get; private set; }

        public Room CurrentRoom { get => roomMap[CurrentPosX, CurrentPosY]; }

        public Room GetRoom(int x, int y) => roomMap[x, y];

        Room[,] roomMap;

        public RoomManager()
        {
            CenterX = (Size - 1) / 2;
            CenterY = (Size - 1) / 2;

            roomMap = new Room[Size, Size];

            Reset();
        }

        public void Reset()
        {
            CurrentPosX = CenterX;
            CurrentPosY = CenterY;


            // clear map
            Array.Clear(roomMap, 0, roomMap.Length);
        }

        public void PlaceRoom(Room room)
        {
            roomMap[room.X, room.Y] = room;
        }

        public void Move(int relX, int relY)
        {

            int newPosX = CurrentPosX + relX;
            int newPosY = CurrentPosY + relY;

            if (newPosX < 0 || newPosX >= roomMap.GetLength(0)) return;
            if (newPosY < 0 || newPosY >= roomMap.GetLength(1)) return;

            if (roomMap[newPosX, newPosY] == null) return;

            CurrentPosX = newPosX;
            CurrentPosY = newPosY;

            OnRoomChanged?.Invoke(CurrentRoom.RoomIndex);

        }

        public void PrintMap()
        {

            // DEBUG
            for (int y = 0; y < roomMap.GetLength(1); y++)
            {
                for (int x = 0; x < roomMap.GetLength(0); x++)
                {
                    if (GetRoom(x, y) == null)
                    {
                        Console.Write(". ");
                    }
                    else
                    {
                        Console.Write($"{roomMap[x, y].ID}{(x == CurrentPosX && y == CurrentPosY ? 'P' : ' ')}");
                    }
                }
                Console.WriteLine();
            }
        }

        public void GenLayout(int size)
        {


            int IDCounter = 0;
            List<Room> rooms = new();

            // place starting room at center
            Room start = new Room(IDCounter++, 1, CenterX, CenterY);
            rooms.Add(start);

            // randomly distribute nodes
            for (int i = 0; i < size;)
            {
                int x = Random.Shared.Next(0, roomMap.GetLength(0));
                int y = Random.Shared.Next(0, roomMap.GetLength(1));

                if (GetRoom(x, y) != null) continue;

                rooms.Add(new Room(IDCounter++, Random.Shared.Next(2), x, y));
                i++;
            }

            // make each node connect to its n nearest neighbors
            int n = 2;

            List<int[]> potentialEdges = new List<int[]>();
            for (int i = 0; i < rooms.Count; i++)
            {
                Room crntRoom = rooms[i];
                var nearestNeighbors = rooms.Where(r => r.ID != crntRoom.ID).Select(r => new { Room = r, Distance = Math.Pow(r.X - crntRoom.X, 2) + Math.Pow(r.Y - crntRoom.Y, 2) }).OrderBy(r => r.Distance).Take(n);

                //Console.WriteLine(crntRoom.ID);
                foreach (var neighbor in nearestNeighbors)
                {
                    //Console.WriteLine($"    {neighbor.Room.ID}");
                    potentialEdges.Add([crntRoom.ID, neighbor.Room.ID, (int)neighbor.Distance]);
                }
            }

            List<int[]> dungeonPassages = KruskalSolver.KruskalsMST(rooms.Count, potentialEdges);

            // add rooms in between nodes
            foreach (var pass in dungeonPassages)
            {
                //Console.WriteLine($"{pass[0]} {pass[1]} {pass[2]}");

                Room r1 = rooms[pass[0]];
                Room r2 = rooms[pass[1]];

                int minX = Math.Min(r1.X, r2.X);
                int maxX = Math.Max(r1.X, r2.X);

                for (int i = minX; i <= maxX; i++)
                {
                    if (rooms.Any(r => r.X == i && r.Y == r1.Y)) continue;

                    Room room = new(IDCounter++, Random.Shared.Next(2), i, r1.Y);
                    rooms.Add(room);
                }

                int minY = Math.Min(r1.Y, r2.Y);
                int maxY = Math.Max(r1.Y, r2.Y);

                for (int i = minY; i <= maxY; i++)
                {
                    if (rooms.Any(r => r.X == r2.X && r.Y == i)) continue;

                    Room room = new(IDCounter++, Random.Shared.Next(2), r2.X, i);
                    rooms.Add(room);
                }
            }


            Reset();

            // add rooms to map
            foreach (Room room in rooms)
            {
                PlaceRoom(room);
            }

            if (CurrentRoom != null)
            {
                OnRoomChanged?.Invoke(CurrentRoom.RoomIndex);
            }

        }
    }

    public class Room
    {
        public int ID { get; private set; }
        public int RoomIndex { get; private set; }

        public int X { get; set; }
        public int Y { get; set; }

        public List<Entity>? Entities { get; set; }

        public Room(int id, int roomIndex, int x, int y)
        {
            this.ID = id;
            this.RoomIndex = roomIndex;
            this.X = x;
            this.Y = y;
        }
    }
}