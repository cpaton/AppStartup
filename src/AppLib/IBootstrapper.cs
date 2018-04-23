namespace AppLib
{
    /// <summary>
    /// A bootstrapper is responsible for initialising an application and creating its instance that can be run
    /// </summary>
    public interface IBootstrapper
    {
        IApplication Bootstrap();
    }
}