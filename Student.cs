class Student
{
    public string Name { get; set; }
    public int Exams { get; set; }
    public int Grade { get; set; }

    public Student(string name, int exams, int grade)
    {
        Name = name;
        Exams = exams;
        Grade = grade;
    }

    public override string ToString()
    {
        return Grade.ToString();
    }

}