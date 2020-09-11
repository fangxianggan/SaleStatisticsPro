using NetRedisUtil;
using NetStackExchangeRedisUtil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Test22();
        }


        //public static void Test1()
        //{
        //    RedisUtil redis = new RedisUtil();

        //    redis.HashSet("auth-token", "15255458934", "nngngngngngngngngngngnngngng");

        //    //存字符串
        //    string str = "苍";


        //    redis.StringSet("name", str);//设置StringSet(key, value)
        //    List<string> dd = new List<string>();
        //    //dynamic ff = redis.hfhh(dd);//结果：苍
        //    redis.StringSet("name_two", str, TimeSpan.FromSeconds(10));//设置时间，10s后过期。

        //    redis.HashDelete("ff", new List<string> { "dd" });
        //    redis.HashDeleteAsync("dd", "ffff");

        //    Console.WriteLine("string Key：{0}，Value：{1}", "A", str);
        //    Console.ReadLine();

        //}

        public static void Test2()
        {
            RedisUtil redis = new RedisUtil();
            int count = 1;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 1000000; i++)
            {
                redis.StringSet("name" + i, i);//设置StringSet(key, value)
                count++;
                Console.WriteLine("正在执行中....");
            }
            stopwatch.Stop();
            var ss1 = stopwatch.Elapsed.TotalSeconds;
            Console.WriteLine("执行次数：{0}，耗时：{1}", count, ss1);
            Console.ReadLine();
        }


        public static void Test22()
        {

            DoRedisSet doRedisSet = new DoRedisSet();
            int count = 1;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 1000000; i++)
            {
                doRedisSet.Add("name" + i, i.ToString());//设置StringSet(key, value)
                count++;
                Console.WriteLine("正在执行中....");
            }
            stopwatch.Stop();
            var ss1 = stopwatch.Elapsed.TotalSeconds;
            Console.WriteLine("执行次数：{0}，耗时：{1}", count, ss1);
            Console.ReadLine();
        }

        public static void Test3()
        {
            RedisUtil redis = new RedisUtil();
            int count = 1;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 100000; i++)
            {
                redis.KeyExpire("name" + i, TimeSpan.FromSeconds(3));//设置StringSet(key, value)
            }
            stopwatch.Stop();
            var ss1 = stopwatch.Elapsed.TotalSeconds;
            Console.WriteLine("执行次数：{0}，耗时：{1}", count, ss1);
            Console.ReadLine();
        }


        public static void ClearAll()
        {
            //清空数据库
          //  RedisBase.Core.FlushAll();
        }
    }
}
