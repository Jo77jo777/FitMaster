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
            return $"ID: {Id}, Name: {Name}, Tarif: {Tarif}, Aktiv: {IsActive}";
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
    // Service: Benutzerverwaltung + Login
    // ----------------------------------------
    public class UserService
    {
        private List<User> users = new List<User>();
        public User LoggedInUser { get; private set; }

        public UserService()
        {
            // Standard-Demo-Benutzer
            users.Add(new User { Username = "admin", Password = "1234", Role = UserRole.Admin });
            users.Add(new User { Username = "mitarbeiter", Password = "0000", Role = UserRole.Mitarbeiter });
        }

        // LOGIN
        public bool Login()
        {
            Console.WriteLine("\n--- Login ---");
            Console.Write("Benutzername: ");
            string username = Console.ReadLine();

            Console.Write("Passwort: ");
            string pw = Console.ReadLine();

            foreach (var u in users)
            {
                if (u.Username == username && u.Password == pw)
                {
                    LoggedInUser = u;
                    Console.WriteLine($"Login erfolgreich! Eingeloggt als: {u.Username} ({u.Role})");
                    return true;
                }
            }

            Console.WriteLine("Login fehlgeschlagen!");
            return false;
        }

        // Benutzer anzeigen
        public void ListUsers()
        {
            Console.WriteLine("\n--- Benutzerliste ---");
            foreach (var u in users)
            {
                Console.WriteLine($"Benutzer: {u.Username}, Rolle: {u.Role}");
            }
        }

        // Benutzer hinzufügen
        public void AddUser()
        {
            Console.WriteLine("\n--- Benutzer hinzufügen ---");
            Console.Write("Benutzername: ");
            string name = Console.ReadLine();

            Console.Write("Passwort: ");
            string pw = Console.ReadLine();

            Console.Write("Rolle (Admin/Mitarbeiter): ");
            string roleInput = Console.ReadLine().ToLower();

            UserRole role = roleInput == "admin" ? UserRole.Admin : UserRole.Mitarbeiter;

            users.Add(new User { Username = name, Password = pw, Role = role });

            Console.WriteLine("Benutzer erfolgreich angelegt!");
        }

        // Benutzer löschen
        public void DeleteUser()
        {
            Console.WriteLine("\n--- Benutzer löschen ---");
            Console.Write("Benutzername: ");
            string name = Console.ReadLine();

            var user = users.Find(u => u.Username == name);

            if (user == null)
            {
                Console.WriteLine("Benutzer nicht gefunden!");
                return;
            }

            if (user == LoggedInUser)
            {
                Console.WriteLine("Du kannst dich nicht selbst löschen!");
                return;
            }

            users.Remove(user);
            Console.WriteLine("Benutzer gelöscht!");
        }

        // Passwort ändern
        public void ChangePassword()
        {
            Console.WriteLine("\n--- Passwort ändern ---");
            Console.Write("Benutzername: ");
            string name = Console.ReadLine();

            var user = users.Find(u => u.Username == name);

            if (user == null)
            {
                Console.WriteLine("Benutzer nicht gefunden!");
                return;
            }

            Console.Write("Neues Passwort: ");
            string pw = Console.ReadLine();
            user.Password = pw;

            Console.WriteLine("Passwort wurde geändert!");
        }
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
                return;
            }

            foreach (var m in members)
                Console.WriteLine(m.GetInfo());
        }

        // Mitglied hinzufügen
        public void AddMember()
        {
            Console.WriteLine("\n--- Neues Mitglied hinzufügen ---");
            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Tarif (Basic/Premium/VIP): ");
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

            var m = members.Find(x => x.Id == id);
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
            string active = Console.ReadLine();
            if (active == "j") m.IsActive = true;
            if (active == "n") m.IsActive = false;

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

            var m = members.Find(x => x.Id == id);
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
    // Hauptprogramm
    // ----------------------------------------
    public class Program
    {
        private static MembersService membersService;
        private static UserService userService;

        public static void Main(string[] args)
        {
            membersService = new MembersService();
            userService = new UserService();

            Console.Title = "FitMaster – Fitnessstudio-Verwaltungssystem";

            ShowWelcomeScreen();

            // Login erzwingen
            while (!userService.Login()) { }

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
                Console.WriteLine("2) Anwesenheit erfassen (noch nicht)");
                Console.WriteLine("3) Benutzer verwalten");
                Console.WriteLine("4) Beenden");
                Console.Write("Auswahl: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowMembersMenu();
                        break;

                    case "2":
                        Console.WriteLine("Anwesenheitssystem wird später implementiert.");
                        break;

                    case "3":
                        ShowUserMenu();
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

        // Mitglieder-Menü
        private static void ShowMembersMenu()
        {
            bool back = false;

            while (!back)
            {
                Console.WriteLine("\n--- MITGLIEDER-MENÜ ---");
                Console.WriteLine("1) Mitgliederliste anzeigen");
                Console.WriteLine("2) Mitglied hinzufügen");
                Console.WriteLine("3) Mitglied bearbeiten");
                Console.WriteLine("4) Mitglied löschen");
                Console.WriteLine("5) Zurück");
                Console.Write("Auswahl: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": membersService.ListMembers(); break;
                    case "2": membersService.AddMember(); break;
                    case "3": membersService.EditMember(); break;
                    case "4": membersService.DeleteMember(); break;
                    case "5": back = true; break;
                    default: Console.WriteLine("Ungültige Eingabe!"); break;
                }
            }
        }

        // Benutzer-Menü
        private static void ShowUserMenu()
        {
            // Nur Admin darf Benutzer verwalten:
            if (userService.LoggedInUser.Role != UserRole.Admin)
            {
                Console.WriteLine("Zugriff verweigert! Nur Admins dürfen Benutzer verwalten.");
                return;
            }

            bool back = false;

            while (!back)
            {
                Console.WriteLine("\n--- BENUTZER-MENÜ ---");
                Console.WriteLine("1) Benutzer anzeigen");
                Console.WriteLine("2) Benutzer hinzufügen");
                Console.WriteLine("3) Benutzer löschen");
                Console.WriteLine("4) Passwort ändern");
                Console.WriteLine("5) Zurück");
                Console.Write("Auswahl: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": userService.ListUsers(); break;
                    case "2": userService.AddUser(); break;
                    case "3": userService.DeleteUser(); break;
                    case "4": userService.ChangePassword(); break;
                    case "5": back = true; break;
                    default: Console.WriteLine("Ungültige Eingabe!"); break;
                }
            }
        }
    }
}

