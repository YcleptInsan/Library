using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Library.Objects
{
  public class Copies
  {
    private int _id;
    private int _copiesId;

    public Copies(int copiesId, int id = 0)
    {
      _copiesId = copiesId;
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }
    public int GetCopiesId()
    {
      return _copiesId;
    }

    public override bool Equals(System.Object otherCopies)
    {
      if(!(otherCopies is Copies))
      {
        return false;
      }
      else
      {
        Copies newCopies = (Copies) otherCopies;
        bool copiesIdEquality = this.GetCopiesId() == newCopies.GetCopiesId();
        bool idEquality = this.GetId() == newCopies.GetId();

        return (copiesIdEquality && idEquality);
      }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO copies (copies) OUTPUT INSERTED.id VALUES (@copies)", conn);

      SqlParameter titleParameter = new SqlParameter("@copies", this.GetCopiesId());

      cmd.Parameters.Add(titleParameter);

      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static List<Copies> GetAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM copies", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Copies> copies = new List<Copies>{};
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int testcopies = rdr.GetInt32(1);

        Copies newBook = new Copies(testcopies, id);
        copies.Add(newBook);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return copies;
    }

    public void AddPatrons(Patrons newPatrons)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO patrons_copies (patrons_id, copies_id) OUTPUT INSERTED.id VALUES (@PatronsId, @CopiesId);", conn);

      SqlParameter patronsIdParam = new SqlParameter("@PatronsId", newPatrons.GetId());
      SqlParameter copiesIdParam = new SqlParameter("@CopiesId", this.GetId());

      cmd.Parameters.Add(patronsIdParam);
      cmd.Parameters.Add(copiesIdParam);
      cmd.ExecuteNonQuery();
      Console.WriteLine(newPatrons);

      if(conn != null)
      {
        conn.Close();
      }
    }

    public List<Patrons> GetPatronCopies()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT patrons.* FROM copies JOIN patrons_copies ON (copies.id = patrons_copies.copies_id) JOIN patrons ON (patrons.id = patrons_copies.patrons_id) WHERE copies.id = @CopiesId;", conn);

      SqlParameter BookParameter = new SqlParameter("@CopiesId", this.GetId());
      cmd.Parameters.Add(BookParameter);

      List<Patrons> AllPatrons = new List<Patrons>{};
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string copiesId = rdr.GetString(1);

        Patrons newPatrons = new Patrons(copiesId, id);
        AllPatrons.Add(newPatrons);

      }
      if(conn != null)
      {
        conn.Close();
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      return AllPatrons;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM copies;", conn);
      cmd.ExecuteNonQuery();

      if(conn != null)
      {
        conn.Close();
      }
    }
  }
}
