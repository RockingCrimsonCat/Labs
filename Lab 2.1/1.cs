using System.Collections;
using System.Diagnostics;

/*У колі стоять N людей, пронумерованих від 1 до N. При веденні рахунку по колу викреслюється кожна друга людина, 
  поки не залишиться один. Скласти дві програми, що моделюють процес. 
  Одна з програм повинна використовувати клас ArrayList, а друга - LinkedList. 
  */

bool TryReadValues(out int values)
{
    string inputValues = Console.ReadLine();

    if (!int.TryParse(inputValues, out values))
    {
        Console.WriteLine("Please, write only numbers.");
        return false;
    }
    return true;
}

int GetValues(string message)
{
    int values;
    do
    {
        Console.WriteLine(message);
    } while (!TryReadValues(out values));

    return values;
}

int GetAmount(string massege)
{
    int amount = GetValues("Write the amount of numbers.");

    while (amount < 0)
    {
        Console.WriteLine("The amount of numbers must be bigger than 0.");
        amount = GetValues("Write the amount of numbers.");
    }
    return amount;

}

//Методи LinkedList
void PrintList(LinkedList<int> list)
{
    foreach (int i in list)
    {
        Console.Write(i + "\t");
    }
}
void MakeList(LinkedList<int> list, int amountOfPeople)
{
    for (int i = 1; i <= amountOfPeople; i++)
    {
        list.AddLast(i);
    }
}
void RemovePeople(LinkedList<int> list, int amountOfPeople)
{
    int removeNumber = 0;

    for (int i = 1; i <= amountOfPeople; i++)
    {
        removeNumber += 2;
        list.Remove(removeNumber);

        if (removeNumber > amountOfPeople)
        {
            
            removeNumber = 1;
            removeNumber += 2;
            list.Remove(1);
            list.Remove(removeNumber);

        }
        if (list.Count == 2)
        {
            list.Remove(list.Last.Value);
        }
        Console.WriteLine();
        PrintList(list);
    }
}

    int amountOfPeople = GetAmount("Write the amount of numbers.");
    Console.WriteLine("\nDone with LinkedList");
    LinkedList<int> list = new LinkedList<int>();
    Console.WriteLine();
    MakeList(list, amountOfPeople);
    PrintList(list);
    Console.WriteLine();
    RemovePeople(list, amountOfPeople);



// Методи ArrayList
void PrintArList(ArrayList arlist)
{
    foreach (int i in arlist)
    {
        Console.Write(i + "\t");
    }
}
void MakeArList(ArrayList arlist, int amountOfPeople)
{
    for (int i = 1; i <= amountOfPeople; i++)
    {
        arlist.Add(i);
    }
}
void RemovePeopleInArList (ArrayList arlist, int amountOfPeople)
{
    int removeNumber = 0;

    for (int i = 1; i <= amountOfPeople; i++)
    {
        removeNumber += 2;
        arlist.Remove(removeNumber);

        if (removeNumber > amountOfPeople)
        {

            removeNumber = 1;
            removeNumber += 2;
            arlist.Remove(1);
            arlist.Remove(removeNumber);

        }
        if (arlist.Count == 2)
        {
            arlist.RemoveAt(arlist.Count - 1);
        }
        Console.WriteLine();
        PrintArList(arlist);
    }
}


Console.WriteLine("Done with ArrayList");
    ArrayList arlist = new ArrayList();
    Console.WriteLine();
    MakeArList(arlist, amountOfPeople);
    PrintArList(arlist);
    Console.WriteLine();
    RemovePeopleInArList(arlist, amountOfPeople);
   
