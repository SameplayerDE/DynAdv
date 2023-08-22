using HxLocal;

namespace PatrickAssFucker.Entities;

public enum ItemType
{
    Apple,
    Coal,
    Bread,
    IronSword,
    WoodenSword
}

public class Item
{
    private int _amount;
    private ItemType _type;
    private ItemMeta? _meta;
    private string _translationKey;
    
    public int Amount
    {
        get
        {
            return _amount;
        }
        protected set
        {
            _amount = value;
        }
    }
    public string Name
    {
        get => Localisation.GetString("items." + Type.ToString().ToLower() + ".default");
    }
    public ItemType Type
    {
        get
        {
            return _type;
        }
        set
        {
            _type = value;
        }
    }
    public ItemMeta Meta
    {
        get
        {
            return _meta;// ?? new ItemMeta();
        }
        set
        {
            _meta = value;
        }
    }

    public bool HasMeta => Meta != null;

    public Item(ItemType type, int amount = 1, ItemMeta? meta = null)
    {
        Type = type;
        Amount = amount;
        Meta = meta;
    }

    public ItemMeta CreateMeta()
    {
        if (!HasMeta)
        {
            Meta = new ItemMeta();
        }
        return Meta;
    }
}