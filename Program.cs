using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using ns;

namespace webcomic_generator
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> lines = new List<string>();
            lines.Add("<style>img{max-height:150vh;height:auto;display:block;margin:0 auto;}a{width: 40px;height: 40px;opacity: 0.3;position: fixed;bottom: 50px;right: 100px;display: none;text-indent: -9999px;}</style> <body><span id='top'></span>");


            List<string> files = new List<string>();
            string[] fileEntries = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            NumericComparer ns = new NumericComparer();
            Array.Sort(fileEntries, ns);

            Console.WriteLine("now only support .png .jpg .gif .bmp");

            foreach (string fileName in fileEntries)
            {   
                if (Path.GetExtension(fileName) == ".png" || Path.GetExtension(fileName) == ".jpg" || Path.GetExtension(fileName) == ".gif" || Path.GetExtension(fileName) == ".bmp")
                {
                    Console.WriteLine("Added file: " + fileName);
                    files.Add(fileName);
                }
            }

            for (int i = 1; i < files.Count; i++)
            {
                lines.Add("<img src=\""+files[i]+"\" /> ");
            }

            lines.Add("<script> var img=document.querySelectorAll('img');for(var i=0;i<img.length;i++){img[i].addEventListener('click',function(e){e.stopPropagation(); window.scrollBy(0,this.height/2);});} window.onresize=function(event){var img=document.querySelectorAll('img');for(var i=0;i<img.length;i++){img[i].addEventListener('click',function(e){e.stopPropagation(); window.scrollBy(0,this.height/2);});}}; window.onclick=function(){window.location='#top'}; </script> </body>");

            Console.Write("\n\nFile Name: ");
            String s = Console.ReadLine();
            System.IO.File.WriteAllLines(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)+"/"+s+".html", lines.ToArray<string>());
        }
    }
}
