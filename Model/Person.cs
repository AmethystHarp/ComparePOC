namespace ComparePOC.Model;

public class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    public Address? CurrentAddress { get; set; }
    public Address? MailingAddress { get; set; }

    public Person() { }
}
