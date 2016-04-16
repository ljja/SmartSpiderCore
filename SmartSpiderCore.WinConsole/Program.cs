using System;
using System.IO;
using System.Text;

namespace SmartSpiderCore.WinConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1 || string.IsNullOrEmpty(args[0]) || File.Exists(args[0]) == false) return;

            var config = args[0];

            Console.WriteLine("load config");
            using (var engine = ConfigManage.Instance.Load<SpiderEngine>(config))
            {
                Console.WriteLine("任务:{0}", engine.Title);
                Console.WriteLine("描述:{0}", engine.Description);

                Console.WriteLine("init task");
                engine.Init();

                Console.WriteLine("exec task");
                engine.Exec();

                Console.WriteLine("dispose task");
                engine.Dispose();

            }

            Console.WriteLine("exit");
        }
    }
}
