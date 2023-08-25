using HxLocal;
using PatrickAssFucker.Areas;
using PatrickAssFucker.Commands;
using Spectre.Console;
using System.Diagnostics;
using PatrickAssFucker.Managers;
using PatrickAssFucker.Audio;

namespace PatrickAssFucker
{
    public class Game
    {

        private Dictionary<string, ICommand> _commands = new();

        public Stopwatch Heart;
        public bool IsRunning;

        public Game()
        {
            Heart = new Stopwatch();
            Localisation.OnLanguageChange += UpdateCommands;

            Localisation.FilePath = "Assets";
            var language = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Before you can start, choose a language?")
                    .PageSize(10)
                    .MoreChoicesText("")
                    .AddChoices(Localisation.ListAvailableLanguages()));

            Localisation.LoadLanguage(language);
        }

        private void UpdateCommands(object? sender, LanguageChangedEventArgs e)
        {
            _commands.Clear();
            _commands.Add(Localisation.GetString("commands.look.name"), new LookCommand());
            _commands.Add(Localisation.GetString("commands.take.name"), new TakeCommand());
            _commands.Add(Localisation.GetString("commands.stats.name"), new StatsCommand());
            _commands.Add(Localisation.GetString("commands.place.name"), new PlaceDownCommand());
            _commands.Add(Localisation.GetString("commands.inspect.name"), new InspectCommand());
            _commands.Add(Localisation.GetString("commands.move.name"), new MoveCommand());
            _commands.Add(Localisation.GetString("commands.save.name"), new SaveCommand());
            _commands.Add(Localisation.GetString("commands.load.name"), new LoadCommand());
            _commands.Add(Localisation.GetString("commands.quests.name"), new QuestBookCommand());
            _commands.Add(Localisation.GetString("commands.talk.name"), new TalkCommand());
#if DEBUG   
            _commands.Add("exp", new ExpCommand());
            _commands.Add("lingo", new LanguageCommand());
            _commands.Add("heal", new HealCommand());
#endif
#if DEBUG
            AnsiConsole.MarkupLine("[bold red]Debug[/] [red]> Language has changed from[/] [bold white]" + e.PrevLanguage + "[/] [red]to[/] [white bold]" + e.CurrLanguage + "[/]");
#endif
        }

        private void Init()
        {
            AreaManager.Instance.Add(new City());
            AreaManager.Instance.Add(new Stillbach());
            AreaManager.Instance.Add(new Tannenhain());

            var stillbach = AreaManager.Instance.GetAreaById(AreaIdentifier.Stillbach)!;
            var tannenhain = AreaManager.Instance.GetAreaById(AreaIdentifier.Tannenhain)!;
            var forestPath = AreaManager.Instance.GetAreaById(AreaIdentifier.Tannenhain_ForestPathRiver)!;
            
            //Area.Link(stillbach, tannenhain);
            forestPath.TunnelLink(stillbach, tannenhain);
            
            Brain.Instance.Player.MoveTo(AreaIdentifier.Stillbach);

            //AudioInstance instance = new AudioInstance("Assets/theme.wav", true, 0.1f);
            //instance.Play();
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
                input = System.Text.RegularExpressions.Regex.Replace(input.Trim(), @"\s+", " ").ToLower();
                var commandFound = false;
                foreach (var command in _commands)
                {
                    var key = command.Key;
                    if (!input.StartsWith(key)) continue;
                    string argumentString = input.Substring(key.Length).Trim();
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
