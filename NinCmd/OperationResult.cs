using NinEngine;

namespace NinCmd
{
    public struct OperationResult
    {
        public Statuscode Code;
        public string Message;
        public override string ToString()
        {
            return string.Format("{0}: {1}", (int)Code, Message ?? "Ok");
        }
    }
}
