//Створити суперклас Музичний інструмент і класи Джембе, Барабан, Скрипка, Кларнет, Гітара.
//Подумати, які з вищенаведених підкласів також можуть бути суперкласами. Створити масив об'єктів Оркестр.
//Видати склад оркестру. Видати звук кожного інструменту.


using Lab_2._5;


MusicalInstrument[] orchestra = { new Djembe(), new Drum(), new Violin(), new Guitar(), new Clarinet() };
PlayInstrument playInstrument = new PlayInstrument();
Console.WriteLine("Orchestra: ");
foreach (var instrument in orchestra)
{
    Console.Write($"{instrument.GetType().Name}   ");
}
Console.WriteLine("\n");

foreach (var instrument in orchestra)
{
    playInstrument.PlayingInstrument(instrument);   
}

