using System;                                // Grundlegende Funktionen wie Konsole, Datum, Text usw.
using System.Collections.Generic;             // Ermöglicht Listen (List<T>)
using System.Linq;                            // Ermöglicht Filtern, Sortieren, Gruppieren (LINQ)

namespace FitMaster                           // Übergeordneter Namensraum für das gesamte Projekt
{
    // ----------------------------------------
    // MODEL: MITGLIED
    // ----------------------------------------

    public class Member                       // Klasse "Member" = Bauplan für ein Mitglied
    {
        public int Id { get; set; }           // Eindeutige Mitgliedsnummer
        public string Name { get; set; }      // Name des Mitglieds
        public string Tarif { get; set; }     // Tarif (Basic, Premium, VIP)
        public bool IsActive { get; set; }    // Aktivstatus (true/false)

        public string GetInfo()               // Gibt alle Mitgliedsdaten als Text zurück
        {
            return $"ID: {Id}, Name: {Name}, Tarif: {Tarif}, Aktiv: {IsActive}";
        }
    }

    // ----------------------------------------
    // MODEL: ANWESENHEIT
    // ----------------------------------------

    public class Attendance                    // Klasse für Anwesenheitseinträge
    {
        public int MemberId { get; set; }     // ID des Mitglieds, das eingecheckt hat
        public DateTime Date { get; set; }    // Datum und Uhrzeit des Check-ins
    }

    // ----------------------------------------
    // ENUM: BENUTZERROLLEN
    // ----------------------------------------

    public enum UserRole                      // Rollen für Zugriffskontrolle
    {
        Admin,                                 // Darf alles (Benutzer verwalten)
        Mitarbeiter                            // Darf Mitglieder & Anwesenheit, aber keine Benutzer löschen
    }

    // ----------------------------------------
    // MODEL: BENUTZER
    // ----------------------------------------

    public class User                          // Klasse für Login-Benutzer
    {
        public string Username { get; set; }   // Benutzername
        public string Password { get; set; }   // Passwort
        public UserRole Role { get; set; }     // Rolle (Admin oder Mitarbeiter)
    }

    // ----------------------------------------
    // SERVICE: BENUTZER & LOGIN
    // ----------------------------------------

    public class UserService
    {
        private List<User> users = new List<User>();   // Liste aller Benutzer
        public User LoggedInUser { get; private set; } // Speichert, wer aktuell eingeloggt ist

        public UserService()
        {
            // Beispiel-Benutzer
            users.Add(new User { Username = "johannes", Password = "1234", Role = UserRole.Admin });
            users.Add(new User { Username = "mitarbeiter", Password = "0000", Role = UserRole.Mitarbeiter });
        }

        public bool Login()                   // Login-Funktion
        {
            Console.WriteLine("\n--- Login ---");

            Console.Write("Benutzername: ");
            string username = Console.ReadLine();  // Benutzername eingeben

            Console.Write("Passwort: ");
            string pw = Console.ReadLine();        // Passwort eingeben

            foreach (var u in users)               // Benutzer in Liste durchsuchen
            {
                if (u.Username == username && u.Password == pw)
                {
                    LoggedInUser = u;             // Speichern, wer eingeloggt ist
                    Console.WriteLine($"Login erfolgreich! Eingeloggt als: {u.Username} ({u.Role})");
                    return true;
                }
            }

            Console.WriteLine("Login fehlgeschlagen!");
            return false;
        }

        public void ListUsers()                  // Zeigt alle Benutzer an
        {
            Console.WriteLine("\n--- Benutzerliste ---");
            foreach (var u in users)
                Console.WriteLine($"{u.Username} ({u.Role})");
        }

        public void AddUser()                     // Neuen Benutzer anlegen
        {
            Console.WriteLine("\n--- Benutzer hinzufügen ---");

            Console.Write("Benutzername: ");
            string name = Console.ReadLine();

            Console.Write("Passwort: ");
            string pw = Console.ReadLine();

            Console.Write("Rolle (Admin/Mitarbeiter): ");
            string r = Console.ReadLine().ToLower();

            UserRole role = r == "admin" ? UserRole.Admin : UserRole.Mitarbeiter;

            users.Add(new User { Username = name, Password = pw, Role = role });

            Console.WriteLine("Benutzer erfolgreich angelegt!");
        }

        public void DeleteUser()                 // Benutzer löschen
        {
            Console.WriteLine("\n--- Benutzer löschen ---");

            Console.Write("Benutzername: ");
            string name = Console.ReadLine();

            var u = users.Find(x => x.Username == name);

            if (u == null)
            {
                Console.WriteLine("Benutzer nicht gefunden!");
                return;
            }

            if (u == LoggedInUser)
            {
                Console.WriteLine("Du kannst dich nicht selbst löschen!");
                return;
            }

            users.Remove(u);
            Console.WriteLine("Benutzer gelöscht!");
        }

