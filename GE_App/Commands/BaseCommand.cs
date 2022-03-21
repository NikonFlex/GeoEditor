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

      public abstract void DoCommand();
   }
}
