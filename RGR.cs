using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGR
{
    class Program
    {
        static void Main()
        {
            SortedList<string, int> r_w = new SortedList<string, int>();

            // чтение файла
            StreamReader sr = new StreamReader("input.txt");
            string s = sr.ReadLine();
            int n = Convert.ToInt32(s.Split()[0]);
            int m = Convert.ToInt32(s.Split()[1]);
            for (int i = 0; i < m; i++)
            {
                s = sr.ReadLine();
                r_w.Add(s.Substring(0, 3), Convert.ToInt32(s.Split()[2]));
            }

            // волновой алгоритм
            Dictionary<string, int> ver = new Dictionary<string, int>();
            Dictionary<string, List<string>> used= new Dictionary<string, List<string>>();
            List<string> list = new List<string>();

            foreach (var para in r_w)
            {
                string i = para.Key.Split()[0];
                string j = para.Key.Split()[1];
                if (ver.ContainsKey(i))
                {
                    if (ver.ContainsKey(j))
                    {
                        ver[j] = Math.Max(ver[j], ver[i] + para.Value);

                        if (ver[j] == ver[i] + para.Value)
                        {
                            List<string> str = new List<string>();
                            foreach (string d in used[i])
                            {
                                str.Add(d);
                            }
                            if (!str.Contains(i)) str.Add(i);
                            used[j] = str;
                        }
                    } 
                    else
                    {
                        ver.Add(j, ver[i] + para.Value);
                        List<string> str = new List<string>();
                        foreach (string d in used[i])
                        {
                            str.Add(d);
                        }
                        if (!str.Contains(i)) str.Add(i);
                        used[j] = str;
                        foreach (var p in r_w)
                        {
                            if (p.Key.Split()[0] == j)
                            {
                                if (ver.Keys.Contains(p.Key.Split()[1]))
                                {
                                    int ku = ver[p.Key.Split()[1]]; // значение до изменения
                                    ver[p.Key.Split()[1]] = Math.Max(ku, ku + ver[j]); // изменение
                                    if (ver[p.Key.Split()[1]] == ku + ver[j])
                                    {
                                        var k = used[j].Union(used[p.Key.Split()[1]]);
                                        List<string> str1 = new List<string>();
                                        foreach (string tr in k)
                                        {
                                            str1.Add(tr);
                                        }
                                        if (!str1.Contains(j)) str1.Add(j);
                                        used[p.Key.Split()[1]] = str1;
                                    }
                                }

                            }
                        }
                    } 
                }
                else
                {
                    if (ver.ContainsKey(j))
                    {
                        ver[j] = Math.Max(ver[j], para.Value);
                    }
                    else ver.Add(j, para.Value);
                    if (ver[j] == para.Value)
                    {
                        List<string> str = new List<string>() { i };
                        used[j] = str;
                        foreach (var p in r_w)
                        {
                            if (p.Key.Split()[0] == j)
                            {
                                if (ver.Keys.Contains(p.Key.Split()[1]))
                                {
                                    int pr = ver[p.Key.Split()[1]];
                                    ver[p.Key.Split()[1]] = Math.Max(pr, pr + ver[j]);
                                    if (ver[p.Key.Split()[1]] == pr + ver[j])
                                    {
                                        var k = used[j].Union(used[p.Key.Split()[1]]);
                                        List<string> str1 = new List<string>();
                                        foreach (string tr in k)
                                        {
                                            str1.Add(tr);
                                        }
                                        if (!str1.Contains(j)) str1.Add(j);
                                        used[p.Key.Split()[1]] = str1;
                                    }
                                }

                            }
                        }
                    }
                }
            }
            if (used[Convert.ToString(n)].Contains("1")) Console.WriteLine("Максимальное количество набранных знаний - " + ver[Convert.ToString(n)]);
            else Console.WriteLine("Лабиринт пройти нельзя");
        }
    }
}