        public void ChangePassword()             // Passwort ändern
        {
            Console.WriteLine("\n--- Passwort ändern ---");

            Console.Write("Benutzername: ");
            string name = Console.ReadLine();

            var u = users.Find(x => x.Username == name);

            if (u == null)
            {
                Console.WriteLine("Benutzer nicht gefunden!");
                return;
            }

            Console.Write("Neues Passwort: ");
            u.Password = Console.ReadLine();

            Console.WriteLine("Passwort geändert!");
        }
    }

    // ----------------------------------------
    // SERVICE: MITGLIEDERVERWALTUNG
    // ----------------------------------------

    public class MembersService
    {
        private List<Member> members;     // Alle Mitglieder
        private int nextId = 2;           // Automatische ID-Vergabe

        public MembersService()
        {
            members = new List<Member>(); // Liste erstellen

            // Beispielmitglied
            members.Add(new Member { Id = 1, Name = "Max Mustermann", Tarif = "Basic", IsActive = true });
        }

        public List<Member> GetMembers() => members;  // Gibt Liste zurück

        public void ListMembers()                     // Alle Mitglieder anzeigen
        {
            Console.WriteLine("\n--- Mitgliederliste ---");

            foreach (var m in members)
                Console.WriteLine(m.GetInfo());
        }

        public void AddMember()                       // Neues Mitglied
        {
            Console.WriteLine("\n--- Neues Mitglied hinzufügen ---");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Tarif (Basic/Premium/VIP): ");
            string tarif = Console.ReadLine();

            members.Add(new Member                     // Neues Objekt anlegen
            {
                Id = nextId++,
                Name = name,
                Tarif = tarif,
                IsActive = true
            });

            Console.WriteLine("Mitglied erfolgreich hinzugefügt!");
        }

        public void EditMember()                      // Mitglied bearbeiten
        {
            Console.WriteLine("\n--- Mitglied bearbeiten ---");
            Console.Write("Mitglied-ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ungültige ID!");
                return;
            }

            var m = members.Find(x => x.Id == id);    // Mitglied suchen

            if (m == null)
            {
                Console.WriteLine("Mitglied nicht gefunden!");
                return;
            }

            // Neuer Name
            Console.Write("Neuer Name (leer = keine Änderung): ");
            string newName = Console.ReadLine();
            if (newName != "") m.Name = newName;

            // Neuer Tarif
            Console.Write("Neuer Tarif (leer = keine Änderung): ");
            string newTarif = Console.ReadLine();
            if (newTarif != "") m.Tarif = newTarif;

            // Aktivstatus ändern
            Console.Write("Aktiv? (j/n): ");
            string a = Console.ReadLine();
            if (a == "j") m.IsActive = true;
            if (a == "n") m.IsActive = false;

            Console.WriteLine("Mitglied aktualisiert!");
        }

