using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using core.Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.DataMovement;

namespace core.Storage.Azure
{
    public class AzureService : IStorageService
    {
        private readonly ILogger logger;
        private readonly CloudStorageAccount storageAccount;

        public string Container { get; set; }
        public string ConnectionString { get; set; }

        public AzureService(ILogger logger, IConfiguration configuration)
        {
            this.logger = logger;

            ConnectionString = configuration["storage:remote:azureBlob:connectionString"];
            Container = configuration["storage:remote:azureBlob:container"];

            if (!StringExists(ConnectionString))
            {
                logger.Error("Connection string cannot be null or empty", null);
                return;
            }

            storageAccount = CreateStorageAccountFromConnectionString(ConnectionString);
        }

        public async Task Upload(string file, string container)
        {
            if (!StringExists(ConnectionString)) 
            {
                return;
            }

            logger.Log($"Uploading file(s) to Azure");
            string localFilePath = file.Trim();

            if (StringExists(container)) 
            {
                Container = container;
            }
            else if (!StringExists(Container)) 
            {
                logger.Log("Blob storage container cannot be null or empty");
                return;
            }
            else 
            {
                logger.Log(string.Format("Using default container {0}", Container));
            }

            if (string.Equals(localFilePath, ".")) 
            {
                localFilePath = Environment.CurrentDirectory;
            }

            if (File.Exists(localFilePath)) 
            {
                await TransferLocalFileToAzureBlob(localFilePath);
            } 
            else if (Directory.Exists(localFilePath)) 
            {
                await TransferLocalDirectoryToAzureBlobDirectory(localFilePath);
            } 
            else 
            {
                logger.Error(string.Format("File {0} could not be found", localFilePath), null);
                return;
            }
        }

        public async Task ListContainers()
        {

            if (storageAccount == null) 
            {
                return;
            }

            logger.Log($"Fetching list of containers");

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var results = await blobClient.ListContainersSegmentedAsync(null, blobContinuationToken);
                blobContinuationToken = results.ContinuationToken;
                foreach (CloudBlobContainer item in results.Results)
                {
                    logger.Log(item.Name);
                }
            } while (blobContinuationToken != null);
        }

        public async Task ListBlobs(string container, string outputFile, bool generateToken, int? lifespan)
        {

            if (storageAccount == null) 
            {
                return;
            }
            
            logger.Log($"Fetching list of blobs within container");

            var blobList = new List<string>();
            var token = "";

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(container);

            bool containerExists = await blobContainer.ExistsAsync();

            if (!containerExists) 
            {
                logger.Error("Container does not exist", null);
                return;
            }

            if (generateToken) 
            {
                token = GetContainerSasUri(blobContainer, lifespan);
            }

            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var results = await blobContainer.ListBlobsSegmentedAsync(null, true, BlobListingDetails.None, null, blobContinuationToken, null, null);
                blobContinuationToken = results.ContinuationToken;
                foreach (IListBlobItem item in results.Results)
                {
                    blobList.Add(string.Format("{0}{1}", item.Uri.ToString(), token));
                }
            } 
            while (blobContinuationToken != null);

