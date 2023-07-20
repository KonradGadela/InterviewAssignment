using DemoImplementation;
using DemoSource;
using Xunit;
public class MapperTests
{
    [Fact]
    public void Flatten_Should_FlattenPersonsWithEmails_When_InputContainsEmails()
    {
        // Arrange
        var peopleWithEmails = new List<Person>
        {
            new Person
            {
                Id = "1",
                Name = "John",
                Emails = new List<EmailAddress>
                {
                    new EmailAddress { Email = "john@example.com", EmailType = "Work" },
                    new EmailAddress { Email = "john.doe@example.com", EmailType = "Personal" }
                }
            },
            new Person
            {
                Id = "2",
                Name = "Alice",
                Emails = new List<EmailAddress>
                {
                    new EmailAddress { Email = "alice@example.com", EmailType = "Work" },
                    new EmailAddress { Email = "alice.smith@example.com", EmailType = "Personal" }
                }
            }
        };

        // Act
        var result = Mapper.Flatten(peopleWithEmails);

        // Assert
        Assert.Equal(4, result.Count()); // Corrected the property access here

        // Check the first person's flattened email entries
        Assert.Equal("john1", result.ElementAt(0).SanitizedNameWithId);
        Assert.Equal("john@example.com (Work)", result.ElementAt(0).FormattedEmail);

        Assert.Equal("john1", result.ElementAt(1).SanitizedNameWithId);
        Assert.Equal("john.doe@example.com (Personal)", result.ElementAt(1).FormattedEmail);

        // Check the second person's flattened email entries
        Assert.Equal("alice2", result.ElementAt(2).SanitizedNameWithId);
        Assert.Equal("alice@example.com (Work)", result.ElementAt(2).FormattedEmail);

        Assert.Equal("alice2", result.ElementAt(3).SanitizedNameWithId);
        Assert.Equal("alice.smith@example.com (Personal)", result.ElementAt(3).FormattedEmail);
    }

    [Fact]
    public void Flatten_Should_HandlePersonsWithoutEmails_When_InputContainsPeopleWithoutEmails()
    {
        // Arrange
        var peopleWithoutEmails = new List<Person>
        {
            new Person
            {
                Id = "3",
                Name = "Bob",
                Emails = null // No email addresses
            },
            new Person
            {
                Id = "4",
                Name = "Eve",
                Emails = new List<EmailAddress>() // Empty email list
            }
        };

        // Act
        var result = Mapper.Flatten(peopleWithoutEmails);

        // Assert
        Assert.Equal(2, result.Count()); // Corrected the property access here

        // Check the first person without emails
        Assert.Equal("bob3", result.ElementAt(0).SanitizedNameWithId);
        Assert.Equal("No Email", result.ElementAt(0).FormattedEmail);

        // Check the second person with an empty email list
        Assert.Equal("eve4", result.ElementAt(1).SanitizedNameWithId);
        Assert.Equal("No Email", result.ElementAt(1).FormattedEmail);
    }
}
