using PetShelterDemo.DAL;

namespace PetShelterDemo.Domain;

public class PetShelter : Donations
{
    private readonly IRegistry<Pet> petRegistry;
    private readonly TitleRegistry<Fundraiser> fundRegistry;
    private readonly IRegistry<Person> donorRegistry;
    private Donations donations;

    public PetShelter()
    {
        donorRegistry = new Registry<Person>(new Database());
        petRegistry = new Registry<Pet>(new Database());
       fundRegistry = new Registry<Fundraiser>(new Database());
        donations = new Donations();
    }

    public void RegisterPet(Pet pet)
    {
        petRegistry.Register(pet);
    }

    public IReadOnlyList<Pet> GetAllPets()
    {
        return petRegistry.GetAll().Result; // Actually blocks thread until the result is available.
    }

    public void CreateFundraiser(Fundraiser fund)
    {
        fundRegistry.Register(fund);

    }

    public void DonateFundraiser(Person donor, int amount, string currency )
    {
        donorRegistry.Register(donor);
        donations.AddDonation(currency, amount);


    }


    public Pet GetByName(string name)
    {
        return petRegistry.GetByName(name).Result;
    }

    public Fundraiser GetByTitle(string title)
    {
        return fundRegistry.GetByTitle(title).Result;
    }

    public void Donate(Person donor, int amount, string currency)
    {
        donorRegistry.Register(donor);
        donations.AddDonation(currency, amount);
    }

    public int GetTotalDonationsInRON()
    {
        return donations.GetTotalDonations("RON");
    }

    public IReadOnlyList<Person> GetAllDonors()
    {
        return donorRegistry.GetAll().Result;
    }
    public IReadOnlyList<Fundraiser> GetAllFundraisers()
   {
        return fundRegistry.GetAll().Result; // Actually blocks thread until the result is available.
    }
}