using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface GameMode
{
    void click(Tile tile);
    void updateMousePosition(Tile tile);
    void end();
    void start();
}
