using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Library.Objects
{
  public class Patrons
  {
    private int _id;
    private string _name;

    public Patrons(string name, int id = 0)
    {
      _name = name;
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }
    public void SetId(int newId)
    {
      _id = newId;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }

    public override bool Equals(System.Object otherPatrons)
    {
      if(!(otherPatrons is Patrons))
      {
        return false;
      }
      else
      {
        Patrons newPatrons = (Patrons) otherPatrons;
        bool nameEquality = this.GetName() == newPatrons.GetName();
        bool idEquality = this.GetId() == newPatrons.GetId();
        return (nameEquality && idEquality);
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM patrons;", conn);
      cmd.ExecuteNonQuery();

      if(conn != null)
      {
        conn.Close();
      }
    }

    public static List<Patrons> GetAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM patrons", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Patrons> Patrons = new List<Patrons>{};
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Patrons newPatrons = new Patrons(name, id);
        Patrons.Add(newPatrons);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return Patrons;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO patrons (name) OUTPUT INSERTED.ID values (@PatronsName)", conn);

      SqlParameter nameParameter = new SqlParameter("@PatronsName", this.GetName());
      cmd.Parameters.Add(nameParameter);

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

    public static Patrons Find(int idToFind)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM patrons WHERE id = @PatronsId", conn);
      SqlParameter idParam = new SqlParameter("@PatronsId", idToFind);
      cmd.Parameters.Add(idParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      int id = 0;
      string name = null;

      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        name = rdr.GetString(1);
      }

      Patrons foundPatrons  = new Patrons(name, id);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return foundPatrons;
    }
  }
}
