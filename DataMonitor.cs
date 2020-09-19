using System.Collections.Generic;
using System.Threading;

class DataMonitor<T>
{
    private List<T> Data = new List<T>();
    private const int MAX_VALUE = 10;

    private int AddCount = 0;

    private int PlannedAddCount { get; set; }

    public DataMonitor(int plannedAddCount)
    {
        PlannedAddCount = plannedAddCount;
    }

    public void AddElement(T element)
    {
        lock (this)
        {
            while (Data.Count >= MAX_VALUE)
            {
                Monitor.Wait(this);
            }

            Data.Add(element);
            AddCount++;
            Monitor.PulseAll(this);
        }
    }

    public T GetLastElement()
    {
        lock (this)
        {
            if (AddCount < PlannedAddCount)
            {
                while (Data.Count <= 0)
                {
                    Monitor.Wait(this);
                }

                T lastElement = Data[Data.Count - 1];

                Data.RemoveAt(Data.Count - 1);

                Monitor.PulseAll(this);

                return lastElement;
            }
            else
            {
                return default(T);
            }

        }
    }
}