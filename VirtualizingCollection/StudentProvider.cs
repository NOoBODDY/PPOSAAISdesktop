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
        private int _count;
        APIservice api;

        /// <summary>
        /// Initializes a new instance of the <see cref="DemoCustomerProvider"/> class.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="fetchDelay">The fetch delay.</param>
        public StudentProvider(APIservice API)
        {
            this.api = API;
        }

        /// <summary>
        /// Fetches the total number of items available.
        /// </summary>
        /// <returns></returns>
        public int FetchCount()
        {
            Trace.WriteLine("FetchCount");
            _count = api.GetStudentsCount();
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

            List<Student> list = api.GetPageOfStudents(startIndex, count + startIndex);
            return list;
        }

    }
}
