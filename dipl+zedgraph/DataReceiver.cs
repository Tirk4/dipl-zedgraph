using System;
using System.Threading;

namespace dipl_zedgraph
{
    internal class DataReceiver
    {
        private bool isRunning = false;


        public void StartReceivingData()
        {
            isRunning = true;
            Thread thread = new Thread(ReceiveData);
            thread.Start();
        }

        public void StopReceivingData()
        {
            isRunning = false;
        }

        private void ReceiveData()
        {
            while (isRunning)
            {
                Console.WriteLine("Receiving data...");
                Thread.Sleep(1000); 
            }
        }
    }
}
