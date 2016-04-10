using System;
using System.IO;
using System.Text;

namespace SmartSpiderCore.WinConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //BuildXml();
            //return;

            Console.WriteLine("hello world {0},{1}", args.Length, File.Exists(args[0]));

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

        private static void BuildXml()
        {
            var templateQiYe = File.ReadAllText("qiye.xml");
            var templateGeRen = File.ReadAllText("geren.xml");
            var domainFile = "domain.txt";

            var domainLines = File.ReadAllLines(domainFile);
            foreach (var line in domainLines)
            {
                //区域|省份|城市|域名
                var areaSplit = line.Split('|');

                /*
                 * #AREA#
                 * #SHENGFEN#
                 * #CITY#
                 * #HOST#
                 */

                var qiyeFile = string.Format("{0}.{1}.{2}.{3}.家装服务.企业.xml", areaSplit[0], areaSplit[1], areaSplit[2], areaSplit[3]);
                var gerenFile = string.Format("{0}.{1}.{2}.{3}.家装服务.个人.xml", areaSplit[0], areaSplit[1], areaSplit[2], areaSplit[3]);

                var tempQiYe = templateQiYe.Clone() as string;
                tempQiYe = tempQiYe.Replace("#AREA#", areaSplit[0])
                    .Replace("#SHENGFEN#", areaSplit[1])
                    .Replace("#CITY#", areaSplit[2])
                    .Replace("#HOST#", areaSplit[3]);

                var tempGeRen = templateGeRen.Clone() as string;
                tempGeRen = tempGeRen.Replace("#AREA#", areaSplit[0])
                    .Replace("#SHENGFEN#", areaSplit[1])
                    .Replace("#CITY#", areaSplit[2])
                    .Replace("#HOST#", areaSplit[3]);

                File.AppendAllText(qiyeFile, tempQiYe, Encoding.UTF8);
                File.AppendAllText(gerenFile, tempGeRen, Encoding.UTF8);

                File.AppendAllText("cmd.bat", string.Format("SmartSpiderCore.WinConsole.exe Task/{0}\r\n", qiyeFile));
                File.AppendAllText("cmd.bat", string.Format("SmartSpiderCore.WinConsole.exe Task/{0}\r\n", gerenFile));
            }
        }

    }
}
