using UnityEngine;
using Zenject;

public class UIManagerInstaller : MonoInstaller
{
    [SerializeField]
    private UIManager _uiManagerInstance;
    public override void InstallBindings()
    {
        Container.Bind<UIManager>().FromInstance(_uiManagerInstance).AsSingle();
        Container.QueueForInject(_uiManagerInstance);
    }
}