            if (!StringExists(outputFile)) 
            {
                blobList.ForEach(logger.Log);
            } 
            else 
            {
                var outputList = string.Join(System.Environment.NewLine, blobList);
                File.WriteAllText(outputFile, outputList);
            }
        }

        public async Task MakeContainerPublic(string container)
        {
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer blobContainer = blobClient.GetContainerReference(container);

            bool containerExists = await blobContainer.ExistsAsync();

            if (!containerExists) 
            {
                logger.Error("Container does not exist", null);
                return;
            }

            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            await blobContainer.SetPermissionsAsync(permissions);

            logger.Log(string.Format("Container {0} is now public.", container));
            logger.Log("All blobs are now accessable without autherisation");
            logger.Log(string.Format("URL: {0}", blobContainer.Uri));

        }

        static string GetContainerSasUri(CloudBlobContainer container, int? lifespan)
        {
            SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
            sasConstraints.SharedAccessStartTime = DateTimeOffset.UtcNow.AddMinutes(-5);
            sasConstraints.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddDays(lifespan ?? 1);
            sasConstraints.Permissions = SharedAccessBlobPermissions.Read;

            string sasBlobToken = container.GetSharedAccessSignature(sasConstraints);

            return sasBlobToken;
        }

        private async Task<CloudBlockBlob> GetBlob(string fileName) 
        {
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(Container);

            try
            {
                await container.CreateIfNotExistsAsync();
            }
            catch (Exception e)
            {
                logger.Error(null, e);
                return null;
            }

            CloudBlockBlob blob = container.GetBlockBlobReference(fileName);

            return blob;
        }

        private async Task<CloudBlobDirectory> GetBlobDirectory() 
        {
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(Container);

            try 
            {
                await container.CreateIfNotExistsAsync();
            }
            catch(Exception e) 
            {
                logger.Error(null, e);
                return null;
            }

            CloudBlobDirectory blobDirectory = container.GetDirectoryReference("");

            return blobDirectory;
        }

        private static SingleTransferContext GetSingleTransferContext() 
        {
            SingleTransferContext context = new SingleTransferContext();
            context.ProgressHandler = new Progress<TransferStatus>((progress) => 
            {
                Console.Write("\rBytes transferred: {0}", progress.BytesTransferred);
            });
            return context;
        }

        private static DirectoryTransferContext GetDirectoryTransferContext() 
        {
            DirectoryTransferContext context = new DirectoryTransferContext();
            context.ProgressHandler = new Progress<TransferStatus>((progress) => 
            {
                Console.Write("\rBytes transferred: {0}", progress.BytesTransferred);
            });
            return context;
        }

        private async Task TransferLocalFileToAzureBlob(string file) 
        { 
            CloudBlockBlob blob = await GetBlob(Path.GetFileName(file));

            if (blob == null)
            {
                return;
            }

            SingleTransferContext context = GetSingleTransferContext(); 
            CancellationTokenSource cancellationSource = new CancellationTokenSource();
            logger.Log($"Transfer started...");

            Stopwatch stopWatch = Stopwatch.StartNew();
            Task task;
            ConsoleKeyInfo keyinfo;
            try 
            {
                task = TransferManager.UploadAsync(file, blob, null, context, cancellationSource.Token);

                while (!task.IsCompleted) 
                {
                    if (Console.KeyAvailable) 
                    {
                        keyinfo = Console.ReadKey(true);

                        if (keyinfo.Key == ConsoleKey.C) 
                        {
                            cancellationSource.Cancel();
                        }
                    }
                }
                await task;
            } 
            catch (Exception e) 
            {
                logger.Error(string.Format("The transfer is canceled: {0}", e.Message), null);  
            }

            stopWatch.Stop();
            logger.Log(string.Format("\nTransfer operation completed in {0} seconds.", stopWatch.Elapsed.TotalSeconds));
        }

        private async Task TransferLocalDirectoryToAzureBlobDirectory(string directory) 
        {
            CloudBlobDirectory blobDirectory = await GetBlobDirectory();

            if (blobDirectory == null) 
            {
                return;
            }

            DirectoryTransferContext context = GetDirectoryTransferContext(); 
            CancellationTokenSource cancellationSource = new CancellationTokenSource();
            logger.Log($"Transfer started...");

            Stopwatch stopWatch = Stopwatch.StartNew();
            Task task;
            ConsoleKeyInfo keyinfo;
            UploadDirectoryOptions options = new UploadDirectoryOptions() { Recursive = true };

            try 
            {
                task = TransferManager.UploadDirectoryAsync(directory, blobDirectory, options, context, cancellationSource.Token);

                while(!task.IsCompleted) 
                {
                    if(Console.KeyAvailable) 
                    {
                        keyinfo = Console.ReadKey(true);

                        if(keyinfo.Key == ConsoleKey.C) 
                        {
                            cancellationSource.Cancel();
                        }
                    }
                }
                await task;
            }
            catch(Exception e)
            {
                logger.Error(string.Format("The transfer is canceled: {0}", e.Message), null);
            }

            stopWatch.Stop();
            Console.WriteLine();
            logger.Log(string.Format("Transfer operation completed in {0} seconds.", stopWatch.Elapsed.TotalSeconds));
        }
        private CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;

            try 
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (Exception e) 
            {
                logger.Error(null, e);
                return null;
            }

            return storageAccount;
        }

        private bool StringExists(string s)
        {
            return !string.IsNullOrEmpty(s);
        }
    }
}