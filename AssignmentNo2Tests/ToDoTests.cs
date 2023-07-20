using AssignmentNo2.Models;
using Xunit;

namespace DemoSource.Tests
{
    public class ToDoTests
    {
        [Fact]
        public void MatchPersonToAccount_ShouldReturnExpectedMappings()
        {
            // Arrange
            var person1 = new Person
            {
                Id = "1",
                Name = "John Doe",
                Emails = new List<EmailAddress>
                {
                    new EmailAddress { Email = "john.doe@example.com", EmailType = "Personal" },
                    new EmailAddress { Email = "jdoe@example.com", EmailType = "Work" }
                }
            };

            var person2 = new Person
            {
                Id = "2",
                Name = "Jane Smith",
                Emails = new List<EmailAddress>
                {
                    new EmailAddress { Email = "jane.smith@example.com", EmailType = "Personal" },
                    new EmailAddress { Email = "jsmith@example.com", EmailType = "Work" }
                }
            };

            var account1 = new Account
            {
                Id = "101",
                EmailAddress = new EmailAddress { Email = "john.doe@example.com", EmailType = "Personal" }
            };

            var account2 = new Account
            {
                Id = "102",
                EmailAddress = new EmailAddress { Email = "jane.smith@example.com", EmailType = "Personal" }
            };

            var account3 = new Account
            {
                Id = "201",
                EmailAddress = new EmailAddress { Email = "jdoe@example.com", EmailType = "Work" }
            };

            var groups = new List<Group>
            {
                new Group { Id = "g1", Label = "Group 1", People = new List<Person> { person1 } },
                new Group { Id = "g2", Label = "Group 2", People = new List<Person> { person2 } }
            };

            var accounts = new List<Account> { account1, account2, account3 };

            var emails = new List<string>
            {
                "john.doe@example.com",
                "jane.smith@example.com",
                "jdoe@example.com"
            };

            var toDo = new AssignmentClass();

            // Act
            var result = toDo.MatchPersonToAccount(groups, accounts, emails);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());

            Assert.Contains((account1, person1), result);
            Assert.Contains((account2, person2), result);
            Assert.Contains((account3, person1), result);
        }
    }
}
