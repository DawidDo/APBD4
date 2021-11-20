using System;
using System.Collections.Generic;
using System.Data;
using APBD4.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace APBD4.Controllers
{

    [ApiController]
    [Route("api/animals")]
    public class AnimalsController : ControllerBase
    {
        string conStrin =
            "Data Source=apbd.db;Version=3";

        [HttpPost]
        public IActionResult CreateAnimals(Animal a)
        {
            SQLiteConnection con = new SQLiteConnection(conStrin);
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand();

            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT COUNT(IdAnimal) from Animal";
            cmd.CommandType = CommandType.Text;
            SQLiteDataReader reader = cmd.ExecuteReader();
            int newID = 999;
            while (reader.Read())
            {
                newID = reader.GetInt32(0);
            }

            reader.Close();
            Animal a2 = new Animal
            {
                IdAnimal = newID + 1,
                Name = a.Name,
                Description = a.Description,
                Category = a.Category,
                Area = a.Area
            };

            cmd.CommandText = "insert or replace into Animal(IdAnimal,Name,Description, Category, Area)VALUES ('" +
                              a2.IdAnimal + "','" + a2.Name + "','" + a2.Description + "','" + a2.Category + "','" +
                              a2.Area + "')";

            int rowsAffected = cmd.ExecuteNonQuery();

            con.Close();

            return Ok("Dodano Animalsa  " + rowsAffected);
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateAnimal([FromRoute] string Id, [FromBody] Animal a)
        {
            SQLiteConnection con = new SQLiteConnection(conStrin);
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand();

            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * from Animal where IdAnimal = " + Id;
            cmd.CommandType = CommandType.Text;
            SQLiteDataReader reader = cmd.ExecuteReader();
            
            try
            {
                while (reader.Read())
                {
                  Console.WriteLine("OK");
                }
            }
            catch (Exception e)
            {
                return BadRequest(404);
            }
            reader.Close();
            string Name = a.Name;
            string Description = a.Description;
            string Category = a.Category;
            string Area = a.Area;
            
            cmd = con.CreateCommand();
            cmd.CommandText = "update Animal SET Name ='" + Name + "', Description = '" + Description + "', Category = '" +
                              Category + "',Area = '" + Area + "' WHERE IdAnimal = " + Id;
            cmd.ExecuteNonQuery();
            return Ok();
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteAnimals([FromRoute] string Id)
        {
            SQLiteConnection con = new SQLiteConnection(conStrin);
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand();

            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * from Animal where IdAnimal = " + Id;
            cmd.CommandType = CommandType.Text;
            SQLiteDataReader reader = cmd.ExecuteReader();
            
            try
            {
                while (reader.Read())
                {
                    Console.WriteLine("OK");
                }
            }
            catch (Exception e)
            {
                return BadRequest(404);
            }
            
            reader.Close();
            
            cmd = con.CreateCommand();
            cmd.CommandText = "DELETE  from Animal where IdAnimal = " + Id;
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            

            return Ok("Usunięto Animalsa");
        }

        [HttpGet("{value}")]
        public IActionResult GetAnimals([FromRoute] string value)
        {

            SQLiteConnection con = new SQLiteConnection(conStrin);
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand();
            List<string> lista = new List<string>();
            cmd = con.CreateCommand();
            if (value == null)
                value = "Name";
            cmd.CommandText = "SELECT * from Animal order by "+value+"";
            cmd.CommandType = CommandType.Text;
            SQLiteDataReader reader = cmd.ExecuteReader();
            StringBuilder sb = new StringBuilder();
            while (reader.Read())
            {
                Console.WriteLine("{0}-{1}-{2}-{3}",reader.GetString(1),reader.GetString(2),reader.GetString(3),reader.GetString(4));
                lista.Add(reader.GetString(1)+ reader.GetString(2)+reader.GetString(3)+reader.GetString(4));
                
            }

            foreach (string s in lista)
            {
                sb.AppendLine(s);
            }
            return Ok(sb.ToString());
        }
        
        [HttpGet]
        public IActionResult GetAnimalsByName()
        {
            
            SQLiteConnection con = new SQLiteConnection(conStrin);
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand();
            List<string> lista = new List<string>();
            cmd = con.CreateCommand();
            string value = "Name";
            cmd.CommandText = "SELECT * from Animal order by "+value+"";
            cmd.CommandType = CommandType.Text;
            SQLiteDataReader reader = cmd.ExecuteReader();
            StringBuilder sb = new StringBuilder();
            while (reader.Read())
            {
                Console.WriteLine("{0}-{1}-{2}-{3}",reader.GetString(1),reader.GetString(2),reader.GetString(3),reader.GetString(4));
                lista.Add(reader.GetString(1)+ reader.GetString(2)+reader.GetString(3)+reader.GetString(4));
                
            }

            foreach (string s in lista)
            {
                sb.AppendLine(s);
            }
            return Ok(sb.ToString());
        }
        
}
}