using System.Collections.ObjectModel;
using System.Diagnostics;
using Animals.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Animals.ViewModels;

public partial class AnimalsViewModel : ObservableObject
{
    private readonly PlayersStore _store;
    
    private Animal _selected;

    [ObservableProperty]
    private int _index;
    
    public AnimalsViewModel(PlayersStore store)
    {
        Debug.WriteLine("Create a new instance of ViewModel");
        
        _store = store;
        Animals = new ObservableCollection<PickAnimal>();
        
        _store.Players.Subscribe(players =>
        {
            Animals.Clear();
            
            Enum.GetValues<Animal>().Select(s => new PickAnimal
            {
                Animal = s,
                Available = s != Animal.None && !players.Where((player, i) => i != _index && player.Animal == s).Any(),
            }).ToList().ForEach(Animals.Add);
        });
    }
    
    public ObservableCollection<PickAnimal> Animals { get; }
    
    public Animal Selected
    {
        get => _selected;
        set
        {
            Debug.WriteLine($"Incoming value: {value}, Current value: {_selected}");
            
            if (!SetProperty(ref _selected, value)) return;
            
            _store.SetAnimal(_index, _selected);
        }
    }
}