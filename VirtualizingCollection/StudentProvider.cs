using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientDB.VirtualizingCollection;
using ClientDB.Model;
using System.Diagnostics;
using System.Threading;

namespace ClientDB.VirtualizingCollection
{
    internal class StudentProvider: IItemsProvider<Student>
    {
        private readonly int _count;
        private readonly int _fetchDelay;

        /// <summary>
        /// Initializes a new instance of the <see cref="DemoCustomerProvider"/> class.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="fetchDelay">The fetch delay.</param>
        public StudentProvider(int count, int fetchDelay)
        {
            _count = count;
            _fetchDelay = fetchDelay;
        }

        /// <summary>
        /// Fetches the total number of items available.
        /// </summary>
        /// <returns></returns>
        public int FetchCount()
        {
            Trace.WriteLine("FetchCount");
            Thread.Sleep(_fetchDelay);
            return _count;
        }

        /// <summary>
        /// Fetches a range of items.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The number of items to fetch.</param>
        /// <returns></returns>
        public IList<Student> FetchRange(int startIndex, int count)
        {
            Trace.WriteLine("FetchRange: " + startIndex + "," + count);
            Thread.Sleep(_fetchDelay);

            List<Student> list = new List<Student>();
            Trace.WriteLine("list init");
            for (int i = startIndex; i < startIndex + count; i++)
            {
                Student student = new Student { firstname = "Student " + (i + 1), ProfileTicket = (i+1).ToString() };
                list.Add(student);
            }
            return list;
        }

    }
}
