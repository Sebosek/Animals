namespace Animals.Models;

public class PickAnimal
{
    public Animal Animal { get; set; }

    public bool Available { get; set; }
    
    public bool Selected { get; set; }
}