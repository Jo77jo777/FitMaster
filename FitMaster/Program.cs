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
    // Model: Benutzer
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
        private int nextId = 2;

        public MembersService()
        {
            members = new List<Member>();
            members.Add(new Member { Id = 1, Name = "Max Mustermann", Tarif = "Basic", IsActive = true });
        }

        // Mitglieder anzeigen
        public void ListMembers()
        {
            Console.WriteLine("\n--- Mitgliederliste ---");
            if (members.Count == 0)
            {
                Console.WriteLine("Keine Mitglieder vorhanden.");
            }
            else
            {
                foreach (var m in members)
                    Console.WriteLine(m.GetInfo());
            }
        }

        // Mitglied hinzufügen
        public void AddMember()
        {
            Console.WriteLine("\n--- Neues Mitglied hinzufügen ---");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Tarif (Basic / Premium / VIP): ");
            string tarif = Console.ReadLine();

            Member newMember = new Member
            {
                Id = nextId++,
                Name = name,
                Tarif = tarif,
                IsActive = true
            };

            members.Add(newMember);

            Console.WriteLine("Mitglied erfolgreich hinzugefügt!");
        }

        // Mitglied bearbeiten
        public void EditMember()
        {
            Console.WriteLine("\n--- Mitglied bearbeiten ---");
            Console.Write("Mitglied-ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ungültige ID!");
                return;
            }

            Member m = members.Find(x => x.Id == id);
            if (m == null)
            {
                Console.WriteLine("Mitglied nicht gefunden!");
                return;
            }

            Console.Write("Neuer Name (leer = keine Änderung): ");
            string newName = Console.ReadLine();
            if (newName != "") m.Name = newName;

            Console.Write("Neuer Tarif (leer = keine Änderung): ");
            string newTarif = Console.ReadLine();
            if (newTarif != "") m.Tarif = newTarif;

            Console.Write("Aktiv? (j/n): ");
            string activeInput = Console.ReadLine();
            if (activeInput == "j") m.IsActive = true;
            if (activeInput == "n") m.IsActive = false;

            Console.WriteLine("Mitglied erfolgreich aktualisiert!");
        }

        // Mitglied löschen
        public void DeleteMember()
        {
            Console.WriteLine("\n--- Mitglied löschen ---");
            Console.Write("Mitglied-ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ungültige ID!");
                return;
            }

            Member m = members.Find(x => x.Id == id);
            if (m == null)
            {
                Console.WriteLine("Mitglied nicht gefunden!");
                return;
            }

            members.Remove(m);
            Console.WriteLine("Mitglied wurde gelöscht!");
        }
    }

    // ----------------------------------------
    //  Hauptprogramm
    // ----------------------------------------
    public class Program
    {
        private static MembersService membersService;

        public static void Main(string[] args)
        {
            membersService = new MembersService();
            Console.Title = "FitMaster – Fitnessstudio-Verwaltungssystem (Tag 2 + 3)";
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
                        Console.WriteLine("→ Login & Rollen (Tag 4)");
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
                Console.WriteLine("2) Mitglied hinzufügen");
                Console.WriteLine("3) Mitglied bearbeiten");
                Console.WriteLine("4) Mitglied löschen");
                Console.WriteLine("5) Zurück");
                Console.Write("Auswahl: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        membersService.ListMembers();
                        break;

                    case "2":
                        membersService.AddMember();
                        break;

                    case "3":
                        membersService.EditMember();
                        break;

                    case "4":
                        membersService.DeleteMember();
                        break;

                    case "5":
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


