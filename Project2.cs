using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class UM_Alanı
    {
        public string Alan_Adi;
        public List<string> Il_Adlari;
        public int Ilan_Yili;
    }

    class Stack
    {
        private int maxSize;
        private UM_Alanı[] stackArray;
        private int top;
        public Stack(int max)
        {
            maxSize = max;
            stackArray = new UM_Alanı[maxSize];
            top = -1;
        }
        public void push(UM_Alanı j)
        { stackArray[++top] = j; }
        public UM_Alanı pop()
        { return stackArray[top--]; }
        public bool isEmpty()
        { return top == -1; }
    }


    class Queue
    {
        private int size;
        private UM_Alanı[] queueArray;
        private int first, last;
        private int elementCount;
        public Queue(int size)
        {
            this.size = size;
            queueArray = new UM_Alanı[size];
            first = 0; last = -1; elementCount = 0;
        }
        public void enqueue(UM_Alanı j)
        {
            if (first == size - 1)
                last = -1;
            queueArray[++last] = j;
            elementCount++;
        }
        public UM_Alanı dequeue()
        {
            UM_Alanı temp = queueArray[first++];
            if (first == size)
                first = 0;
            elementCount--;
            return temp;
        }
        public bool isEmpty()
        {
            return elementCount == 0;
        }
    }

    class PriorityQueue
    {
        private List<UM_Alanı> queue;
        public PriorityQueue()
        {
            queue = new List<UM_Alanı>();
        }
        public void Add(UM_Alanı umAlan)
        {
            queue.Add(umAlan);
        }
        public void Remove()
        {
            if (queue.Count > 0)
            {
                UM_Alanı enOncelikli = queue.OrderBy(x => x.Alan_Adi).First();
                queue.Remove(enOncelikli);
            }
            else
            {
                Console.WriteLine("Kuyruk boş.");
            }
        }
        public bool isEmpty()
        {
            return queue.Count == 0;
        }
        public void PrintPriorityQueue()
        {
            foreach (var umAlan in queue)
            {
                Console.WriteLine($"Alan Adı: {umAlan.Alan_Adi}");
                Console.WriteLine($"İller: {string.Join(", ", umAlan.Il_Adlari)}");
                Console.WriteLine($"İlan Yılı: {umAlan.Ilan_Yili}");
                Console.WriteLine();
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<UM_Alanı>[] umAlanlari = new List<UM_Alanı>[7];
            List<UM_Alanı> tumUmAlanlari = new List<UM_Alanı>();

            string[] regions = { "Akdeniz", "Doğu Anadolu", "Ege", "Güneydoğu Anadolu", "İç Anadolu", "Karadeniz", "Marmara" };
            int[] regionIndexes = { 4, 6, 4, 5, 3, 2, 0, 2, 5, 6, 6, 4, 6, 2, 3, 2, 1, 2, 3, 1, 4, 4 }; // To keep track of the regions in which the provinces in the given data are located (0: Akdeniz, 1: Doğu Anadolu, .... 6: Marmara)

            string[] data = {
            "Divriği Ulu Camii ve Darüşşifası (Sivas) 1985",
            "İstanbul'un Tarihi Alanları (İstanbul) 1985",
            "Göreme Millî Parkı ve Kapadokya (Nevşehir) 1985",
            "Hattuşa: Hitit Başkenti (Çorum) 1986",
            "Nemrut Dağı (Adıyaman) 1987",
            "Hieropolis-Pamukkale (Denizli) 1988",
            "Xanthos-Letoon (Antalya-Muğla) 1988 ",
            "Safranbolu Şehri (Karabük) 1994",
            "Truva Arkeolojik Alanı (Çanakkale) 1998",
            "Edirne Selimiye Camii ve Külliyesi (Edirne) 2011",
            "Çatalhöyük Neolitik Alanı (Konya) 2012",
            "Bursa ve Cumalıkızık: Osmanlı İmparatorluğunun Doğuşu (Bursa) 2014",
            "Bergama Çok Katmanlı Kültürel Peyzaj Alanı (İzmir) 2014",
            "Diyarbakır Kalesi ve Hevsel Bahçeleri Kültürel Peyzajı (Diyarbakır) 2015",
            "Efes (İzmir) 2015",
            "Ani Arkeolojik Alanı (Kars) 2016",
            "Aphrodisias (Aydın) 2017",
            "Göbekli Tepe (Şanlıurfa) 2018",
            "Arslantepe Höyüğü (Malatya) 2021",
            "Gordion (Ankara) 2023",
        };
            string lastDatum = "Anadolu’nun Ortaçağ Dönemi Ahşap Hipostil Camiileri (Konya-Eşrefoğlu Camii, Kastamonu-Mahmut Bey Camii, Eskişehir-Sivrihisar Camii, Afyon-Afyon Ulu Camii, Ankara-Arslanhane Camii) 2023";

            int regionIndex = 0;
            foreach (var datum in data)
            {
                string newDatum = NewDatum(datum);
                string[] datumPieces = newDatum.Split('/');

                UM_Alanı umAlan = new UM_Alanı
                {
                    Alan_Adi = datumPieces[0],
                    Il_Adlari = new List<string>(datumPieces[1].Split('-')),
                    Ilan_Yili = int.Parse(datumPieces[2])
                };
                tumUmAlanlari.Add(umAlan);

                foreach (var cities in umAlan.Il_Adlari)
                {
                    if (umAlanlari[regionIndexes[regionIndex]] == null)
                    {
                        umAlanlari[regionIndexes[regionIndex]] = new List<UM_Alanı>();
                    }
                    umAlanlari[regionIndexes[regionIndex]].Add(umAlan);
                    regionIndex++;
                }
            }

            string newLastDatum = NewDatum(lastDatum);
            string[] lastDatumPieces = newLastDatum.Split('/');

            UM_Alanı umAlan2 = new UM_Alanı
            {
                Alan_Adi = lastDatumPieces[0],
                Il_Adlari = new List<string>(lastDatumPieces[1].Split(',')),
                Ilan_Yili = int.Parse(lastDatumPieces[2])
            };
            tumUmAlanlari.Add(umAlan2);

            if (umAlanlari[regionIndexes[regionIndex]] == null)
            {
                umAlanlari[regionIndexes[regionIndex]] = new List<UM_Alanı>();
            }
            umAlanlari[regionIndexes[regionIndex]].Add(umAlan2);
            print(umAlanlari, regions);


            Stack s = new Stack(tumUmAlanlari.Count);
            foreach (var umAlani in tumUmAlanlari)
            {
                s.push(umAlani);
            }
            Console.WriteLine("Yığıt Yazdırma:");
            Console.WriteLine("------------------------------------------------");
            while (!s.isEmpty())
            {
                UM_Alanı poppedUMAlan = s.pop();
                PrintUMAlanlari(poppedUMAlan);
            }


            Queue q = new Queue(tumUmAlanlari.Count);
            foreach (var umAlani in tumUmAlanlari)
            {
                q.enqueue(umAlani);
            }
            Console.WriteLine("Kuyruk Yazdırma:");
            Console.WriteLine("------------------------------------------------");
            while (!q.isEmpty())
            {
                UM_Alanı dequeuedUMAlan = q.dequeue();
                PrintUMAlanlari(dequeuedUMAlan);
            }


            PriorityQueue priorityQueue = new PriorityQueue();
            foreach (var umAlani in tumUmAlanlari)
            {
                priorityQueue.Add(umAlani);
            }
            Console.WriteLine("Öncelikli Kuyruk:");
            Console.WriteLine("---------------------------------------------");
            while (!priorityQueue.isEmpty())
            {
                priorityQueue.Remove();
                priorityQueue.PrintPriorityQueue();
                Console.WriteLine("-----------------------");
            }
            Console.ReadLine();
        }

        public static void print(List<UM_Alanı>[] umAlanlari, string[] regions)
        {
            for (int i = 0; i < umAlanlari.Length; i++)
            {
                Console.WriteLine($"---{regions[i]} Bölgesi---");
                foreach (var umAlan in umAlanlari[i])
                {
                    Console.WriteLine($"Alan Adı: {umAlan.Alan_Adi}");
                    Console.WriteLine($"İller: {string.Join(", ", umAlan.Il_Adlari)}");
                    Console.WriteLine($"İlan Yılı: {umAlan.Ilan_Yili}");
                    Console.WriteLine();
                }
                Console.WriteLine($"Kaç Alan Eklendi: {umAlanlari[i].Count}");
                Console.WriteLine();
            }
        }

        private static string NewDatum(string datum)
        {
            string newDatum = datum.Replace(" (", "/").Replace(") ", "/");
            return newDatum;
        }

        public static void PrintUMAlanlari(UM_Alanı umAlan)
        {
            Console.WriteLine($"Alan Adı: {umAlan.Alan_Adi}");
            Console.WriteLine($"İller: {string.Join(", ", umAlan.Il_Adlari)}");
            Console.WriteLine($"İlan Yılı: {umAlan.Ilan_Yili}");
            Console.WriteLine();
        }
    }
}
