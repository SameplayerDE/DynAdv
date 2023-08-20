namespace PatrickAssFucker
{
    public enum AreaIdentifier
    {
        //Regions
        City = 0x0000,
        Forest = 0x0100,
        Moutains = 0x0200,
        //City
        City_Townhall = 0x0010,
        City_Townhall_Entrance = 0x0011,
        City_Townhall_Dungeon = 0x0012,
        City_Marketplace = 0x0020,
        City_Inn = 0x0030,
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

        public Func<bool> CanEnter = () => true;
        public Action OnEnter;
        public Action OnLeave;

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
        public List<Area> Linked
        {
            get
            {
                return _linked;
            }
        }

        public bool HasLinks => _linked.Count > 0;
        public bool HasInner => _inner.Count > 0;
        public bool HasParent => _parent != null;
        public bool HasEntrance => _entrance != null;
        public bool IsEntrance => _parent?._entrance == this;

        public Area()
        {
            _linked = new List<Area>();
            _inner = new List<Area>();
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
    }
}