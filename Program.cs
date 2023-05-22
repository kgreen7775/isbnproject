// See https://aka.ms/new-console-template for more information
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

Console.WriteLine("Hello, World!");

//Initialied API call
Api.Init();

//loop through list to get ibsn for books
foreach (var item in GetFile())
{
    await GetBooks(item);
}


//Get API call
static async Task GetBooks(string ISBN)
{
    using HttpResponseMessage response = await Api.client.GetAsync($"/isbn/{ISBN}.json");

    //Check if call was successful
    if (response.IsSuccessStatusCode)
    {
        var jsonResponse = await response.Content.ReadAsStringAsync();
       
        //loop through and save to CSV
        foreach (var item in jsonResponse)
        {

        }
    }
    else
    {
        throw new Exception(response.ReasonPhrase);
    }
}

//Get the File with the ISBN Number
List<string> GetFile()
{
    //string[] lines;
    var list = new List<string>();
    var fileStream = new FileStream(@"ISBN_Input_File.txt", FileMode.Open, FileAccess.Read);
    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
    {
        string line;
        while ((line = streamReader.ReadLine()!) != null)
        {
            list.Add(line);
        }
    }
    return list;
}





public class Api
{
   public static HttpClient client = new();

    //Constructor
    public static void Init()
    {
        client.BaseAddress = new Uri("https://openlibrary.org");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

}





