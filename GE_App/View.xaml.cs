using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Reflection;

namespace GeoEditor
{
   public partial class View : Window
   {
      private List<GE_Tool.BaseTool> _tools = new();
      private List<GE_Command.BaseCommand> _commands = new();

      public View()
      {
         InitializeComponent();
         initializeTools();
         initializeCommands();
      }

      private void initializeTools()
      {
         _tools.AddRange(from type in Assembly.GetExecutingAssembly().GetTypes()
                         where type.IsDefined(typeof(GE_Tool.ToolAttribute), true)
                         let constructor = type.GetConstructor(Type.EmptyTypes)
                         let instance = constructor?.Invoke(Type.EmptyTypes)
                         where instance != null
                         select (GE_Tool.BaseTool)instance);
      }

      private void initializeCommands()
      {
         _commands.AddRange(from type in Assembly.GetExecutingAssembly().GetTypes()
                         where type.IsDefined(typeof(GE_Command.CommandAttribute), true)
                         let constructor = type.GetConstructor(Type.EmptyTypes)
                         let instance = constructor?.Invoke(Type.EmptyTypes)
                         where instance != null
                         select (GE_Command.BaseCommand)instance);
      }

      private void activateTool(GE_Tool.ToolID activeToolID)
      {
         foreach (GE_Tool.BaseTool tool in _tools)
         {
            if (tool.IsActive&& tool.ID != activeToolID)
               tool.OnDeActivate();
            else if (tool.ID == activeToolID && !tool.IsActive)
               tool.OnActivate();
         }
      }

      private void doCommand(GE_Command.CommandID commandID)
      {
         foreach (GE_Command.BaseCommand command in _commands)
         {
            if (command.ID == commandID)
               command.DoCommand();
         }
         activateTool(GE_Tool.ToolID.Default);
      }

      private void addSegmentToolButtonClick(object sender, RoutedEventArgs e)
      {
         activateTool(GE_Tool.ToolID.AddSegment);
      }

      private void defaultToolButtonClick(object sender, RoutedEventArgs e)
      {
         activateTool(GE_Tool.ToolID.Default);
      }

      private void moveToolButtonClick(object sender, RoutedEventArgs e)
      {
         activateTool(GE_Tool.ToolID.Move);
      }

      private void deleteCommandButtonClick(object sender, RoutedEventArgs e)
      {
         doCommand(GE_Command.CommandID.Delete);
      }

      private void zoomAllCommandButtonClick(object sender, RoutedEventArgs e)
      {
         doCommand(GE_Command.CommandID.ZoomAll);
      }

      private void SaveCommandButtonClick(object sender, RoutedEventArgs e)
      {
         JsonUtils.Write();
      }

      private void LoadCommandButtonClick(object sender, RoutedEventArgs e)
      {

      }
   }
}
