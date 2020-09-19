class Student
{
    public string Name { get; set; }
    public int Years { get; set; }
    public int Grade { get; set; }

    public Student(string name, int years, int grade)
    {
        Name = name;
        Years = years;
        Grade = grade;
    }

    public override string ToString()
    {
        return Grade.ToString();
    }

}