using Azure.Storage.Blobs;
using Azure.Storage.Blobs .Models;
Console.WriteLine("Azure Blob Storage exercise\n");
// Run the examples asynchronously, wait for the results before proceeding
ProcessAsync().GetAwaiter().GetResult();
Console.WriteLine("Press enter to exit the sample application.");
Console.ReadLine();

static async Task ProcessAsync()
{
    string storageConnectionstring = "<< CONNECTION STRING >>";
    BlobServiceClient blobserviceClient = new BlobServiceClient(storageConnectionstring);

    //create new container
    string containerName = "wtblob" + Guid.NewGuid().ToString();
    BlobContainerClient containerClient = await blobserviceClient.CreateBlobContainerAsync(containerName);
    Console.WriteLine("A container named '" + containerName +"' has been created. " +
    "\nTake a minute to verify in the portal." +
    "\nNext a file will be created and uploaded to the container.");
    Console.WriteLine("Press Enter to continue");
    Console.ReadLine();

    // upload blobs to a container
    // create a local file in the /data/ directory for uploading and downloading
    string localPath = "./data/";
    string fileName = "wtfile"+ Guid.NewGuid().ToString() + ".txt";
    string localFilePath = Path.Combine(localPath, fileName);

    //Write text to file
    await File.WriteAllTextAsync(localFilePath, "Hello, World!");
    //Get a reference to the Blob
    BlobClient blobClient = containerClient.GetBlobClient(fileName);
    Console.WriteLine("Uploading to blob storage as:\n\t {0}\n",blobClient.Uri);
    //open file and upload its data
    using (FileStream uploadFileStream = File.OpenRead(localFilePath))
    {
        await blobClient.UploadAsync(uploadFileStream);
        uploadFileStream.Close();
    }
    Console.WriteLine("\nThe file was uploaded. We'll verify by listing" +
    " the blobs next.");
    Console.WriteLine("Press Enter to continue");
    Console.ReadLine();
    //List the blobs in a container
    Console.WriteLine("Listing blobs...");
    await foreach(BlobItem blobItem in containerClient.GetBlobsAsync())
    {
        Console.WriteLine("\t" + blobItem.Name);
    }
    Console.WriteLine("\nYou can also verify by looking inside the " +
    "container in the portal." +
    "\nNext the blob will be downloaded with an altered file name.");
    Console.WriteLine("Press Enter to continue");
    Console.ReadLine();

    //Download blobs
    string downloadFilePath = localFilePath.Replace(".txt", "DOWNLOADED.txt");
    Console.WriteLine("\nDownloading blob to \n\t{0}\n", downloadFilePath);
    BlobDownloadInfo download = await blobClient.DownloadAsync();
    using (FileStream downloadFilestream = File.OpenWrite(downloadFilePath))
    {
        await download.Content.CopyToAsync(downloadFilestream);
    }
    Console.WriteLine("\nLocate the local file in the data directory created earlier to verify it was downloaded.");
    Console.WriteLine("The next step is to delete the container and local files");
    Console.WriteLine("Press Enter to continue.");
    Console.ReadLine();
    
    // Delete the container and clean up local files created 
    Console.WriteLine("(\n\nDeleting blob container..."); 
    await containerClient.DeleteAsync();

    Console.WriteLine("Deleting the local source and downloaded files...");
    File.Delete(localFilePath);
    File.Delete(downloadFilePath);

    Console.WriteLine("Finished cleaning up.");
}