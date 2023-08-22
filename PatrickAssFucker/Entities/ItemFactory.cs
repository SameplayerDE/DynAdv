namespace PatrickAssFucker.Entities;

public enum AppleState
{
    Decayed,          // Verfault
    Bruised,          // Verbeult oder mit Blaustellen
    Unripe,           // Unreif
    Mature,           // Reif, aber nicht optimal
    Prime,            // Optimaler Reifezustand
    Overripe,         // Überreif
    Sunburned,        // Sonnenverbrannt
    InsectDamaged,    // Von Insekten beschädigta
}

public static class ItemFactory
{
    public static Item CreateApple(AppleState state = AppleState.Prime)
    {
        var apple = new Item(ItemType.Apple);
        var meta = apple.CreateMeta();
        
        switch (state)
        {
            case AppleState.Decayed:
                meta.Displayname = "Verfaulter Apfel";
                break;
            case AppleState.Bruised:
                meta.Displayname = "Verbeulter Apfel";
                break;
            case AppleState.Unripe:
                meta.Displayname = "Unreifer Apfel";
                break;
            case AppleState.Mature:
                meta.Displayname = "Reifer Apfel";
                break;
            case AppleState.Prime:
                meta.Displayname = "Apfel in optimalem Zustand";
                break;
            case AppleState.Overripe:
                meta.Displayname = "Überreifer Apfel";
                break;
            case AppleState.Sunburned:
                meta.Displayname = "Sonnenverbrannter Apfel";
                break;
            case AppleState.InsectDamaged:
                meta.Displayname = "Von Insekten beschädigter Apfel";
                break;
            default:
                meta.Displayname = "Unbekannter Zustand";
                break;
        }

        apple.Meta = meta;
        return apple;
    }
}