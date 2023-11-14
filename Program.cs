namespace WelcomeToDungeon;
internal partial class Program
{
    private static Character player;
    private static ItemInfo item;
    private static Inventory playerInventory;
    string[] itemList = new string[10];
    
    static void Logo()
    {
        Console.WriteLine("=============================================================================");
        Console.WriteLine("         .__                                     .__                         ");
        Console.WriteLine("  _____  |__|  ____    ____ _______       _____  |__|  ____    ____ _______  ");
        Console.WriteLine(" /     \\ |  | /    \\  /  _ \\\\_  __ \\     /     \\ |  | /    \\ _/ __ \\\\_  __ \\ ");
        Console.WriteLine("|  Y Y  \\|  ||   |  \\(  <_> )|  | \\/    |  Y Y  \\|  ||   |  \\\\  ___/ |  | \\/ ");
        Console.WriteLine("|__|_|  /|__||___|  / \\____/ |__|       |__|_|  /|__||___|  / \\___  >|__| ");
        Console.WriteLine("      \\/          \\/                          \\/          \\/      \\/   ");
        Console.WriteLine("=============================================================================");
        Console.WriteLine("                           PRESS ANYKEY TO START                             ");
        Console.WriteLine("=============================================================================");
        Console.ReadKey();
    }

    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }

    public class ItemInfo
    {
        public string ItemName { get; }
        public string ItemExplan { get; }
        public int ItemAtk { get; }
        public int ItemDef { get; }
        public int ItemHp { get; }
        public bool IsEquipped { get; }

        public ItemInfo(string itemName, int itemAtk, int itemDef, int itemHp, string itemExplan, bool isEquipped = false)
        {
            ItemName = itemName;
            ItemAtk = itemAtk;
            ItemDef = itemDef;
            ItemHp = itemHp;
            ItemExplan = itemExplan;
            IsEquipped = isEquipped;
        }

        public bool IsEquiped { get; set; }

        public static int ItemCnt = 0;

        public void PrintItemStatDiscription(bool withNumber = false, int idx = 0)
        {
            Console.Write("-");
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("{0} ", idx);
                Console.ResetColor();
            }
            if (IsEquipped == false)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("E");
                Console.ResetColor();
                Console.Write("]");
            }
            Console.Write(ItemName + "    ");
            Console.Write("ㅣ");

            if (ItemAtk != 0) Console.Write($"ATK {(ItemAtk >= 0 ? "+" : "")}{ItemAtk} ");
            if (ItemDef != 0) Console.Write($"DEF {(ItemDef >= 0 ? "+" : "")}{ItemDef} ");
            if (ItemHp != 0) Console.Write($"Hp {(ItemHp >= 0 ? "+" : "")}{ItemHp} ");

            Console.Write("    ㅣ");
            Console.WriteLine(ItemExplan);
        }


    }

    static void Enter()
    {
        Console.WriteLine();
    }

    enum ItemType
    {
        Weapon,
        Consumable
    }



    class Inventory
    {
        public List<ItemInfo> items;


        public Inventory()
        {
            items = new List<ItemInfo>();
        }

        public void AddItem(ItemInfo itemInfo)
        {
            items.Add(itemInfo);
            items[ItemInfo.ItemCnt] = itemInfo;
            ItemInfo.ItemCnt++;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Got the {itemInfo.ItemName}!");
            Console.ResetColor();
        }

        public void DisplayInventory()
        {
            Console.WriteLine("YOUR IVTR");
            Enter();
            Enter();
            foreach (ItemInfo iteminfo in items)
            {
                iteminfo.PrintItemStatDiscription();
            }

            Enter();
            Console.WriteLine("0. Out");
            Console.WriteLine("1. Equipment Management");
            Enter();

            int input = CheckValidInput(0, 1); //GameMainScene max input

            switch (input)
            {
                case 0:
                    Console.Clear();
                    GameMainScene();
                    break;

                case 1:
                    Console.Clear();
                    EquipMenu();
                    Enter();
                    Enter();
                    Thread.Sleep(3000);
                    GameMainScene();
                    break;

            }


        }

        private void EquipMenu()
        {
            Console.WriteLine("YOUR IVTR - MANAGEMENT");
            Console.WriteLine("You can manage the equipment you own");
            Enter();
            Enter();
            for (int i = 0; i < items.Count; i++)
            {
                items[i].PrintItemStatDiscription(true, i + 1);
            }
            Enter();
            Console.WriteLine("0. Out");

            int input = CheckValidInput(0, ItemInfo.ItemCnt); //GameMainScene max input

            switch (input)
            {
                case 0:
                    Console.Clear();
                    DisplayInventory();
                    break;

                default:
                    Console.Clear();
                    ToggleEquipStatus(input - 1);
                    EquipMenu();
                    break;


            }

        }

        private void ToggleEquipStatus(int idx)
        {
            item.IsEquiped = !item.IsEquiped;

        }
    }

    static void Main(string[] args)
    {
        Logo();
        GmaeIntro();
        GameMainScene();
    }


    static void GmaeIntro()
    {
        Console.Clear();
        string charactorName = "";
        string charactorClass = "";
        playerInventory = new Inventory();

        Console.WriteLine("Welcome to the dangerous mine!");
        Thread.Sleep(2000);
        Console.Clear();
        Console.WriteLine("What is your name?");
        Console.Write("name: ");
        charactorName = Console.ReadLine();
        Console.Clear();

        Console.WriteLine("what is your class?" + "\n" + "1. Warrior");
        Console.Write("class: ");
        int input = CheckValidInput(1, 1);

        switch (input)
        {
            case 1:
                charactorClass = "Worrior";
                break;

        }

        player = new Character(charactorName, charactorClass, 1, 10, 5, 100, 1500);

        Thread.Sleep(1000);
        Console.Clear();
    }

    static void AnswerGame()
    {
        Console.WriteLine("You will plan how deep you will go before entering the mine.");
        Thread.Sleep(1500);
        Console.WriteLine("I'll teach you how to plan");
        Thread.Sleep(1500);
        Console.WriteLine("You can add one reward card each time you enter the mine. But each time, a risk card is added one by one.");
        Thread.Sleep(1500);
        Console.WriteLine("You can stop adding reward cards. You will immediately enter the dungeon.");
        Thread.Sleep(1500);
        Console.WriteLine("If you clear it, you can get all the rewards, but if you fail, you will die.");
        Thread.Sleep(1500);
        Console.WriteLine("You'd better plan carefully.");
        Enter();
        Console.WriteLine("Don't die!");
        playerInventory.AddItem(new ItemInfo("Old_Sword", 1, 0, 0, "I feel like it will break", false));

    }


    static void AnswerStatus()
    {
        Console.WriteLine("Your INFO");
        Console.WriteLine($"NAME: {player.Name}");
        Console.WriteLine($"LEVEL: {player.Level}");
        Console.WriteLine($"CLASS: {player.Job}");
        Console.WriteLine($"ATK: {player.Atk}");
        Console.WriteLine($"DEF: {player.Def}");
        Console.WriteLine($"HP: {player.Hp}");
        Console.WriteLine($"GOLD: {player.Gold}");
        Thread.Sleep(1500);
        Enter();
        Console.WriteLine("You are careful!");
        playerInventory.AddItem(new ItemInfo("Old_Armor", 0, 1, 1, "I feel like it will break", false));
    }

    static void GameMainScene()
    {
        Console.WriteLine("Please ask anything you want before entering the mine.");
        Console.WriteLine("1. Dangerous dungeon??" + "\n" + "2. What is my status??" + "\n" + "3. Inventory?" + "\n" + "0. That's enough.");
        Console.Write("Action... ");


        int input = CheckValidInput(0, 4); //GameMainScene max input

        switch (input)
        {
            case 0:
                break;

            case 1:
                Console.Clear();
                AnswerGame();
                Enter();
                Enter();
                Thread.Sleep(3000);
                GameMainScene();
                break;

            case 2:
                Console.Clear();
                AnswerStatus();
                Enter();
                Enter();
                Thread.Sleep(3000);
                GameMainScene();
                break;

            case 3:
                Console.Clear();
                playerInventory.DisplayInventory();
                Enter();
                Enter();
                Thread.Sleep(3000);
                GameMainScene();
                break;
        }
    }

    static int CheckValidInput(int min, int max) //GameMainScene valid
    {
        while (true)
        {
            string? input = Console.ReadLine();

            bool parseSuccess = int.TryParse(input, out var ret);
            if (parseSuccess)
            {
                if (ret >= min && ret <= max)
                    return ret;
            }

            Console.WriteLine("What?");
        }
    }


}