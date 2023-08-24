namespace PatrickAssFucker.GameSystems;

// Describes the type of response better, so the player knows what is meant
public enum InputType
{
    Default, // Because I don't know why I need this
    Question, // Asking questions to an NPC
    Command, // Commands or actions the player can perform (e.g., "Take the sword")
    Trade, // Trading or buying/selling with an NPC
    Threat, // Threatening or intimidating an NPC
    Flirt, // Flirting or social interaction with an NPC
    Insight, // Using special insights or abilities of the player (e.g., discovering a hidden door)
    Accuse, // Accusations or confrontations (e.g., "You are the thief!")
    Farewell // Saying goodbye or ending the conversation
}

public class Dialog
{

    public Func<string>? Output; // What is said to the player (What can I do for you?)
    public Func<string>? InputTitle; // What is shown in the answer selection (Ask for fire)
    public Func<string>? InputText; // What is actually said as a response (Hey, do you have fire?)
    public InputType? InputType;
    public Action? Action; // Executed when the dialog is chosen
    public Func<bool>? IsAvailable; // Shows whether the dialog is displayed as a response option
    public List<Dialog> Options = new(); // The list of response options for this part of the dialog
    public bool Return; // Unused

    // Adds response options to the dialog
    public void Add(Dialog dialog)
    {
        Options.Add(dialog);
    }
}
