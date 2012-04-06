using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace BitbucketBackup
{
    class Program
    {
        static void Main(string[] args)
        {
            new BitbucketBackup().Execute();
        }
    }
}