using Xunit;
using System;
using System.Collections.Generic;

namespace Library.Objects
{
  [Collection("Library")]
  public class PatronsTests : IDisposable
  {
    public PatronsTests()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Patrons_GetAll_DatabaseEmptyOnload()
    {
      List<Patrons> testList = Patrons.GetAll();
      List<Patrons> controlList = new List<Patrons>{};

      Assert.Equal(controlList, testList);
    }

    [Fact]
    public void Patrons_Save_SaveToDatabase()
    {
      Patrons newPatrons = new Patrons("Jenny");
      newPatrons.Save();

      Patrons testPatrons = Patrons.GetAll()[0];
      Assert.Equal(newPatrons, testPatrons);
    }

    [Fact]
    public void Patrons_Equals_PatronsEqualsPatrons()
    {
      Patrons controlPatrons = new Patrons("Jenny");
      Patrons testPatrons = new Patrons("Jenny");

      Assert.Equal(controlPatrons, testPatrons);
    }

    [Fact]
    public void Patrons_Find_FindsPatronsInDB()
    {
      Patrons controlPatrons = new Patrons("Jenny");
      controlPatrons.Save();

      Patrons testPatrons = Patrons.Find(controlPatrons.GetId());

      Assert.Equal(controlPatrons, testPatrons);
    }

    // [Fact]
    // public void Course_AddStudent_AddsStudentToCourse()
    // {
    //   Course newCourse = new Course("Computer Science", "CS101");
    //   newCourse.Save();
    //   Student newStudent1 = new Student("David", new DateTime(2015, 05, 12));
    //   newStudent1.Save();
    //   Student newStudent2 = new Student("John", new DateTime(2016, 05, 22));
    //   newStudent2.Save();
    //
    //   newCourse.AddStudent(newStudent1);
    //   newCourse.AddStudent(newStudent2);
    //
    //   List<Student> testList = newCourse.GetStudents();
    //   List<Student> controlList = new List<Student>{newStudent1, newStudent2};
    //
    //   Assert.Equal(controlList, testList);
    // }
    //
    // [Fact]
    // public void Course_Delete_DeleteCourse()
    // {
    //   Course newCourse = new Course("Computer Science", "CS101");
    //   newCourse.Save();
    //
    //   newCourse.Delete();
    //
    //   List<Course> newList = Course.GetAll();
    //   List<Course> controlList = new List<Course>{};
    //
    //   Assert.Equal(controlList, newList);
    // }
    //
    // [Fact]
    // public void Course_Search_FindsCoursesByName()
    // {
    //   Course course1 = new Course("Computer Science", "CS101");
    //   course1.Save();
    //   Course course2 = new Course("computer science", "CS102");
    //   course2.Save();
    //   Course course3 = new Course("Biology", "SCI101");
    //   course3.Save();
    //   Course course4 = new Course("computer sci", "CS105");
    //   course4.Save();
    //
    //   List<Course> testList = Course.Search("Computer");
    //   List<Course> controlList = new List<Course>{course1, course2, course4};
    //
    //   Assert.Equal(controlList, testList);
    // }

    public void Dispose()
    {
      Copies.DeleteAll();
      Patrons.DeleteAll();
      Books.DeleteAll();
    }
  }
}
