using NAudio.Wave;
using PatrickAssFucker.Audio;
using Spectre.Console;

namespace PatrickAssFucker
{
    public class Brain
    {
        public Player Player;

        public Queue<GameMessage> Events;
        private BackgroundAudioPlayer _backgroundPlayer = new BackgroundAudioPlayer();

        public static Brain Instance { get; } = new Brain();

        static Brain()
        {
        }

        private Brain()
        {
            Player = new Player();
            Player.Health = 32;

            Events = new Queue<GameMessage>();
        }

        public void StartBackgroundSound(string path, bool loop = true)
        {
            _backgroundPlayer.Play(path, loop);
        }

        public void StopBackgroundSound()
        {
            _backgroundPlayer.Stop();
        }

        public void QueueEvent(GameMessage @event)
        {
            Events.Enqueue(@event);
        }

        public void Update()
        {
            while (Events.Count > 0)
            {
                var @event = Events.Dequeue();

                if (@event is PlayerLevelUpMessage message)
                {
                    ShowLevelUpNotification(message);
                }
            }
            Player.QuestBook.CheckQuestCompletion();
        }

        private void PlayLevelUpSound()
        {
            using var audioFile = new AudioFileReader("Assets/rain.wav");
            using var outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.Play();
           //while (outputDevice.PlaybackState == PlaybackState.Playing)
           //{
           //    //Thread.Sleep(100);
           //}
        }

        private void ShowLevelUpNotification(PlayerLevelUpMessage message)
        {

            PlayLevelUpSound();

            var panel = new Panel("[yellow]Du hast Stufe[/] [cyan]" + message.Level + "[/] [yellow]erreicht.[/]")
            {
                Header = new PanelHeader("Stufenauftieg", Justify.Center)
            };
            
            AnsiConsole.Write(panel);
            Thread.Sleep(2000);  // Die Nachricht 2 Sekunden anzeigen

            var table = new Table()
                .AddColumn(new TableColumn("[yellow]Attribut[/]"))
                .AddColumn(new TableColumn("[yellow]Wert[/]"));

            AnsiConsole.Live(table).Start(ctx =>
            {
                // Phase 1: Aktuelle Werte anzeigen
                table.AddRow("[yellow]Lebenspunkte[/]", $"{Player.Health}");
                table.AddRow("[yellow]Stärke[/]", $"{Player.Strength}");
                table.AddRow("[yellow]Verteidigung[/]", $"{Player.Defense}");
                table.AddRow("[yellow]Geschwindigkeit[/]", $"{Player.Speed}");

                ctx.Refresh();

                Thread.Sleep(1000);

                // Phase 2: Erhöhungen hinzufügen
                table.RemoveRow(3);
                table.RemoveRow(2);
                table.RemoveRow(1);
                table.RemoveRow(0);

                table.AddRow("[yellow]Lebenspunkte[/]", $"{Player.Health} [green]+{message.HealthIncrease}[/]");
                table.AddRow("[yellow]Stärke[/]", $"{Player.Strength} [green]+{message.StrengthIncrease}[/]");
                table.AddRow("[yellow]Verteidigung[/]", $"{Player.Defense} [green]+{message.DefenseIncrease}[/]");
                table.AddRow("[yellow]Geschwindigkeit[/]", $"{Player.Speed} [green]+{message.SpeedIncrease}[/]");

                ctx.Refresh();
                Thread.Sleep(1000);

                // Phase 3: Neue Werte anzeigen
                Player.Health += message.HealthIncrease;
                Player.Strength += message.StrengthIncrease;
                Player.Defense += message.DefenseIncrease;
                Player.Speed += message.SpeedIncrease;

                table.RemoveRow(3);
                table.RemoveRow(2);
                table.RemoveRow(1);
                table.RemoveRow(0);

                table.AddRow("[yellow]Lebenspunkte[/]", $"{Player.Health}");
                table.AddRow("[yellow]Stärke[/]", $"{Player.Strength}");
                table.AddRow("[yellow]Verteidigung[/]", $"{Player.Defense}");
                table.AddRow("[yellow]Geschwindigkeit[/]", $"{Player.Speed}");

                ctx.Refresh();
            });
        }

    }
}
