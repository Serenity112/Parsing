using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{
    class FloorsData
    {
        public Dictionary<int, int> floors;
        public FloorsData()
        {
            floors = new Dictionary<int, int>();
            floors.Add(1, 0);
            floors.Add(2, 0);
            floors.Add(3, 0);
            floors.Add(4, 0);
            floors.Add(5, 0);
        }

        public override string ToString()
        {
            return $"1: {floors[1]} 2: {floors[2]} 3: {floors[3]} 4: {floors[4]} 5: {floors[5]}";
        }
    }
}
