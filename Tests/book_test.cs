using Xunit;
using System;
using System.Collections.Generic;

namespace Library.Objects
{
  [Collection("Library")]
  public class BooksTests : IDisposable
  {
    public BooksTests()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Books_GetAll_DatabaseEmptyOnload()
    {
      List<Books> testList = Books.GetAll();
      List<Books> controlList = new List<Books>{};

      Assert.Equal(controlList, testList);
    }

    // [Fact]
    // public void Books_Save_SaveToDatabase()
    // {
    //   Books newBooks = new Books("Jenny", "Jenny", new DateTime(2016, 06, 05), 1);
    //   newBooks.Save();
    //
    //   // Console.WriteLine(newBooks.GetId());
    //
    //   Books testBooks = Books.GetAll()[0];
    //   // Console.WriteLine(testBooks.GetId());
    //   Assert.Equal(newBooks, testBooks);
    // }

    [Fact]
    public void Books_Equals_BooksEqualsBooks()
    {
      Books controlBooks = new Books("Jenny","Jenny", new DateTime(2016, 06, 05), 1);
      Books testBooks = new Books("Jenny", "Jenny", new DateTime(2016, 06, 05), 1);

      Assert.Equal(controlBooks, testBooks);
    }

    // [Fact]
    // public void Books_Find_FindsBooksInDB()
    // {
    //   Books controlBooks = new Books("Jenny", "Jenny", new DateTime(2016, 06, 05), 1);
    //   controlBooks.Save();
    //
    //   Books testBooks = Books.Find(controlBooks.GetId());
    //
    //   Assert.Equal(controlBooks, testBooks);
    // }
    public void Dispose()
    {
      Copies.DeleteAll();
      Patrons.DeleteAll();
      Books.DeleteAll();
    }
  }
}
