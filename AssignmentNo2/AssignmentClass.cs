using System;
using System.Collections.Generic;
using System.Linq;
using AssignmentNo2.Models;

namespace DemoSource
{
    public class AssignmentClass
    {
        public IEnumerable<(Account, Person)> MatchPersonToAccount(
                                    IEnumerable<Group> groups,
                                    IEnumerable<Account> accounts,
                                    IEnumerable<string> emails)
        {
            ILookup<string, Person> emailsToPeopleAndAccounts = CreateEmailAddressToPersonLookup(groups);
            List<Account> filteredAccounts = FilterAccountByAdresses(accounts, emails);
            List<(Account account, Person person)> result = CombineAccountsWithPeople(emailsToPeopleAndAccounts, filteredAccounts);

            return result;
        }

        private static List<(Account account, Person person)> CombineAccountsWithPeople(ILookup<string, Person> emailsToPeopleAndAccounts, List<Account> filteredAccounts)
        {
            return filteredAccounts
                .SelectMany(account => emailsToPeopleAndAccounts[account.EmailAddress.Email.ToLower()]
                    .Select(person => (account, person)))
                .ToList();
        }

        private static List<Account> FilterAccountByAdresses(IEnumerable<Account> accounts, IEnumerable<string> emails)
        {
            return accounts
                .Where(account => emails.Contains(account.EmailAddress.Email.ToLower()))
                .ToList();
        }

        private static ILookup<string, Person> CreateEmailAddressToPersonLookup(IEnumerable<Group> groups)
        {
            return groups.SelectMany(g => g.People.SelectMany(p =>
                                                      p.Emails.Select(e => new
                                                      {
                                                          NormalizedEmail = e.Email.ToLower(),
                                                          Person = p
                                                      })))
                                                        .ToLookup(x => x.NormalizedEmail, y => y.Person);
        }
    }
}
