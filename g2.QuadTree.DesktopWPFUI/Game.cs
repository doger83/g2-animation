using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace g2.Datastructures.DesktopWPFUI;

public class Game
{
    private readonly FPSCounterViewModel fpsCounter;
    private Task? loop;

    public Game(FPSCounterViewModel fpsCounter)
    {
        this.fpsCounter = fpsCounter;
    } 

    public void Update()
    {
        loop = Task.Run( () =>
        {
            while (true)
            {
                fpsCounter.Draw();

                // Perform game logic here
              
                
                StopGameLoop(100);
            }
        });
    }  

    private void StopGameLoop()
    {     
        loop?.Wait();
    }

    private void StopGameLoop(int milliseconds)
    {
        loop?.Wait(milliseconds);
    }
}

