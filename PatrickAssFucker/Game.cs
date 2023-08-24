using HxLocal;
using PatrickAssFucker.Areas;
using PatrickAssFucker.Commands;
using Spectre.Console;
using System.Diagnostics;
using PatrickAssFucker.Managers;

namespace PatrickAssFucker
{
    public class Game
    {

        private Dictionary<string, ICommand> _commands = new Dictionary<string, ICommand>();

        public Stopwatch Heart;
        public bool IsRunning;

        public Game()
        {
            Heart = new Stopwatch();
            Localisation.FilePath = "Assets";
            var language = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Before you can start, choose a language?")
                    .PageSize(10)
                    .MoreChoicesText("")
                    .AddChoices(Localisation.ListAvailableLanguages()));

            Localisation.LoadLanguage(language);

            _commands.Add("look around", new LookCommand());
            _commands.Add("nehmen", new TakeCommand());
            _commands.Add("status", new StatsCommand());
            _commands.Add("ablegen", new PlaceDownCommand());
            _commands.Add("untersuchen", new InspectCommand());
            _commands.Add("gehen", new MoveCommand());
            _commands.Add("sichern", new SaveCommand());
            _commands.Add("laden", new LoadCommand());
            _commands.Add("aufträge", new QuestBookCommand());
            _commands.Add("reden", new TalkCommand());
#if DEBUG
            _commands.Add("exp", new ExpCommand());
            _commands.Add("lingo", new LanguageCommand());
            _commands.Add("heal", new HealCommand());
#endif
        }

        private void Init()
        {
            AreaManager.Instance.Add(new City());
            AreaManager.Instance.Add(new Stillbach());

            Brain.Instance.Player.MoveTo(AreaIdentifier.Stillbach);
        }

        public void Run()
        {
            IsRunning = true;
            Heart.Start();
            Init();
            
            while (IsRunning)
            {
                string input = AnsiConsole.Ask<string>("> ").ToLower();
                //AnsiConsole.Clear();
                input = System.Text.RegularExpressions.Regex.Replace(input.Trim(), @"\s+", " ").ToLower(); // Diese Zeile ändert den String entsprechend deinen Wünschen
                /*string[] parts = input.Split(' ');
                string commandKey = parts[0];
                if (_commands.ContainsKey(commandKey))
                {
                    _commands[commandKey].Execute(parts[1..]);
                }
                else
                {
                    AnsiConsole.WriteLine("Unbekannter Befehl.");
                }*/
                var commandFound = false;
                foreach (var command in _commands)
                {
                    if (!input.StartsWith(command.Key)) continue;
                    string argumentString = input.Substring(command.Key.Length).Trim();
                    string[] arguments = argumentString.Length > 0 ? argumentString.Split(' ') : new string[0];
                    var cmd = command.Value;
                    cmd.Execute(arguments);
                    commandFound = true;
                    break;
                }

                if (!commandFound)
                {
                    AnsiConsole.WriteLine("Unbekannter Befehl.");
                }
                //Brain.Instance.Update();
            }
        }
    }
}
