using System;

public class Upgrade
{
    public string Name { get; }
    public int Cost { get; }
    public Action Action { get; }

    public Upgrade(string name, int cost, Action action, int level)
    {
        Name = name;
        Cost = cost;
        Action = action;
    }
}
