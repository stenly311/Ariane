using System.Collections.Generic;

namespace MEFShared
{
    public interface IOperation
    {
        void Initiate(ICollection<object> processes);
        void Open();
        void Close();
    }
}