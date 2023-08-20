namespace PatrickAssFucker
{

    public enum Material
    {
        NONE,
        TORCH,
        STICK,
        COAL,
        CLOTH,
        OIL,
        FLINT,
        STEEL,
        FLINT_N_STEEL,
        KEY,
        STONE,
        WIRE,
        SWORD,
        KNIFE,
        COIN
    }

    public class Item
    {
        public Material Material;
        public string Name;
        public string Description;

        public Item(Material material, string name)
        {
            Material = material;
            Name = name;
            Description = string.Empty;
        }
    }
}
