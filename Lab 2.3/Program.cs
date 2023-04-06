/*Скласти опис класу для розкладу.
Зберігає список (час початку, час закінчення, місце, текст (коментар)).
Методи: за датою - записи, очистити, додати, замінити.
за місцем - час початку і тривалість,чи можна вставити новий пункт.*/

using Lab_2._3;
using Newtonsoft.Json;
using System;

Date firstDate = new Date();
string newDate = firstDate.WriteDate("10.04");
firstDate.ClearDate(newDate);
firstDate.AddDate("12.04");
firstDate.RewriteDate("yes", "13.04");

Schedule timeOfLeson = new Schedule();
string fullTime = timeOfLeson.FullTime("12.00", "13.30");

Place place = new Place();
string placeAndTime = place.TimeStartAndDuration("Classroom 5", fullTime);

Schedule commentInfo = new Schedule();
string fullInfo = commentInfo.CommentInfo(placeAndTime, "Professor Ackerman ");

Schedule info = new Schedule();
List<string> resultInfo = info.FullInfo(fullInfo);


Console.WriteLine();

Date secondDate = new Date();
secondDate.WriteDate("14.04");

Schedule timeOfLeson2 = new Schedule();
string fullTime2 = timeOfLeson2.FullTime("10.00", "11.30");

Place place2 = new Place();
string placeAndTime2 = place2.TimeStartAndDuration("Classroom 3", fullTime2);

Schedule commentInfo2 = new Schedule();
string fullInfo2 = commentInfo2.CommentInfo(placeAndTime2, "Professor Smith ");

Schedule info2 = new Schedule();
List <string> resultInfo2 = info2.FullInfo(fullInfo2);
string json = Serialize(resultInfo2);
Schedule schedule = Deserialize(@"D:\1.txt");

/*Завдання 2
Створити у попередньому завданні два методи з використанням серіалізації та десеріалізації JSON. 
Метод 1. Зберігає створений об’єкт класу з Завдання 1 у JSON файл 
Метод 2. Відкриває JSON файл з даними та створює об’єкт класу з цими даними для виконання Завдання 1.*/

string Serialize (List <string> info)
{
    string json = JsonConvert.SerializeObject(info);
    string file = @"D:\\1.txt";
    StreamWriter sw = new StreamWriter(file);
    sw.Write(json);
    sw.Close();
    return json;
}

 Schedule Deserialize(string filePath)
{
    using (StreamReader file = new StreamReader(filePath))
    {
        string json = file.ReadToEnd();
        List<string> info = JsonConvert.DeserializeObject<List<string>>(json);
        Schedule schedule = new Schedule();
        schedule.AddDesObject(info);
        return schedule;
    }
}















