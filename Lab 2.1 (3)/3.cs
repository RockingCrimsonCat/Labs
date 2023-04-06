/*Написати програму згідно отриманого завдання використовуючи лише LINQ методи. 
  Дана послідовність цілих чисел. Витягти з неї всі додатні числа, зберігши їх вихідний порядок проходження. */
using System;
var list = new List<int>();
Random randomNum = new Random();
for (int i = 0; i < 10; i++)
{
    int number = randomNum.Next(-20,20);
    list.Add(number);
    Console.Write(number + "\t");
}
var newlist = from i in list
              where i > 0
              select i;
var sum = from i in list
              where i > 0
              select i;
Console.WriteLine();
foreach (var item in newlist)
{
    Console.Write(item + "\t");
}





