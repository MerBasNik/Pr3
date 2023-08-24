using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace ConsoleApp3;

public class CipherCeaser
{
    const string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя"; 

    public string Encryptor(string message, int key)
    {
        string CryptStr = "";
        foreach (char c in message)
        {
            if (alphabet.IndexOf(c) != -1)
                CryptStr = CryptStr + alphabet[(alphabet.IndexOf(c) + key) % alphabet.Length];
            else
            {
                CryptStr += c;
            }
        }
        return CryptStr;
    }

}

internal class Program
{
    static void Main(string[] args)
    {
        const int key = 5;
        using (ApplicationContext db = new ApplicationContext())
        {
            int box = 0;
            while (box != 4)
            {
                Console.WriteLine("Зашифровать(1), расшифровать(2), показать список сообщений(3), выйти(4)");
                box = int.Parse(Console.ReadLine());
                var CipherCease = new CipherCeaser();

                if (box == 1)
                {
                    Console.WriteLine("Введите сообщение:");
                    string text = Console.ReadLine();
                    string enctext = CipherCease.Encryptor(text, key);
                    User word = new User { Text = text, EncText = enctext };
                    db.Users.Add(word);
                    db.SaveChanges();
                    var Users = db.Users.ToList();
                }
                if (box == 2)
                {
                    Console.WriteLine("Введите id:");
                    int nomer = int.Parse(Console.ReadLine());
                    List<User> SelText = db.Users.Where(x => x.Id == nomer).ToList();
                    foreach (User u in SelText)
                    {
                        Console.WriteLine($"{u.Id}. {u.Text}");
                    }
                }
                if (box == 3)
                {
                    List<User> users = db.Users.ToList();
                    Console.WriteLine("Список объектов:");
                    foreach (User u in users)
                    {
                        Console.WriteLine($"{u.Id}. {u.EncText}");
                    }
                }
            }
        }
    }
}