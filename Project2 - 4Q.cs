using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Project24
{
    class Queue
    {
        private int size;
        private int[] queueArray;
        private int first, last;
        private int elementCount;
        public Queue(int size)
        {
            this.size = size;
            queueArray = new int[size];
            first = 0; last = -1; elementCount = 0;
        }
        public void enqueue(int j)
        {
            if (first == size - 1)
                last = -1;
            queueArray[++last] = j;
            elementCount++;
        }
        public int dequeue()
        {
            int temp = queueArray[first++];
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
        private List<int> queue;
        public PriorityQueue()
        {
            queue = new List<int>();
        }
        public void Add(int item)
        {
            queue.Add(item);
            queue.Sort();
        }
        public int Remove()
        {
            if(queue.Count == 0)
            {
                throw new InvalidOperationException("Kuyruk boş.");
            }
            int priority = queue[0];
            queue.RemoveAt(0);
            return priority;
        }
        public bool isEmpty()
        {
            return queue.Count == 0;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] productCounts = { 10, 4, 8, 6, 7, 1, 15, 9, 3, 2 };
            Queue queue = new Queue(productCounts.Length);
            PriorityQueue priorityQueue = new PriorityQueue();
            double totalTimeQueue = 0;
            double totalTimePriorityQueue = 0;
            double tempProcessTime = 0;

            foreach(int productCount in productCounts)
            {
                queue.enqueue(productCount);
            }

            Console.WriteLine("Verilen sırayla müşteriler için işlem tamamlama süreleri:");
            Console.WriteLine("---------------------------------------------------------");
            while (!queue.isEmpty())
            {
                int productCount = queue.dequeue();
                double processTimeQueue = productCount * 2.5 + tempProcessTime;
                tempProcessTime = processTimeQueue;
                Console.WriteLine("Ürün Adedi: " + productCount + " --- İşlem süresi: " + processTimeQueue + " saniye");
                totalTimeQueue += processTimeQueue;
            }

            double averageTimeQueue = totalTimeQueue / productCounts.Length;
            Console.WriteLine();
            Console.WriteLine("Ortalama İşlem Süresi: " + averageTimeQueue + " saniye" + " (Kuyruk için ortalama süre)");
            Console.WriteLine();

            tempProcessTime = 0;
            foreach (int productCount in productCounts)
            {
                priorityQueue.Add(productCount);
            }

            Console.WriteLine("Artan sırayla müşteriler için işlem tamamlama süreleri:");
            Console.WriteLine("-------------------------------------------------------");
            while(!priorityQueue.isEmpty())
            {
                int productCount = priorityQueue.Remove();
                double processTimePriorityQueue = productCount * 2.5 + tempProcessTime;
                tempProcessTime = processTimePriorityQueue;
                Console.WriteLine("Ürün Adedi: " +  productCount + " --- İşlem süresi: " + processTimePriorityQueue + " saniye");
                totalTimePriorityQueue += processTimePriorityQueue;
            }

            double averageTimePriorityQueue = totalTimePriorityQueue / productCounts.Length;
            Console.WriteLine();
            Console.WriteLine("Ortalama İşlem Süresi: " + averageTimePriorityQueue + " saniye" + " (Öncelikli kuyruk için ortalama süre)");
            Console.ReadLine();
        }
    }
}
