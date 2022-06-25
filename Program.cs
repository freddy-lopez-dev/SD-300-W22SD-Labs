Hotel myHotel = new Hotel("Fred's Lounge", "777 New York Ave");
Room room1 = new Room("101", 4, false);
Room room2 = new Room("102", 2, false);
Room room3 = new PremiumRoom("103", 8, false, "Jacuzzi", 20);
Room room4 = new PremiumRoom("104", 12, false, "2 Floors with Cinema", 20);
myHotel.BuildRoom(room1);
myHotel.BuildRoom(room2);
myHotel.BuildRoom(room3);
myHotel.BuildRoom(room4);
Client baseClient1 = new Client("Jim", 121221938282);
Client baseClient2 = new Client("Pam", 213128382138);
Client vipClient1 = new VIPClient(0042, 20, "Dwight", 1231232128);
Client vipClient2 = new VIPClient(0043, 50, "Michael", 1232321321);
myHotel.RegisterClient(baseClient1);
myHotel.RegisterClient(baseClient2);
myHotel.RegisterClient(vipClient1);
myHotel.RegisterClient(vipClient2);

myHotel.GetReservation(room1, baseClient2);
myHotel.GetReservation(room4, baseClient1);


//Check reservation
foreach (Reservation r in myHotel.Reservations)
{
    Console.WriteLine(r.Client.Name);
}




class Hotel
{
    public string Name { get; set; }
    public string Address { get; set; }
    public ICollection<Room> Rooms { get; set; }
    public ICollection<Client> Clients { get; set; }
    public ICollection<Reservation> Reservations { get; set; }

    public Hotel(string name, string address)
    {
        Name = name;
        Address = address;
        Rooms = new HashSet<Room>();
        Clients = new HashSet<Client>();
        Reservations = new HashSet<Reservation>();
    }

    public Room BuildRoom(Room room)
    {
        Rooms.Add(room);
        return room;
    }

    public Client RegisterClient(Client client)
    {
        Clients.Add(client);
        return client;
    }

    public bool checkIfVIP(string name)
    {
        bool isVIP = false;
        foreach (Client c in Clients)
        {
            if (c.Name == name)
            {
                if (c.GetType().FullName == "VIPClient")
                {
                    return isVIP = true;
                }
                else
                {
                    return isVIP;
                }
            }
        }

        throw new Exception("Not a registered Client");
    }

    public bool checkPremRoom(string number)
    {
        bool isPremium = false;
        foreach (Room r in Rooms)
        {
            if (r.Number == number)
            {
                if (r.GetType().FullName == "PremiumRoom")
                {
                    return isPremium = true;
                }
                else
                {
                    return isPremium;
                }
            }
        }

        throw new Exception("Room does not exist");
    }

    public void GetReservation(Room room, Client client)
    {
        // If room is premium and client is based. throw Base client is trying to book a Premium room
        bool isPremiumRoom = checkPremRoom(room.Number);
        bool isVIPClient = checkIfVIP(client.Name);
        if (isPremiumRoom)
        {
            if (isVIPClient)
            {
                Reservation newReservation = new Reservation(client, room);
                Reservations.Add(newReservation);
            }
            else
            {
                Console.WriteLine("Not a valid reservation");
            }
        }
        else
        {
            Reservation newReservation = new Reservation(client, room);
            Reservations.Add(newReservation);
        }
    }
}

class Reservation
{
    public Client Client { get; set; }
    public Room Room { get; set; }
    public int Occupants { get; set; }
    public bool IsCurrent { get; set; }
    public DateTime DateTime = new DateTime();

    public Reservation(Client client, Room room)
    {
        Client = client;
        Room = room;
    }
}

class Client
{
    public string Name { get; set; }
    public long CreditCard { get; set; }
    public ICollection<Reservation> Reservations { get; set; }

    public Client(string name, long cc)
    {
        Name = name;
        CreditCard = cc;
        Reservations = new HashSet<Reservation>();
    }
}

class VIPClient : Client
{
    public int VIPNumber { get; set; }
    public int VIPPoints { get; set; }

    public VIPClient(int vIPNumber, int vIPPoints, string name, long cc) : base(name, cc)
    {
        VIPNumber = vIPNumber;
        VIPPoints = vIPPoints;
    }
}

class Room
{
    public string Number { get; set; }
    public int Capacity { get; set; }
    public bool Occupied { get; set; }
    public ICollection<Reservation> Reservations { get; set; }

    public Room(string number, int capacity, bool occupied)
    {
        Number = number;
        Capacity = capacity;
        Occupied = occupied;
        Reservations = new HashSet<Reservation>();
    }
}
class PremiumRoom : Room
{
    public string AdditionalAmenities { get; set; }
    public int VIPValue { get; set; }
    public PremiumRoom(string number, int capacity, bool occupied, string additionalAmenities, int vIPValue) : base(number, capacity, occupied)
    {
        AdditionalAmenities = additionalAmenities;
        VIPValue = vIPValue;
    }
}