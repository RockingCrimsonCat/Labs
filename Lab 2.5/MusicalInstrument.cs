using Lab_2._5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2._5
{


    interface IMusic
    {
        public void Sound();
    }
    interface IInstrument
    {
        public void InstrumentName();
    }
    abstract class MusicalInstrument : IMusic, IInstrument
    {
        public abstract void Sound();
        public void InstrumentName()
        {
            Console.WriteLine($"{GetType().Name}");
        }
      

    }
    class PlayInstrument : MusicalInstrument
    {
        public override void Sound() { }
        public void PlayingInstrument(MusicalInstrument instrument)
        {
            instrument.InstrumentName();
            instrument.Sound();
        }
    }


        class Djembe : PlayInstrument
        {


            public override void Sound()
            {
                Console.Beep(300, 420);
                Thread.Sleep(230);
                Console.Beep(320, 420);
                Console.Beep(300, 420);
                Thread.Sleep(630);
                Console.Beep(300, 420);
                Thread.Sleep(230);
                Console.Beep(320, 420);
                Console.Beep(300, 420);
                Thread.Sleep(630);
            }
        }

        class Drum : PlayInstrument

        {
            public override void Sound()
            {
                Console.Beep(150, 420);
                Thread.Sleep(230);
                Console.Beep(120, 420);
                Console.Beep(100, 420);
                Thread.Sleep(630);
                Console.Beep(150, 420);
                Thread.Sleep(230);
                Console.Beep(120, 420);
                Console.Beep(100, 420);
                Thread.Sleep(630);
            }
        }




        class Violin : PlayInstrument
        {
            public override void Sound()
            {
                Console.Beep(930, 920);

                Thread.Sleep(20);
            Console.Beep(950, 920);

            Thread.Sleep(20);

        }
        }
        class Guitar : PlayInstrument
        {
            public override void Sound()
            {
                Console.Beep(680, 920);

                Thread.Sleep(20);
            Console.Beep(640, 920);

            Thread.Sleep(20);

        }
        }

        class Clarinet : PlayInstrument
        {
            public override void Sound()
            {
                Console.Beep(1200, 720);
                Thread.Sleep(20);
            Console.Beep(1160, 720);
            Thread.Sleep(20);

        }
        }

    
}