namespace GE_Command
{
   public class CommandAttribute : System.Attribute
   { }

   enum CommandID
   {
      Delete,
   }

   abstract class BaseCommand
   {
      public BaseCommand() { }
      public abstract CommandID GetID();
      public abstract void DoCommand();
   }
}
