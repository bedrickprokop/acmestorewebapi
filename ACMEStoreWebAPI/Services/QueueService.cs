using System;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage;
using ACMEStoreWebAPI.Models;

namespace ACMEStoreWebAPI.Services
{
    public class QueueService
    {
        private static string connectionString = "DefaultEndpointsProtocol=https;AccountName=acmestorageaccount;AccountKey=S5qfkwPaILx8dj2RGSreusmN+peF2S9mxriTmM51mXBLiemaWwb7nRvqdBMkffAeV5EXlAMRA3/7E0bY/OFhxg==;EndpointSuffix=core.windows.net";
        private CloudStorageAccount cloudStorageAccount;
        private CloudQueue queueOne;

        public QueueService()
        {
            connect();
            queueOne = getQueue("queuenotification");
        }

        private void connect()
        {
            if (!CloudStorageAccount.TryParse(connectionString, out cloudStorageAccount))
            {
                Console.WriteLine("Failed to connect to StorageAccount");
                //throw new Exception("Failed to connect to StorageAccount");
                //TODO How can I do Exception treatment in C#??? I m just logging the error message insted to send to the client
            }
        }

        private CloudQueue getQueue(String queueName)
        {
            var cloudQueueClient = cloudStorageAccount.CreateCloudQueueClient();
            var queueInstance = cloudQueueClient.GetQueueReference(queueName);

            queueInstance.CreateIfNotExistsAsync();

            return queueInstance;
        }

        public void sendMessage(MessageQueue messageQueue)
        {
            var cloudQueueMessage = new CloudQueueMessage(messageQueue.message);
            queueOne.AddMessageAsync(cloudQueueMessage);
        }
    }
}
