using System;
using System.Collections.Generic;

namespace FitMaster
{
    // ----------------------------------------
    // Model: Mitglied
    // ----------------------------------------
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tarif { get; set; }
        public bool IsActive { get; set; }

        public string GetInfo()
        {
            return "ID: " + Id + ", Name: " + Name + ", Tarif: " + Tarif + ", Aktiv: " + IsActive;
        }
    }

    // ----------------------------------------
    // Enum: Benutzerrolle
    // ----------------------------------------
    public enum UserRole
    {
        Admin,
        Mitarbeiter
    }

    // ----------------------------------------
    // Model: User
    // ----------------------------------------
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }

    // ----------------------------------------
    // Service: Mitgliederverwaltung
    // ----------------------------------------
    public class MembersService
    {
        private List<Member> members;

        public MembersService()
        {
            members = new List<Member>();
            // Beispiel-Daten
            members.Add(new Member { Id = 1, Name = "Max Mustermann", Tarif = "Basic", IsActive = true });
        }

        public void ListMembers()
        {
            Console.WriteLine("\n--- Mitgliederliste ---");
            if (members.Count == 0)
            {
                Console.WriteLine("Keine Mitglieder vorhanden.");
            }
            else
            {
                for (int i = 0; i < members.Count; i++)
                {
                    Console.WriteLine(members[i].GetInfo());
                }
            }
        }
    }

    // ----------------------------------------
    // Hauptprogramm
    // ----------------------------------------
    public class Program
    {
        private static MembersService membersService;

        public static void Main(string[] args)
        {
            membersService = new MembersService();
            Console.Title = "FitMaster – Fitnessstudio-Verwaltungssystem (Grundsystem)";
            ShowWelcomeScreen();
            RunMainMenu();
        }

        private static void ShowWelcomeScreen()
        {
            Console.WriteLine("=======================================");
            Console.WriteLine("     FitMaster Verwaltungssystem");
            Console.WriteLine("=======================================\n");
        }

        private static void RunMainMenu()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- HAUPTMENÜ ---");
                Console.WriteLine("1) Mitglieder verwalten");
                Console.WriteLine("2) Anwesenheit erfassen");
                Console.WriteLine("3) Login / Benutzerverwaltung");
                Console.WriteLine("4) Beenden");
                Console.Write("Auswahl: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowMembersMenu();
                        break;
                    case "2":
                        Console.WriteLine("→ Anwesenheitssystem (noch nicht implementiert)");
                        break;
                    case "3":
                        Console.WriteLine("→ Login & Rollen (noch nicht implementiert)");
                        break;
                    case "4":
                        running = false;
                        Console.WriteLine("Programm wird beendet...");
                        break;
                    default:
                        Console.WriteLine("Ungültige Eingabe!");
                        break;
                }
            }
        }

        private static void ShowMembersMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n--- MITGLIEDER ---");
                Console.WriteLine("1) Liste anzeigen");
                Console.WriteLine("2) Zurück");
                Console.Write("Auswahl: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        membersService.ListMembers();
                        break;
                    case "2":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Ungültige Eingabe!");
                        break;
                }
            }
        }
    }
}
