using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Generation.MarkovChain
{
    public interface IMarkovChain<out T>
    {
        T NextValue();
    }
}
