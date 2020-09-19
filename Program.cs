using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace cs_lygiagretumas
{
    class Program
    {
        static void Main(string[] args)
        {
            //Nuskaitomi duomenys iš failų
            List<Student> students = ReadData();

            List<Thread> threads = new List<Thread>();

            ResultsMonitor resultsMonitor = new ResultsMonitor();
            DataMonitor<Student> dataMonitor = new DataMonitor<Student>(students.Count);

            //Paleidžiamos darbininkių gijos, kurios ims duomenis iš duomenų monitoriaus
            for (int i = 0; i < 20; i++)
            {
                threads.Add(new Thread(() => ThreadWorker(ref dataMonitor, ref resultsMonitor)));
            }

            threads.ForEach(thread =>
            {
                thread.Start();
            });

            //Pagrindinė gija deda elementus į duomenų monitorių
            students.ForEach(x =>
            {
                dataMonitor.AddElement(x);
            });

            threads.ForEach(thread =>
            {
                thread.Join();
            });

            Console.WriteLine("REZULTATAI: " + resultsMonitor);

            Console.ReadKey();
        }


        static void ThreadWorker(ref DataMonitor<Student> dataMonitor, ref ResultsMonitor resultsMonitor)
        {
            Student student;
            while ((student = (Student)dataMonitor.GetLastElement()) != null)
            {

                if (IsValid(student))
                {
                    resultsMonitor.AddElement(student);
                }
            }
            Console.WriteLine("GIJA BAIGĖ DARBA");
            return;
        }

        static Boolean IsValid(Student student)
        {
            BusyWait(2000);
            return student.Grade > 0;
        }
        static void BusyWait(int milliseconds)
        {
            var sw = Stopwatch.StartNew();

            while (sw.ElapsedMilliseconds < milliseconds)
                Thread.SpinWait(1000);
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
