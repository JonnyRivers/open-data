// See https://aka.ms/new-console-template for more information
using Testbed;

Console.WriteLine("Hello, World!");

string path = args[0];

var client = new OpenDataClient();
client.Load(path);