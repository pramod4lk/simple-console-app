using System;
using System.IO;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace SimpleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            var connectionString = configuration.GetConnectionString("StorageAccount");
            
            // Get blog container
            string containerName = "textfiles";
            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
            container.CreateIfNotExists();

            string blobName = "sampletextfile";
            string fileName = "sampletextfile.txt";
            BlobClient blobClient = container.GetBlobClient(blobName);
            blobClient.Upload(fileName, true);

            var blobs = container.GetBlobs();
            foreach (var blob in blobs)
            {
                Console.WriteLine($"{blob.Name} --> Created On: {blob.Properties.CreatedOn:yyyy-MM-dd HH:mm:ss}  Size: {blob.Properties.ContentLength}");
            }
        }
    }
}
