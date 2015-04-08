﻿using System;

namespace scrypt
{
    public class Item
    {
        public Func<Item, string> Convert { get; set; }

        public int Index { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return Convert(this);
        }
    }
}