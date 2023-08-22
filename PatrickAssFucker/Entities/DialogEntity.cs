using Spectre.Console;

namespace PatrickAssFucker.Entities;

public class DialogOption
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
}

public class DialogEntity : Entity, ITalkable
{
    private Dialogue _startDialogue;
    private Dialogue _currentDialogue;

    public DialogEntity(Dialogue startDialogue)
    {
        _startDialogue = startDialogue;
        _currentDialogue = _startDialogue;
    }

    public void ResetDialogue()
    {
        _currentDialogue = _startDialogue;
    }

    public void Talk()
    {
        ResetDialogue();
        while (_currentDialogue != null)
        {

            AnsiConsole.Clear();

            AnsiConsole.WriteLine(_currentDialogue.NpcText);

            if (_currentDialogue.Options.Length == 0)
            {
                break; // Beendet die Schleife, wenn es keine Optionen gibt
            }

            var selection = new SelectionPrompt<int>()
                .Title("Wähle eine Option:")
                .UseConverter(i => _currentDialogue.Options[i].Name);

            for (int i = 0; i < _currentDialogue.Options.Length; i++)
            {
                selection.AddChoice(i);
            }

            int choice = AnsiConsole.Prompt(selection);
            _currentDialogue.Options[choice].Action.Invoke();
            _currentDialogue = _currentDialogue.Options[choice].NextDialogue;
        }

        AnsiConsole.Clear();
    }

}