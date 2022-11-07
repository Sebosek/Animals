using System.Reactive.Linq;
using System.Reactive.Subjects;
using Animals.Models;

namespace Animals;

public class PlayersStore
{
    private readonly BehaviorSubject<List<Player>> _players = new(Enumerable.Range(0, 6).Select(_ => new Player()).ToList());

    public IObservable<IReadOnlyCollection<Player>> Players => _players.AsObservable();

    public void SetAnimal(int i, Animal animal)
    {
        _players
            .Take(1)
            .Subscribe(players =>
            {
                var data = players.Select((player, index) =>
                {
                    if (index != i) return player;
        
                    player.Animal = animal;
                    return player;
                }).ToList();
        
                _players.OnNext(data);
            });
    }
}