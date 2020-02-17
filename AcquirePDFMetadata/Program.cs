using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcquirePDFMetadata
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now.ToString());
            procuraArquivos(@"F:\PMF - Nov_2019\");
            Console.WriteLine(DateTime.Now.ToString());
            Console.WriteLine("Mission Accomplished");
            Console.ReadLine();
        }

        static void procuraArquivos(string path)
        {
            var csv = new StringBuilder();

            DirectoryInfo dire = new DirectoryInfo(path);
            DirectoryInfo[] diretorios = dire.GetDirectories();

            int cont = 0;
            Console.Write(cont);

            foreach (DirectoryInfo info in diretorios)
            {
                FileInfo[] f = info.GetFiles("*.pdf", SearchOption.TopDirectoryOnly);

                foreach (FileInfo ff in f)
                {
                    var newLine = string.Format("{0};{1};{2};{3};{4}", ff.Name, ff.FullName, GetNumberOfPages(ff.FullName), ff.Length, info.FullName);
                    csv.AppendLine(newLine);

                    cont++;
                    Console.Write("\b \b");
                    Console.Write(cont);
                }

                if (info.GetDirectories().Length > 0)
                {
                    procuraArquivos(info.FullName);
                }

            }

            File.WriteAllText(@"c:\temp\fileMetaData.csv", csv.ToString());
        }

        static int GetNumberOfPages(string ppath)
        {
            try
            {
                PdfReader pdfReader = new PdfReader(ppath);
                return pdfReader.NumberOfPages;
            }
            catch
            {
                return 0;
            }
        }
    }
}