        public void DeleteMember()                   // Mitglied löschen
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
            Console.WriteLine("Mitglied gelöscht!");
        }
    }

    // ----------------------------------------
    // SERVICE: ANWESENHEIT / STATISTIKEN
    // ----------------------------------------

    public class AttendanceService
    {
        private List<Attendance> attendance = new List<Attendance>(); // Alle Check-ins
        private MembersService membersService;                        // Zugriff auf Mitglieder

        public AttendanceService(MembersService mem)
        {
            membersService = mem;
        }

        public void CheckIn()                         // Check-in eines Mitglieds
        {
            Console.WriteLine("\n--- Anwesenheit erfassen ---");

            Console.Write("Mitglied-ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ungültige ID!");
                return;
            }

            var member = membersService.GetMembers().Find(m => m.Id == id);

            if (member == null)
            {
                Console.WriteLine("Mitglied nicht gefunden!");
                return;
            }

            attendance.Add(new Attendance                // Check-in speichern
            {
                MemberId = id,
                Date = DateTime.Now
            });

            Console.WriteLine($"{member.Name} wurde eingecheckt!");
        }

        public void ShowAttendance()                     // Alle Check-ins anzeigen
        {
            Console.WriteLine("\n--- Anwesenheitsliste ---");

            foreach (var a in attendance)
            {
                var m = membersService.GetMembers().First(x => x.Id == a.MemberId);

                Console.WriteLine($"{m.Name} | {a.Date}");
            }
        }

        public void ShowDailyStats()                    // Zeigt Check-ins von heute
        {
            Console.WriteLine("\n--- Tagesstatistik ---");

            int count = attendance.Count(a => a.Date.Date == DateTime.Today);

            Console.WriteLine($"Heutige Check-ins: {count}");
        }

        public void ShowMemberStats()                   // Statistiken über Mitglieder
        {
            Console.WriteLine("\n--- Mitgliederstatistik ---");

            var members = membersService.GetMembers();

            int active = members.Count(m => m.IsActive);
            int inactive = members.Count(m => !m.IsActive);

            Console.WriteLine($"Aktive Mitglieder: {active}");
            Console.WriteLine($"Inaktive Mitglieder: {inactive}");

            Console.WriteLine("\nMitglieder pro Tarif:");
            var groups = members.GroupBy(m => m.Tarif);

            foreach (var g in groups)
            {
                Console.WriteLine($"{g.Key}: {g.Count()} Mitglieder");
            }
        }
    }

    // ----------------------------------------
    // HAUPTPROGRAMM / MENÜS
    // ----------------------------------------

    public class Program
    {
        private static MembersService membersService;        // Für Mitgliederverwaltung
        private static UserService userService;              // Für Login & Benutzerverwaltung
        private static AttendanceService attendanceService;  // Für Anwesenheit & Statistik

        public static void Main(string[] args)
        {
            membersService = new MembersService();           // Objekte erstellen
            userService = new UserService();
            attendanceService = new AttendanceService(membersService);

            Console.Title = "FitMaster – Verwaltungssystem"; // Fenstertitel

            ShowWelcome();                                   // Begrüßung

            while (!userService.Login()) { }                 // Login bis erfolgreich

            MainMenu();                                      // Hauptmenü starten
        }

        private static void ShowWelcome()                    // Begrüßungstext
        {
            Console.WriteLine("=======================================");
            Console.WriteLine("          FitMaster System");
            Console.WriteLine("=======================================\n");
        }

        // -----------------------
        // HAUPTMENÜ
        // -----------------------

        private static void MainMenu()
        {
            bool run = true;

            while (run)
            {
                Console.WriteLine("\n--- HAUPTMENÜ ---");
                Console.WriteLine("1) Mitglieder verwalten");
                Console.WriteLine("2) Anwesenheit erfassen");
                Console.WriteLine("3) Statistiken anzeigen");
                Console.WriteLine("4) Benutzer verwalten");
                Console.WriteLine("5) Beenden");
                Console.Write("Auswahl: ");

                switch (Console.ReadLine())
                {
                    case "1": MembersMenu(); break;
                    case "2": AttendanceMenu(); break;
                    case "3": StatsMenu(); break;
                    case "4": UserMenu(); break;
                    case "5": run = false; break;
                    default: Console.WriteLine("Ungültige Eingabe!"); break;
                }
            }
        }

        // -----------------------
        // MITGLIEDER-MENÜ
        // -----------------------

        private static void MembersMenu()
        {
            bool back = false;

            while (!back)
            {
                Console.WriteLine("\n--- MITGLIEDER-MENÜ ---");
                Console.WriteLine("1) Liste anzeigen");
                Console.WriteLine("2) Mitglied hinzufügen");
                Console.WriteLine("3) Mitglied bearbeiten");
                Console.WriteLine("4) Mitglied löschen");
                Console.WriteLine("5) Zurück");

                switch (Console.ReadLine())
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

        // -----------------------
        // ANWESENHEITS-MENÜ
        // -----------------------

        private static void AttendanceMenu()
        {
            bool back = false;

            while (!back)
            {
                Console.WriteLine("\n--- ANWESENHEIT ---");
                Console.WriteLine("1) Check-in erfassen");
                Console.WriteLine("2) Anwesenheitsliste anzeigen");
                Console.WriteLine("3) Heute-Statistik");
                Console.WriteLine("4) Zurück");

                switch (Console.ReadLine())
                {
                    case "1": attendanceService.CheckIn(); break;
                    case "2": attendanceService.ShowAttendance(); break;
                    case "3": attendanceService.ShowDailyStats(); break;
                    case "4": back = true; break;
                    default: Console.WriteLine("Ungültige Eingabe!"); break;
                }
            }
        }

        // -----------------------
        // STATISTIK-MENÜ
        // -----------------------

        private static void StatsMenu()
        {
            bool back = false;

            while (!back)
            {
                Console.WriteLine("\n--- STATISTIKEN ---");
                Console.WriteLine("1) Mitglieder-Statistik");
                Console.WriteLine("2) Zurück");

                switch (Console.ReadLine())
                {
                    case "1": attendanceService.ShowMemberStats(); break;
                    case "2": back = true; break;
                    default: Console.WriteLine("Ungültige Eingabe!"); break;
                }
            }
        }

        // -----------------------
        // BENUTZER-MENÜ
        // -----------------------

        private static void UserMenu()
        {
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

                switch (Console.ReadLine())
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
