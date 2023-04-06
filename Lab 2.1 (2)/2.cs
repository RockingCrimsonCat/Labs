/*Вивести у форматі словника ті значення заданого словника,
    ключі якого більші, або дорівнюють заданому значенню.
Якщо результатом виконання програми є словник, зберегти цей результат у JSON файл*/

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

bool TryReadValues(out int values)
{
    string inputValues = Console.ReadLine();

    if (!int.TryParse(inputValues, out values))
    {
        Console.WriteLine("Будь ласка використовуйте лише чила");
        return false;
    }
    return true;
}

int GetValues(string message)
{
    int values;
    do
    {
        Console.Write(message);
    } while (!TryReadValues(out values));

    return values;
}
int GetDictionaryValue(string massege)
{
    int value = GetValues("Напишiть значення для словника: ");
    return value;
}
int GetDictionaryKey(string massege)
{
    int key = GetValues("Напишiть ключ до словника : ");
    return key;
}
int GetSpecialKey (string massege)
{
    int specialKey = GetValues("Задайте значення ключа, щоб отримати словник ключi якого бiльшi, або дорiвнюють заданому значенню: ");
    return specialKey;
}

void MakeOriginalDictionary(Dictionary<int, int> dictionary)
{
    do
    {
        int value = GetDictionaryValue("Напишiть значення для словника: ");
        int key = GetDictionaryKey("Напишiть ключ до словника : ");
        dictionary.Add(key,value);
        Console.WriteLine($"\nКлюч : {key}\tЗначення : {value} ");
        Console.Write("\nХочете додати значення? Напишiть лише (так) або (нi) : ");
    }
    while (Console.ReadLine() == "так");
   
}
void PrintDictionary (Dictionary<int, int> dictionary)
{
    
    foreach (var item in dictionary)
    {
        Console.WriteLine(item.Key + " " + item.Value);
    }
}


    Dictionary<int, int> dictionary = new Dictionary<int, int>();
    MakeOriginalDictionary(dictionary);
    PrintDictionary(dictionary);
    int specialKey = GetSpecialKey("Задайте значення ключа, щоб отримати словник ключі якого більші, або дорівнюють заданому значенню: ");
    var resultDictionary = dictionary.Where(i => i.Key >= specialKey);
    foreach (var item in resultDictionary)
    {
    Console.WriteLine(item.Key + " " + item.Value);
    }
    string json = JsonConvert.SerializeObject(resultDictionary);
    string file = @"D:\\1.txt";
    StreamWriter sw = new StreamWriter(file);
    sw.Write(json);
    sw.Close();


