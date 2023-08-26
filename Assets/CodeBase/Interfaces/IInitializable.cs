public interface IInitializable
{
    void Initialize();
}

public interface IInitializableComponents<T>
{
    void InitializeComponent(T component);
}
