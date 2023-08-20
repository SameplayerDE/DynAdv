namespace PatrickAssFucker.Areas
{
    public class AreaManager
    {
        public static AreaManager Instance { get; } = new AreaManager();

        private List<Area> _areas = new List<Area>();

        static AreaManager()
        {
        }

        private AreaManager()
        {
        }

        public void Add(Area area)
        {
            if (_areas.Contains(area))
            {
                return;
            }
            _areas.Add(area);
        }

        public Area? GetAreaById(AreaIdentifier identifier)
        {
            foreach (var area in _areas)
            {
                if (identifier == area.Id)
                {
                    return area;
                }
            }
            return null;
        }

    }
}
