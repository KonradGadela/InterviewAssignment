using DemoTarget;

namespace DemoImplementation
{
    public class Mapper
    {
        public static IEnumerable<PersonWithEmail> Flatten(IEnumerable<DemoSource.Person> people)
        {
            return people.SelectMany(person => FlattenPerson(person));
            //PRZYPADKI UZYCIA
            //Prezentacja w interfejsach uzytkownika
            //Przygotowanie danych do zapisu w bazie (Jesli wymaga tego struktura bazy)
            //Integracja z zewnetrznymi serwisami 

            //KONSEKWENCJE
            //Redundancja danych: Spłaszczona lista moze powodowac redundancje, jeśli wielokrotnie powtarzają się dane użytkownika 
            //Jeśli mamy duza liczbe użytkownikow z wieloma adresami email, splaszczanie kolekcji może wpłynąć na wydajność aplikacji.
            //Utrata informacji o hierarchii danych.
        }

        private static IEnumerable<PersonWithEmail> FlattenPerson(DemoSource.Person person)
        {
            if (person.Emails != null && person.Emails.Any())
            {
                return person.Emails.Select(email => new PersonWithEmail
                {
                    SanitizedNameWithId = SanitizeNameWithId(person.Name, person.Id),
                    FormattedEmail = FormatEmail(email.Email, email.EmailType)
                });
            }
            else
            {
                return new List<PersonWithEmail>
                {
                    new PersonWithEmail
                    {
                        SanitizedNameWithId = SanitizeNameWithId(person.Name, person.Id),
                        FormattedEmail = "No Email"
                    }
                };
            }
        }

        private static string SanitizeNameWithId(string name, string id)
        {
            string sanitized = new string(name.Where(char.IsLetterOrDigit).ToArray()).ToLower();
            return sanitized + id;
        }

        private static string FormatEmail(string email, string emailType)
        {
            return $"{email} ({emailType})";
        }
    }
}

