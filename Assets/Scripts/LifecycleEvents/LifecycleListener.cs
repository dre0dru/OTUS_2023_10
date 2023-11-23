namespace LifecycleEvents
{
    public interface ILifecycleListener
    {
    }

    public interface IStartListener : ILifecycleListener
    {
        void OnStartGame();
    }

    public interface IFinishListener : ILifecycleListener
    {
        void OnFinishGame();
    }

    public interface IPauseListener : ILifecycleListener
    {
        void OnPauseGame();
    }

    public interface IResumeListener : ILifecycleListener
    {
        void OnResumeGame();
    }

    public interface IUpdateListener : ILifecycleListener
    {
        void OnUpdate(float deltaTime);
    }

    public interface IFixedUpdateListener : ILifecycleListener
    {
        void OnFixedUpdate(float deltaTime);
    }

    public interface ILateUpdateListener : ILifecycleListener
    {
        void OnLateUpdate(float deltaTime);
    }
}
