using System.Collections.ObjectModel;
using Animals.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Animals.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    private static readonly string[] Names = {
        "First player",
        "Second player",
        "Third player",
        "Fourth player",
        "Fifth player",
        "Sixth player",
    };

    public MainPageViewModel(PlayersStore store)
    {
        Players = new ObservableCollection<PickPlayer>();
        store.Players.Subscribe(players =>
        {
            Players.Clear();
            players.Select((player, i) => new PickPlayer
            {
                Index = i,
                Animal = player.Animal,
                Name = Names[i],
            }).ToList().ForEach(Players.Add);
        });
    }
    
    public ObservableCollection<PickPlayer> Players { get; }
    
    [RelayCommand]
    public Task SelectAnimalAsync(PickPlayer item)
    {
        return Shell.Current.GoToAsync($"{nameof(AnimalsPage)}?i={item.Index}");
    }
}