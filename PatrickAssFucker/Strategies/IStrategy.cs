namespace PatrickAssFucker.Strategies;

public interface IStrategy
{
    public void Simulate(int teamA, int teamB);
}

public class EinfacheStrategy : IStrategy
{
    public void Simulate(int teamA, int teamB)
    {
        //rechnen
    }
}

public class ExtremeStrategy : IStrategy
{
    public void Simulate(int teamA, int teamB)
    {
        //rechnen
    }
}