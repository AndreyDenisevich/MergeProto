using UnityEngine;
using Zenject;

public class PoolManagerInstaller : MonoInstaller
{
    [SerializeField]
    private PoolManager _poolManagerInstance;
    public override void InstallBindings()
    {
        Container.Bind<PoolManager>().FromInstance(_poolManagerInstance).AsSingle();
        Container.QueueForInject(_poolManagerInstance);
    }
}