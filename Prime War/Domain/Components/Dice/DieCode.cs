﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeWarEngine.Domain.Components.Dice
{
    public class DieCode
    {
        public int d4;

        public int d8;

        public int d12;

        public List<DieFaces> guaranteedFaces;

        public DieCode(int d4, int d8, int d12, List<DieFaces> guranteed = null)
        {
            this.d4 = d4;
            this.d8 = d8;
            this.d12 = d12;
            this.guaranteedFaces = guranteed != null ? guranteed : new List<DieFaces>();
        }
        public override string ToString()
        {
            return d4 + "d4s," + d8 + "d8s," + d12 + "d12s";// + guaranteedFaces != null && guaranteedFaces.Count > 0 ? ", " + string.Join(", ",guaranteedFaces) : "";
        }
    }
}
