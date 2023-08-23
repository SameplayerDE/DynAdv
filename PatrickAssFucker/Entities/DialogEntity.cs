using HxLocal;
using PatrickAssFucker.GameSystems;
using Spectre.Console;

namespace PatrickAssFucker.Entities;

/*public class DialogOption
{
    public string Text { get; }
    public Action Action { get; }
    public DialogOption[] NextOptions { get; }

    public DialogOption(string text, Action action, DialogOption[] nextOptions = null)
    {
        Text = text;
        Action = action;
        NextOptions = nextOptions;
    }
}

public class Dialogue
{
    public string NpcText { get; set; }
    public Option[] Options { get; set; }
}

public class Option
{
    public string Name { get; set; }
    public Action Action { get; set; }
    public Dialogue NextDialogue { get; set; }
}*/

public class DialogEntity : Entity, ITalkable
{
    //private Dialogue _startDialogue;
    //private Dialogue _currentDialogue;

    private GameSystems.Dialog? _start;
    private GameSystems.Dialog? _current;
    
    public DialogEntity(Dialog dialog)
    {
        _start = dialog;
        _current = _start;
    }

    public void ResetDialogue()
    {
        _current = _start;
    }

    public void Talk()
    {
        _current = _start;
        while (_current != null)
        {
            if (_current.Input != null)
            {
                AnsiConsole.MarkupLine("[purple italic]" + _current.Input() + "[/]");
            }
            if (_current.Output != null)
            {
                AnsiConsole.MarkupLine("[green bold]" + _current.Output() + "[/]");
            }

            if (_current.Options.Count <= 0)
            {
                break;
            }

            var selection =
                new SelectionPrompt<int>()
                    .Title(Localisation.GetString("commands.talk_dialog_selection_title"))
                    .UseConverter(index => _current.Options[index].Input?.Invoke()!);
            
            var index = 0;
            for (; index < _current.Options.Count; index++)
            {
                var isAvailable = _current.Options[index].IsAvailable;
                if (isAvailable != null && !isAvailable())
                {
                    continue;
                }
                selection.AddChoice(index);
            }



            var choice = AnsiConsole.Prompt(selection);
            _current.Options[choice].Action?.Invoke();

            _current = _current.Options[choice].Return ? _start : _current.Options[choice];
        }
    }
}