namespace GE_Command
{
   public class CommandAttribute : System.Attribute
   { }

   enum CommandID
   {
      Delete,
      ZoomAll,
   }

   abstract class BaseCommand
   {
      public BaseCommand() { }
      public abstract CommandID ID { get; }
      protected GE_ViewModel.DeskViewModel VM => GE_ViewModel.DeskViewModel.Instance;

      public abstract void DoCommand();
   }
}
