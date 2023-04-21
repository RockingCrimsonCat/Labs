//Створити об'єкт класу Персона, використовуючи клас Викладач, Студент.
//Методи: курс студента, наявність в аудиторії, перевірка домашніх завдань, вік.

using Lab_2._4;


Person personTeacher = new Person();
string teacher = personTeacher.GetInfo("Yan", "Mostovyi", 58, true);
Console.WriteLine(teacher);

Person personStudent = new Person();
string student = personStudent.GetInfo("Yan", "Mostovyi", 18, true);
Console.WriteLine(student);

Console.WriteLine(FindDublicate(personTeacher, personStudent));  

Teacher work = new Teacher();
string isChecked = work.CheckHomework(false);

Student personStudent1 = new Student();
string student1 = personStudent1.InfoStudent("Vlad", "Dorichenko", 18, 1, false, isChecked);
Console.WriteLine(student1);

Teacher work1 = new Teacher();
string isChecked1 = work1.CheckHomework(true);

Student personStudent2 = new Student();
string student2 = personStudent2.InfoStudent("Lina", "Polikarpova", 18, 1, true, isChecked1);
Console.WriteLine(student2);


string FindDublicate (Person person, Person person1)
{
    bool personEqualsPerson1 = person.Equals(person1);
    if (personEqualsPerson1)
    {
        
        return "This name and surname were already added\n";
        
    }
   
        return "This is new person. Dublicate was not found\n"; 
}
