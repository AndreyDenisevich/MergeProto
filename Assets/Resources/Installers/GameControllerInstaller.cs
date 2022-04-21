using UnityEngine;
using Zenject;

public class GameControllerInstaller : MonoInstaller
{
    [SerializeField]
    private GameController _gameControllerInstance;
    public override void InstallBindings()
    {
        Container.Bind<GameController>().FromInstance(_gameControllerInstance).AsSingle();
        Container.QueueForInject(_gameControllerInstance);
        _gameControllerInstance.container = Container;
    }
}