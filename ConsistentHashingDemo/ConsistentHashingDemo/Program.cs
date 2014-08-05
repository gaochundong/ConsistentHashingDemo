using System;
using System.Collections.Generic;

namespace ConsistentHashingDemo
{
  class Program
  {
    static void Main(string[] args)
    {
      List<CacheServer> servers = new List<CacheServer>();
      for (int i = 0; i < 5; i++)
      {
        servers.Add(new CacheServer(i));
      }

      var consistentHashing = new ConsistentHash<CacheServer>(
        new MurmurHash2HashAlgorithm(), 10000);
      consistentHashing.Initialize(servers);

      int searchNodeCount = 10;

      SortedList<int, int> bucketNodes = new SortedList<int, int>();
      for (int i = 0; i < searchNodeCount; i++)
      {
        string item = i.ToString();
        int serverId = consistentHashing.GetItemNode(item).ID;
        bucketNodes[i] = serverId;

        Console.WriteLine("Item[{0}] is in Node[{1}]", i, bucketNodes[i]);
      }

      Console.Read();
    }
  }

  public class CacheServer
  {
    public CacheServer(int serverId)
    {
      ID = serverId;
    }

    public int ID { get; set; }

    public override int GetHashCode()
    {
      return ("CacheServer-" + ID).GetHashCode();
    }
  }
}