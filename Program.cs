using demo;
class Program
{
    public static void Main(string[] args)
    {
        //SerialTaskEngineExecute();

        ConditionalSerialEngineExecute();

    }


    static void SerialTaskEngineExecute()
    {
        var engine = new SerialTaskEngine();
        var rules = "A->B->C";

        engine.parseRule(rules);

        engine.fire();
    }

    static void ConditionalSerialEngineExecute()
    {

        var rules = "A.2->B.20->C.5->D.13";

        var engine = new ConditionalSerialEngine();

        engine.SetContextItem("conditionVar", 3);

        engine.ParseRule(rules);

        engine.Fire();

    }
}