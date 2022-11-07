using System.Collections.ObjectModel;
using Animals.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Animals.ViewModels;

[QueryProperty("Index", "i")]
public partial class AnimalsViewModel : ObservableObject
{
    private readonly PlayersStore _store;

    [ObservableProperty]
    private int _index;

    private Animal _selected = Animal.None;
    
    public AnimalsViewModel(PlayersStore store)
    {
        _store = store;
        Animals = new ObservableCollection<PickAnimal>();

        _store
            .Players
            .Subscribe(players =>
            {
                Animals.Clear();

                Enum.GetValues<Animal>().Select(s => new PickAnimal
                {
                    Animal = s,
                    Available = s != Animal.None &&
                                !players.Where((player, i) => i != _index && player.Animal == s).Any(),
                    Selected = s == _selected,
                }).ToList().ForEach(Animals.Add);
            });
    }
    
    public ObservableCollection<PickAnimal> Animals { get; }

    [RelayCommand]
    public void HandleChange(PickAnimal pick)
    {
        if (pick.Animal == _selected || !pick.Selected) return;

        _selected = pick.Animal;
        _store.SetAnimal(_index, _selected);
    }
}