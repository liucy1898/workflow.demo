using demo;
class Program
{
    public static void Main(string[] args)
    {
        var engine = new engine();
        var rules = "A->B->C";

        engine.parseRule(rules);

        engine.fire();

    }
}