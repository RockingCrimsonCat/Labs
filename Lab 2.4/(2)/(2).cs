//Мобільний зв'язок. Визначити ієрархію тарифів мобільного компанії. Створити список тарифів компанії.
//Підрахувати загальну чисельність клієнтів. 
//Знайти тариф в компанії, що відповідає заданому діапазону параметрів.

using Lab_2._4;

Clients clients = new Clients();
Console.WriteLine(clients.AmountOfClients(clients.ClientsList()));

Tariffs tariffs = new Tariffs();
tariffs.SortedTariffsList();





