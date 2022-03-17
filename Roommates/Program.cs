using System;
using System.Collections.Generic;
using Roommates.Repositories;
using Roommates.Models;

namespace Roommates
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true;TrustServerCertificate=true;";

        static void Main(string[] args)
        {
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);
            
            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Show all rooms"):
                        List<Room> rooms = roomRepo.GetAll();
                        foreach (Room r in rooms)
                        {
                            Console.WriteLine($"{r.Name} has an Id of {r.Id} and a max occupancy of {r.MaxOccupancy}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Search for room"):
                        Console.Write("Room Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Room room = roomRepo.GetById(id);

                        Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Add a room"):
                        Console.Write("Room name: ");
                        string name = Console.ReadLine();

                        Console.Write("Max occupancy: ");
                        int max = int.Parse(Console.ReadLine());

                        Room roomToAdd = new Room()
                        {
                            Name = name,
                            MaxOccupancy = max
                        };

                        roomRepo.Insert(roomToAdd);

                        Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Show all chores"):
                        List<Chore> chores = choreRepo.GetAll();
                        foreach (Chore ch in chores)
                        {
                            Console.WriteLine($"{ch.Name}");
                        };
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Search for a chore"):
                        Console.Write("Chore Id: ");
                        int cId = int.Parse(Console.ReadLine());

                        Chore chore = choreRepo.GetById(cId);

                        Console.WriteLine($"{chore.Id} - {chore.Name}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Add a chore"):
                        Console.Write("Chore name: ");
                        string chName = Console.ReadLine();

                        Chore choreToAdd = new Chore()
                        {
                            Name = chName
                        };

                        choreRepo.Insert(choreToAdd);

                        Console.WriteLine($"{choreToAdd.Name} has been added and assigned an Id of {choreToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Search for a roommate"):
                        Console.Write("Roommate Id: ");
                        int rmId = int.Parse(Console.ReadLine());

                        Roommate roommate = roommateRepo.GetById(rmId);

                        Console.WriteLine($"{roommate.FirstName} in the {roommate.Room.Name} pays {roommate.RentPortion}%");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Show unassigned chores"):
                        List<Chore> unassignedChores = choreRepo.GetUnassignedChores();
                        foreach (Chore ch in unassignedChores)
                        {
                            Console.WriteLine($"{ch.Name} has yet to be assigned! Get to work!");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Assign chore to roommate"):
                        List<Chore> listAllChores = choreRepo.GetAll();
                        foreach (Chore ch in listAllChores)
                        {
                            Console.WriteLine($"{ch.Name} has an Id of {ch.Id}");
                        }

                        Console.WriteLine();
                        Console.Write("Please select a chore by Id: ");
                        int selectedChoreId = int.Parse(Console.ReadLine());

                        List<Roommate> allRoommates = roommateRepo.GetAll();
                        foreach (Roommate r in allRoommates)
                        {
                            Console.WriteLine($"{r.FirstName} {r.LastName} has an Id of {r.Id}");
                        }

                        Console.WriteLine();
                        Console.Write("Please select a roommate by Id: ");
                        int selectedRoommateId = int.Parse(Console.ReadLine());

                        choreRepo.AssignChore(selectedRoommateId, selectedChoreId);
                        Console.WriteLine("Get to Work!");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Exit"):
                        runProgram = false;
                        break;
                }
            }

        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
            {
                "Show all rooms",
                "Search for room",
                "Add a room",
                "Show all chores",
                "Search for a chore",
                "Add a chore",
                "Search for a roommate",
                "Show unassigned chores",
                "Assign chore to roommate",
                "Exit"
            };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }
        }
    }
}