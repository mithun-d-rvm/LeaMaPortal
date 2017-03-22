using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaMaPortal
{
    public static class PagingProperty
    {
        public static List<int> DefaultPagelist=new List<int> {10,25,50,100 };
        public const int DefaultPageSize = 10;
    }
}