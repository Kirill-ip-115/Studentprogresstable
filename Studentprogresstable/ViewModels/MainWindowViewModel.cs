using Studentprogresstable.Models;
using Studentprogresstable.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Net.Http.Headers;
using System.Reactive;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Studentprogresstable.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, Unit> SaveStudents { get; }
        public ReactiveCommand<Unit, Unit> AddStudent { get; }
        public ReactiveCommand<Unit, Unit> DeleteStudent { get; }
        public ReactiveCommand<Unit, Unit> LoadStudents { get; }
        public bool flag = true;
        public int count = 0;
        double q = 0;
        string StudentFio = string.Empty;
        ushort MarkFizra = 0;
        public ushort MarkOOP = 0;
        public ushort MarkTrpo = 0;
        public ushort MarkPhysics = 0;
        public double[] AverageScore = { 0, 0, 0, 0, 0 };
        public string[] AverageColor = { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };

        public MainWindowViewModel()
        {
            AddStudent = ReactiveCommand.Create(() =>
            {
                Student[] temp = students;
                count++;
                Array.Resize(ref temp, count);

                temp[temp.Length - 1] = new Student { fio = StudentFio, PmarkFizra = MarkFizra, PmarkTrpo = MarkTrpo, PmarkOOP = MarkOOP, PmarkPhysics = MarkPhysics, GetAverage = 0, GetFizraColor = string.Empty, GetAverageColor = string.Empty, GetPhysicsColor = string.Empty, GetTrpoColor = string.Empty, GetOOPColor = string.Empty };
                Students = temp;
                AllAverage(students);
                flag = false;
            });
            DeleteStudent = ReactiveCommand.Create(() =>
            {
                Student[] temp = students;
                int how = -1;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i].FIO == StudentFio)
                    {
                        how = i;
                        break;
                    }
                }
                if (how != -1)
                {
                    for (int i = how; i < temp.Length - 1; i++)
                    {
                        temp[i] = temp[i + 1];
                    }
                    count--;
                    Array.Resize(ref temp, count);
                    Students = temp;
                    AllAverage(students);
                    if (count == 0)
                    {
                        flag = true;
                    }
                }
            });
            SaveStudents = ReactiveCommand.Create(() =>
            {
                StreamWriter file = new StreamWriter("dat.txt");
                if (count != 0)
                {
                    for (int i = 0; i < students.Length; i++)
                    {
                        file.WriteLine(students[i].fio.ToString() + " " + students[i].PmarkFizra.ToString() + " " + students[i].PmarkOOP.ToString() + " " + students[i].PmarkTrpo.ToString() + " " + students[i].PmarkPhysics.ToString());
                    }
                }
                file.Close();
            });
            LoadStudents = ReactiveCommand.Create(() =>
            {
                if (flag)
                {
                    if (System.IO.File.Exists("dat.txt"))
                    {
                        StreamReader file = new StreamReader("dat.txt");
                        while (!file.EndOfStream)
                        {
                            int g = 0;
                            string line = file.ReadLine();
                            char[] ch = line.ToCharArray();
                            string[] res_array = { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
                            ushort[] int_array = { 0, 0, 0, 0 };
                            for (int i = 0; i < ch.Length; i++)
                            {
                                if (ch[i] == ' ')
                                {
                                    if (ch[i + 1] >= '0' && ch[i + 1] <= '9')
                                    {
                                        g++;
                                        i++;
                                    }
                                }
                                res_array[g] += ch[i].ToString();
                            }
                            for (int i = 1; i < res_array.Length; i++)
                            {
                                int_array[i - 1] = ushort.Parse(res_array[i]);
                            }
                            Student[] temp = students;
                            count++;
                            Array.Resize(ref temp, count);
                            temp[temp.Length - 1] = new Student { fio = res_array[0], PmarkFizra = int_array[0], PmarkTrpo = int_array[2], PmarkOOP = int_array[1], PmarkPhysics = int_array[3], GetAverage = 0, GetFizraColor = string.Empty, GetAverageColor = string.Empty, GetPhysicsColor = string.Empty, GetTrpoColor = string.Empty, GetOOPColor = string.Empty };
                            Students = temp;
                            AllAverage(students);
                        }
                        file.Close();
                        flag = false;
                    }
                }
            });

        }
        public void AllAverage(Student[] students)
        {
            if (students.Length != 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    AverageScore[i] = 0;
                }
                for (int j = 0; j < students.Length; j++)
                {
                    GetAverageFizra += students[j].Ofizra;
                    GetAverageOOP += students[j].Ooop;
                    GetAverageTrpo += students[j].Otrpo;
                    GetAveragePhysics += students[j].Ophysics;
                }
                GetAverageFizra /= students.Length;
                GetAverageColorFizra = setColor(GetAverageFizra);
                GetAverageOOP /= students.Length;
                GetAverageColorOOP = setColor(GetAverageOOP);
                GetAverageTrpo /= students.Length;
                GetAverageColorTrpo = setColor(GetAverageTrpo);
                GetAveragePhysics /= students.Length;
                GetAverageColorPhysics = setColor(GetAveragePhysics);
                GetQ = (GetAveragePhysics + GetAverageTrpo + GetAverageOOP + GetAverageFizra) / 4.0;
                GetAverageColorI = setColor(GetQ);
            }
            else
            {
                GetAverageFizra = 0;
                GetAverageOOP = 0;
                GetAverageTrpo = 0;
                GetAveragePhysics = 0;
                GetQ = 0;
                GetAverageColorFizra = setColor(GetAverageFizra);
                GetAverageColorOOP = setColor(GetAverageOOP);
                GetAverageColorTrpo = setColor(GetAverageTrpo);
                GetAverageColorPhysics = setColor(GetAveragePhysics);
                GetQ = (GetAveragePhysics + GetAverageTrpo + GetAverageOOP + GetAverageFizra) / 4.0;
                GetAverageColorI = setColor(GetQ);
            }
        }
        public string setColor(double Score)
        {
            string result = string.Empty;
            if (Score < 1)
            {
                result = "Red";
            }
            if (Score >= 1 && Score <= 1.5)
            {
                result = "Yellow";
            }
            if (Score > 1.5)
            {
                result = "Green";
            }
            return result;
        }
        public Student[] Students { get => students; set => this.RaiseAndSetIfChanged(ref students, value); }
        private Student[] students;
        public string GetName { get => StudentFio; set { this.RaiseAndSetIfChanged(ref StudentFio, value); } }
        public ushort GetMarkFizra { get => MarkFizra; set { this.RaiseAndSetIfChanged(ref MarkFizra, value); } }
        public ushort GetMarkOOP { get => MarkOOP; set { this.RaiseAndSetIfChanged(ref MarkOOP, value); } }
        public ushort GetMarkTrpo { get => MarkTrpo; set { this.RaiseAndSetIfChanged(ref MarkTrpo, value); } }
        public ushort GetMarkPhysics { get => MarkPhysics; set { this.RaiseAndSetIfChanged(ref MarkPhysics, value); } }
        public double GetAverageFizra { get => AverageScore[0]; set { this.RaiseAndSetIfChanged(ref AverageScore[0], value); } }
        public double GetAverageOOP { get => AverageScore[1]; set { this.RaiseAndSetIfChanged(ref AverageScore[1], value); } }
        public double GetAverageTrpo { get => AverageScore[2]; set { this.RaiseAndSetIfChanged(ref AverageScore[2], value); } }
        public double GetAveragePhysics { get => AverageScore[3]; set { this.RaiseAndSetIfChanged(ref AverageScore[3], value); } }
        public string GetAverageColorFizra { get => AverageColor[0]; set { this.RaiseAndSetIfChanged(ref AverageColor[0], value); } }
        public string GetAverageColorOOP { get => AverageColor[1]; set { this.RaiseAndSetIfChanged(ref AverageColor[1], value); } }
        public string GetAverageColorTrpo { get => AverageColor[2]; set { this.RaiseAndSetIfChanged(ref AverageColor[2], value); } }
        public string GetAverageColorPhysics { get => AverageColor[3]; set { this.RaiseAndSetIfChanged(ref AverageColor[3], value); } }
        public string GetAverageColorI { get => AverageColor[4]; set { this.RaiseAndSetIfChanged(ref AverageColor[4], value); } }
        public double GetQ { get => q; set { this.RaiseAndSetIfChanged(ref q, value); } }
        public string GetAverageColor { get => AverageColor[4]; set { this.RaiseAndSetIfChanged(ref AverageColor[4], value); } }
    }
}
