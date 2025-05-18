// ===============================
// Project: Student Management System
// Language: C# (WinForms/Console)
// Database: SQLite
// ===============================

using System;
using System.Collections.Generic;
using System.Data.SQLite;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public int Year { get; set; }
}

public class StudentDatabase
{
    private const string connectionString = "Data Source=students.db";

    public static void Initialize()
    {
        using var conn = new SQLiteConnection(connectionString);
        conn.Open();
        string sql = "CREATE TABLE IF NOT EXISTS Students (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Department TEXT, Year INTEGER)";
        new SQLiteCommand(sql, conn).ExecuteNonQuery();
    }

    public static void AddStudent(Student s)
    {
        using var conn = new SQLiteConnection(connectionString);
        conn.Open();
        var cmd = new SQLiteCommand("INSERT INTO Students (Name, Department, Year) VALUES (@Name, @Department, @Year)", conn);
        cmd.Parameters.AddWithValue("@Name", s.Name);
        cmd.Parameters.AddWithValue("@Department", s.Department);
        cmd.Parameters.AddWithValue("@Year", s.Year);
        cmd.ExecuteNonQuery();
    }

    public static List<Student> GetAllStudents()
    {
        var list = new List<Student>();
        using var conn = new SQLiteConnection(connectionString);
        conn.Open();
        var cmd = new SQLiteCommand("SELECT * FROM Students", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Student
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
                Department = reader["Department"].ToString(),
                Year = Convert.ToInt32(reader["Year"])
            });
        }
        return list;
    }
}

public class Program
{
    public static void Main()
    {
        StudentDatabase.Initialize();

        var student = new Student { Name = "Alice", Department = "CSE", Year = 2 };
        StudentDatabase.AddStudent(student);

        var allStudents = StudentDatabase.GetAllStudents();
        foreach (var s in allStudents)
        {
            Console.WriteLine($"ID: {s.Id}, Name: {s.Name}, Dept: {s.Department}, Year: {s.Year}");
        }
    }
}
