using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace cs_lygiagretumas
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = ReadData();
            List<Thread> threads = new List<Thread>();

            ResultsMonitor results = new ResultsMonitor();
            DataMonitor
            for (int i = 0; i < 20; i++)
            {
                threads.Add(new Thread(() => ThreadWorker());
            }

            threads.ForEach(thread =>
            {
                thread.Start();
            });

            threads.ForEach(thread =>
            {
                thread.Join();
            });

            Console.ReadKey();
        }


        static void ThreadWorker(ref DataMonitor<Student> dataMonitor, ref ResultsMonitor resultsMonitor, int dataSize)
        {
            Student student = (Student)dataMonitor.GetLastElement();
            Console.WriteLine("Gija studentas" + student);
            resultsMonitor.AddElement(student);

        }

        static List<Student> ReadData()
        {
            string[] Failai = new String[] { "IFF_8-10_LauzadisR_L1_dat_1.txt", "IFF_8-10_LauzadisR_L1_dat_2.txt", "IFF_8-10_LauzadisR_L1_dat_3.txt" };
            List<Student> students = new List<Student>();
            foreach (string failas in Failai)
            {
                using (StreamReader reader = new StreamReader(@failas))
                {

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] vertes = line.Split(' ');
                        string name = vertes[0];
                        int years = int.Parse(vertes[1]);
                        int grade = int.Parse(vertes[2]);
                        Student newStudent = new Student(name, years, grade);
                        students.Add(newStudent);
                    }
                }

            }
            return students;
        }
    }

}
