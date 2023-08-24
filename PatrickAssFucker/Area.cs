using System.Collections.ObjectModel;
using PatrickAssFucker.Entities;

namespace PatrickAssFucker
{
    public enum AreaIdentifier
    {
        //Regions
        City = 0x0000,
        Forest = 0x0100,
        Moutains = 0x0200,
        Stillbach = 0x0300,
        Tannenhain = 0x0400,
        Weidennordheim = 0x0500,
        //City
        City_Townhall = 0x0010,
        City_Townhall_Entrance = 0x0011,
        City_Townhall_Dungeon = 0x0012,
        City_Marketplace = 0x0020,
        City_Inn = 0x0030,
        //Stillbad
        Stillbach_Road = 0x0310,
        Stillbach_Blacksmith = 0x0320,
        Stillbach_Blacksmith_GroundFloor = 0x0321,
        Stillbach_Blacksmith_FirstFloor = 0x03022,
        Stillbach_WellPlace = 0x0330,
        //Tannenhain
        Tannenhain_Clearing = 0x0410,
        Tannenhain_Clearing_Shed = 0x0411,
        //Weidennordheim
        Weidennordheim_Road_From_Tannenhain = 0x0510,
        Weidennordheim_Square = 0x0520,
        Weidennordheim_Townhall = 0x0530,
        Weidennordheim_Townhall_Entrance_Hall = 0x0531,
    }

    public class Area
    {
        private AreaIdentifier _id;
        private string _name;
        private string _description;
        private List<Area> _linked;
        private List<Area> _inner;
        private Area? _parent;
        private Area? _entrance;
        private List<Entity> _entities;
        private List<Item> _items;
        
        public Func<bool> CanEnter = () => true;
        public Func<bool> CanLeave = () => true;
        public Func<bool> CanSee = () => true;
        
        public Action? OnEnter;
        public Action? OnLeave;

        public Action? OnEnterAttempt;
        public Action? OnLeaveAttempt;
        
        public virtual AreaIdentifier Id
        {
            get { return _id; }
            protected set { _id = value; }
        }
        public virtual string Name
        {
            get { return _name; }
            protected set { _name = value; }
        }
        public virtual string Description
        {
            get { return _description; }
            protected set { _description = value; }
        }
        public Area? Entrance
        {
            get
            {
                return _entrance;
            }
            protected set
            {
                _entrance = value;
            }
        }
        public Area? Parent
        {
            get
            {
                return _parent;
            }
            protected set
            {
                _parent = value;
            }
        }
        public ReadOnlyCollection<Area> Linked => _linked.AsReadOnly();
        public ReadOnlyCollection<Entity> Entities => _entities.AsReadOnly();
        public ReadOnlyCollection<Item> Items => _items.AsReadOnly();

        public bool HasLinks => _linked.Count > 0;
        public bool HasInner => _inner.Count > 0;
        public bool HasEntities => _entities.Count > 0;
        public bool HasItems => _items.Count > 0;
        public bool HasParent => _parent != null;
        public bool HasEntrance => _entrance != null;
        public bool IsEntrance => _parent?._entrance == this;

        public Area()
        {
            _linked = new List<Area>();
            _inner = new List<Area>();
            _entities = new List<Entity>();
            _items = new List<Item>();
        }

        public Area(AreaIdentifier id) : this()
        {
            Id = id;
        }

        public static void Link(Area a, Area b)
        {
            a.AddLink(b);
            b.AddLink(a);
        }

        protected void AddLink(Area other)
        {
            if (!_linked.Contains(other))
            {
                _linked.Add(other);
            }
        }

        public void Add(Entity entity)
        {
            if (_entities.Contains(entity))
            {
                return;
            }
            _entities.Add(entity);
        }
        
        public void Remove(Entity entity)
        {
            if (!_entities.Contains(entity))
            {
                return;
            }
            _entities.Remove(entity);
        }
        
        public void Add(Item item)
        {
            _items.Add(item);
        }
        
        public void Remove(Item item)
        {
            _items.Remove(item);
        }
        
        public void Add(Area inner)
        {
            if (_inner.Contains(inner))
            {
                return;
            }
            //Area.Link(this, inner);
            _inner.Add(inner);
            inner._parent = this;
        }

        public bool IsLinkedWith(Area other)
        {
            return _linked.Contains(other);
        }

        public Area? GetInnerById(AreaIdentifier id)
        {
            foreach (Area inner in _inner)
            {
                if (inner.Id == id)
                {
                    return inner;
                }
            }
            return null;
        }

        public List<Area> GetAllInner()
        {
            var complete = new List<Area>();
            foreach (var inner in _inner)
            {
                complete.Add(inner);
                complete.AddRange(inner.GetAllInner()); 
            }
            return complete;
        }
        
        public string GetFullName()
        {
            if (!HasParent)
            {
                return Name;
            }
            else
            {
                return $"{Parent.GetFullName()} -> {Name}";
            }
        }
        
        public string GetShortenedName(int maxLength = 30)
        {
            string fullName = GetFullName();
            if (fullName.Length > maxLength)
            {
                return fullName.Substring(0, maxLength - 3) + "...";
            }
            return fullName;
        }
        
        public string GetTopParentName()
        {
            Area currentArea = this;
            while (currentArea.HasParent)
            {
                currentArea = currentArea.Parent;
            }
            return currentArea.Name;
        }
    }
}