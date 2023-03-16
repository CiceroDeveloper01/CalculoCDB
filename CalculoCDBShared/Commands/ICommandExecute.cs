namespace CalculoCDBShared.Commands
{
    public interface ICommandExecute<T> where T : ICommand
    {
        ICommandResult Execute(T command);
    }
}