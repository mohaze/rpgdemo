using System.Collections.Generic;
using System.IO;

namespace DefaultNamespace
{
    public class ResNode
    {
        public string fullName;
        public string md5;
        public HashSet<string> children = new HashSet<string>();
        public HashSet<string> parents  = new HashSet<string>();

    }
}