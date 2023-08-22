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
    private Dialogue _currentDialogue;

    public DialogEntity(Dialogue startDialogue)
    {
        _currentDialogue = startDialogue;
    }
    public void Talk()
    {
        while (_currentDialogue != null)
        {
            Console.WriteLine(_currentDialogue.NpcText);
            for (int i = 0; i < _currentDialogue.Options.Length; i++)
            {
                Console.WriteLine($"{i}. {_currentDialogue.Options[i].Name}");
            }

            int choice = int.Parse(Console.ReadLine());
            _currentDialogue.Options[choice].Action.Invoke();
            _currentDialogue = _currentDialogue.Options[choice].NextDialogue;
        }

        Console.WriteLine("Dialog vorbei");
    }

}