using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;

namespace ConsoleApp9
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = 5;
            var soundList = File.ReadAllLines(@"list.txt");
            var answer = new List<string>();
            var sounds = soundList.Where(a => a != "").Select(a => new SoundPlayer(@"sounds\"+a)).ToArray();
            var dataNum = sounds.Length;
            var r = new Random();
            var index = Enumerable.Range(0, n*dataNum).OrderBy(_ => r.Next()).Select(a => a%dataNum);
            var sound_a = new int[dataNum];
            var sound_e = new int[dataNum];
            var time = Enumerable.Range(0, dataNum).Select(_ => new List<long>()).ToArray();

            var s = new System.Diagnostics.Stopwatch();

            Console.WriteLine(index.ToArray().Length);

            foreach (var a in index)
            {
                s.Restart();
                sounds[a].Play();
                Console.WriteLine("Type e or a!! then enter");
                var b = Console.ReadLine();
                s.Stop();
                if (b == "a")
                    ++sound_a[a];
                else if (b == "e")
                    ++sound_e[a];
                else
                {
                    Console.WriteLine("input is wrong");
                    Console.ReadLine();
                }
                var t = s.ElapsedMilliseconds;
                time[a].Add(t);
                answer.Add(a.ToString() + "," + soundList[a] + "," + b + "," + t.ToString());
                Console.Clear();
            }

            File.WriteAllLines(@"answer.txt", answer.ToList());
            for(int i = 0; i < dataNum; ++i)
            {
                Console.WriteLine("{0} a{1}{2}e time:{3:f1}", soundList[i].PadLeft(15), new string('+', sound_a[i]), new string('*', sound_e[i]), time[i].Average());
            }
            Console.ReadLine();
        }
    }
}
