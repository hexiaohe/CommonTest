using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhantomJsConsole
{
    //队列临时类  
    public class QueueInfo
    {
        public long UserId { get; set; }

        public string OpenId { get; set; }
    }  

    /// <summary>
    /// 原理：利用生产者消费者模式进行入列出列操作  
    /// </summary>
    public class BusinessInfoHelper
    {
        #region

        public readonly static BusinessInfoHelper Instance = new BusinessInfoHelper();
        private BusinessInfoHelper()
        { }

        private readonly Random _rand = new Random();
        private Queue<QueueInfo> queueList1 = new Queue<QueueInfo>();
        private Queue<QueueInfo> queueList2 = new Queue<QueueInfo>();

        /// <summary>
        /// 获得随机数
        /// </summary>
        /// <returns></returns>
        public int GetRandom()
        {
            return _rand.Next(0, 2);
        }

        /// <summary>
        /// 入列
        /// </summary>
        /// <param name="queueNo"></param>
        /// <param name="userId"></param>
        /// <param name="openId"></param>
        public void AddQueue(int queueNo, long userId, string openId)
        {
            var queueInfo = new QueueInfo { UserId = userId, OpenId = openId };
            if (queueNo == 0)
            {
                queueList1.Enqueue(queueInfo);
            }
            else
            {
                queueList2.Enqueue(queueInfo);
            }
        }

        /// <summary>
        /// TODO 注意删除
        /// </summary>
        /// <param name="queueNo"></param>
        /// <returns></returns>
        public Queue<QueueInfo> GetQueueInfo(int queueNo)
        {
            return queueNo == 0 ? queueList1 : queueList2;
        }

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            var thread1 = new Thread(ThreadStart1) { IsBackground = true };
            thread1.Start();

            var thread2 = new Thread(ThreadStart2) { IsBackground = true };
            thread2.Start();
        }

        private void ThreadStart1()
        {
            while (true)
            {
                if (queueList1.Count > 0)
                {
                    ScanQueue(queueList1);
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }

        private void ThreadStart2()
        {
            while (true)
            {
                if (queueList2.Count > 0)
                {
                    ScanQueue(queueList2);
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }

        /// <summary>
        /// 业务代码
        /// </summary>
        private void ScanQueue(Queue<QueueInfo> queueList)
        {
            while (queueList.Count > 0)
            {
                try
                {
                    //从队列中取出  
                    QueueInfo queueinfo = queueList.Dequeue();

                    //取出的queueinfo就可以用了，里面有你要的东西  
                    //以下就是处理程序了  
                    //。。。。。。  
                    Console.WriteLine("userId:" + queueinfo.UserId + ";OpenId:" + queueinfo.OpenId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion  
    }
}
