using Xunit;
using System;
using System.Collections.Generic;

namespace Library.Objects
{
  [Collection("Library")]
  public class CopiesTest : IDisposable
  {
    public CopiesTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Copies_GetAll_DatabaseEmpty()
    {
      List<Copies> newCopies = Copies.GetAll();
      List<Copies> testCopies = new List<Copies>{};

      Assert.Equal(newCopies, testCopies);
    }

    [Fact]
    public void Copies_Save_SaveCopiesToDatabase()
    {
      Copies newCopy = new Copies(10, 1);
      newCopy.Save();

      List<Copies> controlList = Copies.GetAll();
      Console.WriteLine(controlList[0].GetCopiesId());
      List<Copies> testList = new List<Copies>{newCopy};
      Console.WriteLine("copiestestline36", controlList[0]);

      Assert.Equal(controlList, testList);
      Console.WriteLine("copiestestline36", controlList[0]);
    }

    [Fact]
    public void Copies_AddBook_AddCopiesToPatrons()
    {
      Copies newCopies = new Copies(1);
      newCopies.Save();
      Books newBook = new Books("Of mice and men", "John Steinbeck", new DateTime(2017, 05, 06), 1);
      newBook.Save();

      Patrons newPatrons1 = new Patrons("Jerry", 1);
      newPatrons1.Save();
      Patrons newPatrons2 = new Patrons("Jerry", 2);
      newPatrons2.Save();

      // Console.WriteLine(newCopies.GetId());
      // Console.WriteLine(testCopies.GetId());

      newCopies.AddPatrons(newPatrons1);
      newCopies.AddPatrons(newPatrons2);


      List<Patrons> testList = newCopies.GetPatronCopies();
      List<Patrons> controlList = new List<Patrons>{newPatrons1, newPatrons2};

      Assert.Equal(controlList, testList);
    }

    public void Dispose()
    {
      Copies.DeleteAll();
      Patrons.DeleteAll();
      Books.DeleteAll();
    }
  }
}
