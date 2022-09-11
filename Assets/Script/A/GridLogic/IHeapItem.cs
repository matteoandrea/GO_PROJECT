using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.A.GridLogic
{
    public interface IHeapItem<T>: IComparable<T>
    {
        public int HeapIndex { get; set; }
    }
}
