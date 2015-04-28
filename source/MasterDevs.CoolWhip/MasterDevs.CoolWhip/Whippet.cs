using Microsoft.Build.Utilities;

namespace MasterDevs.CoolWhip
{
    public class Whippet : Task
    {
        public override bool Execute()
        {
            Log.LogMessage("I ran a task today and it was liberating");
            //Log.LogError("Fail");
            return true;
        }
    }
}