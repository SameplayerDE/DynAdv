using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatrickAssFucker.Managers
{
    internal class GameStateSaver
    {

        public string GlobalPath;

        public static GameStateSaver Instance { get; } = new();

        static GameStateSaver()
        {

        }

        private GameStateSaver()
        {
            EnsurePathExists();
        }

        private void EnsurePathExists()
        {
            GlobalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".assFucker");
            if (!Directory.Exists(GlobalPath))
            {
                Directory.CreateDirectory(GlobalPath);
                return;
            }
        }

        public void Save(GameState state, string keyWord = "default")
        {
            var path = Path.Combine(GlobalPath, keyWord);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //var storyProgressJson = JsonConvert.SerializeObject(StoryProgress.Instance, Formatting.Indented);
            var gameStateJson = JsonConvert.SerializeObject(state, Formatting.Indented);

        }
    }
}
