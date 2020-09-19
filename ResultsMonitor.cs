using System.Collections.Generic;
class ResultsMonitor
{
    private List<Student> Data = new List<Student>();

    public ResultsMonitor() { }

    public int Size()
    {
        return Data.Count;
    }

    public override string ToString()
    {
        return string.Join(", ", Data);
    }

    public List<Student> GetData()
    {
        return Data;
    }

    public void AddElement(Student element)
    {
        lock (this)
        {
            for (int i = 0; i < Data.Count; i++)
            {
                if (Data[i].Grade <= element.Grade)
                {
                    Data.Insert(i, element);
                    return;
                }
            }
            Data.Add(element);

        }
    }
}