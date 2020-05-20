using Newtonsoft.Json;
using NModbus.Tools.Base.Model;
using NModbus.Tools.Base.View;
using NModbus.Tools.Base.ViewModel;
using NModbus.Tools.Interfaces;
using System;
using System.IO;
using System.Windows;

namespace NModbus.Tools.Base
{
    public class ConnectionSelectionService : IConnectionSelectionService
    {
        private static readonly string FilePath;

        static ConnectionSelectionService()
        {
            FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NModbus Tools", "connections.json");
        }

        public Connection GetConnection()
        {
            var connections = GetConnections();

            var viewModel = new ConnectionSelectionViewModel(connections);

            var view = new ConnectionSelectionDialog
            {
                DataContext = viewModel
            };

            if (view.ShowDialog() == true)
            {
                SaveConnections(viewModel.ToModel());

                return viewModel.SelectedConnection.ToModel();
            }
            
            return null;
        }

        private void SaveConnections(SavedConnections connections)
        {
            var json = JsonConvert.SerializeObject(connections);

            var directory = Path.GetDirectoryName(FilePath);

            Directory.CreateDirectory(directory);

            File.WriteAllText(FilePath, json);
        }

        private SavedConnections GetConnections()
        {
            try
            {
                var json = File.ReadAllText(FilePath);

                var connections = JsonConvert.DeserializeObject<SavedConnections>(json);

                if (connections.Connections == null)
                {
                    connections.Connections = new Connection[] { };
                }

                foreach(var connection in connections.Connections)
                {
                    if (connection.ConnectionTimeout <= 0)
                    {
                        connection.ConnectionTimeout = 5000;
                    }
                }

                return connections;
            }
            catch(Exception)
            {
                return new SavedConnections
                {
                    Connections = new Connection[] { }
                };
            }

        }
    }
}
