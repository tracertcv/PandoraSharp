namespace PandoraSharp.Responses
{
    interface IPandoraResponseResult
    {
        //This must be implemented explicitly in every class that uses this interface because dynamics are stupid
        void populate(string data);
    }
}