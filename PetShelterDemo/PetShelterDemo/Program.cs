//// See https://aka.ms/new-console-template for more information
//// Syntactic sugar: Starting with .Net 6, Program.cs only contains the code that is in the Main method.
//// This means we no longer need to write the following code, but the compiler still creates the Program class with the Main method:
//// namespace PetShelterDemo
//// {
////    internal class Program
////    {
////        static void Main(string[] args)
////        { actual code here }
////    }
//// }
///
//Expand the demo code by introducing the concept of a Fundraiser.
//A fundraiser is an event that aims to gather funds for a specific purpose (such as a particularly expensive medical need of a pet shelter resident).
//The Fundraiser should have a title, a description, a donation target, its own total donations, and a list of donors (Persons). 
//We should be able to create Fundraisers, and donate to them via the console interface.
//Fundraisers should be stored in our false Database object in the DAL.
//Bonus points: implement the Donations as a custom class that can handle multiple currencies (EUR, RON, etc.), and refactor the PetShelter to also use this same class.

using PetShelterDemo.DAL;
using PetShelterDemo.Domain;

var shelter = new PetShelter();

Console.WriteLine("Hello, Welcome the the Pet Shelter!");

var exit = false;
try
{
    while (!exit)
    {
        PresentOptions(
            "Here's what you can do.. ",
            new Dictionary<string, Action>
            {
                { "Register a newly rescued pet", RegisterPet },
                { "Donate", Donate },
                { "See current donations total", SeeDonations },
                { "See our residents", SeePets },
                { "Create a Fundraiser", CreateFundraiser },
                { "Donate to an existing Fundraiser", DonateToFundraiser },
                { "Break our database connection", BreakDatabaseConnection },
                { "Leave:(", Leave }
            }
        );
    }
}
catch (Exception e)
{
    Console.WriteLine($"Unfortunately we ran into an issue: {e.Message}.");
    Console.WriteLine("Please try again later.");
}

void CreateFundraiser()
{
    var title = ReadString("Title?");
    var description = ReadString("Description?");
    var target = ReadString("Target?");


    var Fund = new Fundraiser(title, description,target);

    shelter.CreateFundraiser(Fund);


}

void DonateToFundraiser()
{
    var funds = shelter.GetAllFundraisers();

    var fundOptions = new Dictionary<string, Action>();
    foreach (var fund in funds)
    {
        fundOptions.Add(fund.Title, () => SeeDonorDetailsByName(fund.Title));
    }

    PresentOptions("The existing Fundraisers are..", fundOptions);
    Console.WriteLine($"The existing Fundraisers are.. { fundOptions}");



    Console.WriteLine("What's your name? (So we can credit you.)");
    var name = ReadString();

    Console.WriteLine("What's your personal Id? (No, I don't know what GDPR is. Why do you ask?)");
    var id = ReadString();
    var person = new Person(name, id);

    Console.WriteLine("How much would you like to donate?");
    var amountInRon = ReadInteger();
    Console.WriteLine("In what currency is this?");
    string currency = ReadString();
    shelter.DonateFundraiser(person, amountInRon,currency);



}

void RegisterPet()
{
    var name = ReadString("Name?");
    var description = ReadString("Description?");

    var pet = new Pet(name, description);

    shelter.RegisterPet(pet);
}

void Donate()
{
    Console.WriteLine("What's your name? (So we can credit you.)");
    var name = ReadString();

    Console.WriteLine("What's your personal Id? (No, I don't know what GDPR is. Why do you ask?)");
    var id = ReadString();
    var person = new Person(name, id);

    Console.WriteLine("How much would you like to donate?");
    var amountInRon = ReadInteger();
    Console.WriteLine("In what currency is this?");
    string currency = ReadString();
    shelter.Donate(person, amountInRon, currency);
}

void SeeDonations()
{
    Console.WriteLine($"Our current donation total is {shelter.GetTotalDonationsInRON()}RON");
    Console.WriteLine("Special thanks to our donors:");
    var donors = shelter.GetAllDonors();
    foreach (var donor in donors)
    {
        Console.WriteLine(donor.Name);
    }
}

void SeePets()
{

    var pets = shelter.GetAllPets();

    var petOptions = new Dictionary<string, Action>();
    foreach (var pet in pets)
    {
        petOptions.Add(pet.Name, () => SeePetDetailsByName(pet.Name));
    }

    PresentOptions("We got..", petOptions);
}

void SeePetDetailsByName(string name)
{
    var pet = shelter.GetByName(name);
    Console.WriteLine($"A few words about {pet.Name}: {pet.Description}");
}

void SeeDonorDetailsByName(string name)
{
    var donor = shelter.GetByName(name);
    Console.WriteLine($"A few words about {donor.Name}: {donor.Description}");
}


void SeeFundDetailsByName(string title)
{
    var fund = shelter.GetByTitle(title);
    Console.WriteLine($"A few words about {fund.Title}: {fund.Description} with the target of {fund.Target}");
}

void BreakDatabaseConnection()
{
    Database.ConnectionIsDown = true;
}

void Leave()
{
    Console.WriteLine("Good bye!");
    exit = true;
}

void PresentOptions(string header, IDictionary<string, Action> options)
{

    Console.WriteLine(header);

    for (var index = 0; index < options.Count; index++)
    {
        Console.WriteLine(index + 1 + ". " + options.ElementAt(index).Key);
    }

    var userInput = ReadInteger(options.Count);

    options.ElementAt(userInput - 1).Value();
}

string ReadString(string? header = null)
{
    if (header != null) Console.WriteLine(header);

    var value = Console.ReadLine();
    Console.WriteLine("");
    return value;
}

int ReadInteger(int maxValue = int.MaxValue, string? header = null)
{
    if (header != null) Console.WriteLine(header);

    var isUserInputValid = int.TryParse(Console.ReadLine(), out var userInput);
    if (!isUserInputValid || userInput > maxValue)
    {
        Console.WriteLine("Invalid input");
        Console.WriteLine("");
        return ReadInteger(maxValue, header);
    }

    Console.WriteLine("");
    return userInput;
}