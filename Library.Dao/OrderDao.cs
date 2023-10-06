using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dao
{
    public class OrderDao
    {
        public OrderDao()
        {
            DataAccessTool = new Library.Common.DataAccessTool();
        }

        public Library.Common.DataAccessTool DataAccessTool
        {
            get;
            set;
        }
    }
}
